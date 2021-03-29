using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Common
{
	public class City : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			Name,
			ProvinceGuid
		}

		public City()
			: base(TableNames.Cities.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.ProvinceGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid CityGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int ID
		{
			get
			{
				return Helper.GetInt(this[TableFields.ID.ToString()]);
			}
			set
			{
				this[TableFields.ID.ToString()] = value;
			}
		}

		public string Name
		{
			get
			{
				return Helper.GetString(this[TableFields.Name.ToString()]);
			}
			set
			{
				this[TableFields.Name.ToString()] = value;
			}
		}

		public Guid ProvinceGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ProvinceGuid.ToString()]);
			}
			set
			{
				this[TableFields.ProvinceGuid.ToString()] = value;
			}
		}
	}
}
