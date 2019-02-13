using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace WebApplication3.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                // ET: Damit die Anwengung nicht im RootVerzeichnigs laufen muss, wurde "/" mit ApplicationPath ausgetauscht.
                // So wird dir richtige URL zurückgegeben 
                Uri expectedRootUri = new Uri(context.Request.Uri, System.Web.HttpContext.Current.Request.ApplicationPath);


                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
                else if (context.ClientId == "web")
                {
                    var expectedUri = new Uri(context.Request.Uri, System.Web.HttpContext.Current.Request.ApplicationPath);
                    context.Validated(expectedUri.AbsoluteUri);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}