using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ccr.Scraping.API.Web;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using TetrioStats.Api.Extensions;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api
{
	public class TetrioHttpClient
		: IHttpClient,
			IDisposable
	{
		private const string userAgent =
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
			"AppleWebKit/537.36 (KHTML, like Gecko) " +
			"Chrome/91.0.4472.124 Safari/537.36";

		private const string acceptToken =
			"text/html,application/xhtml+xml,application/xml;" +
			"q=0.9,image/avif,image/webp,image/apng,*/*;" +
			"q=0.8,application/signed-exchange;v=b3;q=0.9";

		private const string userBearerToken =
			"ENTER BEARER TOKEN HERE";



		private static readonly string _cacheSessionID;
		private readonly HttpClient _client;


		static TetrioHttpClient()
		{
			var sessionId = Math.Floor(
				new Random().NextDouble() * int.MaxValue);

			_cacheSessionID = $"SESS-${sessionId}";
		}

		public TetrioHttpClient()
		{
			_client = new HttpClient();

			_client
				.WithDefaultRequestHeader("X-Session-ID", _cacheSessionID)
				.WithDefaultRequestHeader("authority", "ch.tetr.io")
				.WithDefaultRequestHeader("pragma", "no-cache")
				.WithDefaultRequestHeader("cache-control", "no-cache")
				.WithDefaultRequestHeader("upgrade-insecure-requests", "1")
				.WithDefaultRequestHeader("user-agent", userAgent)
				.WithDefaultRequestHeader("accept", acceptToken)
				.WithDefaultRequestHeader("sec-gpc", "1")
				.WithDefaultRequestHeader("sec-fetch-site", "none")
				.WithDefaultRequestHeader("sec-fetch-mode", "navigate")
				.WithDefaultRequestHeader("sec-fetch-user", "?1")
				.WithDefaultRequestHeader("sec-fetch-dest", "document")
				.WithDefaultRequestHeader("accept-language", "en-US,en;q=0.9")
				.WithDefaultRequestHeader("cookie", "ceriad_exempt=1")
				.WithDefaultRequestHeader("Authorization", $"Bearer {userBearerToken}");
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
}