using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace DailyFriendshipIncreaser
{
    public class DailyFriendshipIncreaser : Mod
    {
        public override void Entry(params object[] objects)
        {
            ModConfig = new SocialConfig();
            ModConfig = ModConfig.InitializeConfig<SocialConfig>(base.BaseConfigPath);
            TimeEvents.OnNewDay += Event_OnNewDay;
        }

        private static void Event_OnNewDay(object sender, EventArgsNewDay e)
        {
            // This calculation needs to be triggered at the end of the day / before saving
            // So only perform action if e.IsNewDay = true as per SMAPI doc
            if (ModConfig.enabled && e.IsNewDay == true)
            {
                Log.SyncColour($"{Environment.NewLine}Friendship increaser enabled. Starting friendship calculation.{Environment.NewLine}", ConsoleColor.Green);
                List<IndividualNpcConfig> individualNpcConfigs = ModConfig.individualConfigs;
                SortedDictionary<string, IndividualNpcConfig> npcConfigsMap = new SortedDictionary<string, IndividualNpcConfig>();

                foreach(IndividualNpcConfig individualNpcConfig in individualNpcConfigs)
                {
                    npcConfigsMap.Add(individualNpcConfig.name, individualNpcConfig);
                }

                // Add default configuration if it's not found in the configuration file
                if (!npcConfigsMap.ContainsKey("Default"))
                {
                    npcConfigsMap.Add("Default", new IndividualNpcConfig("Default", 2, 0, 2500));
                }

                string[] npcNames = Player.friendships.Keys.ToArray<string>();
                foreach (string npcName in npcNames)
                {
                    IndividualNpcConfig config = npcConfigsMap.ContainsKey(npcName) ? npcConfigsMap[npcName] : npcConfigsMap["Default"];
                    int[] friendshipParams = Player.friendships[npcName];
                    int friendshipValue = friendshipParams[0];
                    Log.SyncColour($"{npcName}'s starting friendship value is {Player.getFriendshipLevelForNPC(npcName)}.", ConsoleColor.Green);
                    Log.SyncColour($"{npcName}'s current heart level is {Player.getFriendshipHeartLevelForNPC(npcName)}.", ConsoleColor.Green);

                    // Not sure why there's a special condition added for spouse. Disabling.
                    //if ((Player.spouse != null) && npcName.Equals(Player.spouse))
                    //{
                    //    friendshipValue += config.baseIncrease + 20;
                    //}

                    if (Player.hasPlayerTalkedToNPC(npcName))
                    {
                        friendshipValue += config.talkIncrease;
                        Log.SyncColour($"Talked to {npcName} today. Increasing friendship value by {config.talkIncrease}.", ConsoleColor.Green);
                    }
                    else
                    {
                        friendshipValue += config.baseIncrease;
                        Log.SyncColour($"Didn't talk to {npcName} today. Increasing friendship value by {config.baseIncrease}.", ConsoleColor.Red);
                    }

                    if (friendshipValue > config.max)
                    {
                        friendshipValue = config.max;
                    }

                    Log.SyncColour($"{npcName}'s new friendship value is {friendshipValue}. Maximum permitted value is {config.max}.", ConsoleColor.Green);
                    Player.friendships[npcName][0] = friendshipValue;
                }

                Log.SyncColour($"{Environment.NewLine}Finished friendship calculation.{Environment.NewLine}", ConsoleColor.Green);
            }
        }

        public static SocialConfig ModConfig
        {
            get;
            private set;
        }

        public static Farmer Player =>
            Game1.player;

        public static Game1 TheGame =>
            StardewModdingAPI.Program.gamePtr;
    }
}
