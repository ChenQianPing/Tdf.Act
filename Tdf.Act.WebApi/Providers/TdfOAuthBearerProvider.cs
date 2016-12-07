using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;

namespace Tdf.Act.WebApi.Providers
{
    public class TdfOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        readonly string _name;

        public TdfOAuthBearerProvider(string name)
        {
            _name = name;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var value = context.Request.Query.Get(_name);
            if (!string.IsNullOrEmpty(value))
            {
                context.Token = value;
            }
            return Task.FromResult<object>(null);
        }
    }
}