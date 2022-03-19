using System;
using Newtonsoft.Json;

namespace TetrioStats.Core.Data.Common.Users
{
	public sealed class Badge
	{
		/** The badge's id and icon file name  */
		public string BadgeID { get; }

		/** The badge's label */
		public string Label { get; }

		/** The Date the badge has been gotten by the user */
		public DateTimeOffset? AchievedTimeStamp { get; }
		
		/** The name of the user the badge belongs to */
		public string Username { get; }

		/** The badge's icon file url */
		[JsonIgnore]
		public string IconUrl
		{
			get => $"https://tetr.io/res/badges/{BadgeID}.png";
		}

		public Badge()
		{
			
		}
		//constructor(data: any)
		//{
		//	this.id = data.id;
		//	this.label = data.label;
		//	this.gottenAt = data.gottenAt;
		//	this.iconUrl = 'https://tetr.io/res/badges/' + this.id + '.png';
		//	this.user = data.user;
		//}
	}
}