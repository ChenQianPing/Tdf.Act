using System;
using Tdf.Act.Domain.Repositories;
using Tdf.Act.Domain.Services;
using Tdf.Commanding;
using Tdf.Utils.Excp;

namespace Tdf.Act.Domain.Commands.Executors
{
    public class RegisterCommandExecutor : ICommandExecutor<RegisterCommand>
    {
        public IUserRepository _repository;

        public RegisterCommandExecutor(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(RegisterCommand cmd)
        {
            #region 验证传入的Command对象是否合法
            if (String.IsNullOrEmpty(cmd.UserName))
                throw new CustomException(-1, "UserName is required.");
            if (String.IsNullOrEmpty(cmd.Email))
                throw new CustomException(-1, "Email is required.");
            if (String.IsNullOrEmpty(cmd.Phone))
                throw new CustomException(-1, "Phone is required.");

            if (cmd.Password != cmd.ConfirmPassword)
                throw new CustomException(-1, "Password not match.");

            // other command validation logics

            #endregion

            #region 调用领域模型完成操作
            var service = new UserService(_repository);
            var user = service.Register(cmd.UserName, cmd.Password, cmd.Phone, cmd.Email);

            cmd.ExecutionResult = new RegisterCommandResult
            {
                GeneratedUserId = user.Id.ToString()
            };
            #endregion
        }
    }
}
