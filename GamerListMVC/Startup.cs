using DataAccess;
using GamerListMVC.Models;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(GamerListMVC.Startup))]
namespace GamerListMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
