using System.ComponentModel;

namespace TetrioStats.Core.Data.Common.Users
{
	/**
	 * A user's role in the game. It can be either:
	 *   - anon   - an anonymous player
	 *   - user   - a regular player
	 *   - bot    - a Tetrio approved bot 
	 *   - mod    - a Tetrio moderator
	 *   - admin  - a Tetrio administrator  
	**/
	public enum UserRole
	{
		[Description("anon")]
		Anonymous,

		[Description("user")]
		User,

		[Description("bot")]
		Bot,

		[Description("mod")]
		Moderator,

		[Description("admin")]
		Administrator,
	}
}
