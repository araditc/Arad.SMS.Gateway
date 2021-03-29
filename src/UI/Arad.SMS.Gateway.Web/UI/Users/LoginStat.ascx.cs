using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class LoginStat : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public LoginStat()
		{
			AddDataBinderHandlers("gridLoginStats", new DataBindHandler(gridLoginStats_OnDataBind));
			AddDataRenderHandlers("gridLoginStats", new CellValueRenderEventHandler(gridLoginStats_OnDataRender));
		}

		public DataTable gridLoginStats_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			//Common.LoginStat loginStat = new Common.LoginStat();
			//loginStat.UserGuid = UserGuid;
			//loginStat.IP = Helper.ImportData(searchFiletrs, "IP");
			//loginStat.CreateDate = DateManager.GetChristianDateForDB(Helper.ImportData(searchFiletrs, "CreateDate"));
			//int type = Helper.GetInt(Helper.ImportData(searchFiletrs, "Type"));
			return Facade.LoginStat.GetUserLoginStats(UserGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridLoginStats_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string imgPattern = @"<img src=""{0}"" title=""{1}"" />";
			switch (sender.FieldName)
			{
				case "Type":
					if (e.CurrentRowGenerateType == RowGenerateType.Normal)
					{
						if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)Arad.SMS.Gateway.Business.LoginStatsType.SignIn)
							return string.Format(imgPattern, "/pic/signin.png", Language.GetString("SignIn"));
						else if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)Arad.SMS.Gateway.Business.LoginStatsType.SignOut)
							return string.Format(imgPattern, "/pic/signout.png", Language.GetString("SignOut"));
					}
					else
						return (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)Arad.SMS.Gateway.Business.LoginStatsType.SignIn) ? Language.GetString("SignIn") : Language.GetString("SignOut");
					break;
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.LoginStat);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_LoginStat;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_LoginStat.ToString());
		}

	}
}