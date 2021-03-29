using DocumentFormat.OpenXml.Spreadsheet;
using Arad.SMS.Gateway.GeneralLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GeneralTools.DataGrid
{
	[Serializable]
	[ToolboxItem(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	[ParseChildren(true)]
	public class DataGrid : WebControl
	{
		#region Properties
		private List<DataGridColumnInfo> columns;
		private ListSelectMode selectMode;
		private string defaultSortField;
		private string sortField;
		private string listCaption;
		private int listHeight;
		private int listDifferenceHeight;
		private string postBackUrl;
		private string postBackToExportDataUrl;
		private string jqGridListID;
		private string jqGridPagerID;
		private Direction listDirection;
		private string booleanOn;
		private string booleanOff;
		private bool showRowNumber;
		private string searchDivID;
		private bool showAdvancedSearch;
		private bool showSearchToolbar;
		private bool showFooterRow;
		private bool disableNavigationBar;
		private bool showPagerToTop;
		private string onEnterKeyPress;
		private bool showToolBar;
		private ToolbarPosition toolbarPosition;
		private string topToolbarItems;
		private string bottomToolbarItems;
		private bool showExportData;
		private string disableMultiSelectField;
		private string statusMultiSelectField;
		private bool disableActiveMultiSelectField;
		private bool hasCellEditable;
		private string userData;
		private bool enableGrouping;
		private string groupField;
		private bool groupColumnShow;
		private bool groupCollapse;
		private string gridComplete;
		//static private DataTable outputData = new DataTable();

		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<DataGridColumnInfo> Columns
		{
			get
			{
				return columns;
			}
			internal set
			{
				columns = value;
			}
		}

		public string DefaultSortField
		{
			get
			{
				return defaultSortField;
			}
			set
			{
				defaultSortField = value;
			}
		}

		public string SortField
		{
			get
			{
				string ret = string.Empty;

				if (sortField != string.Empty)
					ret = sortField;

				//if (ret == string.Empty)
				//  ret = GetSetting("SortField");

				if (ret == string.Empty)
					ret = DefaultSortField;

				sortField = ret;
				return ret;
			}
			internal set
			{
				//SetSetting("SortField", value);
				sortField = value;
			}
		}

		public string ListCaption
		{
			get
			{
				return listCaption;
			}
			set
			{
				listCaption = value;
			}
		}

		public bool ShowRowNumber
		{
			get
			{
				return showRowNumber;
			}
			set
			{
				showRowNumber = value;
			}
		}

		public int ListHeight
		{
			get
			{
				return listHeight;
			}
			set
			{
				listHeight = value;
			}
		}

		public int ListDifferenceHeight
		{
			get
			{
				return listDifferenceHeight;
			}
			set
			{
				listDifferenceHeight = value;
			}
		}

		public ListSelectMode SelectMode
		{
			set
			{
				selectMode = value;
			}
			get
			{
				return selectMode;
			}
		}

		public Direction ListDirection
		{
			set
			{
				listDirection = value;
			}
			get
			{
				return listDirection;
			}
		}

		public string SearchDivID
		{
			set
			{
				searchDivID = value;
			}
			get
			{
				return searchDivID;
			}
		}

		public bool ShowAdvancedSearch
		{
			get
			{
				return showAdvancedSearch;
			}
			set
			{
				showAdvancedSearch = value;
			}
		}

		public bool ShowSearchToolbar
		{
			get
			{
				return showSearchToolbar;
			}
			set
			{
				showSearchToolbar = value;
			}
		}

		public bool ShowFooterRow
		{
			get
			{
				return showFooterRow;
			}
			set
			{
				showFooterRow = value;
			}
		}

		public bool DisableNavigationBar
		{
			get { return disableNavigationBar; }
			set { disableNavigationBar = value; }
		}

		public bool ShowPagerToTop
		{
			get { return showPagerToTop; }
			set { showPagerToTop = value; }
		}

		public bool ShowToolbar
		{
			get { return showToolBar; }
			set { showToolBar = value; }
		}

		public string OnEnterKeyPress
		{
			get
			{
				return onEnterKeyPress;
			}
			set
			{
				onEnterKeyPress = value;
			}
		}

		public ToolbarPosition ToolbarPosition
		{
			get { return toolbarPosition; }
			set { toolbarPosition = value; }
		}

		public string TopToolbarItems
		{
			get { return topToolbarItems; }
			set { topToolbarItems = value; }
		}

		public string BottomToolbarItems
		{
			get { return bottomToolbarItems; }
			set { bottomToolbarItems = value; }
		}

		public bool ShowExportData
		{
			get { return showExportData; }
			set { showExportData = value; }
		}

		public string DisableMultiSelectField
		{
			get { return disableMultiSelectField; }
			set { disableMultiSelectField = value; }
		}

		public string StatusMultiSelectField
		{
			get { return statusMultiSelectField; }
			set { statusMultiSelectField = value; }
		}

		public bool DisableActiveMultiSelectField
		{
			get { return disableActiveMultiSelectField; }
			set { disableActiveMultiSelectField = value; }
		}

		public string UserData
		{
			get { return userData; }
			set { userData = value; }
		}

		public bool EnableGrouping
		{
			get { return enableGrouping; }
			set { enableGrouping = value; }
		}

		public string GroupField
		{
			get { return groupField; }
			set { groupField = value; }
		}

		public bool GroupColumnShow
		{
			get { return groupColumnShow; }
			set { groupColumnShow = value; }
		}

		public bool GroupCollapse
		{
			get { return groupCollapse; }
			set { groupCollapse = value; }
		}

		public string GridComplete
		{
			get { return gridComplete; }
			set { gridComplete = value; }
		}

		private string PostBackUrl
		{
			get
			{
				return postBackUrl;
			}
			set
			{
				postBackUrl = value;
			}
		}

		private string PostBackToExportDataUrl
		{
			get
			{
				return postBackToExportDataUrl;
			}
			set
			{
				postBackToExportDataUrl = value;
			}
		}

		private string JQGridListID
		{
			get
			{
				return jqGridListID;
			}
			set
			{
				jqGridListID = value;
			}
		}

		private string JQGridPagerID
		{
			get
			{
				return jqGridPagerID;
			}
			set
			{
				jqGridPagerID = value;
			}
		}

		private bool HasCellEditable
		{
			get { return hasCellEditable; }
			set { hasCellEditable = value; }
		}

		internal string BooleanOn
		{
			get { return booleanOn; }
			set { booleanOn = value; }
		}

		internal string BooleanOff
		{
			get { return booleanOff; }
			set { booleanOff = value; }
		}
		#endregion

		public DataGrid()
		{
			columns = new List<DataGridColumnInfo>();
			selectMode = ListSelectMode.Single;
            if (Context.Session["Language"].ToString() == "en")
            {
                listDirection = Direction.LeftToRight;
            }
            else
            {
                listDirection = Direction.RightToLeft;
            }
            defaultSortField = string.Empty;
			sortField = string.Empty;
			listCaption = string.Empty;
			listHeight = 0;
			listDifferenceHeight = 0;
			postBackUrl = string.Empty;
			postBackToExportDataUrl = string.Empty;
			jqGridListID = string.Empty;
			jqGridPagerID = string.Empty;
			booleanOn = string.Empty;
			booleanOff = string.Empty;
			searchDivID = string.Empty;
			showAdvancedSearch = false;
			showFooterRow = false;
			disableNavigationBar = false;
			onEnterKeyPress = string.Empty;
			ShowToolbar = false;
			ToolbarPosition = ToolbarPosition.Top;
			TopToolbarItems = string.Empty;
			BottomToolbarItems = string.Empty;
			ShowExportData = true;
			disableMultiSelectField = string.Empty;
			statusMultiSelectField = string.Empty;
			hasCellEditable = false;

			DataGridColumnInfo keyColumn = new DataGridColumnInfo();
			keyColumn.Hidden = true;
			keyColumn.FieldName = "Guid";
			keyColumn.FormattingMethod = CellFormattingMethod.Encrypt;
			keyColumn.Sortable = false;
			columns.Add(keyColumn);
		}

		#region Bind, Render And Export Methods
		protected override void OnPreRender(EventArgs e)
		{
			postBackUrl = "DataGridHandler.aspx/GetDataInfo";
			postBackToExportDataUrl = "DataGridHandler.aspx?method=GetDataToExport";
			jqGridListID = "JQGridList_" + this.ID;
			jqGridPagerID = "JQGridPager_" + this.ID;
			booleanOn = GetImageResourceUrl("BooleanOn.png");
			booleanOff = GetImageResourceUrl("BooleanOff.png");

			base.OnPreRender(e);
		}

		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(CreateStyleAndJavaScripts() + GenerateInlineScripts() + CraeteBodyControl());
		}

		public DataGridResult DataBind(string sortField, string sortOrder, int pageNo, int pageSize, string searchFiletrs, string toolbarFilters, Delegate OnDataBindHandler, Delegate OnRenderEvent)
		{
			DataGridResult result = new DataGridResult();
			if (OnDataBindHandler == null)
				return result;
			//throw new Exception("OnDataBind event not implement!!");

			int resultCount = 0;
			int rowIndex = 1;
			int columnIndex = 0;
			string customData = string.Empty;

			if (sortField.StartsWith("["))
				sortField = sortField + " " + sortOrder;
			else
				sortField = "[" + sortField + "] " + sortOrder;

			DataTable data = ((DataBindHandler)OnDataBindHandler)(sortField, searchFiletrs, toolbarFilters, this.UserData, pageNo, pageSize, ref resultCount, ref customData);
			DataRow summaryDataRow = null;
			List<DataGridRowData> dataRows = new List<DataGridRowData>();
			DataGridRowData newRow;

			foreach (DataRow currentRow in data.Rows)
			{
				if (currentRow.RowError == "SummaryRow")
				{
					summaryDataRow = currentRow;
					continue;
				}
				newRow = new DataGridRowData();
				newRow.id = rowIndex++;
				newRow.cell = new string[Columns.Count];  //total number of columns

				columnIndex = 0;
				foreach (DataGridColumnInfo columnInfo in Columns)
					newRow.cell[columnIndex++] = GetCellValue(columnInfo, new CellValueRenderEventArgs(currentRow, RowGenerateType.Normal, false), OnRenderEvent);

				dataRows.Add(newRow);
			}

			if (summaryDataRow != null)
			{
				string summaryData = string.Empty;
				columnIndex = 0;

				foreach (DataGridColumnInfo columnInfo in Columns)
				{
					try
					{
						if (summaryDataRow[columnInfo.FieldName] != DBNull.Value)
							summaryData += string.Format("{0}:'{1}',", columnInfo.FieldName, GetCellValue(columnInfo, new CellValueRenderEventArgs(summaryDataRow, RowGenerateType.Normal, true), OnRenderEvent));
					}
					catch { }
				}

				result.userdata = "[{" + summaryData.TrimEnd(',') + "}";
				result.userdata += string.IsNullOrEmpty(customData) ? string.Empty : ",{" + customData + "}";
				result.userdata += "]";
			}

			result.rows = dataRows.ToArray();
			result.page = pageNo;
			result.total = (int)Math.Ceiling((double)resultCount / pageSize);
			result.records = resultCount;

			return result;
		}

		public byte[] ExportToPdf(string sortField, string sortOrder, string searchFiletrs, string toolbarFilters, Delegate OnDataBindHandler, Delegate OnRenderEvent)
		{
			if (OnDataBindHandler == null)
				return new byte[0];

			int resultCount = 0;
			string customData = string.Empty;

			if (sortField.StartsWith("["))
				sortField = sortField + " " + sortOrder;
			else
				sortField = "[" + sortField + "] " + sortOrder;

			DataTable data = ((DataBindHandler)OnDataBindHandler)(sortField, searchFiletrs, toolbarFilters, this.UserData, 0, 0, ref resultCount, ref customData);

			Document pdfDoc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 15, 15, 35, 15);
			MemoryStream pdfStream = new MemoryStream();
			PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, pdfStream);
			ColumnText columnText = null;
			PdfPTable pdfTable = null;
			PdfPCell pdfPCell = null;
			Chunk chunk = null;
			int pdfTableWidth = 0;

			pdfDoc.Open();//Open Document to write
			pdfDoc.NewPage();

			FontFactory.RegisterDirectories();
			iTextSharp.text.Font normalTahomaFont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
			iTextSharp.text.Font boldTahomaFont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
			iTextSharp.text.Font tahomaTitleFont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

			List<DataGridColumnInfo> columnValidList = new List<DataGridColumnInfo>();
			foreach (DataGridColumnInfo column in this.Columns)
				if (!column.Hidden && column.ShowInExport)
					columnValidList.Add(column);

			columnValidList.Reverse();
			pdfTable = new PdfPTable(columnValidList.Count);

			#region Add Header Of The Pdf Table
			foreach (DataGridColumnInfo columnInfo in columnValidList)
			{
				chunk = new Chunk(this.Translate(columnInfo.Caption), boldTahomaFont);

				pdfPCell = new PdfPCell(new Phrase(chunk));
				pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
				pdfPCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
				pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;

				pdfTable.AddCell(pdfPCell);

				pdfTableWidth += columnInfo.CellWidth;
			}
			#endregion

			#region Add Data Row
			foreach (DataRow currentRow in data.Rows)
				foreach (DataGridColumnInfo columnInfo in columnValidList)
				{
					try
					{
						if (currentRow.RowError == "SummaryRow" && currentRow[columnInfo.FieldName] == DBNull.Value)
							chunk = new Chunk(string.Empty, normalTahomaFont);
						else
							chunk = new Chunk(GetCellValue(columnInfo, new CellValueRenderEventArgs(currentRow, RowGenerateType.ToExportData, currentRow.RowError == "SummaryRow"), OnRenderEvent), normalTahomaFont);
					}
					catch
					{
						chunk = new Chunk(string.Empty, normalTahomaFont);
					}

					pdfPCell = new PdfPCell(new Phrase(chunk));
					pdfPCell.RunDirection = columnInfo.Direction == Direction.LeftToRight ? PdfWriter.RUN_DIRECTION_LTR : PdfWriter.RUN_DIRECTION_RTL;
					pdfPCell.HorizontalAlignment = columnInfo.Align == CellAlign.Inherit || columnInfo.Align == CellAlign.Center ? Element.ALIGN_CENTER : (columnInfo.Align == CellAlign.Right ? Element.ALIGN_LEFT : Element.ALIGN_RIGHT);

					if (currentRow.RowError == "SummaryRow")
						pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;

					pdfTable.AddCell(pdfPCell);
				}
			#endregion

			pdfTable.RunDirection = this.ListDirection == Direction.LeftToRight ? PdfWriter.RUN_DIRECTION_LTR : PdfWriter.RUN_DIRECTION_RTL;

			columnText = new ColumnText(pdfWriter.DirectContent);
			columnText.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
			columnText.SetSimpleColumn(new Rectangle(15, 150, 800, 585));//iTextSharp.text.PageSize.A4.Rotate());
			columnText.AddText(new Phrase(this.Translate(this.ListCaption), tahomaTitleFont));

			columnText.Go();
			pdfDoc.Add(pdfTable);

			pdfDoc.Close();
			pdfWriter.Close();

			return pdfStream.ToArray();
		}

		public string ExportToPdfFile(string sortField, string sortOrder, string searchFiletrs, string toolbarFilters, Delegate OnDataBindHandler, Delegate OnRenderEvent)
		{
			if (OnDataBindHandler == null)
				return string.Empty;

			int resultCount = 0;
			string customData = string.Empty;

			if (sortField.StartsWith("["))
				sortField = sortField + " " + sortOrder;
			else
				sortField = "[" + sortField + "] " + sortOrder;

			DataTable data = ((DataBindHandler)OnDataBindHandler)(sortField, searchFiletrs, toolbarFilters, this.UserData, 0, 0, ref resultCount, ref customData);

			string fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Exports/Temp"), Guid.NewGuid() + ".pdf");

			Document pdfDoc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 35, 15);
			try
			{
				PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, new FileStream(fileName, FileMode.Create));
				ColumnText columnText = null;
				PdfPTable pdfTable = null;
				PdfPCell pdfPCell = null;
				Chunk chunk = null;
				int pdfTableWidth = 0;

				pdfDoc.Open();//Open Document to write
				pdfDoc.NewPage();

				FontFactory.RegisterDirectories();
				iTextSharp.text.Font normalTahomaFont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
				iTextSharp.text.Font boldTahomaFont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
				iTextSharp.text.Font tahomaTitleFont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

				List<DataGridColumnInfo> columnValidList = new List<DataGridColumnInfo>();
				foreach (DataGridColumnInfo column in this.Columns)
					if (!column.Hidden && column.ShowInExport)
						columnValidList.Add(column);

				columnValidList.Reverse();
				pdfTable = new PdfPTable(columnValidList.Count);

				#region Add Header Of The Pdf Table
				foreach (DataGridColumnInfo columnInfo in columnValidList)
				{
					chunk = new Chunk(this.Translate(columnInfo.Caption), boldTahomaFont);

					pdfPCell = new PdfPCell(new Phrase(chunk));
					pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
					pdfPCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
					pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;

					pdfTable.AddCell(pdfPCell);

					pdfTableWidth += columnInfo.CellWidth;
				}
				#endregion

				#region Add Data Row
				foreach (DataRow currentRow in data.Rows)
					foreach (DataGridColumnInfo columnInfo in columnValidList)
					{
						try
						{
							if (currentRow.RowError == "SummaryRow" && currentRow[columnInfo.FieldName] == DBNull.Value)
								chunk = new Chunk(string.Empty, normalTahomaFont);
							else
								chunk = new Chunk(GetCellValue(columnInfo, new CellValueRenderEventArgs(currentRow, RowGenerateType.ToExportData, currentRow.RowError == "SummaryRow"), OnRenderEvent), normalTahomaFont);
						}
						catch
						{
							chunk = new Chunk(string.Empty, normalTahomaFont);
						}

						pdfPCell = new PdfPCell(new Phrase(chunk));
						pdfPCell.RunDirection = columnInfo.Direction == Direction.LeftToRight ? PdfWriter.RUN_DIRECTION_LTR : PdfWriter.RUN_DIRECTION_RTL;
						pdfPCell.HorizontalAlignment = columnInfo.Align == CellAlign.Inherit || columnInfo.Align == CellAlign.Center ? Element.ALIGN_CENTER : (columnInfo.Align == CellAlign.Right ? Element.ALIGN_LEFT : Element.ALIGN_RIGHT);

						if (currentRow.RowError == "SummaryRow")
							pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;

						pdfTable.AddCell(pdfPCell);
					}
				#endregion

				pdfTable.RunDirection = this.ListDirection == Direction.LeftToRight ? PdfWriter.RUN_DIRECTION_LTR : PdfWriter.RUN_DIRECTION_RTL;

				columnText = new ColumnText(pdfWriter.DirectContent);
				columnText.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
				columnText.SetSimpleColumn(new Rectangle(15, 150, 800, 585));//iTextSharp.text.PageSize.A4.Rotate());
				columnText.AddText(new Phrase(this.Translate(this.ListCaption), tahomaTitleFont));

				columnText.Go();
				pdfDoc.Add(pdfTable);

				pdfDoc.Close();
				pdfWriter.Close();

				//return pdfStream.ToArray();
				return fileName;
			}
			catch (Exception ex)
			{
				pdfDoc.Close();
				throw ex;
			}
			finally
			{
				pdfDoc.Close();
			}
		}

		public string ExportToExcel(string sortField, string sortOrder, string searchFiletrs, string toolbarFilters, Delegate OnDataBindHandler, Delegate OnRenderEvent)
		{
			if (OnDataBindHandler == null)
				return string.Empty;

			int resultCount = 0;
			string customData = string.Empty;

			if (sortField.StartsWith("["))
				sortField = sortField + " " + sortOrder;
			else
				sortField = "[" + sortField + "] " + sortOrder;

			DataTable data = ((DataBindHandler)OnDataBindHandler)(sortField, searchFiletrs, toolbarFilters, this.UserData, 0, 0, ref resultCount, ref customData);

			DataRow newRow;
			StringWriter stringWriter = new StringWriter();
			DataTable outputData = new DataTable();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			System.Web.UI.WebControls.GridView dataGridControl = new System.Web.UI.WebControls.GridView();
			System.Web.UI.WebControls.BoundField newColumn;

			List<DataGridColumnInfo> columnValidList = new List<DataGridColumnInfo>();
			foreach (DataGridColumnInfo column in this.Columns)
				if (!column.Hidden && column.ShowInExport)
					columnValidList.Add(column);

			columnValidList.Reverse();

			dataGridControl.AutoGenerateColumns = false;
			dataGridControl.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
			dataGridControl.AlternatingRowStyle.BackColor = System.Drawing.Color.LightBlue;
			dataGridControl.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;

			#region Add Header Of The Excel Table
			foreach (DataGridColumnInfo columnInfo in columnValidList)
			{
				newColumn = new System.Web.UI.WebControls.BoundField();
				newColumn.DataField = columnInfo.FieldName;
				newColumn.HeaderText = this.Translate(columnInfo.Caption);

				dataGridControl.Columns.Add(newColumn);

				outputData.Columns.Add(columnInfo.FieldName);
			}
			#endregion

			#region Add Data Row
			foreach (DataRow currentRow in data.Rows)
			{
				newRow = outputData.NewRow();
				foreach (DataGridColumnInfo columnInfo in columnValidList)
				{
					try
					{
						if (currentRow.RowError == "SummaryRow" && currentRow[columnInfo.FieldName] == DBNull.Value)
							newRow[columnInfo.FieldName] = string.Empty;
						else
							newRow[columnInfo.FieldName] = GetCellValue(columnInfo, new CellValueRenderEventArgs(currentRow, RowGenerateType.ToExportData, currentRow.RowError == "SummaryRow"), OnRenderEvent);
					}
					catch
					{
						newRow[columnInfo.FieldName] = string.Empty;
					}
				}

				outputData.Rows.Add(newRow);
			}
			#endregion

			dataGridControl.DataSource = outputData;
			dataGridControl.DataBind();

			foreach (GridViewRow row in dataGridControl.Rows)
			{
				foreach (TableCell cell in row.Cells)
				{
					cell.CssClass = "textmode";
				}
			}

			dataGridControl.RenderControl(htmlTextWriter);

			return stringWriter.ToString();
		}

		public string ExportToExcelFile(string sortField, string sortOrder, string searchFiletrs, string toolbarFilters, Delegate OnDataBindHandler, Delegate OnRenderEvent)
		{
			if (OnDataBindHandler == null)
				return string.Empty;

			int resultCount = 0;
			int counter = 0;
			string cellValue = string.Empty;
			string customData = string.Empty;

			if (sortField.StartsWith("["))
				sortField = sortField + " " + sortOrder;
			else
				sortField = "[" + sortField + "] " + sortOrder;

			DataTable data = ((DataBindHandler)OnDataBindHandler)(sortField, searchFiletrs, toolbarFilters, this.UserData, 0, 0, ref resultCount, ref customData);

			List<DataGridColumnInfo> columnValidList = new List<DataGridColumnInfo>();
			foreach (DataGridColumnInfo column in this.Columns)
				if (!column.Hidden && column.ShowInExport)
					columnValidList.Add(column);

			columnValidList.Reverse();

			string columnName = string.Empty;
			string columnValue = string.Empty;
			string tableFields = string.Empty;

			foreach (DataGridColumnInfo columnInfo in columnValidList)
			{
				tableFields += string.Format("[{0}] {1}", columnInfo.FieldName, "varchar(200),");
				columnName += string.Format("[{0}],", columnInfo.FieldName);
				columnValue += "?,";
			}

			string strCreateTable = string.Format("CREATE TABLE Export({0})", tableFields.TrimEnd(','));
			string strInsert = string.Format("INSERT INTO Export({0}) Values({1})", columnName.TrimEnd(','), columnValue.TrimEnd(','));

			string fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Exports/Temp"), Guid.NewGuid() + ".xlsx");
			string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=""Excel 12.0 Xml;HDR=YES;""", fileName);
			using (OleDbConnection con = new OleDbConnection(connectionString))
			{
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}

				OleDbCommand cmd = new OleDbCommand(strCreateTable.ToString(), con);
				cmd.ExecuteNonQuery();

				OleDbCommand cmdInsert = new OleDbCommand(strInsert, con);
				foreach (DataGridColumnInfo columnInfo in columnValidList)
				{
					cmdInsert.Parameters.Add("?", OleDbType.VarChar, 200);
				}

				foreach (DataRow currentRow in data.Rows)
				{
					counter = 0;
					foreach (DataGridColumnInfo columnInfo in columnValidList)
					{
						try
						{
							if (currentRow.RowError == "SummaryRow" && currentRow[columnInfo.FieldName] == DBNull.Value)
								cellValue = string.Empty;
							else
								cellValue = GetCellValue(columnInfo, new CellValueRenderEventArgs(currentRow, RowGenerateType.ToExportData, currentRow.RowError == "SummaryRow"), OnRenderEvent);
						}
						catch
						{
							cellValue = string.Empty;
						}

						cmdInsert.Parameters[counter].Value = cellValue;
						counter++;
					}

					cmdInsert.ExecuteNonQuery();
				}
			}

			return fileName;
		}

		private string GetCellValue(DataGridColumnInfo sender, CellValueRenderEventArgs e, Delegate OnRenderEvent)
		{
			string outputTemplate = "{0}";
			if (e.CurrentRowGenerateType != RowGenerateType.ToExportData &&
					sender.Direction != Direction.Inherit &&
					sender.FormattingMethod != CellFormattingMethod.Image &&
					sender.FormattingMethod != CellFormattingMethod.ImageButton &&
					sender.FormattingMethod != CellFormattingMethod.BooleanOnOff &&
					sender.FormattingMethod != CellFormattingMethod.DateTimeShortDate &&
					sender.FormattingMethod != CellFormattingMethod.DateTimeShortDateTime)
				outputTemplate = "<bdo dir='" + (sender.Direction == Direction.Inherit ? sender.Direction.ToString() : (sender.Direction == Direction.RightToLeft ? "rtl" : "ltr")) + "'>{0}</bdo>";

			if (sender.FormattingMethod == CellFormattingMethod.CustomRender)
			{
				if (OnRenderEvent != null)
					return string.Format(outputTemplate, ((CellValueRenderEventHandler)OnRenderEvent)(sender, e));
				else
					throw new Exception("OnRender event not implement!!");
			}

			object fieldValue = null;
			if (sender.FormattingMethod != CellFormattingMethod.ImageButton)
				fieldValue = e.CurrentRow[sender.FieldName];

			switch (sender.FormattingMethod)
			{
				case CellFormattingMethod.Encrypt:
					return string.Format(outputTemplate, Encrypt(fieldValue));

				case CellFormattingMethod.BooleanYesNo:
					return string.Format(outputTemplate, this.Translate(Helper.GetBool(fieldValue) ? "Yes" : "No"));

				case CellFormattingMethod.BooleanOnOff:
					if (e.CurrentRowGenerateType != RowGenerateType.ToExportData)
						return string.Format(outputTemplate, Helper.GetBool(fieldValue) ? GetImageHtmlTag(BooleanOn) : GetImageHtmlTag(BooleanOff));
					else
						return string.Format(outputTemplate, this.Translate(Helper.GetBool(fieldValue) ? "Yes" : "No"));

				case CellFormattingMethod.BooleanOneZero:
					return string.Format(outputTemplate, Helper.GetBool(fieldValue) ? "1" : "0");

				case CellFormattingMethod.NumberDecimal:
					return string.Format(outputTemplate, Helper.FormatDecimalForDisplay(fieldValue));

				case CellFormattingMethod.DateTimeShortDate:
				case CellFormattingMethod.DateTimeShortDateTime:
					if (e.CurrentRowGenerateType != RowGenerateType.ToExportData)
						return "<bdo dir='ltr'>" + DateFormatter(fieldValue, sender.FormattingMethod) + "</bdo>";
					else
						return DateFormatter(fieldValue, sender.FormattingMethod);

				case CellFormattingMethod.DateTimeDetailedDate:
				case CellFormattingMethod.DateTimeDetailedDateTime:
				case CellFormattingMethod.DateTimeTime:
					return string.Format(outputTemplate, DateFormatter(fieldValue, sender.FormattingMethod));

				case CellFormattingMethod.TimeHourMinute:
					return string.Format(outputTemplate, DateFormatter(fieldValue, sender.FormattingMethod));
				case CellFormattingMethod.ImageButton:
					if (OnRenderEvent != null)
						return ((CellValueRenderEventHandler)OnRenderEvent)(sender, e);
					else
						throw new Exception("OnRender event not implement!!");

				case CellFormattingMethod.Image:
					if (OnRenderEvent != null)
						return ((CellValueRenderEventHandler)OnRenderEvent)(sender, e);
					else
						throw new Exception("OnRender event not implement!!");

				case CellFormattingMethod.Raw:
				default:
					return string.Format(outputTemplate, Helper.GetString(fieldValue));
			}
		}
		#endregion

		#region Private Methods
		private string CreateStyleAndJavaScripts()
		{
			string styleHeader = "<link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"{0}\" />";
			string scriptHeader = "<script type=\"text/javascript\" src=\"{0}\"></script>";
			string cssStyles = string.Empty;
			string javaScripts = string.Empty;

			cssStyles = string.Format(styleHeader, GetStyleResourceUrl(GeneralTools.DataGrid.DataGridResources.Style_UiGrid));
            //cssStyles += string.Format(styleHeader, GetStyleResourceUrl(GeneralTools.DataGrid.DataGridResources.Themes_Lightness));

            if (Context.Session["Language"].ToString() == "en")
            {
                javaScripts = string.Format(scriptHeader, GetLocalizationResourceUrl(GeneralTools.DataGrid.DataGridResources.JavaScript_En));
            }
            else
            {
                javaScripts = string.Format(scriptHeader, GetLocalizationResourceUrl(GeneralTools.DataGrid.DataGridResources.JavaScript_Fa));
            }

			javaScripts += string.Format(scriptHeader, GetScriptResourceUrl(GeneralTools.DataGrid.DataGridResources.JavaScript_jqueryGridMin));
			javaScripts += string.Format(scriptHeader, GetScriptResourceUrl(GeneralTools.DataGrid.DataGridResources.JavaScript_JqueryJson));

			return cssStyles + javaScripts;
		}

		private string CraeteBodyControl()
		{
			return string.Format("<table id=\"{0}\"></table><div id=\"{1}\"></div>", JQGridListID, JQGridPagerID);
		}

		private string GenerateInlineScripts()
		{
			string bodyPattern = @"<script type=""text/javascript"">
															var {13} = {{
																						SelectedGuids:'',
																						SelectedGuid:'',
																						SearchFilters:'',
																						SelectedRow:null,
																						LastSel:null,
																						Event:null,

																						GetDataIDs:function(){{
																							return $('#{0}').jqGrid('getDataIDs');
																						}},

																						GetRowIndex:function(rowID){{
																							return $('#{0}').getInd(rowID);
																						}},

																						GetSelectedRowFieldValue:function (fieldName){{
																							if(this.SelectedRow == null)
																								return '';
																							return this.SelectedRow[fieldName];
																						}},
 
																						GetRowFieldValue:function (rowIndex,columnIndex,columnName) {{
																							var colLen = $('#{0}')[0].p.colModel.length;
																							var colIndex;
																							for(var index=0; index < colLen; index++){{
																								if($('#{0}')[0].p.colModel[index].name==columnName){{
																									colIndex = index;
																									break;
																								}}
																							}}
 
																							if($('#{0}')[0].p.colModel[colIndex].editable == true)
																								return $('#{0}').editCell(rowIndex,columnIndex, true)[0].rows[rowIndex].cells[colIndex].title ;
																							else
																								return $('#{0}')[0].rows[rowIndex].cells[colIndex].title;
																						}},

																						GetMultipleSeletedRow:function(){{
																							return $('#{0}').jqGrid('getGridParam', 'selarrrow');
																						}},
 
																						GetSelectedRowID:function (){{
																							return $('#{0}').jqGrid('getGridParam','selrow');
																						}},
 
																						GetAllRowData:function (columnList,fromRowNo,toRowNo){{
																							var colLen = $('#{0}')[0].p.colModel.length;
																							var rowIDs = $('#{0}').jqGrid('getDataIDs');
																							var recordLen = rowIDs.length;
																							var resultCount = 0;
																							var result = '';
																							var row;
																							var rowIndex;
 
																							fromRowNo = fromRowNo && fromRowNo>=0 ? (fromRowNo - 1) : 0;
																							toRowNo = toRowNo && toRowNo<=recordLen ? (toRowNo - 1) : recordLen;
 
																							for(var i=fromRowNo;i<toRowNo;i++){{
																								if (rowIDs[i]){{
																									row = $('#{0}').jqGrid('getRowData',rowIDs[i]);
 
																									if(columnList){{
																										for(var j=0;j<columnList.length;j++)
																										{{
																											var x;
																											for(var n=0;n<colLen;n++){{
																												if($('#{0}')[0].p.colModel[n].name==columnList[j]){{
																													x=n;
																													break;
																												}}
																											}}
																											if($('#{0}')[0].p.colModel[x].name==columnList[j] && $('#{0}')[0].p.colModel[x].editable==true){{
																												rowIndex = $('#{0}').getInd(rowIDs[i]);
																												result += columnList[j] + i + '{{(' + $('#{0}').editCell(rowIndex, j, true)[0].rows[rowIndex].cells[x].title + ')}}';
																											}}
																											else if($('#{0}')[0].p.colModel[x].name==columnList[j])
																												result += columnList[j] + i + '{{(' + row[columnList[j]] + ')}}';
																										}}
																									}}
																									else{{
																										for(var j=0;j<colLen;j++)
																											result += $('#{0}')[0].p.colModel[j].name + i + '{{(' + row[$('#{0}')[0].p.colModel[j].name] + ')}}';
																									}}
 
																									resultCount++;
																								}}
																							}}
																							result+= 'resultCount{{('+ resultCount +')}}';
 
																							return result;
																						}},
																						GetSelectedRowData: function(columnList,fromRowNo,toRowNo){{
																							var rowIDs=$('#{0}').jqGrid('getGridParam', 'selarrrow');
																							var colLen = $('#{0}')[0].p.colModel.length;
																							var recordLen = rowIDs.length;
																							var resultCount = 0;
																							var result = '';
																							var row;
																							var rowIndex;
 
																							fromRowNo = fromRowNo && fromRowNo>=0 ? (fromRowNo - 1) : 0;
																							toRowNo = toRowNo && toRowNo<=recordLen ? (toRowNo - 1) : recordLen;
 
																							for(var i=fromRowNo;i<toRowNo;i++){{
																								if (rowIDs[i]){{
																									row = $('#{0}').jqGrid('getRowData',rowIDs[i]);
 
																									if(columnList){{
																										for(var j=0;j<columnList.length;j++)
																										{{
																											var colIndex;
																											for(var index=0; index < colLen; index++){{
																												if($('#{0}')[0].p.colModel[index].name==columnList[j]){{
																													colIndex = index;
																													break;
																												}}
																											}}
																											if($('#{0}')[0].p.colModel[colIndex].name==columnList[j] && $('#{0}')[0].p.colModel[colIndex].editable==true){{
																												rowIndex = $('#{0}').getInd(rowIDs[i]);
																												result += columnList[j] + i + '{{(' + $('#{0}').editCell(rowIndex, j, true)[0].rows[rowIndex].cells[colIndex].title + ')}}';
																											}}
																											else if($('#{0}')[0].p.colModel[colIndex].name==columnList[j])
																												result += columnList[j] + i + '{{(' + row[columnList[j]] + ')}}';
																										}}
																									}}
																									else{{
																										for(var j=0;j<colLen;j++)
																											result += $('#{0}')[0].p.colModel[j].name + i + '{{(' + row[$('#{0}')[0].p.colModel[j].name] + ')}}';
																									}}

																									resultCount++;
																								}}
																							}}
																							result+= 'resultCount{{('+ resultCount +')}}';

																							return result;
																						}},

																						ToggleSelectRow:function (id){{
																							if(id){{
																								$('#{0}').jqGrid('setSelection',id);
																							}}
																						}},
 
																						SelectRow:function (id){{
																							if(!id){{
																								var target = this.Event.target || this.Event.srcElement;
																								if($(target).parents('tr:first').attr('id'))
																									id = $(target).parents('tr:first').attr('id');
																							}}
 
																							if(id){{
																									$('#{0}').jqGrid('setSelection',id);
																									if(this.SelectedRow == null)
																										$('#{0}').jqGrid('setSelection',id);
																								}}
																						}},
 
																						IsSelectedRow:function (){{
																							this.SelectRow();
																							if(this.SelectedRow == null)
																								return false;
																							else {{
																								var agent = $.browser;
																								if(agent.msie)
																									this.Event.cancelBubble = true;
																								else
																									this.Event.stopPropagation();
 
																								return true;
																							}}
																						}},
 
																						AddRow:function (dataRow,posation,rowID){{
																							var newRow=null;
																							if(dataRow == null)
																								newRow=$('#{0}').jqGrid('addRow');
																							else
																								newRow=$('#{0}').jqGrid('addRowData',rowID,dataRow,posation);
 
																							return newRow;
																						}},
 
																						DeleteRow:function (rowID){{
																							return $('#{0}').jqGrid('delRowData',rowID);
																						}},
 
																						ShowAdvancedSearch:function (){{
																							$('#{0}').jqGrid('searchGrid','{15}');
																						}},
 
																						Search:function (){{
																							$('#{0}')[0].p.search = true;
																							$('#{0}').jqGrid('setGridParam',{{postData:{{SearchFiletrs:this.SearchFilters}},page:1}}).trigger('reloadGrid');
																						}},
 
																						SetFooterRowData:function (dataRow){{
																							$('#{0}').jqGrid('footerData','set',dataRow);
																						}},
 
																						GetRecordCount:function (){{
																							return $('#{0}').jqGrid('getGridParam','records');
																						}},

																						GetUserData:function(){{
																							return $('#{0}').jqGrid('getGridParam','userData');
																						}},
																						
																						GetFilters:function(){{
																							return $('#{0}').getGridParam('postData').filters;
																						}},

																						TriggerToolbar:function(){{
																							$('#{0}')[0].triggerToolbar();
																						}},

																						Export: function (exportType){{
																							var mapForm = document.createElement('form');
																							mapForm.target = '_blank';
																							mapForm.method = 'POST'; 
																							mapForm.action = '{22}';
 
																							var mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'ExportType';
																							mapInput.value = exportType;
																							mapForm.appendChild(mapInput);
 
																							mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'SerializeObject';
																							mapInput.value = $('#{0}').jqGrid('getGridParam','serializeObject');
																							mapForm.appendChild(mapInput);
 
																							mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'DataGridID';
																							mapInput.value = '{13}';
																							mapForm.appendChild(mapInput);
 
																							mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'SearchFiletrs';
																							mapInput.value = this.SearchFilters;
																							mapForm.appendChild(mapInput);

																							var posted_data = $('#{0}').jqGrid('getGridParam', 'postData');
																							var filters = $.parseJSON(posted_data.filters);
																							var rules='';
																							if(filters!=null)
																								rules = JSON.stringify(filters.rules);
																							mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'ToolbarFilters';
																							mapInput.value = rules;
																							mapForm.appendChild(mapInput);
 
																							mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'SortField';
																							mapInput.value = $('#{0}').jqGrid('getGridParam','sortname');
																							mapForm.appendChild(mapInput);
 
																							mapInput = document.createElement('input');
																							mapInput.type = 'hidden';
																							mapInput.name = 'SortOrder';
																							mapInput.value = $('#{0}').jqGrid('getGridParam','sortorder');;
																							mapForm.appendChild(mapInput);
 
																							document.body.appendChild(mapForm);
																							mapForm.submit();
																							$(mapForm).remove();
																						}},
 
																						ExportToExcel: function (){{
																							this.Export('excel')
																						}},
 
																						ExportToPdf: function (){{
																							this.Export('pdf')
																						}}
																				}}
 
 
															$(document).ready(
																		function () {{
		 																			$.jgrid.defaults = $.extend($.jgrid.defaults,
																										{{ datatype: 'json' }},
																										{{ ajaxGridOptions: {{ contentType: 'application/json',
																												success: function (data, textStatus) {{
																												if (textStatus == 'success') {{
																													var thegrid = $('#{0}')[0];
																													if(data && data.d.userdata)
																														data.d.userdata = eval(data.d.userdata);
																													thegrid.addJSONData(data.d);
																													thegrid.grid.hDiv.loading = false;
																													switch (thegrid.p.loadui) {{
																														case 'disable':
																															break;
																														case 'enable':
																															$('#load_' + thegrid.p.id).hide();
																															break;
																														case 'block':
																															$('#lui_' + thegrid.p.id).hide();
																															$('#load_' + thegrid.p.id).hide();
																															break;
																													}}
																												}}
																											}}
																										}}
																						}});
																					$('#{0}').jqGrid({{
																												url: '{1}',
																												datatype: 'json',
																												mtype: 'POST',
																												height: {2},
																												colNames: [{3}], 
																												colModel: [{4}],
																												toppager:{31},
																												pager: {5},
																												autowidth:true,
																												rowNum: {24},
																												rowList: [{25}], 
																												sortname: '[{6}]',
																												sortorder: '{7}',
																												viewrecords: true, 
																												gridview: true,
																												{30}
																												caption: '{8}',
																												cellEdit: {27},
																												cellsubmit: 'clientArray',
																												recordpos: 'right',
																												pagerpos: 'center',
																												rownumbers: {9},
																												rownumWidth: 35,
																												altRows: true,
																												forceFit: false,
																												shrinkToFit: true,
																												multiselect: {10},
																												direction: '{11}',
																												serializeObject: '{12}',
																												footerrow: {17},
																												userDataOnFooter: true,
																												toolbar: [{19},'{20}'],
																												onSelectRow: function(id){{
																														var ids = $('#{0}').jqGrid('getGridParam','selarrrow');
																														id = $('#{0}').jqGrid('getGridParam','selrow');
																														if (id && id != {13}.LastSel){{
																															var ret;
																															{13}.SelectedGuids = '';
																															for(var i=0;i<ids.length;i++) {{
																																ret = $('#{0}').jqGrid('getRowData',ids[i]);
																																{13}.SelectedGuids += ret['Guid'] + ',';
																															}}
																															{13}.SelectedGuids = {13}.SelectedGuids.substring(0,{13}.SelectedGuids.length - 1);
																															ret = $('#{0}').jqGrid('getRowData',id);
																															{13}.SelectedGuid = ret['Guid'];
																															{13}.SelectedRow = ret;
																															{13}.LastSel = id;
																														}}
																														else{{
																															{13}.SelectedGuids = '';
																															{13}.SelectedGuid = '';
																															{13}.SelectedRow = null;
																															{13}.LastSel = -1;
																														}}
																												}},
																												gridComplete: function () {{
																													{26}
																												}},
																												beforeSelectRow: function(rowid, e) {{
																													if({10}){{
																														var cbsdis = $('#{0} tr#'+rowid+'.jqgrow>td>input.cbox:disabled');
																														if (cbsdis.length === 0) {{
																															var $self = $(this), iCol, cm,
																															$td = $(e.target).closest('tr.jqgrow>td'),
																															$tr = $td.closest('tr.jqgrow'),
																															p = $self.jqGrid('getGridParam');

																															if ($(e.target).is('input[type=checkbox]') && $td.length > 0) {{
																																	iCol = $.jgrid.getCellIndex($td[0]);
																																	cm = p.colModel[iCol];
																																	if (cm != null && cm.name === 'cb') {{
																																			$self.jqGrid('setSelection', $tr.attr('id'), true ,e);
																																	}}
																															}}
																															return false;
																														}} else {{
																															return false;
																														}}
																													}}
																													else
																														return true;
																												}},
																												onSelectAll: function(aRowids,status) {{
																													if (status) {{
																														var cbs = $('tr.jqgrow > td > input.cbox:disabled');
																														for(var i=0;i<cbs.length;i++){{
																															var rowID=cbs[i].id.replace('jqg_{0}_','');
																															ret = $('#{0}').jqGrid('getRowData',rowID);
																															 if('{28}' != '' && ret['{28}'] == '1' && {29}==true)
																																$(cbs[i]).attr('checked', true);
																															 else
																																$(cbs[i]).removeAttr('checked');
																														}}
																													}}
																													else{{
																														var cbs = $('tr.jqgrow > td > input.cbox:disabled');
																														for(var i=0;i<cbs.length;i++){{
																															var rowID=cbs[i].id.replace('jqg_{0}_','');
																															ret = $('#{0}').jqGrid('getRowData',rowID);
																															 if('{28}' != '' && ret['{28}'] == '1' && {29}==true)
																																$(cbs[i]).attr('checked', true);
																														}}
																													}}
																													var cbs = $('tr.jqgrow:has(td > input.cbox:checked)');
																													$('#{0}')[0].p.selarrrow = cbs.map(function() {{ return this.id; }}).get();
																												}},
																												serializeGridData: function (data) {{
																													return $.toJSON(data);
																												}},
																											}});
 
																					$('#{0}').jqGrid('navGrid', {5}, {{ edit: false, add: false, del: false, search: {14}, refresh: true, searchtext:'{15}', searchDivID:'{16}',cloneToTop: {31}}});
																					$('#{0}').jqGrid('bindKeys', {{'onEnter':function(rowid,event) {{event=event.originalEvent;event.returnValue=false; {18} return true; }} }});
																					$('#{0}').jqGrid('gridResize',{{minWidth:350, maxWidth:800, minHeight:80, maxHeight:350}});
																					$('#{0}').jqGrid('filterToolbar',{{searchOperators:true,stringResult: true,searchOnEnter : true,beforeSearch:function(){{search('{0}');}}}});
																					{32}
																					{21}{23}
																			}});
															</script>";

			string columnInfoPattern = "{{ name: '{0}', index: '{1}', width: {2}, align: '{3}',search:{4} {5}, sortable:{6}, resize:{7}, hidden:{8}, frozen:{9} {10} {11} {12}}},";
			string columnNames = string.Empty;
			string columnInfoList = string.Empty;

			foreach (DataGridColumnInfo column in columns)
			{
				columnNames += string.Format("'{0}',", this.Translate(column.Caption));
				columnInfoList += string.Format(columnInfoPattern,
																				column.FieldName,
																				column.FieldName,
																				column.CellWidth,
																				column.Align,
																				column.Search.ToString().ToLower(),
																				GetSearchOptions(column.Search, column.SearchType, column.SearchOptions),
																				column.Sortable.ToString().ToLower(),
																				column.Resizable.ToString().ToLower(),
																				column.Hidden.ToString().ToLower(),
																				column.Frozen.ToString().ToLower(),
																				GetCellEditingValues(column.Editable, column.EditType, column.EditOptions),
																				string.IsNullOrEmpty(column.SearchFilterCssClass) ? string.Empty : string.Format(",class:'{0}'", column.SearchFilterCssClass),
																				column.MaxLength > 0 ?
																					string.Format(@",cellattr:function(rowId, cellValue, rawObject, cm, rdata)
																																		{{return 'style="""" title=""' + rdata.{0} + '""';}},
																													 formatter: function(cellvalue) {{ return cellvalue.length>{1}?cellvalue.substring(0, {1})+'...':cellvalue;}}", column.FieldName, column.MaxLength) : string.Empty
																				);
			}

			return string.Format(bodyPattern,
																this.JQGridListID,//0
																this.PostBackUrl,//1
																this.ListHeight != 0 ? Helper.GetString(this.ListHeight) : string.Format("Math.abs($(document).height() - 80 - {0})", Helper.GetInt(this.listDifferenceHeight)),//2
																columnNames.TrimEnd(','),//3
																columnInfoList.TrimEnd(','),//4
																this.DisableNavigationBar ? "null" : ("'#" + this.JQGridPagerID + "'"),//5
																this.SortField,//6
																"DESC",//7
																this.Translate(this.ListCaption),//8
																this.ShowRowNumber.ToString().ToLower(),//9
																this.SelectMode == ListSelectMode.Single ? "false" : "true",//10
																this.ListDirection == Direction.Inherit ? this.ListDirection.ToString() : (this.ListDirection == Direction.RightToLeft ? "rtl" : "ltr"),//11
																this.Encrypt(SerializationTools.SerializeToXml(GetSchema(), typeof(DataGridSchema))),//12
																this.ID,//13
																this.ShowAdvancedSearch.ToString().ToLower(),//14
																this.Translate("AdvancedSearch"),//15
																this.SearchDivID,//16
																this.ShowFooterRow.ToString().ToLower(),//17
																this.OnEnterKeyPress != string.Empty ? (this.OnEnterKeyPress + "(rowid,event);") : string.Empty,//18
																this.ShowToolbar.ToString().ToLower(),//19
																this.ToolbarPosition.ToString().ToLower(),//20
																GetToolbarItem(),//21
																this.PostBackToExportDataUrl,//22
																GetExportItem(),//23
																ConfigurationManager.DataGridAvailablePageSizes == string.Empty || DisableNavigationBar ? "1000" : ConfigurationManager.DefaultDataGridPageSizes,//24
																ConfigurationManager.DataGridAvailablePageSizes == string.Empty ? "5, 25, 50, 100, 150" : ConfigurationManager.DataGridAvailablePageSizes,//25
																GetGridCompleteScript(this.SelectMode != ListSelectMode.Single),//26
																HasCellEditable.ToString().ToLower(),//27
																this.StatusMultiSelectField,//28
																this.DisableActiveMultiSelectField.ToString().ToLower(),//29
																GetGridGroupingInfo(this.EnableGrouping),//30
																this.showPagerToTop.ToString().ToLower(),//31
																this.ShowSearchToolbar ? string.Empty : string.Format("$('#{0}')[0].toggleToolbar();", this.JQGridListID)//32 show search toolbar
																).Replace("\t", string.Empty);
		}

		private object GetCellEditingValues(bool editable, EditType editType, string editOptions)
		{
			if (editable)
			{
				HasCellEditable = true;

				string editInfo = string.Format(", editable:{0}, edittype:'{1}'",
																				 editable.ToString().ToLower(),
																				 editType.ToString().ToLower());
				switch (editType)
				{
					case EditType.Select:
						editInfo += ", editoptions={" + editOptions + "}";
						break;
				}

				return editInfo;
			}
			else
				return string.Empty;
		}

		private object GetSearchOptions(bool search, SearchType searchType, string searchOptions)
		{
			if (!search)
				return string.Empty;
			string parseSearchoptions = string.Empty;

			string searchInfo = string.Empty;
			string value = string.Empty;

			switch (searchType)
			{
				case SearchType.Select:
					if (!string.IsNullOrEmpty(searchOptions))
					{
						searchOptions = "[" + searchOptions + "]";
						JArray array = JArray.Parse(searchOptions);
						foreach (JObject content in array.Children<JObject>())
						{
							foreach (JProperty prop in content.Properties())
							{
								switch (prop.Name)
								{
									//case "dataInit":
									case "buildSelect":
										parseSearchoptions += string.Format(@",{0}:function(element){{ return {1}(element); }}", prop.Name.Replace("\r\n", ""), prop.Value);
										break;
									default:
										value = prop.Value.ToString();
										value = value.Replace("False", "false");
										value = value.Replace("True", "true");
										parseSearchoptions += string.Format(@",{0}:{1}", prop.Name, value);
										break;
								}
							}
						}
						searchInfo += string.Format(@",stype:'{0}', searchoptions:{{dataUrl:'DataGridHandler.aspx/GetColumnSearchData' {1}}}", searchType.ToString().ToLower(), parseSearchoptions.Replace("\r\n", ""));
					}
					break;
				case SearchType.Date:
					if (!string.IsNullOrEmpty(searchOptions))
					{
						searchOptions = "[" + searchOptions + "]";
						JArray array = JArray.Parse(searchOptions);
						foreach (JObject content in array.Children<JObject>())
						{
							foreach (JProperty prop in content.Properties())
							{
								switch (prop.Name)
								{
									case "sopt":
										parseSearchoptions += string.Format(@",sopt:{0}", prop.Value);
										break;
									default:
										value = prop.Value.ToString();
										value = value.Replace("False", "false");
										value = value.Replace("True", "true");
										parseSearchoptions += string.Format(@",{0}:{1}", prop.Name, value);
										break;
								}
							}
						}

                        if (Context.Session["Language"].ToString() == "en")
                        {
                            searchInfo += string.Format(@",searchoptions:{{ dataInit:function(element){{ $(element).datepicker(); }} {0} }}", parseSearchoptions.Replace("\r\n", ""));
                        }else
                            searchInfo += string.Format(@",searchoptions:{{ dataInit:function(element){{ $(element).css('direction','ltr');$(element).addClass('dateTimePicker');$(element).attr('data-MdDateTimePicker',true);$(element).attr('data-enabletimepicker',true);$(element).attr('data-targetselector','.dateTimePicker'); }} {0} }}", parseSearchoptions.Replace("\r\n", ""));

                        //searchInfo += string.Format(@",searchoptions:{{ dataInit:function(element){{ $(element).datepicker(); }} {0} }}", parseSearchoptions.Replace("\r\n", ""));
                        //searchInfo += string.Format(@",searchoptions:{{ dataInit:function(element){{ $(element).css('direction','ltr');$(element).addClass('dateTimePicker');$(element).attr('data-MdDateTimePicker',true);$(element).attr('data-enabletimepicker',true);$(element).attr('data-targetselector','.dateTimePicker'); }} {0} }}", parseSearchoptions.Replace("\r\n", ""));
                    }
					break;
				default:
					if (!string.IsNullOrEmpty(searchOptions))
						searchInfo += string.Format(@",searchoptions: {0}", searchOptions);
					break;
			}

			return searchInfo;
		}

		private string GetToolbarItem()
		{
			string toolbarItemScript = string.Empty;
			if (this.ShowToolbar)
			{
				switch (this.ToolbarPosition)
				{
					case ToolbarPosition.Top:
						toolbarItemScript = string.Format("$('#t_{0}').append('{1}');", this.JQGridListID, this.TopToolbarItems);
						break;
					case ToolbarPosition.Bottom:
						toolbarItemScript = string.Format("$('#tb_{0}').append('{1}');", this.JQGridListID, this.BottomToolbarItems);
						break;
					case ToolbarPosition.Both:
						toolbarItemScript = string.Format("$('#t_{0}').append('{1}');$('#tb_{0}').append('{2}');", this.JQGridListID, this.BottomToolbarItems, this.BottomToolbarItems);
						break;
				}
			}

			return toolbarItemScript;
		}

		private string GetExportItem()
		{
			if (this.ShowExportData && !this.DisableNavigationBar)
				return string.Format(@"$('#{0}').jqGrid('navButtonAdd', '#{1}', {{caption:'{2}', buttonicon:'fa fa-2x fa-file-excel-o green', onClickButton:function(){{ {3}.ExportToExcel();}},position:'last' }}); 
															 $('#{0}').jqGrid('navButtonAdd', '#{1}', {{caption:'{4}', buttonicon:'fa fa-2x fa-file-pdf-o red', onClickButton:function(){{ {3}.ExportToPdf();}},position:'last' }});
															 //$('#{0}').jqGrid('navButtonAdd', '#{1}', {{caption:'Filter',title:'Toggle Searching Toolbar',buttonicon: 'fa fa-2x fa-filter',onClickButton: function () {{$('#{0}')[0].toggleToolbar();}} }});

															 $('#{0}').jqGrid('navButtonAdd', '#{0}_toppager', {{caption:'{2}', buttonicon:'fa fa-2x fa-file-excel-o green', onClickButton:function(){{ {3}.ExportToExcel();}},position:'last' }}); 
															 $('#{0}').jqGrid('navButtonAdd', '#{0}_toppager', {{caption:'{4}', buttonicon:'fa fa-2x fa-file-pdf-o red', onClickButton:function(){{ {3}.ExportToPdf();}},position:'last' }});
															 //$('#{0}').jqGrid('navButtonAdd', '#{0}_toppager', {{caption:'Filter',title:'Toggle Searching Toolbar',buttonicon: 'fa fa-2x fa-filter',onClickButton: function () {{$('#{0}')[0].toggleToolbar();}} }});",
																		this.JQGridListID,//0
																		this.JQGridPagerID,//1
																		string.Empty,//this.Translate("ExportToExcel"),
																		this.ID,//3
																		string.Empty//this.Translate("ExportToPdf")//4
																		);
			else
				return string.Empty;
		}

		private object GetGridCompleteScript(bool isMultiSelect)
		{
			string retValue = @"if({0}) {{
														var rowIDs = $('#{1}').jqGrid('getDataIDs');
														var ret;
														var rowIndex;
														if (rowIDs.length > 0) {{
															for (var i = 0; i < rowIDs.length; i++) {{
																ret = $('#{1}').jqGrid('getRowData',rowIDs[i]);
																rowIndex=$('#{1}').getInd(rowIDs[i]);
																if ('{2}' != '' && (ret['{2}'] == '1' || ret['{2}'] == 'true' || ret['{2}'] == 'True')) {{
																	$('#{1}').jqGrid('setSelection', rowIDs[i] , true);
																}}
																if('{3}' != '' && ret['{3}']=='1' && {5}==true){{
																	$('#jqg_{1}_' + rowIDs[i]).attr('disabled', true);
																	$('#jqg_{1}_' + rowIDs[i]).css('opacity', '0.3');
																}}
															}}
														}}
												  }}
													{6}
												";

			return string.Format(retValue,
														isMultiSelect.ToString().ToLower(),//0
														this.JQGridListID,//1
														this.StatusMultiSelectField,//2
														this.DisableMultiSelectField,//3
														this.ID,//4
														this.DisableActiveMultiSelectField.ToString().ToLower(),//5
														string.IsNullOrEmpty(GridComplete) ? string.Empty : string.Format("{0}({1});", GridComplete, this.JQGridListID)//6
													);
		}

		private object GetGridGroupingInfo(bool enableGrouping)
		{
			string groupingInfo = string.Empty;
			if (enableGrouping)
			{
				groupingInfo = @"	grouping: true,
													groupingView : {{
														groupField : ['{0}'],
														groupColumnShow : [{1}], 
														groupText : ['<b>{{0}}</b>'], 
														groupCollapse : {2},
													}},";
				groupingInfo = string.Format(groupingInfo, GroupField,//0
																		GroupColumnShow.ToString().ToLower(),//1
																		GroupCollapse.ToString().ToLower()//2
											);
			}
			return groupingInfo;
		}

		private string GetImageResourceUrl(string imageName)
		{
			try
			{
				return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.DataGrid.Images." + imageName);
			}
			catch
			{
				return string.Empty;
			}
		}

		private string GetStyleResourceUrl(string cssName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.DataGrid.Themes." + cssName);
		}

		private string GetLocalizationResourceUrl(string localizationScriptName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.DataGrid.JavaScripts.i18n." + localizationScriptName);
		}

		private string GetScriptResourceUrl(string scriptName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.DataGrid.JavaScripts." + scriptName);
		}

		private string DateFormatter(object value, CellFormattingMethod cellFormattingMethod)
		{
			if (value.GetType() != typeof(DateTime))
				return value.ToString();

			DateTime dateTime = Helper.GetDateTime(value);

			switch (cellFormattingMethod)
			{
				case CellFormattingMethod.DateTimeDetailedDate:
					return DateManager.GetDisplayDetailedDate(dateTime);

				case CellFormattingMethod.DateTimeDetailedDateTime:
					return DateManager.GetDisplayDetailedDateTime(dateTime);

				case CellFormattingMethod.DateTimeTime:
					return DateManager.GetTime(dateTime);

				case CellFormattingMethod.DateTimeShortDate:
					return DateManager.GetDisplayDate(dateTime);

				case CellFormattingMethod.DateTimeShortDateTime:
					return DateManager.GetDisplayDateTime(dateTime);

				case CellFormattingMethod.TimeHourMinute:
					return dateTime.Hour + ":" + dateTime.Minute;

				default:
					return value.ToString();
			}
		}

		private string Translate(string value)
		{
			return Language.GetString(value);
		}

		private string Encrypt(object value)
		{
			return Helper.Encrypt(value, HttpContext.Current.Session);
		}

		private string GetImageHtmlTag(string imagePath)
		{
			return string.Format("<img src=\"{0}\" />&nbsp;", imagePath);
		}

		private DataGridSchema GetSchema()
		{
			DataGridSchema schema = new DataGridSchema();

			schema.Columns = this.Columns;
			schema.ListCaption = this.ListCaption;
			schema.ListDirection = this.ListDirection;
			schema.BooleanOff = this.BooleanOff;
			schema.BooleanOn = this.BooleanOn;
			schema.ID = this.ID;
			schema.UserData = this.UserData;

			return schema;
		}
		#endregion
	}
}
