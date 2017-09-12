using Common.Layer.Auth;
using Domain.Model.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Sql.Server.Access.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Server.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public object AudiencesStore { get; private set; }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudienceStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            UserDetails user;

            using (UserDetailsRepository userDetailsRepository = new UserDetailsRepository())
            {
                user = userDetailsRepository.GetBy(ud => ud.Email.Equals(context.UserName)).FirstOrDefault();
                if (user == null)
                {
                    context.SetError("invalid_grant", "The email or password is incorrect");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if(!PasswordManager.ValidatePassword(context.Password, user.Password))
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect");
                        return Task.FromResult<object>(null);
                    }
                }
            }


            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            identity.AddClaim(new Claim("firstChange", user.FirstChange.ToString()));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }
    }
}