using GeneralLibrary;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi
{
	public class CustomHttpAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
			{
				OwinContext owinContext = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
				string ipAddress = owinContext.Request.RemoteIpAddress;

				List<string> lstValidIps = ConfigurationManager.GetSetting("ValidIp").Split(',').ToList();

				//if (!lstValidIps.Contains(ipAddress))
				//	actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "Ip Not Valid!" };
			}
		}
	}
}
