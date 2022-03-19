using System;

namespace TetrioStats.Core.Scheduler
{
	public abstract class ExecutionTask
	{
		protected bool _isRunning;


		public DateTime TimeRequested { get; }

		public TimeSpan RequestedTimeToComplete { get; }


		protected ExecutionTask(
			TimeSpan requestedTimeToComplete)
		{
			TimeRequested = DateTime.UtcNow;
			RequestedTimeToComplete = requestedTimeToComplete;
		}
	}
}