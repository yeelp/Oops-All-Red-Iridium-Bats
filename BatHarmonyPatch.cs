using System;
using Microsoft.Xna.Framework;
using StardewValley.Monsters;

namespace OopsAllRedIridiumBats;

internal class BatHarmonyPatch
{
    internal static void AlterBat(ref int mineLevel, out BatTransformer.BatType __state)
    {
        BatTransformer.BatType type = __state = BatTransformer.GetBatType(mineLevel);
        if(BatTransformer.GetInstance().ShouldTransformBat(type))
        {
            mineLevel = 1000;
        }
    }

    internal static void AlterBatAfter(BatTransformer.BatType __state, Bat __instance)
    {
        Action<Bat>? action = BatTransformer.GetInstance().PreserveOriginalDrops(__state);
        if(action is not null)
        {
            action(__instance);
        }
    }

    internal static void AlterExtraDrops(Bat __instance)
    {
        BatTransformer.GetInstance().PreserveExtraDrops(__instance);
    }
}