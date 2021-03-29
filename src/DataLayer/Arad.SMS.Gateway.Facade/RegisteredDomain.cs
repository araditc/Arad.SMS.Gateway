using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class RegisteredDomain : FacadeEntityBase
	{

		public static DataTable GetDataTableDomainStatus(string[] domainsList)
		{
			DataTable dataTableDomainStatus = new DataTable();
			try
			{
				dataTableDomainStatus.Columns.Add("Guid", typeof(Guid));
				dataTableDomainStatus.Columns.Add("Status", typeof(int));
				dataTableDomainStatus.Columns.Add("DomainName", typeof(string));
				dataTableDomainStatus.Columns.Add("Extention", typeof(int));
				//dataTableDomainStatus.Columns.Add("DomainExtention", typeof(string));

				foreach (string domain in domainsList)
				{
					string domainName = domain.Split(':')[0];
					string status = domain.Split(':')[1];
					string extention = domainName.Substring(domainName.IndexOf('.'));

					Business.DomainStatus domainStatus = (Business.DomainStatus)Enum.Parse(typeof(Business.DomainStatus),
																																								 status,
																																								 true);
					Business.DomainExtention domainExtention = (Business.DomainExtention)Enum.Parse(typeof(Business.DomainExtention),
																																													extention.Replace('.', '_'),
																																													true);
					dataTableDomainStatus.Rows.Add(Guid.NewGuid(), (int)domainStatus, domainName, (int)domainExtention);
				}

				return dataTableDomainStatus;
			}
			catch
			{
				return dataTableDomainStatus;
			}
		}

		public static bool InsertNicDomain(Common.RegisteredDomain registeredDomain)
		{
			Business.RegisteredDomain registeredDomainController = new Business.RegisteredDomain();
			return registeredDomainController.InsertNicDomain(registeredDomain);
		}

		public static bool InsertDirectDomain(Common.RegisteredDomain registeredDomain)
		{
			Business.RegisteredDomain registeredDomainController = new Business.RegisteredDomain();
			return registeredDomainController.InsertDirectDomain(registeredDomain);
		}
	}
}