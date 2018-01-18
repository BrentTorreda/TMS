using Microsoft.Owin;
using Owin;
using TaskManager.App_Start;
using TaskManager;

[assembly: OwinStartupAttribute(typeof(TaskManager.Startup))]
namespace TaskManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
