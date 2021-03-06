﻿using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class UserPrivateNumber : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UserPrivateNumber()
		{
			AddDataBinderHandlers("gridUserPrivateNumbers", new DataBindHandler(gridUserPrivateNumbers_OnDataBind));
			AddDataRenderHandlers("gridUserPrivateNumbers", new CellValueRenderEventHandler(gridUserPrivateNumbers_OnDataRender));
		}

		public DataTable gridUserPrivateNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PrivateNumber.GetUserNumbers(UserGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public string gridUserPrivateNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string action = string.Empty;
			bool activeShare = true;
			switch (sender.FieldName)
			{
				case "Action":

					if (Helper.GetInt(e.CurrentRow["UseForm"]) == (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber &&
							e.CurrentRow["Number"].ToString() == string.Empty)
						activeShare = false;

					if (Helper.GetBool(e.CurrentRow["IsExpired"]))
						action = string.Format(@"<a href=""/PageLoader.aspx?c={0}&Guid={1}""><span class='ui-icon fa fa-check green' title=""{2}""></span></a>",
																		Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_ExtendedNumber, Session),
																		e.CurrentRow["Guid"],
																		Language.GetString("ExtentionNumber"));

					action += string.Format(@"<a href=""/PageLoader.aspx?c={0}&Guid={1}""><span class='ui-icon fa fa-share' title=""{2}""></span></a>",
																	Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_SetTrafficRelay, Session),
																	e.CurrentRow["Guid"],
																	Language.GetString("TrafficRelay"));
					action += string.Format(@"<span class='ui-icon fa fa-share-alt orange' title=""{0}"" onclick=""{1}"" style=""{2}""></span>",
																	Language.GetString("Public"),
																	activeShare ? "setPublic(event);" : string.Empty,
																	activeShare ? string.Empty : "opacity: 0.5;filter: Alpha(Opacity=20);");

					return action;
				case "Number":
					switch (Helper.GetInt(e.CurrentRow["UseForm"]))
					{
						case (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.Mask:
						case (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.OneNumber:
							return e.CurrentRow["Number"].ToString();
						case (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber:
							return string.Format("{0}", e.CurrentRow["Range"]);
						default:
							return string.Empty;
					}

				case "Type":
					return Helper.GetString((Business.TypePrivateNumberAccesses)Helper.GetInt(e.CurrentRow["Type"]));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			gridUserPrivateNumbers.TopToolbarItems = string.Format("<a href=\"/PageLoader.aspx?c={0}\" class=\"btn btn-success toolbarButton\" >{1}</a>",
																															Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_NumberStatus, Session), Language.GetString("UserLines"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManagePrivateNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_UserPrivateNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PrivateNumbers_UserPrivateNumber.ToString());
		}
	}
}