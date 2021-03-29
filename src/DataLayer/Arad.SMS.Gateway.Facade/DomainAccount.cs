using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class DomainAccount : FacadeEntityBase
	{
		public static bool InsertNicAccountActualPerson(Common.DomainAccount domainAccount)
		{
			Business.DomainAccount DomainAccountController = new Business.DomainAccount();
			return DomainAccountController.InsertNicAccountActualPerson(domainAccount) != Guid.Empty ? true :false;
		}

		public static bool InsertNicAccountCivilPerson(Common.DomainAccount domainAccount)
		{
			Business.DomainAccount DomainAccountController = new Business.DomainAccount();
			return DomainAccountController.InsertNicAccountCivilPerson(domainAccount) != Guid.Empty ? true : false;
		}

		public static bool InsertDirectAccount(Common.DomainAccount domainAccount)
		{
			Business.DomainAccount DomainAccountController = new Business.DomainAccount();
			return DomainAccountController.InsertDirectAccount(domainAccount) != Guid.Empty ? true : false;
		}
	}
}
