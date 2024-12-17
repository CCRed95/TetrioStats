using TetrioStats.Api.Domain.Users.UserRecords;
using TetrioStats.Data.Enums;

namespace TetrioStats.Api.Tests;

public abstract class TetrioApiTestsBase
{
	protected static readonly TetrioApiClient client = new();

	public async Task CalculateUserStatsAsync()
	{
		//  // Get the big tetra league data
		//  let tlDataPromise = getData("users/lists/league/all")
		//  let tStart = Date.now();
		var tStart = DateTime.Now;

		//  if (!usernameNode) {
		//    return;
		//  }

		//  addTrait(
		//    "TETR.IO VERGE IS LOADING. . .",
		//    "This may take up to 5 seconds depending on your internet connection and TETR.IO API response times.",
		//    "#b3f4b6",
		//    "loading");

		 const string username = "ccred95";//= (usernameNode.textContent).toLowerCase();
																			 //  //console.log(user)


		//  // Get the user's data
		//  let mainUserData = await getData(`users/${user}`);
		var mainUserData = await client.Users.FetchUserInfoAsync(username);

		//  // Get the user's tetra league history
		//  let mainUserDataHistoryPromise = getData("streams/league_userrecent_" + mainUserData.data.user._id);
		//  let mainUserDataHistory = await mainUserDataHistoryPromise;

		var mainUserDataHistory = await client.Records
			.FetchRecordsAsync<PersonalLeagueRecordResponse, LeagueRecord>(
				username, GameMode.TetraLeague, Leaderboard.Recent);

		//  // Get the tetra league leaderboard, we call this now because it takes a good while to fetch sometimes
		//  let tlData = await tlDataPromise;

		//  // This is the better version of tlData
		//  let tlUsers = tlData.data.users;

		//  if (!tlData.success || !mainUserData.success || !mainUserDataHistory.data.records.length || user === "sthantom") {
		//    console.warn("User data is either non-existent or could not be fetched.")
		//    document.getElementById("loading").textContent = "USER TETRA LEAGUE DATA CANNOT BE FOUND";
		//    document.getElementById("loading").title = "Party's over. . . This can happen if the user hasn't played any tetra league games.";
		//    return;
		//  }

		//  const mainUserMatchHistory = [];
		//  const mainUserOpponents = [];
		var mainUserMatchHistory = new List<bool>();
		var mainUserOpponents = new List<string>();

		foreach (var record in mainUserDataHistory.Data.Entries)
		{
			//if (record.User.)
			//{

			//}
		}

		//  for (let i = 0; i < mainUserDataHistory.data.records.length; i++) {
		//    if (mainUserDataHistory.data.records[i].endcontext[0].username === user) {
		//      mainUserMatchHistory.push(true);
		//      mainUserOpponents.push(mainUserDataHistory.data.records[i].endcontext[1].username)
		//    } else {
		//      mainUserMatchHistory.push(false);
		//      mainUserOpponents.push(mainUserDataHistory.data.records[i].endcontext[0].username);
		//    }
		//  }

		//  // console.log(mainUserMatchHistory);
		//  // console.log(mainUserOpponents);

		//  // Contribution by Sup3rFire, thanks!
		//  const mainUserMatchHistoryData = await Promise.all(mainUserOpponents.map((user) => getData("users/" + user)));


		//  // console.log(mainUserMatchHistoryData);

		//  // console.log("User data fetched in " + (Date.now() - tStart) / 1000 + " seconds!")

		//  tStart = Date.now();


		//  const pps = [];
		//  const apm = [];
		//  const vs = [];
		//  const tr = [];

		//  // -0.4055 is the secret formula ;)
		//  const skillGroupRange = Math.round((-0.4055 * ((mainUserData.data.user.league.percentile) ** 2 - mainUserData.data.user.league.percentile)) * tlUsers.length + 10);

		//  const lowerSkillGroup = (mainUserData.data.user.league.standing - 1 + skillGroupRange) < tlUsers.length ? (mainUserData.data.user.league.standing - 1 + skillGroupRange) : tlUsers.length;
		//  const upperSkillGroup = (mainUserData.data.user.league.standing - 1 - skillGroupRange) > 0 ? (mainUserData.data.user.league.standing - 1 - skillGroupRange) : 0;

		//  // console.log("Sample size: " + (lowerSkillGroup - upperSkillGroup) + " players");
		//  // console.log(upperSkillGroup)
		//  // console.log(lowerSkillGroup)
		//  // console.log(skillGroupRange);

		//  // console.log(mainUserData.data.user.league.percentile);
		//  for (var i = upperSkillGroup; i < lowerSkillGroup; i++) {
		//    pps.push(tlUsers[i].league.pps);
		//    apm.push(tlUsers[i].league.apm);
		//    vs.push(tlUsers[i].league.vs);
		//    tr.push(tlUsers[i].league.rating);
		//  }

		//  pps.sort(function (a, b) {
		//    return a - b
		//  });
		//  apm.sort(function (a, b) {
		//    return a - b
		//  });
		//  vs.sort(function (a, b) {
		//    return a - b
		//  });
		//  tr.sort(function (a, b) {
		//    return a - b
		//  });

		//  const userPps = mainUserData.data.user.league.pps;
		//  const userApm = mainUserData.data.user.league.apm;
		//  const userVs = mainUserData.data.user.league.vs;
		//  //var userTr = mainUserData.data.user.league.rating;
		//  const userApp = userApm / 60 / userPps;
		//  const userVsapm = userVs / userApm;
		//  const userDsps = userVs / 100 - userApm / 60;
		//  const userDspp = userDsps / userPps;
		//  const userGe = 2 * userApp * userDsps / userPps;

		//  // Thanks to sheetbot's creators for the formulas below!! I can't thank you enough <3

		//  const srarea = (userApm * 0) + (userPps * 135) + (userVs * 0) + (userApp * 290) + (userDsps * 0) + (userDspp * 700) + (userGe * 0);
		//  const statrank = 11.2 * Math.atan((srarea - 93) / 130) + 1;


		//  // console.log("Current user: " + mainUserData.data.user.username);


		//  let goodMood = 0;
		//  let badMood = 0;
		//  let overConfident = 0;
		//  let vengeance = 0;
		//  let wins = 0;
		//  let losses = 0;

		//  const precision = 3;

		//  for (var i = 0; i < mainUserDataHistory.data.records.length - 1; i++) {
		//    // If the user won the match and the previous match, increment good mood score
		//    if ((mainUserDataHistory.data.records[i].endcontext[0].username === user) && (mainUserDataHistory.data.records[i + 1].endcontext[0].username === user)) {
		//      goodMood++;
		//      wins++;
		//      // If the user lost the match and the previous match, increment bad mood score
		//    } else if (!(mainUserDataHistory.data.records[i].endcontext[0].username === user) && !(mainUserDataHistory.data.records[i + 1].endcontext[0].username === user)) {
		//      badMood++;
		//      losses++;
		//      // If the user lost the match but won the previous match, increment over confident score
		//    } else if (!(mainUserDataHistory.data.records[i].endcontext[0].username === user) && (mainUserDataHistory.data.records[i + 1].endcontext[0].username === user)) {
		//      overConfident++;
		//      losses++;
		//      // If the user won the match but lost the previous match, increment vengeance score
		//    } else if ((mainUserDataHistory.data.records[i].endcontext[0].username === user) && !(mainUserDataHistory.data.records[i + 1].endcontext[0].username === user)) {
		//      vengeance++;
		//      wins++;
		//    } else {
		//      // console.log("oh nyooo")
		//    }
		//  }

		//  const goodMoodScore = goodMood / (goodMood + overConfident);
		//  const badMoodScore = badMood / (badMood + vengeance);
		//  const overConfidentScore = overConfident / (overConfident + goodMood);
		//  const vengeanceScore = vengeance / (vengeance + badMood);

		//  // Remove loading tag
		//  document.getElementById("loading").remove();

		//  // Custom tags
		//  if (mainUserData.data.user._id === "643fe1195290461b4c184095") {
		//    addTrait("GOLDEN RATIO", "A painter meets their subject eye to eye.", "#F3F4B3");

		//  } else if (mainUserData.data.user._id === "62a78e80dea07bce363aa343") {
		//    addTrait("GIRL!", "Girl!", "#F3F4B3");
		//  } else if (mainUserData.data.user._id === "636865c4f9ffbe7fac583f41") {
		//    if (Math.random() > 0.5) {
		//      addTrait("GIRL!", "Girl!", "#F3F4B3");
		//    }
		//  } else if (mainUserData.data.user._id === "5ee9bfa12738825f8bf29b01") {
		//    addTrait("ALMOST KYRA", "We are not the same.", "#F3F4B3");
		//  } else if (mainUserData.data.user._id === "61716f3de09631548041968d") {
		//    addTrait("BAD RNG", "This user has abysmally low RNG.", "#f4b6b3");
		//  } else if (mainUserData.data.user._id === "60638e856aab4f0c1a1e8d03") {
		//    addTrait("UMBREON", "This user mains misdrop cannon.", "f4b6b3");
		//  } else if (mainUserData.data.user._id === "61b544b1e9c8d503799ba8e7") {
		//    addTrait("MOON", "This user combos a lot.", "F3F4B3");
		//  }


		//  if (mainUserDataHistory.data.records[0].endcontext[0].username === user) {


		//    if (goodMoodScore > .75) {
		//      // console.log("Good mood: This user tends to continue winning if they won their previous match (Winrate " + (goodMoodScore * 100).toFixed(precision) + "%)");
		//      addTrait("GOOD MOOD", "This user tends to continue winning if they won their previous match (Winrate " + (goodMoodScore * 100).toFixed(precision) + "%)", "#b6b3f4")
		//    } else if (overConfidentScore > .75) {
		//      // console.log("Over confident: This user tends to lose if they won their previous match. (Winrate " + overConfidentScore * 100 + "%)");
		//      addTrait("OVER CONFIDENT", "This user tends to lose if they won their previous match. (Winrate " + (100 - overConfidentScore * 100).toFixed(precision) + "%)", 1, "#f4b6b3")
		//    } else {
		//      // console.log("Level headed: This user's winrate is not heavily affected if they won their previous match. (Winrate " + ((goodMoodScore) * 100).toFixed(precision) + "%)");
		//      addTrait("LEVEL HEADED", "This user's winrate is not heavily affected if they won their previous match. (Winrate " + ((goodMoodScore) * 100).toFixed(precision) + "%)", "#b6b3f4")
		//    }
		//  } else {
		//    if (badMoodScore > .75) {
		//      // console.log("Bad mood: This user tends to continue losing if they lost their previous match. (Winrate " + (badMoodScore * 100).toFixed(precision) + "%)");
		//      addTrait("BAD MOOD", "This user tends to continue losing if they lost their previous match. (Winrate " + (100 - badMoodScore * 100).toFixed(precision) + "%)", "#f4b6b3")

		//    } else if (vengeanceScore > .75) {
		//      // console.log("Vengeance: This user tends to win if they lost their previous match. (Winrate " + (vengeanceScore * 100).toFixed(precision) + "%)");
		//      addTrait("VENGEANCE", "This user tends to win if they lost their previous match. (Winrate " + (vengeanceScore * 100).toFixed(precision) + "%)", "#b6b3f4")
		//    } else {
		//      // console.log("Level headed: This user's winrate is not heavily affected if they lost their previous match. (Winrate " + ((vengeanceScore) * 100).toFixed(precision) + "%)");
		//      addTrait("LEVEL HEADED", "This user's winrate is not heavily affected if they lost their previous match. (Winrate " + ((vengeanceScore) * 100).toFixed(precision) + "%)", "#b6b3f4")
		//    }
		//  }
		//  // console.log("This user's winrate is " + (100 * wins / 9).toFixed(precision) + "%.");

		//  if (calculatePercentile(userPps, pps).toFixed(precision) > 75) {
		//    addTrait("HIGH PPS", "This user has a high average PPS compared to other players (Top " + calculatePercentile(userPps, pps).toFixed(precision) + " %)", "#b6b3f4");
		//  } else if (calculatePercentile(userPps, pps).toFixed(precision) < 25) {
		//    addTrait("LOW PPS", "This user has a low average PPS compared to other players (Bottom " + calculatePercentile(userPps, pps).toFixed(precision) + " %)", "#f4b6b3");
		//  }
		//  if (calculatePercentile(userApm, apm).toFixed(precision) > 75) {
		//    addTrait("HIGH APM", "This user has a high average APM compared to other players (Top " + calculatePercentile(userApm, apm).toFixed(precision) + " %)", "#b6b3f4");
		//  } else if (calculatePercentile(userApm, apm).toFixed(precision) < 25) {
		//    addTrait("LOW APM", "This user has a low average APM compared to other players (Bottom " + calculatePercentile(userApm, apm).toFixed(precision) + " %)", "#f4b6b3");
		//  }
		//  if (calculatePercentile(userVs, vs).toFixed(precision) > 75) {
		//    addTrait("HIGH VS", "This user has a high average VS compared to other players (Top " + calculatePercentile(userVs, vs).toFixed(precision) + " %)", "#b6b3f4");
		//  } else if (calculatePercentile(userVs, vs).toFixed(precision) < 25) {
		//    addTrait("LOW VS", "This user has a low average VS compared to other players (Bottom " + calculatePercentile(userVs, vs).toFixed(precision) + " %)", "#f4b6b3");
		//  }

		//  const openerScore = Number(Number(Number((((userApm / srarea) / ((0.069 * 1.0017 ** ((statrank ** 5) / 4700)) + statrank / 360) - 1) + (((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) * 0.75) + (((userVsapm / (-(((statrank - 16) / 36) ** 2) + 2.133) - 1)) * -10) + ((userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) * 0.75) + ((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) * -0.25)) / 3.5) + 0.5).toFixed(4));
		//  const plonkScore = Number(Number((((userGe / (statrank / 350 + 0.005948424455 * 3.8 ** ((statrank - 6.1) / 4) + 0.006) - 1) + (userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) + ((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) * 0.75) + (((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) * -1)) / 2.73) + 0.5).toFixed(4));
		//  const strideScore = Number(Number((((((userApm / srarea) / ((0.069 * 1.0017 ** ((statrank ** 5) / 4700)) + statrank / 360) - 1) * -0.25) + ((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) + ((userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) * -2) + ((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) * -0.5)) * 0.79) + 0.5).toFixed(4));
		//  const infdsScore = Number(Number((((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) + ((userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) * -0.75) + (((userApm / srarea) / ((0.069 * 1.0017 ** ((statrank ** 5) / 4700)) + statrank / 360) - 1) * 0.5) + ((userVsapm / (-(((statrank - 16) / 36) ** 2) + 2.133) - 1) * 1.5) + (((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) * 0.5)) * 0.9) + 0.5).toFixed(4));

		//  const playstyle = [openerScore, plonkScore, strideScore, infdsScore];

		//  const descriptions = [{title: "OPENER", description: "This user is likely an opener main"}, {
		//    title: "PLONKER",
		//    description: "This user is likely a plonker"
		//  }, {title: "STRIDER", description: "This user is likely a strider"}, {
		//    title: "INF DS'ER",
		//    description: "This user is likely an infnite downstacker"
		//  }];

		//  const mainPlaystyle = playstyle.indexOf(Math.max(...playstyle));
		//  playstyle[mainPlaystyle] = -playstyle[mainPlaystyle];
		//  const secondaryPlaystyle = playstyle[playstyle.indexOf(Math.max(...playstyle))] > -0.75 * playstyle[mainPlaystyle] ? playstyle.indexOf(Math.max(...playstyle)) : undefined;

		//  // console.log(secondaryPlaystyle)
		//  // console.log(mainPlaystyle);
		//  // console.log(playstyle);


		//  // console.log("Opener score: " + openerScore);
		//  // console.log("Plonk score: " + plonkScore);
		//  // console.log("Stride score: " + strideScore);
		//  // console.log("Infds score: " + infdsScore);

		//  const playstyleWinrate = [{wins: 0, played: 0}, {wins: 0, played: 0}, {wins: 0, played: 0}, {
		//    wins: 0,
		//    played: 0
		//  }];

		//  for (let i = 0; i < mainUserMatchHistoryData.length; i++) {
		//    let userPps = mainUserMatchHistoryData[i].data.user.league.pps;
		//    let userApm = mainUserMatchHistoryData[i].data.user.league.apm;
		//    let userVs = mainUserMatchHistoryData[i].data.user.league.vs;
		//    let userApp = userApm / 60 / userPps;
		//    let userVsapm = userVs / userApm;
		//    let userDsps = userVs / 100 - userApm / 60;
		//    let userDspp = userDsps / userPps;
		//    let userGe = 2 * userApp * userDsps / userPps

		//    let srarea = (userApm * 0) + (userPps * 135) + (userVs * 0) + (userApp * 290) + (userDsps * 0) + (userDspp * 700) + (userGe * 0);
		//    let statrank = 11.2 * Math.atan((srarea - 93) / 130) + 1;

		//    let openerScore = Number(Number(Number((((userApm / srarea) / ((0.069 * 1.0017 ** ((statrank ** 5) / 4700)) + statrank / 360) - 1) + (((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) * 0.75) + (((userVsapm / (-(((statrank - 16) / 36) ** 2) + 2.133) - 1)) * -10) + ((userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) * 0.75) + ((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) * -0.25)) / 3.5) + 0.5).toFixed(4))
		//    let plonkScore = Number(Number((((userGe / (statrank / 350 + 0.005948424455 * 3.8 ** ((statrank - 6.1) / 4) + 0.006) - 1) + (userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) + ((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) * 0.75) + (((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) * -1)) / 2.73) + 0.5).toFixed(4))
		//    let strideScore = Number(Number((((((userApm / srarea) / ((0.069 * 1.0017 ** ((statrank ** 5) / 4700)) + statrank / 360) - 1) * -0.25) + ((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) + ((userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) * -2) + ((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) * -0.5)) * 0.79) + 0.5).toFixed(4))
		//    let infdsScore = Number(Number((((userDspp / (0.02136327583 * (14 ** ((statrank - 14.75) / 3.9)) + statrank / 152 + 0.022) - 1) + ((userApp / (0.1368803292 * 1.0024 ** ((statrank ** 5) / 2800) + statrank / 54) - 1) * -0.75) + (((userApm / srarea) / ((0.069 * 1.0017 ** ((statrank ** 5) / 4700)) + statrank / 360) - 1) * 0.5) + ((userVsapm / (-(((statrank - 16) / 36) ** 2) + 2.133) - 1) * 1.5) + (((userPps / srarea) / (0.0084264 * (2.14 ** (-2 * (statrank / 2.7 + 1.03))) - statrank / 5750 + 0.0067) - 1) * 0.5)) * 0.9) + 0.5).toFixed(4))


		//    let playstyle = [openerScore, plonkScore, strideScore, infdsScore];
		//    let mainPlaystyle = playstyle.indexOf(Math.max(...playstyle));
		//    playstyle[mainPlaystyle] = -playstyle[mainPlaystyle];
		//    let secondaryPlaystyle = playstyle[playstyle.indexOf(Math.max(...playstyle))] > -0.75 * playstyle[mainPlaystyle] ? playstyle.indexOf(Math.max(...playstyle)) : undefined;

		//    playstyleWinrate[mainPlaystyle].played++;
		//    Number.isInteger(secondaryPlaystyle) ? playstyleWinrate[secondaryPlaystyle].played++ : "mrow" === "mrow";

		//    // If the main user won against this user...
		//    if (mainUserMatchHistory[i]) {
		//      playstyleWinrate[mainPlaystyle].wins++;
		//      Number.isInteger(secondaryPlaystyle) ? playstyleWinrate[secondaryPlaystyle].wins++ : "mrow" === "mrow";
		//    }


		//  }
		//  // TODO: Optimize it. Too bad!

		//  // console.log(playstyleWinrate);
		//  const playstyleWinrateDescriptions = ["openers", "plonkers", "striders", "inf ds'ers"];
		//  for (let i = 0; i < playstyleWinrate.length; i++) {
		//    if (playstyleWinrate[i].wins / playstyleWinrate[i].played > 0.65) {
		//      addTrait("WINS AGAINST " + playstyleWinrateDescriptions[i].toUpperCase(), "This user has a high winrate against " + playstyleWinrateDescriptions[i] + ". (Winrate " + (100 * playstyleWinrate[i].wins / playstyleWinrate[i].played).toFixed(precision) + "%)", "#b6b3f4")
		//    } else if (playstyleWinrate[i].wins / playstyleWinrate[i].played < 0.35) {
		//      addTrait("LOSES AGAINST " + playstyleWinrateDescriptions[i].toUpperCase(), "This user has a low winrate against " + playstyleWinrateDescriptions[i] + ". (Winrate " + (100 * playstyleWinrate[i].wins / playstyleWinrate[i].played).toFixed(precision) + "%)", "#f4b6b3")
		//    }
		//  }


		//  Number.isInteger(secondaryPlaystyle) ? addTrait(descriptions[secondaryPlaystyle].title, descriptions[secondaryPlaystyle].description) : "nya" === "nya";

		//  addTrait(descriptions[mainPlaystyle].title, descriptions[mainPlaystyle].description);
		//  // console.log("User statistics calculated in " + (Date.now() - tStart) / 1000 + " seconds!")

		//}

	}
}