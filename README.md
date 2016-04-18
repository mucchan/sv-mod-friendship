# DailyFriendshipIncreaser

This is a mod for Stardew Valley which adjusts the amount of friendship points added for each NPC at the end of the day depending on whether they have been talked to or not.

### Requirements

Stardew Modding API (SMAPI) 0.39.3 or above.

### Installation

Unpack the mod files into their own directory inside 
```
%appdata%\StardewValley\Mods
```

### Configuration

A *config.json* file will be generated the first time you run this mod. This file will look like this:

```
{
    "enabled": true,
    "individualConfigs": [
        {
            "name": "Default",
            "baseIncrease": 2,
            "talkIncrease": 0,
            "max": 2500
        }
    ]
}
```

**enabled** is used to enable or disable the mod. If this is false, then the normal decay mechanism takes effect regardless of all your other configuration.

**individualConfigs** contains a collection of different configuration values for different characters. Any character not defined will use values from the collection which has the name set as "Default" as shown above. This means you must at least have the default collection specified as per the example above.
name is the name of the NPC (case sensitive).

**baseIncrease** is the amount of friendship points that will be added at the end of the day if you haven' talked to that NPC that day.

**talkIncrease** is the amount of friendship points that will be added at the end of the day if you talked to that NPC that day.

**max** is the maximum value of friendship points that can be accumulated for that particular NPC i.e. the cap.