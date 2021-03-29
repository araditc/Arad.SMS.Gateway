using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class RegisteredDomain : CommonEntityBase
	{
		private enum TableFields
		{
			Guid,
			UserGuid,
			DomainName,
			DomainExtention,
			Type,
			DNS1,
			DNS2,
			DNS3,
			DNS4,
			IP1,
			IP2,
			IP3,
			IP4,
			Period,
			CreateDate,
			ExpireDate,
			IsPayment,
			Status,
			CustomerID,
			OfficeRelation,
			TechnicalRelation,
			FinancialRelation,
			Email,
		}

		public RegisteredDomain()
			: base(TableNames.RegisteredDomains.ToString())
		{
			AddField(TableFields.Guid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.DomainName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.DomainExtention.ToString(), SqlDbType.Int);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.DNS1.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.DNS2.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.DNS3.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.DNS4.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.IP1.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.IP2.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.IP3.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.IP4.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Period.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ExpireDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsPayment.ToString(), SqlDbType.Bit);
			AddField(TableFields.Status.ToString(), SqlDbType.Int);
			AddField(TableFields.CustomerID.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.OfficeRelation.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.TechnicalRelation.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.FinancialRelation.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Email.ToString(), SqlDbType.NVarChar, 50);
		}

		public Guid RegisteredDomainGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
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

		public int Type
		{
			get
			{
				return Helper.GetInt(this[TableFields.Type.ToString()]);
			}
			set
			{
				this[TableFields.Type.ToString()] = value;
			}
		}

		public string DomainName
		{
			get
			{
				return Helper.GetString(this[TableFields.DomainName.ToString()]);
			}
			set
			{
				this[TableFields.DomainName.ToString()] = value;
			}
		}

		public int DomainExtention
		{
			get
			{
				return Helper.GetInt(this[TableFields.DomainExtention.ToString()]);
			}
			set
			{
				this[TableFields.DomainExtention.ToString()] = value;
			}
		}

		public string DNS1
		{
			get
			{
				return Helper.GetString(this[TableFields.DNS1.ToString()]);
			}
			set
			{
				this[TableFields.DNS1.ToString()] = value;
			}
		}

		public string DNS2
		{
			get
			{
				return Helper.GetString(this[TableFields.DNS2.ToString()]);
			}
			set
			{
				this[TableFields.DNS2.ToString()] = value;
			}
		}

		public string DNS3
		{
			get
			{
				return Helper.GetString(this[TableFields.DNS3.ToString()]);
			}
			set
			{
				this[TableFields.DNS3.ToString()] = value;
			}
		}

		public string DNS4
		{
			get
			{
				return Helper.GetString(this[TableFields.DNS4.ToString()]);
			}
			set
			{
				this[TableFields.DNS4.ToString()] = value;
			}
		}

		public string IP1
		{
			get
			{
				return Helper.GetString(this[TableFields.IP1.ToString()]);
			}
			set
			{
				this[TableFields.IP1.ToString()] = value;
			}
		}

		public string IP2
		{
			get
			{
				return Helper.GetString(this[TableFields.IP2.ToString()]);
			}
			set
			{
				this[TableFields.IP2.ToString()] = value;
			}
		}

		public string IP3
		{
			get
			{
				return Helper.GetString(this[TableFields.IP3.ToString()]);
			}
			set
			{
				this[TableFields.IP3.ToString()] = value;
			}
		}

		public string IP4
		{
			get
			{
				return Helper.GetString(this[TableFields.IP4.ToString()]);
			}
			set
			{
				this[TableFields.IP4.ToString()] = value;
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

		public DateTime ExpireDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.ExpireDate.ToString()]); 
			}
			set
			{
				this[TableFields.ExpireDate.ToString()] = value; 
			}
		}

		public bool IsPayment
		{
			get 
			{
				return Helper.GetBool(this[TableFields.IsPayment.ToString()]); 
			}
			set
			{
				this[TableFields.IsPayment.ToString()] = value; 
			}
		}

		public int Status
		{
			get
			{
				return Helper.GetInt(this[TableFields.Status.ToString()]);
			}
			set
			{
				this[TableFields.Status.ToString()] = value;
			}
		}

		public string CustomerID
		{
			get
			{
				return Helper.GetString(this[TableFields.CustomerID.ToString()]);
			}
			set
			{
				this[TableFields.CustomerID.ToString()] = value;
			}
		}

		public string OfficeRelation
		{
			get
			{
				return Helper.GetString(this[TableFields.OfficeRelation.ToString()]);
			}
			set
			{
				this[TableFields.OfficeRelation.ToString()] = value;
			}
		}

		public string TechnicalRelation
		{
			get
			{
				return Helper.GetString(this[TableFields.TechnicalRelation.ToString()]);
			}
			set
			{
				this[TableFields.TechnicalRelation.ToString()] = value;
			}
		}

		public string FinancialRelation
		{
			get
			{
				return Helper.GetString(this[TableFields.FinancialRelation.ToString()]);
			}
			set
			{
				this[TableFields.FinancialRelation.ToString()] = value;
			}
		}

		public string Email
		{
			get
			{
				return Helper.GetString(this[TableFields.Email.ToString()]);
			}
			set
			{
				this[TableFields.Email.ToString()] = value;
			}
		}
	}
}