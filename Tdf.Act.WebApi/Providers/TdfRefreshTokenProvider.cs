using Microsoft.Owin.Security.Infrastructure;
using System;
using Tdf.Act.Domain.Commands;
using Tdf.Commanding;
using Tdf.Dependency;
using Tdf.Utils.Config;
using Tdf.Utils.GuidHelper;

namespace Tdf.Act.WebApi.Providers
{
    /// <summary>
    /// 刷新Token,生成与验证Token
    /// </summary>
    public class TdfRefreshTokenProvider : AuthenticationTokenProvider
    {
        /// <summary>
        /// 授权服务
        /// </summary>
        private ICommandBus _commandBus;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService">授权服务</param>
        public TdfRefreshTokenProvider(ICommandBus commandBus)
        {
            this._commandBus = commandBus;
        }

        public TdfRefreshTokenProvider()
            : this(ObjectContainer.Resolve<ICommandBus>())
        {
        }

        /// <summary>
        /// 创建refreshToken
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public override void Create(AuthenticationTokenCreateContext context)
        {
            if (string.IsNullOrEmpty(context.Ticket.Identity.Name)) return;

            var refreshTokenLifeTime = ConfigHelper.GetValue("TokenExpireMinute", "120");
            if (string.IsNullOrEmpty(refreshTokenLifeTime)) return;

            // generate access token
            var refreshTokenId = new RegularGuidGenerator().Create();

            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(double.Parse(refreshTokenLifeTime));

            var cmd = new SaveTokenCommand()
            {
                RefreshTokenId = refreshTokenId,
                UserId = new Guid(context.Ticket.Identity.Name),
                IssuedUtc = DateTime.Parse(context.Ticket.Properties.IssuedUtc.ToString()),
                ExpiresUtc = DateTime.Parse(context.Ticket.Properties.ExpiresUtc.ToString()),
                ProtectedTicket = context.SerializeTicket()
            };

            // Token没有过期的情况强行刷新，删除老的Token保存新的Token
            _commandBus.Send(cmd);
            context.SetToken(refreshTokenId.ToString());
        }

        /// <summary>
        /// 刷新refreshToken[刷新access token时，refresh token也会重新生成]
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            var getTokenCmd = new GetTokenCommand();
            var removeTokenCmd = new RemoveTokenCommand();
            getTokenCmd.TokenId = context.Token;
            _commandBus.Send(getTokenCmd);

            var refreshToken = getTokenCmd.ExecutionResult;
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                removeTokenCmd.TokenId = context.Token;
                _commandBus.Send(removeTokenCmd);
            }
        }

    }
}