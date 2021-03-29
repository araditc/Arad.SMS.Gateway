using Arad.SMS.Gateway.GeneralLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Arad.SMS.Gateway.Web.UI.Handlers
{
	public class UploadAndGetColumnInfo : IHttpHandler
	{
		private class Column
		{
			public int index { get; set; }
			public string name { get; set; }
		}

		private class Info
		{
			public string FileName { get; set; }
			public string Path { get; set; }
			public List<Column> lstFileColumn { get; set; }
		}
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/json";
			context.Response.Expires = -1;
			var columns = new List<Column>();
			var info = new Info();

			try
			{
				string[] validExtension = { ".xls", ".xlsx" };

				string responseValue = string.Empty;
				HttpPostedFile postedFile = context.Request.Files["upload"];

				string fileName = postedFile.FileName;
				string fileExtention = Path.GetExtension(fileName).ToLower();

				if (!validExtension.Contains(fileExtention))
					throw new Exception(Language.GetString("InvalidFileExtension"));

				string savePath = string.Empty;

				string uploadFolder = @"~\Uploads\P2P\";

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


				DataTable dtb = ImportFile.ImportExcel((savePath + fileName), true, 5);

				foreach (DataColumn dc in dtb.Columns)
				{
					if (dc.DataType.Name.ToLower() != "string")
						throw new Exception(Language.GetString("TextFileExtension"));

					var c = new Column();
					c.index = dc.Ordinal + 1;
					c.name = dc.ColumnName;

					columns.Add(c);
				}

				info.FileName = fileName;
				info.Path = string.Format(@"{0}{1}\{2}", uploadFolder.Substring(1), newDirectoryName, fileName);
				info.lstFileColumn = columns;

				context.Response.Write(JsonConvert.SerializeObject(info));
				context.Response.StatusCode = 200;
			}
			catch (Exception ex)
			{
				context.Response.Write(ex.Message);
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
