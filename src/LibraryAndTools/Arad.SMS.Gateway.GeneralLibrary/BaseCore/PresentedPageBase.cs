using GeneralLibrary.DependencyInjection;
using System;
using System.Web;
using System.Web.UI;

namespace GeneralLibrary.BaseCore
{
	public class PresentedPageBase<TPresenter> : Page
		where TPresenter : PresenterBase
	{
		TPresenter presenter;

		private string MasterPage
		{
			get
			{
				if (presenter.Layout == StandardLayouts.AdminLayout)
					return Server.MapPath(string.Format(@"~/HomePages/{0}/Admin.master", Helper.GetString(HttpContext.Current.Session["Theme"])));
				else
					return Server.MapPath(string.Format(@"~/HomePages/{0}/Home.master", Helper.GetString(HttpContext.Current.Session["Theme"])));
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (!IsPostBack)
				presenter.FirstTimeInit();

			presenter.Load();
		}

		protected override void OnPreInit(EventArgs e)
		{
			presenter = CreatePresenter();

			this.MasterPageFile = MasterPage;
			base.OnPreInit(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if (!presenter.CheckServicePermissions())
				Response.Redirect(string.Format("~/ErrorHandler.aspx?ErrorType={0}", (int)ErrorType.OneServiceError));
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			presenter.PreRender();
		}

		private TPresenter CreatePresenter()
		{
			var iocContainer = WindsorIocContainer.Instance;
			return iocContainer.Resolve<TPresenter>();
		}

		public Uri Uri
		{
			get
			{
				return HttpContext.Current.Request.Url;
			}
		}
	}
}