using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class DomainAccount : CommonEntityBase
	{
		private enum TableFields
		{
			Guid,
			Type,
			UserName,
			Password,
			FirstName,
			LastName,
			NationalCode,
			CompanyName,
			Address,
			City,
			Province,
			Country,
			PostalCode,
			Telephone,
			FaxNumber,
			Email,
			NICType,
			CivilType,
			CountryOfCompany,
			ProvinceOfCompany,
			CityOfCompany,
			RegisteredCompanyName,
			CompanyID,
			CompanyType,
			CreateDate,
			UserGuid,
			IsDeleted,
		}

		public DomainAccount()
			: base(TableNames.DomainAccounts.ToString())
		{
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.UserName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Password.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.FirstName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.LastName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.NationalCode.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Address.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.City.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Province.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Country.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.PostalCode.ToString(), SqlDbType.Char, 10);
			AddField(TableFields.Telephone.ToString(), SqlDbType.Char, 50);
			AddField(TableFields.FaxNumber.ToString(), SqlDbType.Char, 50);
			AddField(TableFields.Email.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.NICType.ToString(), SqlDbType.Int);
			AddField(TableFields.CivilType.ToString(), SqlDbType.Int);
			AddField(TableFields.CountryOfCompany.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.ProvinceOfCompany.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CityOfCompany.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.RegisteredCompanyName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyID.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyType.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid DomainAccountGuid
		{
			get
			{
				return PrimaryKey;
			}
			set
			{
				PrimaryKey = value;
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

		public string UserName
		{
			get
			{
				return Helper.GetString(this[TableFields.UserName.ToString()]);
			}
			set
			{
				this[TableFields.UserName.ToString()] = value;
			}
		}

		public string Password
		{
			get
			{
				return Helper.GetString(this[TableFields.Password.ToString()]);
			}
			set
			{
				if (value.Length >= 8 && value.Length <= 15)
					this[TableFields.Password.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage += Language.GetString("DirectAccountPasswordError");
				}
			}
		}

		public string FirstName
		{
			get
			{
				return Helper.GetString(this[TableFields.FirstName.ToString()]);
			}
			set
			{
				this[TableFields.FirstName.ToString()] = value;
			}
		}

		public string LastName
		{
			get
			{
				return Helper.GetString(this[TableFields.LastName.ToString()]);
			}
			set
			{
				this[TableFields.LastName.ToString()] = value;
			}
		}

		public string NationalCode
		{
			get
			{
				return Helper.GetString(this[TableFields.NationalCode.ToString()]);
			}
			set
			{
				this[TableFields.NationalCode.ToString()] = value;
			}
		}

		public string CompanyName
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyName.ToString()]);
			}
			set
			{
				this[TableFields.CompanyName.ToString()] = value;
			}
		}

		public string Address
		{
			get
			{
				return Helper.GetString(this[TableFields.Address.ToString()]);
			}
			set
			{
				this[TableFields.Address.ToString()] = value;
			}
		}

		public string City
		{
			get
			{
				return Helper.GetString(this[TableFields.City.ToString()]);
			}
			set
			{
				this[TableFields.City.ToString()] = value;
			}
		}

		public string Province
		{
			get
			{
				return Helper.GetString(this[TableFields.Province.ToString()]);
			}
			set
			{
				this[TableFields.Province.ToString()] = value;
			}
		}

		public string Country
		{
			get
			{
				return Helper.GetString(this[TableFields.Country.ToString()]);
			}
			set
			{
				this[TableFields.Country.ToString()] = value;
			}
		}

		public string PostalCode
		{
			get
			{
				return Helper.GetString(this[TableFields.PostalCode.ToString()]);
			}
			set
			{
				this[TableFields.PostalCode.ToString()] = value;
			}
		}

		public string Telephone
		{
			get
			{
				return Helper.GetString(this[TableFields.Telephone.ToString()]);
			}
			set
			{
				this[TableFields.Telephone.ToString()] = value;
			}
		}

		public string FaxNumber
		{
			get
			{
				return Helper.GetString(this[TableFields.FaxNumber.ToString()]);
			}
			set
			{
				this[TableFields.FaxNumber.ToString()] = value;
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

		public int NICType
		{
			get
			{
				return Helper.GetInt(this[TableFields.NICType.ToString()]);
			}
			set
			{
				this[TableFields.NICType.ToString()] = value;
			}
		}

		public int CivilType
		{
			get
			{
				return Helper.GetInt(this[TableFields.CivilType.ToString()]);
			}
			set
			{
				this[TableFields.CivilType.ToString()] = value;
			}
		}

		public string CountryOfCompany
		{
			get
			{
				return Helper.GetString(this[TableFields.CountryOfCompany.ToString()]);
			}
			set
			{
				this[TableFields.CountryOfCompany.ToString()] = value;
			}
		}

		public string ProvinceOfCompany
		{
			get
			{
				return Helper.GetString(this[TableFields.ProvinceOfCompany.ToString()]);
			}
			set
			{
				this[TableFields.ProvinceOfCompany.ToString()] = value;
			}
		}

		public string CityOfCompany
		{
			get
			{
				return Helper.GetString(this[TableFields.CityOfCompany.ToString()]);
			}
			set
			{
				this[TableFields.CityOfCompany.ToString()] = value;
			}
		}

		public string RegisteredCompanyName
		{
			get
			{
				return Helper.GetString(this[TableFields.RegisteredCompanyName.ToString()]);
			}
			set
			{
				this[TableFields.RegisteredCompanyName.ToString()] = value;
			}
		}

		public string CompanyID
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyID.ToString()]);
			}
			set
			{
				this[TableFields.CompanyID.ToString()] = value;
			}
		}

		public int CompanyType
		{
			get
			{
				return Helper.GetInt(this[TableFields.CompanyType.ToString()]);
			}
			set
			{
				this[TableFields.CompanyType.ToString()] = value;
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