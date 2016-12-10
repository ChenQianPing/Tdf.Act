using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.Utils.Networking
{
    public class ServiceResult
    {
        private int _retCode = 0;
        private string _msg = "success";
        private object _result = "";

        /// <summary>
        /// 返回码
        /// </summary>
        public int retCode
        {
            get { return _retCode; }
            set { _retCode = value; }
        }

        /// <summary>
        /// 返回说明
        /// </summary>
        public string msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        /// <summary>
        /// 返回结果集
        /// </summary>
        public object result
        {
            get { return _result; }
            set { _result = value; }
        }

        public ServiceResult() { }


        public ServiceResult(object result)
        {
            _result = result;
        }

        public ServiceResult(string msg, object result)
        {
            _retCode = 0;
            _msg = msg;
            _result = result;
        }

        public ServiceResult(int retCode, string msg, object result)
        {
            _retCode = retCode;
            _msg = msg;
            _result = result;
        }

    }
}
