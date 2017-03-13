using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using ToDoTogetherAppService.DataObjects;
using ToDoTogetherAppService.Models;

namespace ToDoTogetherAppService.Controllers
{
    class FacebookInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    [MobileAppController]
    public class FacebookController : ApiController
    {
        FacebookCredentials credentials;
        string fbRequestUrl;
        ToDoTogetherAppContext context;
        EntityDomainManager<User> domainManager;

        // GET api/<controller>
        public async Task<User> Get()
        {
            if (credentials == null)
            {
                // get the credentials of the user who already signed in
                credentials = await User.GetAppServiceIdentityAsync<FacebookCredentials>(Request);
            }
            fbRequestUrl = "http://graph.facebook.com/me?fields=name,email,id&access_token=" + credentials.AccessToken;
            var client = new HttpClient();
            var resp = await client.GetAsync(fbRequestUrl);
            resp.EnsureSuccessStatusCode();

            var fbInfo = await resp.Content.ReadAsStringAsync();

            FacebookInfo info = JsonConvert.DeserializeObject<FacebookInfo>(fbInfo);
            context = new ToDoTogetherAppContext();
            domainManager = new EntityDomainManager<DataObjects.User>(context, Request);

            var user = context.Users.FirstOrDefault(u => u.Email == info.Email);

            // create new user in the database if one doesn't already exist
            if (user == null)
            {
                user = new DataObjects.User { Email = info.Email, UserName = info.Name, ProviderId = info.Id };
                await domainManager.InsertAsync(user);
            }
            // add user to the database with just an email (user was previously added by someone else to the project as a colaborator)
            else if (string.IsNullOrEmpty(user.ProviderId))
            {
                user.UserName = info.Name;
                user.ProviderId = info.Id;
                await context.SaveChangesAsync();
            }

            return user;
        }
    }
}