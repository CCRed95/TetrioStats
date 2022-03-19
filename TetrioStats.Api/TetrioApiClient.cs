using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TetrioStats.Api.Domain.Json.Leaderboards;
using TetrioStats.Api.Domain.Json.Streams;
using TetrioStats.Api.Domain.Json.UserMe;
using TetrioStats.Api.Domain.Json.Users;
using TetrioStats.Api.Domain.Streams;
using TetrioStats.Api.Utilities;
using TetrioStats.Data.Context;
using TetrioStats.Data.Domain;

namespace TetrioStats.Api
{
	public class TetrioApiClient
		: IDisposable
	{
		private static readonly IList<User> _cachedUsers = new List<User>();

		private readonly CoreContext _coreContext;
		private readonly LocalCoreContext _localContext;

		[JsonIgnore]
		private static readonly JsonSerializerSettings _converterSettings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			NullValueHandling = NullValueHandling.Ignore,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};


		public TetrioApiClient()
		{
			_coreContext = new CoreContext();
			_localContext = new LocalCoreContext();
		}


		internal static async Task<bool> ValidateBearerToken(
			string token)
		{
			var userMe = await FetchUserMeAsync(token);

			return true;
		}

		public User FetchUser(string userIdOrUsername)
		{
			return ResolveUser(userIdOrUsername);
			
			if (UserInfoUtilities.IsValidUserID(userIdOrUsername))
			{
				var cachedUser = _cachedUsers.FirstOrDefault(
					t => t.TetrioUserID == userIdOrUsername);

				if (cachedUser != null)
					return cachedUser;

				cachedUser = _localContext.Users.FirstOrDefault(
					t => t.TetrioUserID == userIdOrUsername);

				if (cachedUser != null)
				{
					_cachedUsers.Add(cachedUser);
					return cachedUser;
				}

				var user = ResolveUser(userIdOrUsername);

				_localContext.Users.Add(user);
				_localContext.SaveChanges();

				_cachedUsers.Add(user);
				return user;
			}
			else
			{
				var cachedUser = _cachedUsers.FirstOrDefault(
					t => t.Username == userIdOrUsername);

				if (cachedUser != null)
					return cachedUser;

				cachedUser = _localContext.Users.FirstOrDefault(
					t => t.Username == userIdOrUsername);

				if (cachedUser != null)
				{
					_cachedUsers.Add(cachedUser);
					return cachedUser;
				}

				var user = ResolveUser(userIdOrUsername);

				_localContext.Users.Add(user);
				_localContext.SaveChanges();

				_cachedUsers.Add(user);
				return user;
			}
		}
		

		public async Task<UserDataResponse> FetchUserDataAsync(
			string userID)
		{
			var url = TetrioRequestUrls.FetchUserDataUrl(userID);

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = await httpClient.SendAsync(request);

			var jsonContent = await response.Content.ReadAsStringAsync();

			var userDataResponse = JsonConvert
				.DeserializeObject<UserDataResponse>(
					jsonContent);

			return userDataResponse;
		}
		
		public UserDataResponse FetchUserData(
			string userID)
		{
			var url = TetrioRequestUrls.FetchUserDataUrl(userID);

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);
			
			var response = httpClient.SendAsync(request)
				.GetAwaiter()
				.GetResult();

			var jsonContent = response.Content
				.ReadAsStringAsync()
				.GetAwaiter()
				.GetResult();

			var userDataResponse = JsonConvert
				.DeserializeObject<UserDataResponse>(
					jsonContent);

			return userDataResponse;
		}

		public User ResolveUser(
			string username)
		{
			var url = TetrioRequestUrls.ResolveUserUrl(username);

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = httpClient.SendAsync(request)
				.GetAwaiter()
				.GetResult();

			var jsonContent = response.Content
				.ReadAsStringAsync()
				.GetAwaiter()
				.GetResult();

			var resolvedUserIDResponse = JsonConvert
				.DeserializeObject<ResolvedUserID>(
					jsonContent);

			if (resolvedUserIDResponse == null
				|| !resolvedUserIDResponse.WasSuccessful)
				return null;

			return new User(resolvedUserIDResponse.UserID, username);
		}

		public async Task<User> ResolveUserAsync(
			string username)
		{
			var url = TetrioRequestUrls.ResolveUserUrl(username);

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = await httpClient.SendAsync(request);

			var jsonContent = await response.Content.ReadAsStringAsync();

			var resolvedUserIDResponse = JsonConvert
				.DeserializeObject<ResolvedUserID>(jsonContent);

			if (resolvedUserIDResponse == null
				|| !resolvedUserIDResponse.WasSuccessful)
				return null;

			return new User(resolvedUserIDResponse.UserID, username);
		}



		internal static async Task<UserMeResponse> FetchUserMeAsync(
			string token)
		{
			var url = TetrioRequestUrls.UsersMeUrl();

			using var httpClient = new TetrioHttpClient(token);

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = await httpClient.SendAsync(request);

			var jsonContent = await response.Content.ReadAsStringAsync();

			var userMeResponse = JsonConvert
				.DeserializeObject< UserMeResponse>(jsonContent);

			return userMeResponse;
		}


		//public async Task<User> ResolveUserAsync(
		//	string username)
		//{
		//	var url = TetrioRequestUrls.ResolveUserUrl(username);

		//	using var httpClient = new TetrioHttpClient();

		//	using var request = new HttpRequestMessage(
		//		new HttpMethod("GET"), url);

		//	var response = await httpClient.SendAsync(request);

		//	var jsonContent = await response.Content.ReadAsStringAsync();

		//	var resolvedUserIDResponse = JsonConvert
		//		.DeserializeObject<ResolvedUserID>(jsonContent);

		//	if (resolvedUserIDResponse == null
		//		|| !resolvedUserIDResponse.WasSuccessful)
		//		return null;

		//	return new User(resolvedUserIDResponse.UserID, username);
		//}


		/// <summary>
		/// Asynchronously Fetches a complete, full export of the Tetra League Leaderboard rankings list
		/// for all users, globally across the platform.
		/// </summary>
		/// <remarks>
		/// GET /users/lists/league/all
		/// Cache time: 1 hour
		/// </remarks>
		/// <returns>
		/// Returns a full export of the Tetra League Leaderboard rankings list for all users, globally
		/// across the platform. 
		/// </returns>
		public static async Task<IEnumerable<UserLeaderboardRanking>> FetchGlobalLeaderboardRankingsAsync()
		{
			var url = TetrioRequestUrls.FetchGlobalLeaderboardRankingsUrl();

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = await httpClient.SendAsync(request);

			var jsonContent = await response.Content.ReadAsStringAsync();

			var globalLeaderboardRankingResponse = JsonConvert
				.DeserializeObject<GlobalLeaderboardRankingResponse>(jsonContent);

			return globalLeaderboardRankingResponse?.Response?.Payload;
		}

		/// <summary>
		/// Fetches a complete, full export of the Tetra League Leaderboard rankings list for all users,
		/// globally across the platform.
		/// </summary>
		/// <remarks>
		/// GET /users/lists/league/all
		/// Cache time: 1 hour
		/// </remarks>
		/// <returns>
		/// Returns a full export of the Tetra League Leaderboard rankings list for all users, globally
		/// across the platform. 
		/// </returns>
		public static IEnumerable<UserLeaderboardRanking> FetchGlobalLeaderboardRankings()
		{
			var url = TetrioRequestUrls.FetchGlobalLeaderboardRankingsUrl();

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);
			
			var response = httpClient.SendAsync(request)
				.GetAwaiter()
				.GetResult();

			var jsonContent = response.Content
				.ReadAsStringAsync()
				.GetAwaiter()
				.GetResult();

			var globalLeaderboardRankingResponse = JsonConvert
				.DeserializeObject<GlobalLeaderboardRankingResponse>(
					jsonContent, _converterSettings);

			return globalLeaderboardRankingResponse?.Response?.Payload;
		}

		
		public GameRecordsResponse FetchStream(
			StreamID streamID)
		{
			var url = TetrioRequestUrls.FetchStreamUrl(streamID);

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = httpClient.SendAsync(request)
				.GetAwaiter()
				.GetResult();

			var jsonContent = response.Content
				.ReadAsStringAsync()
				.GetAwaiter()
				.GetResult();

			var gameRecordsResponse = GameRecordsResponse.FromJson(jsonContent);

			return gameRecordsResponse;
		}

		public async Task<GameRecordsResponse> FetchStreamAsync(
			StreamID streamID)
		{
			var url = TetrioRequestUrls.FetchStreamUrl(streamID);

			using var httpClient = new TetrioHttpClient();

			using var request = new HttpRequestMessage(
				new HttpMethod("GET"), url);

			var response = await httpClient.SendAsync(request);

			var jsonContent = await response.Content.ReadAsStringAsync();

			var gameRecordsResponse = GameRecordsResponse.FromJson(jsonContent);

			return gameRecordsResponse;
		}


		public void Dispose()
		{
			_coreContext?.Dispose();
			_localContext?.Dispose();
		}
	}
}


//public UserDataResponse FetchRankingDistribution()
//{
//	//var queryBuilder = TetrioApiClientQueryBuilder
//	//	.Builder
//	//	.ForUsername(userID);

//	var url = "";

//	using var httpClient = new TetrioHttpClient();

//	using var request = new HttpRequestMessage(
//		new HttpMethod("GET"), url);

//	var response = httpClient.SendAsync(request)
//		.GetAwaiter()
//		.GetResult();

//	var jsonContent = response.Content
//		.ReadAsStringAsync()
//		.GetAwaiter()
//		.GetResult();

//	var userDataResponse = JsonConvert
//		.DeserializeObject<UserDataResponse>(
//		jsonContent);

//	return userDataResponse;
//}



//public GameRecordsResponse FetchUserLatest40LinesGames(
//	string userID)
//{
//	if (!UserInfoUtilities.IsValidUserID(userID))
//	{
//		var userData = FetchUserData(userID);
//		userID = userData.Response.Payload.UserID;
//	}

//	//var queryBuilder = TetrioApiClientQueryBuilder
//	//	.Builder
//	//	.ForUsername(userID);


//	var url = $"https://ch.tetr.io/api/streams/any_userrecent_{userID}";

//	//queryBuilder.BuildRequestUrl(RequestBuilder);

//	using var httpClient = new TetrioHttpClient();

//	using var request = new HttpRequestMessage(
//		new HttpMethod("GET"), url);

//	var response = httpClient.SendAsync(request)
//		.GetAwaiter()
//		.GetResult();

//	var jsonContent = response.Content
//		.ReadAsStringAsync()
//		.GetAwaiter()
//		.GetResult();

//	var gameRecordsResponse = GameRecordsResponse.FromJson(jsonContent);

//	return gameRecordsResponse;
//}