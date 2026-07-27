using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using OopsAllRedIridiumBats.Config;
using StardewValley;
using StardewValley.Monsters;

namespace OopsAllRedIridiumBats;

internal sealed class BatTransformer
{
    private static BatTransformer? _instance;
    private static readonly int QUARRY_FLOOR = 77377;

    private delegate void ParseMonsterInfo(Monster monster, string name); 
    private static readonly ParseMonsterInfo PARSE_MONSTER_INFO = AccessTools.MethodDelegate<ParseMonsterInfo>(AccessTools.Method(typeof(Monster), "parseMonsterInfo"));

    private static readonly BatType BAT = new("Bat", (c) => c.TurnAllBatsIntoRedIridiumBats, 0, 39);
    private static readonly BatType FROST_BAT = new("Frost Bat", (c) => c.TurnAllBatsIntoRedIridiumBats, 40, 79);
    private static readonly BatType LAVA_BAT = new("Lava Bat", (c) => c.TurnAllBatsIntoRedIridiumBats, 80, 120);
    private static readonly BatType MAGMA_SPRITE = new("Magma Sprite", (c) => c.ChangeMagmaSprites, -555);
    private static readonly BatType MAGMA_SPARKER = new("Magma Sparker", (c) => c.ChangeMagmaSprites, -556);
    private static readonly BatType CURSED_DOLL = new(null, (c) => c.ChangeHauntedDolls, -666, -789);
    private static readonly BatType SKULL_CAVERN_LAVA_BAT = new("Lava Bat", (c) => c.TurnAllBatsIntoRedIridiumBats, 121, 170);
    private static readonly BatType IRIDIUM_BAT = new(null, (c) => true, 171, int.MaxValue, QUARRY_FLOOR);
    private static readonly BatType HAUNTED_SKULL = new(null, (c) => c.ChangeHauntedSkulls, QUARRY_FLOOR);

    private static readonly BatType[] BAT_TYPES = {BAT, FROST_BAT, LAVA_BAT, MAGMA_SPRITE, MAGMA_SPARKER, CURSED_DOLL, SKULL_CAVERN_LAVA_BAT, IRIDIUM_BAT, HAUNTED_SKULL};
    private static readonly BatType[] SKULL_CAVERN_BATS = {SKULL_CAVERN_LAVA_BAT, IRIDIUM_BAT};
    private static readonly BatType[] EMPTY_DROPS = {CURSED_DOLL, HAUNTED_SKULL};
    private readonly ModConfig _config;
    private readonly IList<Bat> _hauntedSkulls, _cursedDolls;

    internal BatTransformer(ModConfig config)
    {
        _config = config;
        _hauntedSkulls = new List<Bat>();
        _cursedDolls = new List<Bat>();
    }

    internal void Reset()
    {
        _hauntedSkulls.Clear();
        _cursedDolls.Clear();
    }

    public static BatTransformer GetInstance()
    {
        return _instance ??= new(OopsAllRedIridiumBats.Config);
    }

    internal static BatType GetBatType(int mineLevel)
    {
        return BAT_TYPES.First((bt) => bt.IsBatType(mineLevel));
    }

    internal bool ShouldTransformBat(BatType type)
    {
        if(!type.ShouldTransformBat(_config))
        {
            return false;
        }
        if(_config.OnlyChangeBatsInSkullCavern)
        {
            return !Game1.currentLocation.IsFarm && SKULL_CAVERN_BATS.Contains(type);        
        }
        return true;
    }

    internal Action<Bat>? PreserveOriginalDrops(BatType type)
    {
        if(!_config.PreserveOriginalDrops)
        {
            return null;
        }
        if(EMPTY_DROPS.Contains(type))
        {
            return (b) => {
                b.objectsToDrop.Clear();
                _cursedDolls.Add(b);
                if(type == HAUNTED_SKULL)
                {
                    _hauntedSkulls.Add(b);
                }
            };
        }
        if(type.originalName is not null)
        {
            return (b) => PARSE_MONSTER_INFO(b, type.originalName);
        }
        return null;
    }

    internal void PreserveExtraDrops(Bat b)
    {
        if(b.cursedDoll.Value = _cursedDolls.Remove(b))
        {
            Logger.GetInstance().Debug("Is Cursed Doll Equivalent");
            b.hauntedSkull.Value = _hauntedSkulls.Remove(b);
        }
    }

    internal readonly struct BatType
    {
        private readonly int _floorMin, _floorMax;
        private readonly int? _floorExclusion;
        public readonly string? originalName;
        private readonly Predicate<ModConfig> _enabled;

        internal BatType(string? name, Predicate<ModConfig> enabled, int floor) : this(name, enabled, floor, floor) {}

        internal BatType(string? name, Predicate<ModConfig> enabled, int floorMin, int floorMax) : this(name, enabled, floorMin, floorMax, null) {}

        internal BatType(string? name, Predicate<ModConfig> enabled, int floorMin, int floorMax, int? floorExclusion)
        {
            originalName = name;
            _enabled = enabled;
            _floorMin = floorMin;
            _floorMax = floorMax;
            _floorExclusion = floorExclusion;
        }

        internal readonly bool IsBatType(int floor)
        {
            return _floorMin <= floor && floor <= _floorMax && _floorExclusion != floor;
        }

        internal readonly bool ShouldTransformBat(ModConfig config)
        {
            return _enabled(config);
        }

        public static bool operator ==(BatType t1, BatType t2)
        {
            return t1._floorMin == t2._floorMin && t1._floorMax == t2._floorMax;
        }

        public static bool operator !=(BatType t1, BatType t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object? obj)
        {
            if(obj is BatType type)
            {
                return this == type;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (_floorMin | _floorMax).GetHashCode();
        }
    } 
}