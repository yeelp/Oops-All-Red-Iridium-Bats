![](https://raw.githubusercontent.com/yeelp/Oops-All-Red-Iridium-Bats/refs/heads/master/oopsallrediridiumbatslogo.png)

[![CurseForge Downloads](https://img.shields.io/curseforge/dt/1629534?style=flat&logo=curseforge&logoColor=f16436&label=CurseForge%20Downloads&color=f16436)](https://www.curseforge.com/stardewvalley/mods/oops-all-red-iridium-bats)
[![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/yeelp/Oops-All-Red-Iridium-Bats/total?style=flat&logo=github&label=GitHub%20Downloads&color=white)](https://github.com/yeelp/Oops-All-Red-Iridium-Bats/releases) 
[![Discord](https://img.shields.io/discord/750481601107853373?style=flat&logo=discord&logoColor=white&color=5662f6)](https://discord.gg/hwzWdXQ)
![](https://img.shields.io/github/v/release/yeelp/Oops-All-Red-Iridium-Bats?include_prereleases)
[![](https://img.shields.io/github/issues/yeelp/Oops-All-Red-Iridium-Bats)](https://github.com/yeelp/Oops-All-Red-Iridium-Bats/issues)

# About
Transforms all Iridium Bats to their scarier red counterparts!

## What's a Red Iridium Bat?
[Red Iridium Bats are a vanilla feature](https://stardewvalleywiki.com/Bats#Trivia)! They replace Iridium Bats in the Skull Cavern at floor 880 and beyond! Red Iridium Bats have double health of regular Iridium Bats (600 health), accelerate around 3 times faster, and move up to 60% faster!

# Config
Under `Stardew Valley/mods/OopsAllRedIridiumBats` is a `config.json` with the following options:

- `DebugMode`: Adds some lines to SMAPI's output log. The exact lines may vary from release to release.
- `TurnAllBatsIntoRedIridiumBats`: When set to `true`, all bats (Bats, Frost Bats, Lava Bats and Iridium Bats) will be transformed into Red Iridium Bats, not just Iridium Bats.
- `OnlyChangeBatsInSkullCavern`: When set to `true`, only bats within Skull Cavern are changed. Iridium Bats are changed as normal, and Lava Bats are only changed if `TurnAllBatsIntoIridiumBats` is set to `true`. All other options do nothing if this is set to `true`.
- `ChangeHauntedSkulls`: Haunted Skulls are technically bats within the game's code, so with this option set to `true`, all Haunted Skulls will transform into Red Iriridum Bats.
- `ChangeMagmaSprites`: Magma Sprites (Both Magma Sprites and Magma Sparkers variants) are technically bats within the game's code, so with this option set to `true`, all Magma Sprites and Magma Sparkers will transform into Red Iridium Bats.
- `ChangeHauntedDolls`: Cursed Dolls (that appear in the witch's swamp after turning a child into a dove) count as a bat, so with this option set to `true`, Cursed Dolls will transform into Red Iridium Bats.
- `PreserveOriginalDrops`: When set to `true`, monster drops are preserved; that means that the new Red Iridium Bat will drop whatever the monster it replaced normally drops. To prevent early access to iridium by slaying Red Iridium Bats on the Farm or in the Mines (though that might be hard enough as is).

# Compatibility
Mods that alter bat behaviour and monster spawning may pose problems. Given the lightweight nature of this mod, that is unlikely.