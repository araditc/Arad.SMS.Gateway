using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class DomainPrice : CommonEntityBase
	{
		private enum TableFields
		{
			Extention,
			Period,
			Price,
			CreateDate,
			DomainGroupPriceGuid,
		}

		public DomainPrice()
			: base(TableNames.DomainPrices.ToString())
		{
			AddField(TableFields.Extention.ToString(), SqlDbType.NChar, 50);
			AddField(TableFields.Period.ToString(), SqlDbType.Int);
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.DomainGroupPriceGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid DomainPriceGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int Extention
		{
			get
			{
				return Helper.GetInt(this[TableFields.Extention.ToString()]);
			}
			set
			{
				if (value != 0)
					this[TableFields.Extention.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage += Language.GetString("InvalidDomainExtention");
				}
			}
		}

		public int Period
		{
			get
			{
				return Helper.GetInt(this[TableFields.Period.ToString()]);
			}
			set
			{
				this[TableFields.Period.ToString()] = value;
			}
		}

		public decimal Price
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Price.ToString()]);
			}
			set
			{
				this[TableFields.Price.ToString()] = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]);
			}
			set
			{
				this[TableFields.CreateDate.ToString()] = value;
			}
		}

		public Guid DomainGroupPriceGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.DomainGroupPriceGuid.ToString()]);
			}
			set
			{
				if (value != Guid.Empty)
					this[TableFields.DomainGroupPriceGuid.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage += Language.GetString("InvalidDomainGroupPriceGuid");
				}
			}
		}
	}
}
