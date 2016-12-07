using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tdf.Act.Domain.Commands;
using Tdf.Commanding;
using Tdf.Dependency;

namespace Tdf.Act.WebApi.Providers
{
    /// <summary>
    /// Resource Owner Password Credentials Grant 授权
    /// </summary>
    public class TdfAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Password Grant 授权服务
        /// </summary>
        private ICommandBus _commandBus;

        public TdfAuthorizationServerProvider(ICommandBus commandBus)
        {
            this._commandBus = commandBus;
        }

        public TdfAuthorizationServerProvider()
            : this(ObjectContainer.Resolve<ICommandBus>())
        {
        }

        /// <summary>
        /// Resource Owner Password Credentials Grant 的授权方式；
        /// 验证用户名与密码 [Resource Owner Password Credentials Grant[username与password]|grant_type=password&username=irving&password=654321]
        /// 重载 OAuthAuthorizationServerProvider.GrantResourceOwnerCredentials() 方法即可
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var cmd = new LoginCommand();
            cmd.UserName = context.UserName;
            cmd.Password = context.Password;
            _commandBus.Send(cmd);

            var userInfo = cmd.ExecutionResult;
            if (userInfo == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            else
            {
                // 验证context.UserName与context.Password 
                // 调用后台的登录服务验证用户名与密码
                var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, userInfo.UserId));
                oAuthIdentity.AddClaim(new Claim("UserId", userInfo.UserId));
                oAuthIdentity.AddClaim(new Claim("UserName", userInfo.UserName));

                var props = new AuthenticationProperties(new Dictionary<string, string> {
                    { "user_id", userInfo.UserId },
                    { "user_name", userInfo.UserName }
                });

                var ticket = new AuthenticationTicket(oAuthIdentity, props);

                context.Validated(ticket);

                await base.GrantResourceOwnerCredentials(context);
            }
        }

        /// <summary>
        /// 把Context中的属性加入到token中
        /// 示例如下：
        /// user_id：6a671bd3-8a5b-4e04-b88e-c9a1a64214a6
        /// user_name：Bobby
        /// issued：Tue, 08 Nov 2016 06:37:46 GMT
        /// expires：Tue, 08 Nov 2016 08:37:46 GMT
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>  
        /// 验证客户端 [Authorization Basic Base64(clientId:clientSecret)|Authorization: Basic 5zsd8ewF0MqapsWmDwFmQmeF0Mf2gJkW]
        /// 对third party application 认证，  
        /// 为third party application颁发appKey和appSecrect，在此省略了颁发appKey和appSecrect的环节，  
        /// 认为所有的third party application都是合法的  
        /// </summary>  
        /// <param name="context"></param>  
        /// <returns></returns>  
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // 表示所有允许此third party application请求  
            context.Validated();
            return Task.FromResult<object>(null);
        }

    }
}