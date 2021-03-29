using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GeneralLibrary;

namespace MessagingSystem.UI.Controls
{
    public partial class NavigationBar : System.Web.UI.UserControl
    {
        private int rowCount;

        public int RowCount
        {
            set
            {
                ViewState["RowCount"] = value;
                rowCount = value;
                FillPageNoDropDown();

                SetNavigationInfo();
            }
        }
        public int PageSize
        {
            get
            {
                if (drpPageSize.SelectedValue != "")
                    ViewState["PageSize"] = drpPageSize.SelectedValue;
                else
                    ViewState["PageSize"] = 10;

                return Helper.GetInt(ViewState["PageSize"]);
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }
        public int PageNo
        {
            get
            {
                if (drpPageNo.SelectedValue != "")
                    ViewState["PageNo"] = drpPageNo.SelectedValue;
                else
                    ViewState["PageNo"] = 1;

                return Helper.GetInt(ViewState["PageNo"]);
            }
            set
            {
                ViewState["PageNo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetNavigationInfo()
        {
            if (rowCount != 0)
            {
                lblRecordNo.Text = "نمایش " + ((PageNo - 1) * PageSize + 1) + " تا " + (PageNo * PageSize) + " از " + rowCount;
                drpPageSize.SelectedValue = PageSize.ToString();
            }
            else
            {
                lblRecordNo.Text = "";
                drpPageSize.SelectedValue = "10";
            }
        }

        private void FillPageNoDropDown()
        {
            int pageCount = rowCount / PageSize;
            int startPage = 1;
            int pageNo = PageNo;

            if (PageNo > pageCount)
                pageNo = 1;

            if (pageNo >= 11)
            {
                startPage = pageNo - 3;
                drpPageNo.Items.Add(new ListItem("... ", (startPage - 1).ToString()));
            }

            pageCount = (rowCount % PageSize) == 0 ? pageCount : pageCount + 1;
            drpPageNo.Items.Clear();

            for (int counter = startPage; (counter < pageCount && counter < startPage + 10); counter++)
                drpPageNo.Items.Add(new ListItem(" " + counter + " از " + pageCount, counter.ToString()));

            if (pageCount > startPage + 10)
                drpPageNo.Items.Add(new ListItem("... ", (startPage + 10).ToString()));

            drpPageNo.SelectedValue = pageNo.ToString();
        }
    }
}