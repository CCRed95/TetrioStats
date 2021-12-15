using System;
using Ccr.Scraping.API.Infrastructure;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using TetrioStats.Api.Domain.Streams;
using TetrioStats.Api.Utilities;

namespace TetrioStats.Api
{
	internal static class TetrioRequestUrls
	{
		private const string coreApiDomain = "https://tetr.io/api/";
		private const string chApiDomain = "https://ch.tetr.io/api/";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="countryCode">
		/// Optional parameter, specifying the ISO 3166-1 country code to filter to.
		/// Leave <langword>null</langword> to fetch all leaderboards, not filtered by country.
		/// </param>
		/// <returns>
		/// 
		/// </returns>
		public static string FetchGlobalLeaderboardRankingsUrl(
			[CanBeNull] string countryCode = null)
		{
			var fragment = new DomainFragment(chApiDomain)
				.Builder
				.WithPath("users")
				.WithPath("lists")
				.WithPath("league")
				.WithPath("all");

			if (countryCode != null)
				fragment.WithParameter("country", countryCode);
			
			return fragment.Build();
		}
		
		public static string FetchStreamUrl(
			StreamID streamID)
		{
			return new DomainFragment(chApiDomain)
				.Builder
				.WithPath("streams")
				.WithPath($"{streamID}")
				.Build();
		}

		public static string FetchUserDataUrl(
			[NotNull] string userID)
		{
			userID.IsNotNull(nameof(userID));

			if (!UserInfoUtilities.IsValidUserID(userID))
				throw new ArgumentException(
					$"The value {userID.Quote()} is not valid for the argument {nameof(userID).SQuote()} " +
					$"because it is not a valid tetr.io User ID.", nameof(userID));

			return new DomainFragment(chApiDomain)
				.Builder
				.WithPath("users")
				.WithPath(userID)
				.Build();
		}

		public static string FetchRibbonUrl()
		{
			return new DomainFragment(coreApiDomain)
				.Builder
				.WithPath("server")
				.WithPath("ribbon")
				.Build();
		}

		public static string ResolveUserUrl(
			[NotNull] string name)
		{
			return new DomainFragment(coreApiDomain)
				.Builder
				.WithPath("users")
				.WithPath(name)
				.WithPath("resolve")
				.Build();
		}

		public static string UsersMeUrl()
		{
			return new DomainFragment(coreApiDomain)
				.Builder
				.WithPath("users")
				.WithPath("me")
				.Build();
		}

		public static string FetchDirectMessagesUrl()
		{
			return new DomainFragment(coreApiDomain)
				.Builder
				.WithPath("dms")
				.Build();
		}

		public static string RelationshipsFriendUrl()
		{
			return new DomainFragment(coreApiDomain)
				.Builder
				.WithPath("relationships")
				.WithPath("friend")
				.Build();
		}
		public static string RelationshipsUnfriendUrl()
		{
			return new DomainFragment(coreApiDomain)
				.Builder
				.WithPath("relationships")
				.WithPath("remove")
				.Build();
		}
	}
}

/*
	public static string FetchUserLatest40LinesGamesUrl(
		[NotNull] string userID)
	{
		userID.IsNotNull(nameof(userID));

		if (!UserInfoUtilities.IsValidUserID(userID))
			throw new ArgumentException(
				$"The value {userID.Quote()} is not valid for the argument {nameof(userID).SQuote()} " +
				$"because it is not a valid tetr.io User ID.", nameof(userID));

		return new DomainFragment(domain)
			.Builder
			.WithPath("streams")
			.WithPath($"any_userrecent_{userID}")
			.Build();
	}*/
