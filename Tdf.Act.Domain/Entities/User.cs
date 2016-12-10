using System;
using Tdf.Utils.GuidHelper;
using Tdf.Utils.Helper;

namespace Tdf.Act.Domain.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }

        public User() { }
        
        public User(string userName, string password, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("登录账号不能为空！");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("邮箱不能为空！");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException("手机号不能为空！");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("密码不能为空！");
            }

            if (password.Trim().Length < 6)
            {
                throw new ArgumentNullException("密码长度应超过6个字符！");
            }

            if (!RegexHelper.IsMobilePhone(phone))
            {
                throw new ArgumentNullException("请输入正确的手机号！");
            }

            if (!RegexHelper.IsEmail(email))
            {
                throw new ArgumentNullException("请输入正确的邮箱！");
            }

            Id = new RegularGuidGenerator().Create();
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }

     
    }
}
