using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialCenter.Startup))]
namespace FinancialCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
