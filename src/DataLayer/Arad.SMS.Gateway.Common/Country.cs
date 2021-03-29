using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Common
{
	public class Country : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			Code,
			Name
		}

		public Country()
			: base(TableNames.Countries.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Code.ToString(), SqlDbType.Char, 2);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
		}

		public Guid CountryGuid
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

		public string Code
		{
			get
			{
				return Helper.GetString(this[TableFields.Code.ToString()]);
			}
			set
			{
				this[TableFields.Code.ToString()] = value;
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
	}
}
