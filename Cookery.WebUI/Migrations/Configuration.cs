using Cookery.WebUI.Infrastructure.Identity;
using Cookery.WebUI.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookery.WebUI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cookery.WebUI.Infrastructure.Identity.AppIdentityEntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Cookery.WebUI.Infrastructure.Identity.AppIdentityEntitiesContext";
        }

        protected override void Seed(Cookery.WebUI.Infrastructure.Identity.AppIdentityEntitiesContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "mypassword";
            string email = "admin@example.ru";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email },
                    password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            foreach (AppUser dbUser in userMgr.Users)
            {
                dbUser.City = Cities.MOSCOW;
            }

            foreach (AppUser dbUser in userMgr.Users)
            {
                if (dbUser.Country == Countries.NONE)
                    dbUser.SetCountryFromCity(dbUser.City);
            }
            context.SaveChanges();
        }
    }
}
