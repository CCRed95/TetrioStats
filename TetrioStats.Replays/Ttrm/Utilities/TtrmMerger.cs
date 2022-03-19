using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

/*
 * Copyright (c) GlitchyPSI and contributors.
 * TETR.IO belongs to osk
 * Source: https://github.com/GlitchyPSIX/TTReMix
 * Licensed under the MIT license.
 */

namespace TetrioStats.Replays.Ttrm.Utilities {
    public static class TtrmMerger {
        /// <summary>
        /// Checks that the End Context for both replays have the same players.
        /// </summary>
        /// <param name="replay1">The deserialized JObject for the start replay</param>
        /// <param name="replay2">The deserialized JObject for the end replay</param>
        /// <returns>True if both replays have the same players</returns>
        public static bool ValidateEndContext(JObject replay1, JObject replay2) {
            // This may fail if the orders are different but they may not be
            return
                (replay1["endcontext"][0]["user"]["_id"].Value<string>() == replay2["endcontext"][0]["user"]["_id"].Value<string>()) &&
                (replay1["endcontext"][1]["user"]["_id"].Value<string>() == replay2["endcontext"][1]["user"]["_id"].Value<string>());
        }

        /// <summary>
        /// Merges two TTRMs worth of game replay data.
        /// </summary>
        /// <param name="replay1">The deserialized JObject for the start replay</param>
        /// <param name="replay2">The deserialized JObject for the end replay</param>
        /// <param name="stitch">Whether to delete the last replay of the first replay. Used to manage disconnections. Defaults to <strong>true</strong>.</param>
        /// <returns>JSON String for the merged TTRM</returns>
        public static string MergeTtrm(JObject replay1, JObject replay2, bool stitch = true) {
            if (replay1 == null || replay2 == null) throw new InvalidDataException("One of the provided replays isn't a TTRM.");
            bool valid = ValidateEndContext(replay1, replay2);
            JObject result = new JObject(replay1);
            if (!valid) throw new InvalidDataException("These replays appear to feature different players or is not a Multiplayer TTR.");
            if (stitch) {
                ((JArray)result.Root["data"]).Last.Remove();

                // This scares me I hope it works
                JToken winningPlayer = (result.Root["endcontext"].AsEnumerable()).OrderBy(x => x["wins"].Value<int>()).First();
                winningPlayer["wins"] = new JValue(winningPlayer["wins"].Value<int>() - 1);
            }
            foreach (JToken replay in ((JArray)replay2["data"])) {
                ((JArray)result.Root["data"]).Add(replay);
            }

            // Ugly. Make better
            ((JArray)result.Root["endcontext"])[0]["wins"] = new JValue(result.Root["endcontext"][0]["wins"].Value<int>() +
                                                                        ((JArray)result.Root["endcontext"])[0]["wins"].Value<int>());

            ((JArray)result.Root["endcontext"])[1]["wins"] = new JValue(result.Root["endcontext"][1]["wins"].Value<int>() +
                                                                        replay2.Root["endcontext"][1]["wins"].Value<int>());

            result.Root["ttremixed"] = new JValue(true);

            return result.ToString();
        }

        /// <summary>
        /// Merges TTRMs by filepath. Doesn't do checking of existence.
        /// </summary>
        /// <param name="path1">Path to start replay</param>
        /// <param name="path2">Path to end replay</param>
        /// <param name="stitch">Stitch replays? Defaults to <strong>true</strong></param>
        /// <returns>JSON String for the merged TTRM</returns>
        public static string MergeTtrmPath(string path1, string path2, bool stitch = true) {
            JObject s1 = ReadTtrm(new StreamReader(path1));
            JObject s2 = ReadTtrm(new StreamReader(path2));
            return MergeTtrm(s1, s2, stitch);
        }

        /// <summary>
        /// Parses a TTRM JSON from a StreamReader
        /// </summary>
        /// <param name="sr">The StreamReader to be validated</param>
        /// <returns>JObject for the JSON if it's a TTRM, <strong>null</strong> if it's not</returns>
        static JObject ReadTtrm(StreamReader sr) {
            string json = sr.ReadToEnd();
            JObject jr;
            try {
                jr = JObject.Parse(json);
            }
            catch {
                return null;
            }
            return (jr.Root["ismulti"].Value<bool>() ? jr : null);
        }
    }
}
