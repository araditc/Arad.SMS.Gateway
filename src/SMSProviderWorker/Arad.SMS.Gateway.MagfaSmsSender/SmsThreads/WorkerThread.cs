using System;
using System.Threading;

namespace MagfaSmsSender
{
	public abstract class WorkerThread : IDisposable
	{
		#region Events
		public event ThreadExceptionEventHandler ThreadException;
		#endregion

		#region Member variables
		private Thread thread;
		private int operationInterval;
		private bool throwOnAbort;
		private ManualResetEvent stopThread;
		private ManualResetEvent threadStopped;
		protected int shutdownTimeOut;
		#endregion

		#region Properties
		public string Name
		{ get; set; }

		public int OperationInterval
		{
			get { return this.operationInterval; }
			set { this.operationInterval = value; }
		}

		public bool ThrowOnThreadAbort
		{
			get { return this.throwOnAbort; }
			set { this.throwOnAbort = value; }
		}

		public bool IsAlive
		{
			get
			{
				return (this.thread != null && this.thread.IsAlive);
			}
		}

		protected bool IsStopSignaled
		{
			get { return this.stopThread.WaitOne(0, false); }
		}
		#endregion

		#region Constructor
		protected WorkerThread(int operationIntervalInMillisecond, int shutdownTimeoutInMillisecond)
			: this(operationIntervalInMillisecond)
		{
			this.shutdownTimeOut = shutdownTimeoutInMillisecond;
		}

		protected WorkerThread(int operationIntervalInMillisecond)
		{
			this.thread = null;
			this.operationInterval = operationIntervalInMillisecond;
			this.shutdownTimeOut = 10000;
			this.stopThread = new ManualResetEvent(false);
			this.threadStopped = new ManualResetEvent(false);
			this.throwOnAbort = true;
		}
		#endregion

		#region Abstract member functions
		protected abstract void WorkerThreadFunction();
		#endregion

		#region Public member functions
		public virtual bool Start()
		{
			if (this.thread != null)
				return false;

			this.stopThread.Reset();
			this.threadStopped.Reset();
			this.thread = new Thread(new ThreadStart(InternalThreadFunction));
			this.thread.Name = this.Name;
			this.thread.Start();

			return true;
		}

		public virtual void Stop()
		{
			if (this.thread != null)
			{
				#region Stop the thread
				this.stopThread.Set();
				if (Thread.CurrentThread == thread)
					try
					{
						this.thread.Abort();
					}
					catch { }
				else if (!this.threadStopped.WaitOne(this.shutdownTimeOut, false))
				{
					#region Abort the thread
					try
					{
						this.thread.Abort();
					}
					catch { }
					#endregion
				}
				#endregion

				this.thread = null;
			}
		}

		#region IDisposable Members
		public void Dispose()
		{
			Stop();
		}
		#endregion
		#endregion

		#region Protected member functions
		protected void OnThreadException(Exception ex)
		{
			if (this.ThreadException != null)
				this.ThreadException(this, new ThreadExceptionEventArgs(ex));
		}

		protected bool WaitForTimeOut()
		{
			return this.stopThread.WaitOne(this.operationInterval, false);
		}
		#endregion

		#region Private member functions
		private void InternalThreadFunction()
		{
			try
			{
				bool threadHasBeenAborted = false;
				while (true)
				{
					try
					{
						WorkerThreadFunction();
					}
					catch (ThreadAbortException)
					{
						threadHasBeenAborted = true;
						if (!this.throwOnAbort)
						{
							Thread.ResetAbort();
						}
						// Log thread abort
						break;
					}
					catch (Exception ex)
					{
						// Log the exception or raise an exception event
						OnThreadException(ex);
					}
					if (threadHasBeenAborted || this.stopThread.WaitOne(this.operationInterval, false))
						break;
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				this.threadStopped.Set();
			}
		}
		#endregion
	}
}
