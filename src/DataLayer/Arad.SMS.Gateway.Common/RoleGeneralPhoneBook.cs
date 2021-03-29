using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class RoleGeneralPhoneBook : CommonEntityBase
	{
		public enum TableFields
		{
			Price,
			RoleGuid,
			GeneralPhoneBookGuid
		}

		public RoleGeneralPhoneBook()
			: base(TableNames.RoleGeneralPhoneBooks.ToString())
		{
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.RoleGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.GeneralPhoneBookGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid RoleGeneralPhoneBookGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public decimal Price
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Price.ToString()]);
			}
			set
			{
				if (value >= 0)
					this[TableFields.Price.ToString()] = value;
				else
				{
					ErrorMessage += Language.GetString("PriceValueIsNotValid");
					HasError = true;
				}
			}
		}

		public Guid RoleGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.RoleGuid.ToString()]);
			}
			set
			{
				if (value == Guid.Empty)
				{
					HasError = true;
					ErrorMessage = Language.GetString("SelectRole");
				}
				else
				{
					this[TableFields.RoleGuid.ToString()] = value;
				}
			}
		}

		public Guid GeneralPhoneBookGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.GeneralPhoneBookGuid.ToString()]);
			}
			set
			{
				if (value == Guid.Empty)
				{
					HasError = true;
					ErrorMessage = Language.GetString("IncorectInfo");
				}
				else
				{
					this[TableFields.GeneralPhoneBookGuid.ToString()] = value;
				}
			}
		}
	}
}