using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class SmsRate : CommonEntityBase
	{
		public enum TableFields
		{
			Farsi,
			Latin,
			Operator,
			UserGuid,
			SmsSenderAgentGuid,
			IsDeleted,
		}

		public SmsRate()
			: base(TableNames.SmsRates.ToString())
		{
			AddField(TableFields.Farsi.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.Latin.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.Operator.ToString(), SqlDbType.Int);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.SmsSenderAgentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid SmsRateGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public decimal Farsi
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Farsi.ToString()]);
			}
			set
			{
				this[TableFields.Farsi.ToString()] = value;
			}
		}

		public decimal Latin
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Latin.ToString()]);
			}
			set
			{
				this[TableFields.Latin.ToString()] = value;
			}
		}

		public int Operator
		{
			get
			{
				return Helper.GetInt(this[TableFields.Operator.ToString()]);
			}
			set
			{
				this[TableFields.Operator.ToString()] = value;
			}
		}

		public Guid UserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserGuid.ToString()]);
			}
			set
			{
				this[TableFields.UserGuid.ToString()] = value;
			}
		}

		public Guid SmsSenderAgentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsSenderAgentGuid.ToString()]);
			}
			set
			{
				this[TableFields.SmsSenderAgentGuid.ToString()] = value;
			}
		}
	}
}
