using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class UserPrivateNumber : CommonEntityBase
	{
		private enum TableFields
		{
			StartDate,
			EndDate,
			CreateDate,
			IsActive,
			UseForChildren,
			DecreaseFromPanel,
			Type,
			UseType,
			Price,
			ActivationUserGuid,
			UserPrivateNumberParentGuid,
			UserGuid,
			PrivateNumberGuid,
		}

		public UserPrivateNumber()
			: base(TableNames.UserPrivateNumbers.ToString())
		{
			AddField(TableFields.StartDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.EndDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.UseForChildren.ToString(), SqlDbType.Bit);
			AddField(TableFields.DecreaseFromPanel.ToString(), SqlDbType.Bit);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.UseType.ToString(), SqlDbType.Int);
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.ActivationUserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserPrivateNumberParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid UserPrivateNumberGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public DateTime StartDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.StartDate.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteStartValidity");
				}
				else
					this[TableFields.StartDate.ToString()] = value;
			}
		}

		public DateTime EndDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.EndDate.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteEndValidity");
				}
				else
					this[TableFields.EndDate.ToString()] = value;
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

		public bool IsActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsActive.ToString()]);
			}
			set
			{
				this[TableFields.IsActive.ToString()] = value;
			}
		}

		public bool UseForChildren
		{
			get
			{
				return Helper.GetBool(this[TableFields.UseForChildren.ToString()]);
			}
			set
			{
				this[TableFields.UseForChildren.ToString()] = value;
			}
		}

		public bool DecreaseFromPanel
		{
			get
			{
				return Helper.GetBool(this[TableFields.DecreaseFromPanel.ToString()]);
			}
			set
			{
				this[TableFields.DecreaseFromPanel.ToString()] = value;
			}
		}

		public int Type
		{
			get
			{
				return Helper.GetInt(this[TableFields.Type.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteTypePrivateNumber");
				}
				else
					this[TableFields.Type.ToString()] = value;
			}
		}

		public int UseType
		{
			get
			{
				return Helper.GetInt(this[TableFields.UseType.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteUseTypePrivateNumber");
				}
				else
				this[TableFields.UseType.ToString()] = value;
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

		public Guid ActivationUserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ActivationUserGuid.ToString()]);
			}
			set
			{
				this[TableFields.ActivationUserGuid.ToString()] = value;
			}
		}

		public Guid UserPrivateNumberParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserPrivateNumberParentGuid.ToString()]);
			}
			set
			{
				this[TableFields.UserPrivateNumberParentGuid.ToString()] = value;
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

		public Guid PrivateNumberGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PrivateNumberGuid.ToString()]);
			}
			set
			{
				this[TableFields.PrivateNumberGuid.ToString()] = value;
			}
		}
	}
}
