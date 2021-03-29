using Common;
using GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public class Job : BusinessEntityBase
	{
		public Job(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Jobs.ToString(), dataAccessProvider) { }

		public DataTable GetJobs(Guid parentGuid)
		{
			return FetchSPDataTable("GetJobs", "@ParentGuid", parentGuid);
		}
	}
}
