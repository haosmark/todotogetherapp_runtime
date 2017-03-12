using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoTogetherAppService.DataObjects
{
    public class User : EntityData
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        // id that's provided by the authentication service
        public string ProviderId { get; set; }
    }
}