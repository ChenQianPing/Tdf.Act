using System.Text.RegularExpressions;

namespace Tdf.Utils.Helper
{
    public class RegexHelper
    {
        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^13\\d{9}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的Email地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
    }
}
