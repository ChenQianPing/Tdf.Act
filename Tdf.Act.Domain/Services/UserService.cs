using System;
using Tdf.Act.Domain.Entities;
using Tdf.Act.Domain.Repositories;
using Tdf.Utils.Excp;

namespace Tdf.Act.Domain.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region ChangePwd
        public User ChangePwd(Guid userId, string oldPwd, string newPwd)
        {
            var user = _userRepository.FirstOrDefault(p => p.Id == userId);
            if (user == null)
                throw new CustomException(-1, "该会员信息不存在！");

            var pwd = oldPwd;
            if (user.Password != pwd)
                throw new CustomException(-1, "会员密码不正确！");

            user.Password = newPwd;

            var result = _userRepository.Update(user);
            return result;

        }
        #endregion

        #region CheckUserRegistration
        private void CheckUserRegistration(User user)
        {
            if (_userRepository.IsExist(p => p.UserName == user.UserName))
            {
                throw new CustomException(-1, "登录账号已注册，不能重复注册！");
            }

            if (_userRepository.IsExist(p => p.Email == user.Email))
            {
                throw new CustomException(-1, "邮箱已注册，不能重复注册！");
            }

            if (_userRepository.IsExist(p => p.Phone == user.Phone))
            {
                throw new CustomException(-1, "手机号已注册，不能重复注册！");
            }
        }
        #endregion

        #region Register
        public User Register(string userName, string password, string phone, string email)
        {
            var user = new User(userName, password, phone, email);
            CheckUserRegistration(user);

            var result = _userRepository.Insert(user);
            return result;
        }
        #endregion

        #region Login
        public User Login(string userName, string pwd)
        {
            var entity = _userRepository.FirstOrDefault(p => p.UserName == userName || p.Email == userName || p.Phone == userName);

            if (entity == null)
                throw new CustomException(-1, "未找到数据!");

            string password = pwd;
            if (!password.Equals(entity.Password))
                throw new CustomException(-1, "用户名和密码错误!");

            return entity;
        }
        #endregion

    }
}
