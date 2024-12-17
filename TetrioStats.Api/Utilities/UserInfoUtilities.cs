using System.Text.RegularExpressions;

namespace TetrioStats.Api.Utilities
{
	public static class UserInfoUtilities
	{
		private static readonly Regex _userIdRegex = new(@"[a-f\d]{24}$");


		public static bool IsValidUserID(
			string userId)
		{
			return _userIdRegex.IsMatch(userId);
		}
	}
}