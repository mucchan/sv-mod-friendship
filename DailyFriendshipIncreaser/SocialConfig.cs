using StardewModdingAPI;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace DailyFriendshipIncreaser
{
    public class SocialConfig : Config
    {
        public bool enabled { get; set; }
        public List<IndividualNpcConfig> individualConfigs { get; set; }

        public override T GenerateDefaultConfig<T>()
        {
            this.enabled = true;
            this.individualConfigs = new List<IndividualNpcConfig>();
            individualConfigs.Add(new IndividualNpcConfig("Default", 2, 0, 2500));

            return (this as T);
        }
    }

    public class IndividualNpcConfig
    {
        public string name { get; set; }
        public int baseIncrease { get; set; }
        public int talkIncrease { get; set; }
        public int max { get; set; }

        public IndividualNpcConfig(string name, int baseIncrease, int talkIncrease, int max)
        {
            this.name = name;
            this.baseIncrease = baseIncrease;
            this.talkIncrease = talkIncrease;
            this.max = max;
        }
    }
}

