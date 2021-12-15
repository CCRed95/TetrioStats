using System.ComponentModel;

namespace TetrioStats.Api.Domain.Users
{  
	/**
	 * A user's role in the game. It can be either:
	 *   - anon   - an anonymous player
	 *   - user   - a regular player
	 *   - bot    - a bot 
	 *   - mod    - a tetr.io moderator
	 *   - admin  - a tetr.io administrator  
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
