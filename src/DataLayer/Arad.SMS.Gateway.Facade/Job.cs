using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Facade
{
	public class Job : FacadeEntityBase
	{
		public static DataTable GetJobs(Guid parentGuid)
		{
			Business.Job jobController = new Business.Job();
			return jobController.GetJobs(parentGuid);
		}
	}
}
