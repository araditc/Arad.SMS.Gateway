using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Common
{
	public class Province : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			Name,
			CountryGuid
		}

		public Province()
			: base(TableNames.Provinces.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CountryGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid ProvinceGuid
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

		public Guid CountryGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.CountryGuid.ToString()]);
			}
			set
			{
				this[TableFields.CountryGuid.ToString()] = value;
			}
		}
	}
}
