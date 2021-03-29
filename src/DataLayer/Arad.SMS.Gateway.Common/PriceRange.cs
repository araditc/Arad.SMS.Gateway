using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class PriceRange : CommonEntityBase
	{
		public enum TableFields
		{
			Ratio,
			CreateDate,
			GroupPriceGuid,
			SmsSenderAgentGuid,
		}

		public PriceRange()
			: base(TableNames.PriceRanges.ToString())
		{
			AddField(TableFields.Ratio.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.GroupPriceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.SmsSenderAgentGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid PriceRangeGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public decimal Ratio
		{
			get { return Helper.GetDecimal(this[TableFields.Ratio.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePrice");
					HasError = true;
				}
				else if (!Helper.CheckDataConditions(value).IsDecimalNumber)
				{
					ErrorMessage += Language.GetString("InvalidPrice");
					HasError = true;
				}
				else
					this[TableFields.Ratio.ToString()] = value;
			}
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public Guid GroupPriceGuid
		{
			get { return Helper.GetGuid(this[TableFields.GroupPriceGuid.ToString()]); }
			set { this[TableFields.GroupPriceGuid.ToString()] = value; }
		}

		public Guid SmsSenderAgentGuid
		{
			get { return Helper.GetGuid(this[TableFields.SmsSenderAgentGuid.ToString()]); }
			set { this[TableFields.SmsSenderAgentGuid.ToString()] = value; }
		}
	}
}
