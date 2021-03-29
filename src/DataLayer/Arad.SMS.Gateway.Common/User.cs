// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using System;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Common
{
	public class User : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			UserName,
			Password,
			SecondPassword,
			FirstName,
			LastName,
			FatherName,
			NationalCode,
			ZipCode,
			ShCode,
			Email,
			Phone,
			Mobile,
			FaxNumber,
			Address,
			ZoneGuid,
			BirthDate,
			CreateDate,
			ExpireDate,
			Credit,
			CompanyName,
			CompanyNationalId,
			EconomicCode,
			CompanyAddress,
			CompanyPhone,
			CompanyZipCode,
			CompanyCEOName,
			CompanyCEONationalCode,
			CompanyCEOMobile,
			PanelPrice,
			Type,
			IsActive,
			IsAuthenticated,
			IsActiveSend,
			IsAdmin,
			IsSuperAdmin,
			IsMainAdmin,
			MaximumAdmin,
			MaximumUser,
			MaximumPhoneNumber,
			MaximumEmailAddress,
			ParentGuid,
			DomainGroupPriceGuid,
			PriceGroupGuid,
			IsFixPriceGroup,
			DomainGuid,
			RoleGuid,
			IsDeleted,
		}

		public User()
			: base(TableNames.Users.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.UserName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Password.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.SecondPassword.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.FirstName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.LastName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.FatherName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.NationalCode.ToString(), SqlDbType.NChar, 10);
			AddField(TableFields.ZipCode.ToString(), SqlDbType.NChar, 10);
			AddField(TableFields.ShCode.ToString(), SqlDbType.NVarChar, 16);
			AddField(TableFields.Email.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Phone.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Mobile.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.FaxNumber.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Address.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.ZoneGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.BirthDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ExpireDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Credit.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.CompanyName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyNationalId.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.EconomicCode.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyAddress.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyPhone.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyZipCode.ToString(), SqlDbType.NChar, 10);
			AddField(TableFields.CompanyCEOName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CompanyCEONationalCode.ToString(), SqlDbType.NChar, 10);
			AddField(TableFields.CompanyCEOMobile.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.PanelPrice.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsAuthenticated.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsActiveSend.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsAdmin.ToString(), SqlDbType.Bit);
			AddField(TableFields.MaximumAdmin.ToString(), SqlDbType.Int);
			AddField(TableFields.MaximumUser.ToString(), SqlDbType.Int);
			AddField(TableFields.MaximumPhoneNumber.ToString(), SqlDbType.Int);
			AddField(TableFields.MaximumEmailAddress.ToString(), SqlDbType.Int);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.DomainGroupPriceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.PriceGroupGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsFixPriceGroup.ToString(), SqlDbType.Bit);
			AddField(TableFields.DomainGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddReadOnlyField(TableFields.IsSuperAdmin.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsMainAdmin.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.RoleGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid UserGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public long ID
		{
			get
			{
				return Helper.GetLong(this[TableFields.ID.ToString()]);
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteUserNameField");
					HasError = true;
				}
				else
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePasswordField");
					HasError = true;
				}
				else
					this[TableFields.Password.ToString()] = value;
			}
		}

		public string SecondPassword
		{
			get
			{
				return Helper.GetString(this[TableFields.SecondPassword.ToString()]);
			}
			set
			{
				this[TableFields.SecondPassword.ToString()] = value;
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteFirstNameField");
					HasError = true;
				}
				else
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteLastNameField");
					HasError = true;
				}
				else
					this[TableFields.LastName.ToString()] = value;
			}
		}

		public string FatherName
		{
			get
			{
				return Helper.GetString(this[TableFields.FatherName.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteFatherNameField");
					HasError = true;
				}
				else
					this[TableFields.FatherName.ToString()] = value;
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteNationalCodeField");
					HasError = true;
				}
				else
					this[TableFields.NationalCode.ToString()] = value;
			}
		}

		public string ZipCode
		{
			get
			{
				return Helper.GetString(this[TableFields.ZipCode.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteZipCodeField");
					HasError = true;
				}
				else
					this[TableFields.ZipCode.ToString()] = value;
			}
		}

		public string ShCode
		{
			get
			{
				return Helper.GetString(this[TableFields.ShCode.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteShCodeField");
					HasError = true;
				}
				else
					this[TableFields.ShCode.ToString()] = value;
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
				CheckDataConditionsResult result = Helper.CheckDataConditions(value);
				if (result.IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteEmailField");
					HasError = true;
				}
				else if (!result.IsEmail)
				{
					ErrorMessage += Language.GetString("EmailFieldIsNotCorectFormat");
					HasError = true;
				}
				else
					this[TableFields.Email.ToString()] = value;
			}
		}

		public string Phone
		{
			get
			{
				return Helper.GetString(this[TableFields.Phone.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePhoneField");
					HasError = true;
				}
				else
					this[TableFields.Phone.ToString()] = value;
			}
		}

		public string Mobile
		{
			get
			{
				return Helper.GetString(this[TableFields.Mobile.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteMobileNoField");
					HasError = true;
				}
				else
					this[TableFields.Mobile.ToString()] = value;
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

		public string Address
		{
			get
			{
				return Helper.GetString(this[TableFields.Address.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteAddressField");
					HasError = true;
				}
				else

					this[TableFields.Address.ToString()] = value;
			}
		}

		public Guid ZoneGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ZoneGuid.ToString()]);
			}
			set
			{
				if (Helper.GetGuid(value) == Guid.Empty)
				{
					ErrorMessage += Language.GetString("CompleteCityField");
					HasError = true;
				}
				else
					this[TableFields.ZoneGuid.ToString()] = value;
			}
		}

		public DateTime BirthDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.BirthDate.ToString()]);
			}
			set
			{
				this[TableFields.BirthDate.ToString()] = value;
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
				if (Helper.GetDateTime(value) == DateTime.MaxValue)
				{
					ErrorMessage += Language.GetString("CompleteExpireDateField");
					HasError = true;
				}
				else
					this[TableFields.ExpireDate.ToString()] = value;
			}
		}

		public decimal Credit
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Credit.ToString()]);
			}
			set
			{
				this[TableFields.Credit.ToString()] = value;
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyNameField");
					HasError = true;
				}
				else
					this[TableFields.CompanyName.ToString()] = value;
			}
		}

		public string CompanyNationalId
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyNationalId.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyNationalIdField");
					HasError = true;
				}
				else
					this[TableFields.CompanyNationalId.ToString()] = value;
			}
		}

		public string EconomicCode
		{
			get
			{
				return Helper.GetString(this[TableFields.EconomicCode.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteEconomicCodeField");
					HasError = true;
				}
				else
					this[TableFields.EconomicCode.ToString()] = value;
			}
		}

		public string CompanyAddress
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyAddress.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyAddressField");
					HasError = true;
				}
				else
					this[TableFields.CompanyAddress.ToString()] = value;
			}
		}

		public string CompanyPhone
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyPhone.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyPhoneField");
					HasError = true;
				}
				else
					this[TableFields.CompanyPhone.ToString()] = value;
			}
		}

		public string CompanyZipCode
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyZipCode.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyZipCodeField");
					HasError = true;
				}
				else
					this[TableFields.CompanyZipCode.ToString()] = value;
			}
		}

		public string CompanyCEOName
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyCEOName.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyCEONameField");
					HasError = true;
				}
				else
					this[TableFields.CompanyCEOName.ToString()] = value;
			}
		}

		public string CompanyCEONationalCode
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyCEONationalCode.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyCEONationalCodeField");
					HasError = true;
				}
				else
					this[TableFields.CompanyCEONationalCode.ToString()] = value;
			}
		}

		public string CompanyCEOMobile
		{
			get
			{
				return Helper.GetString(this[TableFields.CompanyCEOMobile.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteCompanyCEOMobileField");
					HasError = true;
				}
				else
					this[TableFields.CompanyCEOMobile.ToString()] = value;
			}
		}

		public decimal PanelPrice
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.PanelPrice.ToString()]);
			}
			set
			{
				this[TableFields.PanelPrice.ToString()] = value;
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

		public bool IsAuthenticated
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsAuthenticated.ToString()]);
			}
			set
			{
				this[TableFields.IsAuthenticated.ToString()] = value;
			}
		}

		public bool IsActiveSend
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsActiveSend.ToString()]);
			}
			set
			{
				this[TableFields.IsActiveSend.ToString()] = value;
			}
		}

		public bool IsAdmin
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsAdmin.ToString()]);
			}
			set
			{
				this[TableFields.IsAdmin.ToString()] = value;
			}
		}
		public bool IsSuperAdmin
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsSuperAdmin.ToString()]);
			}
			set
			{
				this[TableFields.IsSuperAdmin.ToString()] = value;
			}
		}

		public bool IsMainAdmin
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsMainAdmin.ToString()]);
			}
			set
			{
				this[TableFields.IsMainAdmin.ToString()] = value;
			}
		}

		public int MaximumAdmin
		{
			get
			{
				return Helper.GetInt(this[TableFields.MaximumAdmin.ToString()]);
			}
			set
			{
				this[TableFields.MaximumAdmin.ToString()] = value;
			}
		}

		public int MaximumUser
		{
			get
			{
				return Helper.GetInt(this[TableFields.MaximumUser.ToString()]);
			}
			set
			{
				this[TableFields.MaximumUser.ToString()] = value;
			}
		}

		public int MaximumPhoneNumber
		{
			get
			{
				return Helper.GetInt(this[TableFields.MaximumPhoneNumber.ToString()]);
			}
			set
			{
				this[TableFields.MaximumPhoneNumber.ToString()] = value;
			}
		}

		public int MaximumEmailAddress
		{
			get
			{
				return Helper.GetInt(this[TableFields.MaximumEmailAddress.ToString()]);
			}
			set
			{
				this[TableFields.MaximumEmailAddress.ToString()] = value;
			}
		}

		public Guid ParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]);
			}
			set
			{
				this[TableFields.ParentGuid.ToString()] = value;
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
				this[TableFields.DomainGroupPriceGuid.ToString()] = value;
			}
		}

		public Guid PriceGroupGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PriceGroupGuid.ToString()]);
			}
			set
			{
				this[TableFields.PriceGroupGuid.ToString()] = value;
			}
		}

		public bool IsFixPriceGroup
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsFixPriceGroup.ToString()]);
			}
			set
			{
				this[TableFields.IsFixPriceGroup.ToString()] = value;
			}
		}

		public Guid DomainGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.DomainGuid.ToString()]);
			}
			set
			{
				this[TableFields.DomainGuid.ToString()] = value;
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
				this[TableFields.RoleGuid.ToString()] = value;
			}
		}
	}
}
