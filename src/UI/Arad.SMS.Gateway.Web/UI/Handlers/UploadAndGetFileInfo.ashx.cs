using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Web.UI.Handlers
{
	public class UploadAndGetFileInfo : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/HTML";
			context.Response.Expires = -1;

			try
			{
				string[] validExtension = { ".xls", ".xlsx", ".csv" };
				string responseValue = string.Empty;
				HttpPostedFile postedFile = context.Request.Files["upload"];

				string fileName = postedFile.FileName;
				string fileExtention = Path.GetExtension(fileName).ToLower();

				if (!validExtension.Contains(fileExtention))
					throw new Exception(Language.GetString("InvalidFileExtension"));

				string savePath = string.Empty;

				string uploadFolder = "~/Uploads/";

				string newDirectoryName = new Random().Next(100, 99999).ToString();

				savePath = context.Server.MapPath(uploadFolder + newDirectoryName);

				while (Directory.Exists(savePath))
				{
					newDirectoryName = new Random().Next(100, 999999).ToString();
					savePath = context.Server.MapPath(uploadFolder + newDirectoryName);
				}

				Directory.CreateDirectory(savePath);

				if (!savePath.EndsWith("\\")) savePath += "\\";

				postedFile.SaveAs(savePath + fileName);

				List<string> lstNumbers = new List<string>();
				Dictionary<string, string> dicFileInfo = Facade.PhoneNumber.GetFileNumberInfo((savePath + fileName), lstNumbers);


				string fileInfo = string.Format("<p>" + Language.GetString("File") + " {0}</p>" +
                    "<p>"+ Language.GetString("NumberCount") + "</p>" +
                    "< p>" + Language.GetString("RepetitiousNumberCount") + "</p>" +
                    "< p>" + Language.GetString("CorrectNumberCount") + "</p>", fileName, dicFileInfo["TotalNumberCount"], dicFileInfo["DuplicateNumberCount"], dicFileInfo["CorrectNumberCount"]);

				responseValue += "Result{(OK)}Info{(" + fileInfo + ")}CorrectNumberCount{(" + dicFileInfo["CorrectNumberCount"] + ")}Path{(" + newDirectoryName + "/" + fileName + ")}";

				context.Response.Write(responseValue);
				context.Response.StatusCode = 200;
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
