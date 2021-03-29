using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using Common;
using System.Data;
using GeneralLibrary;

namespace Business
{
	public class DomainAccount : BusinessEntityBase
	{
		public DomainAccount(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DomainAccounts.ToString(), dataAccessProvider) { }

		public Guid InsertNicAccountActualPerson(Common.DomainAccount domainAccount)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				base.ExecuteSPCommand("InsertNicAccountActualPerson", "@Guid", guid,
																														"@Type", domainAccount.Type,
																														"@FirstName", domainAccount.FirstName,
																														"@LastName", domainAccount.LastName,
																														"@NationalCode", domainAccount.NationalCode,
																														"@CompanyName", domainAccount.CompanyName,
																														"@Address", domainAccount.Address,
																														"@City", domainAccount.City,
																														"@Province", domainAccount.Province,
																														"@Country", domainAccount.Country,
																														"@PostalCode", domainAccount.PostalCode,
																														"@Telephone", domainAccount.Telephone,
																														"@FaxNumber", domainAccount.FaxNumber,
																														"@Email", domainAccount.Email,
																														"@NICType", domainAccount.NICType,
																														"@CreateDate", domainAccount.CreateDate,
																														"@UserGuid", domainAccount.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public Guid InsertNicAccountCivilPerson(Common.DomainAccount domainAccount)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				base.ExecuteSPCommand("InsertNicAccountCivilPerson", "@Guid", guid,
																															"@Type", domainAccount.Type,
																															"@FirstName", domainAccount.FirstName,
																															"@LastName", domainAccount.LastName,
																															"@CompanyName", domainAccount.CompanyName,
																															"@Address", domainAccount.Address,
																															"@City", domainAccount.City,
																															"@Province", domainAccount.Province,
																															"@Country", domainAccount.Country,
																															"@PostalCode", domainAccount.PostalCode,
																															"@Telephone", domainAccount.Telephone,
																															"@FaxNumber", domainAccount.FaxNumber,
																															"@Email", domainAccount.Email,
																															"@NICType", domainAccount.NICType,
																															"@CivilType",domainAccount.CivilType,
																															"@CountryOfCompany",domainAccount.CountryOfCompany,
																															"@ProvinceOfCompany",domainAccount.ProvinceOfCompany,
																															"@CityOfCompany",domainAccount.CityOfCompany,
																															"@RegisteredCompanyName",domainAccount.RegisteredCompanyName,
																															"@CompanyID",domainAccount.CompanyID,
																															"@NationalCode", domainAccount.NationalCode,
																															"@CompanyType",domainAccount.CompanyType,
																															"@CreateDate", domainAccount.CreateDate,
																															"@UserGuid", domainAccount.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public Guid InsertDirectAccount(Common.DomainAccount domainAccount)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				base.ExecuteSPCommand("InsertDirectAccount", "@Guid", guid,
																										"@Type", domainAccount.Type,
																										"@UserName", domainAccount.LastName,
																										"@Password", domainAccount.LastName,
																										"@FirstName", domainAccount.FirstName,
																										"@CompanyName", domainAccount.CompanyName,
																										"@Address", domainAccount.Address,
																										"@City", domainAccount.City,
																										"@Province", domainAccount.Province,
																										"@Country", domainAccount.Country,
																										"@PostalCode", domainAccount.PostalCode,
																										"@Telephone", domainAccount.Telephone,
																										"@CreateDate", domainAccount.CreateDate,
																										"@UserGuid", domainAccount.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}
	}
}
