using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Ccr.Scraping.API.Web;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using TetrioStats.Api.Extensions;
using TetrioStats.Core.Settings;
using static Ccr.Terminal.ExtendedConsole;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api
{
	public class TetrioHttpClient : IHttpClient, IDisposable
	{
		private const string userAgent =
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
			"AppleWebKit/537.36 (KHTML, like Gecko) " +
			"Chrome/91.0.4472.124 Safari/537.36";

		private static readonly string _userBearerToken;

		private static readonly string _cacheSessionID;
		private readonly HttpClient _client;


		static TetrioHttpClient()
		{
			var sessionId = Math.Floor(
				new Random().NextDouble() * int.MaxValue);

			_cacheSessionID = $"SESS-${sessionId}";

			_userBearerToken = TetrioStatsSettings.I.AuthorizationBearerKey;

			if (_userBearerToken.IsNullOrEmptyEx())
			{
				XConsole
					.WriteLine()
					.SetBold()
					.WriteLine("Authorization Bearer Token is not Configured!", Color.IndianRed)
					.WriteLine("Enter your tetr.io Authorization Bearer Token: ", Color.MediumVioletRed)
					.SetNormalIntensity();

				var authBearerToken = XConsole.ReadLine().Trim();

				var userMeResponse = new TetrioApiClient()
					.Users
					.FetchUserMeAsync()
					.GetAwaiter()
					.GetResult();

				if (!userMeResponse.WasSuccessful)
				{
					_userBearerToken = null;

					throw new InvalidOperationException(
						"Invalid Authorization Token.");
				}
/*
				if (userMeResponse.Data.Role != UserRole.Bot)
				{
					_userBearerToken = null;

					throw new InvalidOperationException(
						"Client is not a bot. Apply for a bot account by messaging osk#9999 on Discord.");
				}

				TetrioStatsSettings.I.AuthorizationBearerKey = authBearerToken;

				XConsole
					.WriteLine()
					.Write("Username: ", Color.MediumTurquoise)
					.WriteLine(userMeResponse.Data.Username, Color.MediumPurple)
					.Write("Role:     ", Color.MediumTurquoise)
					.WriteLine(userMeResponse.Data.Role.ToString("G"), Color.MediumPurple)
					.WriteLine()
					.WriteLine("Bearer Token Saved!", Color.MediumSpringGreen);
*/
			}
		}

		public TetrioHttpClient()
		{
			_client = new();
			
			_client
				.WithDefaultRequestHeader("X-Session-ID", _cacheSessionID)
				.WithDefaultRequestHeader("user-agent", userAgent);

			//.WithDefaultRequestHeader("accept", jsonAcceptProtocol)
			//.WithDefaultRequestHeader("Authorization", $"Bearer {authBearerKey}");
		}


		public async Task<string> GetContentAsync(string address)
		{
			var stringAsync = await _client.GetStringAsync(address);
			return stringAsync;
		}

		public async Task<HttpResponseMessage> SendAsync(
			[NotNull] HttpRequestMessage message)
		{
			message.IsNotNull(nameof(message));
			var httpResponseMessage = await _client.SendAsync(message);
			return httpResponseMessage;
		}


		public void Dispose()
		{
			_client?.Dispose();
		}
	}

	public static class HttpRequestMessageExtensions
	{
		private const string theoryPackAcceptProtocol = "application/vnd.osk.theorypack";

		private const string messagePackAcceptProtocol = "application/x-msgpack";

		private const string jsonAcceptProtocol = "application/json";


		public static HttpRequestMessage WithAuth(this HttpRequestMessage @this)
		{
			var authBearerKey = TetrioStatsSettings.I.AuthorizationBearerKey;
			@this.Headers.Add("Authorization", $"Bearer {authBearerKey}");
			return @this;
		}

		public static HttpRequestMessage WithTheoryPackAccept(this HttpRequestMessage @this)
		{
			@this.Headers.Add("accept", theoryPackAcceptProtocol);
			return @this;
		}

		public static HttpRequestMessage WithMessagePackAccept(this HttpRequestMessage @this)
		{
			@this.Headers.Add("accept", messagePackAcceptProtocol);
			return @this;
		}

		public static HttpRequestMessage WithJsonAccept(this HttpRequestMessage @this)
		{
			@this.Headers.Add("accept", jsonAcceptProtocol);
			return @this;
		}
	}
}