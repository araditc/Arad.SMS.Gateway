using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using CometResult;

namespace HttpPushFromMsSql
{
	public class CometAsyncHandler : IHttpAsyncHandler, IReadOnlySessionState
	{
		private static List<CometAsyncResult> allWaitingClients = new List<CometAsyncResult>();
		private static object allWaitingClientsLockObject = new object();
		private static bool threadForTimeoutsWorking = false;
		private static int waitTime = 60;

		#region--- IHttpAsyncHandler Methods
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}

		public bool IsReusable
		{
			get { return true; }
		}

		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
		{
			context.Response.ContentType = "text/plain";

			//context.Session
			CometAsyncResult result = new CometAsyncResult(context, callback, waitTime);
			lock (allWaitingClientsLockObject)
			{
				allWaitingClients.Add(result);
				if (allWaitingClients.Count == 1)
					StartClientTimeouter();
			}
			return result;
		}

		public void EndProcessRequest(IAsyncResult result)
		{
			CometAsyncResult cometAsyncResult = (CometAsyncResult)result;

			if (cometAsyncResult.CompletedSynchronously)
				return;

			WriteResponseToClient(cometAsyncResult);
		}
		#endregion

		public void WriteResponseToClient(CometAsyncResult cometAsyncResult)
		{
			if (cometAsyncResult.Result)
				cometAsyncResult.Context.Response.Write(cometAsyncResult.Message);
			else
				cometAsyncResult.Context.Response.Write("TOOLONG-DOITAGAIN"); // timeout - client must make request again
		}

		public static void ProcessAllWaitingClients(string message)
		{
			ThreadPool.QueueUserWorkItem(
					delegate
					{
						lock (allWaitingClientsLockObject) // this will block all new clients that wants to be inserted in list
						{
							foreach (CometAsyncResult asyncResult in allWaitingClients)
							{
								asyncResult.Result = true; // New data available
								asyncResult.Message = message;
								asyncResult.Callback(asyncResult);
							}
							allWaitingClients.Clear();
						}
					});
		}

		public static void StartClientTimeouter()
		{
			lock (allWaitingClientsLockObject)
			{
				if (threadForTimeoutsWorking)
					return;
				else
					threadForTimeoutsWorking = true;
			}

			ThreadPool.QueueUserWorkItem((WaitCallback)ClientTimeouter);
		}

		public static void ClientTimeouter(object state)
		{
			int count;
			lock (allWaitingClientsLockObject)
				count = allWaitingClients.Count;

			while (count > 0)
			{
				// Call Callback() to all timed out requests and remove from list.
				lock (allWaitingClientsLockObject)
					allWaitingClients.RemoveAll(CheckTimeOut);

				Thread.Sleep(1000);

				lock (allWaitingClientsLockObject)
				{
					count = allWaitingClients.Count;
					if (count == 0)
						threadForTimeoutsWorking = false;
				}
			}
		}

		private static bool CheckTimeOut(CometAsyncResult asyncResult)
		{
			if (asyncResult.StartTime.Add(asyncResult.WaitTime) < DateTime.Now)
			{
				asyncResult.Result = false; // timeout
				asyncResult.Callback(asyncResult);
				return true; // true for remove from list
			}

			return false; // not remove (because not timed out)
		}
	}
}
