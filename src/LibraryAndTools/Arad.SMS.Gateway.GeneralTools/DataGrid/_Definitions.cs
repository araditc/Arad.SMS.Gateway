using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace GeneralTools.DataGrid
{
	#region Enumerations
	public enum CellAlign
	{
		Inherit,
		Left,
		Center,
		Right
	}

	public enum Direction
	{
		Inherit,
		LeftToRight,
		RightToLeft
	}

	public enum ToolbarPosition
	{
		Top,
		Bottom,
		Both,
	}

	public enum EditType
	{
		Text,
		TextArea,
		Select,
		CheckBox,
		Password,
		Button,
		Image,
		File,
		Custom,
	}

	public enum SearchType
	{
		Text,
		Select,
		Date,
	}

	public enum CellFormattingMethod
	{
		Raw,
		CustomRender,
		Encrypt,
		BooleanYesNo,
		BooleanOnOff,
		BooleanOneZero,
		DateTimeShortDate,
		DateTimeShortDateTime,
		DateTimeTime,
		TimeHourMinute,
		DateTimeDetailedDate,
		DateTimeDetailedDateTime,
		NumberDecimal,
		ImageButton,
		Image
	}

	public enum ListSelectMode
	{
		ReadOnly,
		Single,
		Multiple,
	}

	public enum RowGenerateType
	{
		Normal,
		ToExportData
	}

	public struct DataGridResult
	{
		public int page;
		public int total;
		public int records;
		public DataGridRowData[] rows;
		public object userdata;
	}

	public struct DataGridRowData
	{
		public int id;
		public string[] cell;
	}
	#endregion

	#region
	public delegate DataTable DataBindHandler(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData);

	public delegate string CellValueRenderEventHandler(DataGridColumnInfo sender, CellValueRenderEventArgs e);

	//public delegate string ColumnSearchValueRenderEventHandler(DataGridColumnInfo sender);

	public class CellValueRenderEventArgs : EventArgs
	{
		private DataRow currentRow;
		private RowGenerateType currentRowGenerateType;
		private bool isFooterRow;

		public DataRow CurrentRow
		{
			get { return currentRow; }
		}
		public RowGenerateType CurrentRowGenerateType
		{
			get { return currentRowGenerateType; }
		}
		public bool IsFooterRow
		{
			get { return isFooterRow; }
		}

		public CellValueRenderEventArgs(DataRow currentRow, RowGenerateType currentRowGenerateType, bool isFooterRow)
		{
			this.currentRow = currentRow;
			this.currentRowGenerateType = currentRowGenerateType;
			this.isFooterRow = isFooterRow;
		}
	}
	#endregion

	[Serializable]
	[ToolboxItem(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public class DataGridColumnInfo
	{
		public DataGridColumnInfo()
		{
			Align = CellAlign.Inherit;
			Direction = Direction.Inherit;
			FormattingMethod = CellFormattingMethod.Raw;
			CellWidth = 100;
			WordWrap = false;
			Sortable = true;
			Resizable = true;
			Frozen = false;
			ShowInExport = true;
			OnCellClick = null;
			ShowCaption = true;
			Editable = false;
			EditType = EditType.Text;
			Search = false;
			SearchType = SearchType.Text;
			SearchFilterCssClass = string.Empty;
			MaxLength = 0;
		}

		public bool Hidden;
		public string FieldName;
		public string Caption;
		public bool ShowCaption;
		public CellAlign Align;
		public Direction Direction;
		public CellFormattingMethod FormattingMethod;
		public int CellWidth;
		public bool WordWrap;
		public bool Sortable;
		public bool Resizable;
		public bool Frozen;
		public bool ShowInExport;
		public string OnCellClick;
		public bool Editable;
		public EditType EditType;
		public string EditOptions;
		public bool Search;
		public SearchType SearchType;
		public string SearchOptions;
		public string SearchFilterCssClass;
		public int MaxLength;
	}

	[Serializable]
	public class DataGridSchema
	{
		internal DataGridSchema()
		{ }

		public string ID
		{
			get;
			set;
		}

		public string ListCaption
		{
			get;
			set;
		}

		public Direction ListDirection
		{
			get;
			set;
		}

		public List<DataGridColumnInfo> Columns
		{
			get;
			set;
		}

		public string BooleanOn
		{
			set;
			get;
		}

		public string BooleanOff
		{
			set;
			get;
		}

		public string UserData
		{
			set;
			get;
		}

		public DataGrid GetDataGrid()
		{
			DataGrid dataGrid = new DataGrid();

			dataGrid.Columns = this.Columns;
			dataGrid.ListCaption = this.ListCaption;
			dataGrid.ListDirection = this.ListDirection;
			dataGrid.BooleanOff = this.BooleanOff;
			dataGrid.BooleanOn = this.BooleanOn;
			dataGrid.ID = this.ID;
			dataGrid.UserData = this.UserData;

			return dataGrid;
		}
	}
}
