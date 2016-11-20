using Example.WebApi;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Example.WebApi
{
    using System;
    using System.Web.Http;

    using Microsoft.Owin;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();
            app.UseCors(CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            
            

            
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
            
            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
