using System;
using System.Web;
using System.Web.UI;

namespace GeneralLibrary.BaseCore
{
	public abstract class PageBase : Page
	{
		public string MasterPage
		{
			get
			{
				return Helper.GetString(HttpContext.Current.Session[""]);
			}
		}

		public bool SetMasterPage { get; set; }
		protected override void OnPreInit(EventArgs e)
		{
			this.MasterPageFile = MasterPage;
			base.OnPreInit(e);
		}
	}
}
