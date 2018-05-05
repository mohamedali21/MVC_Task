using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MVC_Task.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Task.Startup))]
namespace MVC_Task
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRole();
            CreateAdmin();
        }
        //this function to create admin user
        public void CreateAdmin()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser { UserName = "mohamedali@gmail.com", Email = "mohamedali@gmail.com" };
            var result =  userManager.Create(user, "123456Mn@");
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id,"Admins");
            }
        }
        public void CreateRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if(!roleManager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);
            }
        }
    }
}
