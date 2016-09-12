using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrarftedFood.Startup))]
namespace CrarftedFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
