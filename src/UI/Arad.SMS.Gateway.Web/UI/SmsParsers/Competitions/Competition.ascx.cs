﻿using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsParsers.Competitions
{
	public partial class Competition : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public Competition()
		{
			AddDataBinderHandlers("gridCompetition", new DataBindHandler(gridCompetition_OnDataBind));
			AddDataRenderHandlers("gridCompetition", new CellValueRenderEventHandler(gridCompetition_OnDataRender));
		}

		public DataTable gridCompetition_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			Common.SmsParser smsParser = new Common.SmsParser();
			smsParser.Type = (int)Arad.SMS.Gateway.Business.SmsParserType.Competition;
			smsParser.UserGuid = UserGuid;

			return Facade.SmsParser.GetPagedSmsParsers(smsParser, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridCompetition_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-play-circle-o green' title=""{0}"" onClick=""activeParser(event);""></span>",
											Language.GetString("Active")) +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&ParserGuid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_SaveCompetition, Session),
											e.CurrentRow["Guid"],
											Language.GetString("Edit")) +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ParserGuid={1}&ParserType={2}""><span class='ui-icon fa fa-2x fa-mobile orange' title=""{3}""></span></a>",
											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_SelectedOption, Session),
											e.CurrentRow["Guid"],
											(int)Arad.SMS.Gateway.Business.SmsParserType.Competition,
											Language.GetString("NumberReceived")) +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ParserGuid={1}&ParserType={2}""><span class='ui-icon fa fa-2x fa-bar-chart-o purple' title=""{3}""></span></a>",
											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_ParserChart, Session),
											e.CurrentRow["Guid"],
											(int)Arad.SMS.Gateway.Business.SmsParserType.Competition,
											Language.GetString("Chart")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
											Language.GetString("Delete"));
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
			gridCompetition.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																				Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_SaveCompetition, Session),
																				Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Competition);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_Competition;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsParsers_Competitions_Competition.ToString());
		}
	}
}