using Ccr.Scraping.API.Infrastructure;

namespace TetrioStats.Core.Extensions
{
	public static class UrlBuilderExtensions
	{
		public static string BuildUrl(
			this UrlBuilder @this,
			bool includeTrailingForwardSlash = true)
		{
			var url = @this.Build().TrimEnd('/');

			if (!includeTrailingForwardSlash)
				return url;
			
			url += "/";

			return url;
		}
	}
}
