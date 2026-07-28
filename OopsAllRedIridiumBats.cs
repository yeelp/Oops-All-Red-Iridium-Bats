using HarmonyLib;

using Microsoft.Xna.Framework;

using OopsAllRedIridiumBats.Config;

using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace OopsAllRedIridiumBats
{
    internal sealed class OopsAllRedIridiumBats : Mod
    {
        public static ModConfig Config { get; set; } = new();
        public override void Entry(IModHelper helper)
        {
            Harmony harmony = new(ModManifest.UniqueID);
            Logger.CreateLogger(Monitor);
            Config = helper.ReadConfig<ModConfig>() ?? new();

            harmony.Patch(
                original: AccessTools.Constructor(typeof(StardewValley.Monsters.Bat), new System.Type[] {typeof(Vector2), typeof(int)}),
                prefix: new HarmonyMethod(typeof(BatHarmonyPatch), nameof(BatHarmonyPatch.AlterBat)),
                postfix: new HarmonyMethod(typeof(BatHarmonyPatch), nameof(BatHarmonyPatch.AlterBatAfter))
            );
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Monsters.Bat), nameof(StardewValley.Monsters.Bat.getExtraDropItems)),
                prefix: new HarmonyMethod(typeof(BatHarmonyPatch), nameof(BatHarmonyPatch.AlterExtraDrops))
            );
            Logger.GetInstance().Info("Oops, All Red Iridium Bats Patched successfully!");
            helper.Events.GameLoop.DayStarted += OnDayStarted;
        }

        private void OnDayStarted(object? sender, DayStartedEventArgs args)
        {
            BatTransformer.GetInstance().Reset();
        }
    }
}