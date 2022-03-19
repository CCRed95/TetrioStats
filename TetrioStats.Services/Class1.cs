using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Ccr.Colorization.Mappings;
using TetrioStats.Api;
using TetrioStats.Api.Domain.Json.Streams;
using TetrioStats.Api.Domain.Streams;
using TetrioStats.Api.Extensions;
using TetrioStats.Core.Data.Common.Users;
using TetrioStats.Data.Context;
using TetrioStats.Data.Enums;
using static Ccr.Terminal.ExtendedConsole;

namespace TetrioStats.Services
{
	public class TrackedUser
	{
		public UserInfo UserInfo { get; }

		public DateTime DataLastUpdatedUtc { get; set; }


		public TrackedUser(
			UserInfo userInfo)
		{
			UserInfo = userInfo;
			DataLastUpdatedUtc = DateTime.MinValue;
		}

		public List<GameRecordInfo> SavedReplays { get; } = new List<GameRecordInfo>();
	}


	public class TaskSchedulerEngine
	{
		private static readonly TetrioApiClient _client = new TetrioApiClient();
		private static DateTime _timeCacheExpires;
		private static DateTime _time40LCacheExpires;
		private static readonly LocalCoreContext _coreContext = new LocalCoreContext();

		private Thread _workerThread;

		private readonly List<TrackedUser> _trackedUsers
			= new List<TrackedUser>();


		public TaskSchedulerEngine()
		{
			//_workerThread = new Thread(ExecutionWorker);
			//_workerThread.Start();
		}


		public void TrackUser(
			UserInfo userInfo)
		{
			if (IsTrackingUser(userInfo))
				return;

			var trackedUser = new TrackedUser(userInfo);

			_trackedUsers.Add(trackedUser);
		}

		public void StopTrackingUser(
			UserInfo userInfo)
		{
			if (!IsTrackingUser(userInfo))
				return;

			var trackedUser = _trackedUsers
				.Single(t => t.UserInfo.UserId == userInfo.UserId);

			_trackedUsers.Remove(trackedUser);
		}

		public bool IsTrackingUser(
			UserInfo userInfo)
		{
			return _trackedUsers
				.Any(t => t.UserInfo.UserId == userInfo.UserId);
		}


		internal void ExecutionWorker()
		{
			foreach (var trackedUser in _trackedUsers)
			{
				if (DateTime.UtcNow - trackedUser.DataLastUpdatedUtc < TimeSpan.FromMinutes(3))
					return;

				var streamID = StreamID.UserRecent(
					StreamType.Any, trackedUser.UserInfo.UserId);

				var stream = _client.FetchStreamAsync(streamID)
					.GetAwaiter()
					.GetResult();

				foreach (var gameRecord in stream.Content)
				{
					if (trackedUser.SavedReplays.All(
						t => t.ReplayID != gameRecord.ReplayID))
					{
						XConsole
							.WriteLine(
								$"Relay Found: {gameRecord.ReplayID}",
								Swatch.Amber);

						var valueColor = gameRecord.EndContext.GameType == GameType.Single40Lines
							? Color.Yellow
							: Color.IndianRed;

						XConsole
						.Write("   Kind:     ", valueColor)
						.WriteLine(gameRecord.EndContext.GameType.GetDescription(), Color.MediumTurquoise)
						.Write("   Username: ", valueColor)
						.WriteLine(gameRecord.UserInfo.Username, Color.MediumTurquoise)
						.Write("   Score:    ", valueColor)
						.WriteLine(gameRecord.EndContext.Score, Color.MediumTurquoise)
						.Write("   Lines:    ", valueColor)
						.WriteLine(gameRecord.EndContext.LinesCleared,
							Color.MediumTurquoise)
						.WriteLine();

						trackedUser.SavedReplays.Add(gameRecord);
						trackedUser.DataLastUpdatedUtc = DateTime.UtcNow;
					}
					else
					{

					}
				}
			}
			Thread.Sleep(500);
		}
	}

}
