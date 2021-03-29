using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI
{
	public partial class Home : UIUserControlBase
	{
		protected Dictionary<DeliveryStatus, Tuple<int, int>> dictionaryDelivery = new Dictionary<DeliveryStatus, Tuple<int, int>>();
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ReferenceGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ReferenceGuid"); }
		}

		private string ReturnPath
		{
			get
			{
				if (Helper.RequestBool(this, "UsersOutbox"))
					return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserOutbox, Session);
				else
					return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_OutBox, Session);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				btnReturn.Text = Language.GetString(btnReturn.Text);

				if (ReferenceGuid != Guid.Empty)
				{
					DataTable dtReport = Facade.Outbox.GetOutboxReport(ReferenceGuid);

					if (dtReport.Rows.Count > 0)
					{
                        //dictionaryDelivery.Add(DeliveryStatus.SentAndReceivedbyPhone, Helper.GetInt(dtReport.Rows[0]["DeliveredCount"]));
                        //dictionaryDelivery.Add(DeliveryStatus.NotSent, Helper.GetInt(dtReport.Rows[0]["FailedCount"]));
                        //dictionaryDelivery.Add(DeliveryStatus.SentToItc, Helper.GetInt(dtReport.Rows[0]["SentToICTCount"]));
                        //dictionaryDelivery.Add(DeliveryStatus.ReceivedByItc, Helper.GetInt(dtReport.Rows[0]["DeliveredICTCount"]));
                        //dictionaryDelivery.Add(DeliveryStatus.BlackList, Helper.GetInt(dtReport.Rows[0]["BlackListCount"]));
                        dictionaryDelivery.Add(DeliveryStatus.SentAndReceivedbyPhone, new Tuple<int, int>(Helper.RequestInt(this, "DeliveredCount"), Helper.RequestInt(this, "DeliveredSmsCount")));
                        dictionaryDelivery.Add(DeliveryStatus.NotSent, new Tuple<int, int>(Helper.RequestInt(this, "FailedCount"), Helper.RequestInt(this, "FailedSmsCount")));
                        dictionaryDelivery.Add(DeliveryStatus.SentToItc, new Tuple<int, int>(Helper.RequestInt(this, "SentToICTCount"), Helper.RequestInt(this, "SentToICTSmsCount")));
                        dictionaryDelivery.Add(DeliveryStatus.ReceivedByItc, new Tuple<int, int>(Helper.RequestInt(this, "ReceivedByItcCount"), Helper.RequestInt(this, "ReceivedByItcSmsCount")));
                        dictionaryDelivery.Add(DeliveryStatus.BlackList, new Tuple<int, int>(Helper.RequestInt(this, "BlackListCount"), Helper.RequestInt(this, "BlackListSmsCount")));
                    }
				}
				else
				{
					dictionaryDelivery.Add(DeliveryStatus.SentAndReceivedbyPhone, new Tuple<int, int>(Helper.RequestInt(this, "DeliveredCount"), Helper.RequestInt(this, "DeliveredSmsCount")));
					dictionaryDelivery.Add(DeliveryStatus.NotSent, new Tuple<int, int>(Helper.RequestInt(this, "FailedCount"), Helper.RequestInt(this, "FailedSmsCount")));
					dictionaryDelivery.Add(DeliveryStatus.SentToItc, new Tuple<int, int>(Helper.RequestInt(this, "SentToICTCount"), Helper.RequestInt(this, "SentToICTSmsCount")));
					dictionaryDelivery.Add(DeliveryStatus.ReceivedByItc, new Tuple<int, int>(Helper.RequestInt(this, "ReceivedByItcCount"), Helper.RequestInt(this, "ReceivedByItcSmsCount")));
					dictionaryDelivery.Add(DeliveryStatus.BlackList, new Tuple<int, int>(Helper.RequestInt(this, "BlackListCount"), Helper.RequestInt(this, "BlackListSmsCount")));
				}
			}
			catch { }
		}

		protected void btnReturn_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", ReturnPath));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Home;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Home.ToString());
		}
	}
}
