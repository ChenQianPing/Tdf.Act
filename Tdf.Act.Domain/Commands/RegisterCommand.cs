using Tdf.Commanding;

namespace Tdf.Act.Domain.Commands
{
    public class RegisterCommand : ICommand
    {
        public string Email { get; set; }
        public string Phone { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public RegisterCommandResult ExecutionResult { get; set; }

        public RegisterCommand()
        {
        }
    }

    public class RegisterCommandResult
    {
        public string GeneratedUserId { get; set; }
    }
}
