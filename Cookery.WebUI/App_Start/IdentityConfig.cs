using System;
using Cookery.WebUI.Infrastructure.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace Cookery.WebUI
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppIdentityEntitiesContext>(AppIdentityEntitiesContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                ClientId = "479668876732-2k262t59st25ahncbf1tlt00pffq6uv4.apps.googleusercontent.com",
                ClientSecret = "0Y57jvX1w3SVNItEWVIUWuZh",
                Caption = "Авторизация через Google+",
                CallbackPath = new PathString("/GoogleLoginCallback"),
                AuthenticationMode = AuthenticationMode.Passive,
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType(),
                BackchannelTimeout = TimeSpan.FromSeconds(60),
                BackchannelHttpHandler = new System.Net.Http.WebRequestHandler(),
                BackchannelCertificateValidator = null,
                Provider = new GoogleOAuth2AuthenticationProvider()
            }
            );
        }
    }
}