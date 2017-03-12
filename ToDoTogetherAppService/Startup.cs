using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ToDoTogetherAppService.Startup))]

namespace ToDoTogetherAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}