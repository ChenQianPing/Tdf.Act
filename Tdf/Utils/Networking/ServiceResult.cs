using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.Utils.Networking
{
    public class ServiceResult
    {
        private int _code = 0;
        private string _message = "请求成功";
        private object _data = "";

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public ServiceResult(object data)
        {
            _data = data;
        }

        public ServiceResult(string message, object data)
        {
            _code = 0;
            _message = message;
            _data = data;
        }

        public ServiceResult(int code, string message, object data)
        {
            _code = code;
            _message = message;
            _data = data;
        }

    }
}
