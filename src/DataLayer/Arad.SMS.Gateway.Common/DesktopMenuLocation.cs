using System;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class DesktopMenuLocation : CommonEntityBase
	{
		public enum TableFields
		{
			Location,
			Desktop,
			IsDeleted,
			DataCenterGuid
		}

		public DesktopMenuLocation()
			: base(TableNames.DesktopMenuLocations.ToString())
		{
			AddField(TableFields.Location.ToString(), SqlDbType.Int);
			AddField(TableFields.Desktop.ToString(), SqlDbType.Int);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.DataCenterGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid DesktopMenuLocationGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int Location
		{
			get { return Helper.GetInt(this[TableFields.Location.ToString()]); }
			set 
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteDesktopMenuLocationField");
					HasError = true;
				}
				else
					this[TableFields.Location.ToString()] = value; 
			}
		}

		public int Desktop
		{
			get { return Helper.GetInt(this[TableFields.Desktop.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteDesktopMenuLocationDesktopField");
					HasError = true;
				}
				else
					this[TableFields.Desktop.ToString()] = value;
			}
		}

		public Guid DataCenterGuid
		{
			get { return Helper.GetGuid(this[TableFields.DataCenterGuid.ToString()]); }
			set { this[TableFields.DataCenterGuid.ToString()] = value; }
		}

	}
}
