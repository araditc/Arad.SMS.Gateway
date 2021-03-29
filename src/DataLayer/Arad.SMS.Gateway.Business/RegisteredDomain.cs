using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using Common;

namespace Business
{
	public class RegisteredDomain : BusinessEntityBase
	{
		public RegisteredDomain(DataAccessBase dataAccessProvider = null)
			: base(TableNames.RegisteredDomains.ToString(), dataAccessProvider) { }

		public bool InsertNicDomain(Common.RegisteredDomain registeredDomain)
		{
			try
			{
				base.ExecuteSPCommand("InsertNicDomain", "@Guid", registeredDomain.RegisteredDomainGuid,
																								"@UserGuid", registeredDomain.UserGuid,
																								"@Type", registeredDomain.Type,
																								"@DomainName", registeredDomain.DomainName,
																								"@DomainExtention", registeredDomain.DomainExtention,
																								"@DNS1", registeredDomain.DNS1,
																								"@DNS2", registeredDomain.DNS2,
																								"@DNS3", registeredDomain.DNS3,
																								"@DNS4", registeredDomain.DNS4,
																								"@IP1", registeredDomain.IP1,
																								"@IP2", registeredDomain.IP2,
																								"@IP3", registeredDomain.IP3,
																								"@IP4", registeredDomain.IP4,
																								"@Period", registeredDomain.Period,
																								"@CreateDate", registeredDomain.CreateDate,
																								"@ExpireDate", registeredDomain.ExpireDate,
																								"@IsPayment", registeredDomain.IsPayment,
																								"@Status", registeredDomain.Status,
																								"@CustomerID", registeredDomain.CustomerID,
																								"@OfficeRelation", registeredDomain.OfficeRelation,
																								"@TechnicalRelation", registeredDomain.TechnicalRelation,
																								"@FinancialRelation", registeredDomain.FinancialRelation);
				return true;
			}
			catch
			{
				return false;
			}
		}


		public bool InsertDirectDomain(Common.RegisteredDomain registeredDomain)
		{
			try
			{
				base.ExecuteSPCommand("InsertDirectDomain", "@Guid", registeredDomain.RegisteredDomainGuid,
																										"@UserGuid", registeredDomain.UserGuid,
																										"@Type", registeredDomain.Type,
																										"@DomainName", registeredDomain.DomainName,
																										"@DomainExtention", registeredDomain.DomainExtention,
																										"@DNS1", registeredDomain.DNS1,
																										"@DNS2", registeredDomain.DNS2,
																										"@Period", registeredDomain.Period,
																										"@CreateDate", registeredDomain.CreateDate,
																										"@ExpireDate", registeredDomain.ExpireDate,
																										"@IsPayment", registeredDomain.IsPayment,
																										"@Status", registeredDomain.Status,
																										"@CustomerID", registeredDomain.CustomerID,
																										"@Email", registeredDomain.Email);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
