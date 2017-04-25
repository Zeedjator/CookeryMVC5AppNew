using Cookery.WebUI.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Cookery.WebUI.Infrastructure.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
            IOwinContext context)
        {
            AppIdentityEntitiesContext db = context.Get<AppIdentityEntitiesContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));


            manager.UserValidator = new CustomUserValidator();
            return manager;
        }
    }
}