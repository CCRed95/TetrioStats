using System;
using System.Collections.Generic;
using Ccr.Std.Core.Numerics.Ranges;
using TetrioStats.Api;
using TetrioStats.Api.Domain.Rankings;
using TetrioStats.Data.Domain;

namespace TetrioStats.Terminal.Infrastructure
{
	public class RankGradeThreshold
	{
		private readonly UserRankGrade _userRankGrade;
		private readonly DoubleRange _trRange;


		public UserRankGrade RankGrade
		{
			get => _userRankGrade;
		}

		public double MinimumTR
		{
			get => _trRange.Minimum;
		}

		public double MaximumTR
		{
			get => _trRange.Maximum;
		}
		
		public double RankPercentile
		{
			get => _userRankGrade.RankPercentile;
		}


		public RankGradeThreshold(
			UserRankGrade userRankGrade,
			DoubleRange trRange)
		{
			_userRankGrade = userRankGrade;
			_trRange = trRange;
		}
	}

	public class RuntimeData
	{ 
		private readonly RankGradeThreshold[] _cachedRankGradeThresholds;


		public IReadOnlyList<RankGradeThreshold> RankGradeThresholds
		{
			get => _cachedRankGradeThresholds;
		}



	}
	public class TaskSchedulerEngine
	{

	}


	public abstract class ExecutionTask
	{
		public DateTime TimeRequested { get; }

		public TimeSpan RequestedTimeToComplete { get; }


		protected ExecutionTask(
			TimeSpan requestedTimeToComplete)
		{
			TimeRequested = DateTime.UtcNow;
			RequestedTimeToComplete = requestedTimeToComplete;
		}
	}

	public abstract class RepeatingExecutionTask
		: ExecutionTask
	{
		public TimeSpan RequestedRepeatPeriod { get; }


		protected RepeatingExecutionTask(
			TimeSpan requestedTimeToComplete,
			TimeSpan requestedRepeatPeriod)
				: base(requestedTimeToComplete)
		{
			RequestedRepeatPeriod = requestedRepeatPeriod;
		}
	}

	public class TetraLeagueUserDataProcessor
	{

	}

	public class QueryTLGradeRankingThresholdsTask
		: RepeatingExecutionTask,
			IDisposable
	{
		private static readonly TetrioApiClient _client = new TetrioApiClient();

		private readonly User _user;


		public QueryTLGradeRankingThresholdsTask(
			User user,
			TimeSpan requestedTimeToComplete,
			TimeSpan requestedRepeatPeriod) : base(
				requestedTimeToComplete,
				requestedRepeatPeriod)
		{
			_user = user;
		}


		public void Execute()
		{

		}

		public void Dispose()
		{
			_client?.Dispose();
		}
	}


	public class TetraLeagueUserDataQuery
	{
		private readonly DateTime _timeRequestedUtc;
		private IReadOnlyDictionary<UserRankGrade, DoubleRange> _trRankingsThresholds;

		 
		public TetraLeagueUserDataQuery()
		{
			_timeRequestedUtc = DateTime.UtcNow;
		}


		public void Execute()
		{
			_trRankingsThresholds = UserRankExtensions.CalculateRankGradeTRThresholds();
		}
	}
}
