using Arad.SMS.Gateway.GeneralLibrary;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Arad.SMS.Gateway.Web.UI.Handlers
{
	public class UploadUserDocument : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/HTML";

			try
			{
				string[] validExtension = { ".pdf", ".png", ".jpeg", ".jpg" };
				string responseValue = string.Empty;
				string userName = context.Request.Params["user"];
				Business.UserDocumentType userDocument = (Business.UserDocumentType)Helper.GetInt(context.Request.Params["documentId"]);

				HttpPostedFile postedFile = context.Request.Files["upload"];

				string fileName = postedFile.FileName;
				string fileExtention = Path.GetExtension(fileName).ToLower();

				if (!validExtension.Contains(fileExtention))
					throw new Exception(Language.GetString("InvalidFileExtension"));

				string savePath = string.Empty;

				string uploadFolder = "~/Uploads/UserDocuments/";

				savePath = context.Server.MapPath(uploadFolder + userName);

				if (!Directory.Exists(savePath))
					Directory.CreateDirectory(savePath);

				if (!savePath.EndsWith("\\")) savePath += "\\";

				postedFile.SaveAs(savePath + (userDocument + fileExtention));

				responseValue = "Result{(OK)}" +
												"Path{(" + (userName + "/" + userDocument + fileExtention) + ")}" +
												"File{(" + fileName + ")}" +
												"DocumentId{(" + (int)userDocument + ")}";
				context.Response.Write(responseValue);
			}
			catch (Exception ex)
			{
				context.Response.Write("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
