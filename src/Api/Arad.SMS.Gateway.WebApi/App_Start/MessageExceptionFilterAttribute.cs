using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebApi
{
	public class MessageExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			var exception = actionExecutedContext.Exception;

			if (exception is IpNotValidException)
				actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "Ip Not Valid!" };
		}
	}
}
