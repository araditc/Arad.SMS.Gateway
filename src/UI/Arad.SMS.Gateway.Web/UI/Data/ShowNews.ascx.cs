using System;
using System.Collections.Generic;
using GeneralLibrary.BaseCore;
using GeneralLibrary;

namespace MessagingSystem.UI.Data
{
	public partial class ShowNews : UIUserControlBase
	{
		private Guid NewsGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "NewsGuid"); }
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			Common.Data news = Facade.Data.LoadData(NewsGuid);
			if (news == null)
				return;
			ltrTitle.Text = news.Title;
			ltrContent.Text = news.Content;
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Data_News;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Data_News.ToString());
		}

	}
}