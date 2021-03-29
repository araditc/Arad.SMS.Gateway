using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class ShowUserSmsRates : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public ShowUserSmsRates()
		{
			AddDataBinderHandlers("gridAgentRatio", new DataBindHandler(gridAgentRatio_OnDataBind));
			AddDataRenderHandlers("gridAgentRatio", new CellValueRenderEventHandler(gridAgentRatio_OnDataRender));
		}

		public DataTable gridAgentRatio_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string agentGuid = string.Empty;
			string agentTitle = string.Empty;
			decimal ratio;

			List<DataRow> lstAgents = Facade.SmsSenderAgent.GetUserAgents(Facade.SmsSenderAgent.GetFirstParentMainAdmin(UserGuid)).AsEnumerable().ToList();

			DataTable dtAgentRatio = new DataTable();
			dtAgentRatio.Columns.Add("Guid", typeof(Guid));
			dtAgentRatio.Columns.Add("Price", typeof(string));
			dtAgentRatio.Columns.Add("AgentID", typeof(string));
			dtAgentRatio.Columns.Add("Agent", typeof(string));
			dtAgentRatio.Columns.Add("Ratio", typeof(decimal));

			Common.User user = Facade.User.LoadUser(UserGuid);
			Common.GroupPrice groupPrice = Facade.GroupPrice.LoadGroupPrice(user.PriceGroupGuid);
			string price = Helper.FormatDecimalForDisplay(groupPrice.BasePrice);

			var xelement = XElement.Parse(groupPrice.AgentRatio);
			List<XElement> lstOperatorElement = xelement.Elements("Table").ToList();

			foreach (var item in lstOperatorElement)
			{
				agentGuid = item.Element("AgentID").Value;
				agentTitle = lstAgents.Where(agent => Helper.GetGuid(agent["Guid"]) == Helper.GetGuid(agentGuid)).First()["Name"].ToString();
				ratio = Helper.GetDecimal(item.Element("Ratio").Value);

				dtAgentRatio.Rows.Add(Guid.NewGuid(), price, agentGuid, agentTitle, ratio);
			}
			return dtAgentRatio;

		}

		public string gridAgentRatio_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e) { }

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return 0;
		}

		protected override string GetUserControlTitle()
		{
			return string.Empty;
		}
	}
}