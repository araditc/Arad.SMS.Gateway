using Common;
using GeneralLibrary;

namespace GiveBackCredit
{
	class GarbageCollectorThread : WorkerThread
	{
		#region Constructor
		public GarbageCollectorThread(int timeOut) : base(timeOut) { }
		#endregion

		protected override void WorkerThreadFunction()
		{
			long beforeCollect;
			long afterCollect;

			beforeCollect = System.GC.GetTotalMemory(false);
			System.GC.Collect();
			afterCollect = System.GC.GetTotalMemory(true);

			LogController<ServiceLogs>.LogInFile(ServiceLogs.GarbageCollector, string.Format("Garbage Collector:       Memory usage before collection: {0}, after collection: {1}", beforeCollect, afterCollect));
		}
	}
}
