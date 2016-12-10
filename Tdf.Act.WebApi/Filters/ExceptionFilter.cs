using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Tdf.Utils.Excp;
using Tdf.Utils.Networking;

namespace Tdf.WebApi.Filters
{
    /// <summary>
    /// WebApi异常拦截器
    /// 自定义异常特性实现 ExceptionFilterAttribute
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception as CustomException;
            if (exception != null)
            {
                CustomException ex = exception;
                context.Response = context.Request.CreateResponse(HttpStatusCode.OK, new ServiceResult() { retCode = ex.ErrCode, msg = ex.Message });
            }
            else
            {
                Log.Error(context.Exception);
                context.Response = context.Request.CreateResponse(HttpStatusCode.OK, "系统繁忙，此时请稍候再试");
            }
        }
    }
}