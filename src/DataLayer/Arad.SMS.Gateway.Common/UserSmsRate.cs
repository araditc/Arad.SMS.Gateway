using System;
using System.Data;
using GeneralLibrary.BaseCore;
using GeneralLibrary;

namespace Common
{
	public class UserSmsRate : CommonEntityBase
	{
		private enum TableFields
		{
			FarsiHamrahAval,
			FarsiIranCell,
			LatinHamrahAval,
			LatinIranCell,
			UserGuid,
			IsDeleted,
		}

		public UserSmsRate()
			: base(TableNames.UserSmsRates.ToString())
		{
			AddField(TableFields.FarsiHamrahAval.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.FarsiIranCell.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.LatinHamrahAval.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.LatinIranCell.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);

			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid UserSmsRateGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public decimal FarsiHamrahAval
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.FarsiHamrahAval.ToString()]);
			}
			set
			{
				this[TableFields.FarsiHamrahAval.ToString()] = value;
			}
		}

		public decimal FarsiIranCell
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.FarsiIranCell.ToString()]);
			}
			set
			{
				this[TableFields.FarsiIranCell.ToString()] = value;
			}
		}

		public decimal LatinHamrahAval
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.LatinHamrahAval.ToString()]);
			}
			set
			{
				this[TableFields.LatinHamrahAval.ToString()] = value;
			}
		}

		public decimal LatinIranCell
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.LatinIranCell.ToString()]);
			}
			set
			{
				this[TableFields.LatinIranCell.ToString()] = value;
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
	}
}