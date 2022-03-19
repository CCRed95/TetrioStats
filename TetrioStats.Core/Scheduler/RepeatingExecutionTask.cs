using System;
using System.Threading;

namespace TetrioStats.Core.Scheduler
{
	public abstract class RepeatingExecutionTask
		: ExecutionTask
	{
		public TimeSpan RequestedRepeatPeriod { get; }

		private Thread _workerThread;


		protected RepeatingExecutionTask(
			TimeSpan requestedTimeToComplete,
			TimeSpan requestedRepeatPeriod)
			: base(requestedTimeToComplete)
		{
			RequestedRepeatPeriod = requestedRepeatPeriod;
			_workerThread = new Thread(ExecutionWorker);
		}


		internal abstract void ExecutionWorker();


		public void Start()
		{
			if (_isRunning)
				return;

			_workerThread = new Thread(ExecutionWorker);
			_workerThread.Start();

			_isRunning = true;
		}

		public void Stop()
		{
			if (_isRunning)
			{
				var joinResult = _workerThread.Join(2000);
				_isRunning = false;
			}
		}
	}
}
