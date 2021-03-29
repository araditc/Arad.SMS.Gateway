using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsParsers
{
	public partial class ParserChart : UIUserControlBase
	{
		protected Guid ParserGuid
		{
			get { return Helper.RequestGuid(this, "ParserGuid"); }
		}
		private string ReturnPath
		{
			get
			{
				switch (Helper.RequestInt(this, "ParserType"))
				{
					case (int)Arad.SMS.Gateway.Business.SmsParserType.Poll:
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Polls_Poll, Session);
					case (int)Arad.SMS.Gateway.Business.SmsParserType.Competition:
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_Competition, Session);
					default:
						return string.Empty;
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnReturn.Text = Language.GetString(btnReturn.Text);
		}

		protected void btnReturn_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("~/PageLoader.aspx?c={0}",ReturnPath));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			isOptionalPermissions = true;
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Competition);
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Poll);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_ParserChart;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsParsers_ParserChart.ToString());
		}
	}
}