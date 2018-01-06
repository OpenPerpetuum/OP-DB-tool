// Decompiled with JetBrains decompiler
// Type: Perpetuum.ExportedTypes.EffectCategory
// Assembly: Perpetuum.ExportedTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 32C89C4C-6C49-453C-B906-BA0A54641350
// Assembly location: C:\PerpetuumServer\Perpetuum.ExportedTypes.dll

using System;

namespace Perpetuum.ExportedTypes
{
  [Flags]
  public enum EffectCategory : long
  {
    undefined = 0,
    effcat_aggressor = 16384,
    effcat_blob_emission_modulator = 268435456,
    effcat_consumable_effects = 549755813888,
    effcat_detection_stealth = 536870912,
    effcat_eccm = 4294967296,
    effcat_eccmcureable_effects = 8589934592,
    effcat_gang_effect = 64,
    effcat_gang_effect_coordinated_maneuvering = 1048576,
    effcat_gang_effect_core_management = 16777216,
    effcat_gang_effect_defense = 128,
    effcat_gang_effect_ewar = 262144,
    effcat_gang_effect_fast_extraction = 33554432,
    effcat_gang_effect_gathering = 1024,
    effcat_gang_effect_information = 256,
    effcat_gang_effect_maintance = 4194304,
    effcat_gang_effect_precision_firing = 8388608,
    effcat_gang_effect_shared_dataprocessing = 524288,
    effcat_gang_effect_shield_calculations = 2097152,
    effcat_gang_effect_siege = 2048,
    effcat_gang_effect_speed = 512,
    effcat_highway = 2147483648,
    effcat_intrusion_effect = 34359738368,
    effcat_invulnerable = 65536,
    effcat_jamm = 4096,
    effcat_movement_imparing = 1,
    effcat_pbs_booster_effects = 68719476736,
    effcat_pbs_engineering_aura = 2199023255552,
    effcat_pbs_gap_generator_effect = 137438953472,
    effcat_pbs_industry_aura = 4398046511104,
    effcat_pbs_mining_tower_effect = 274877906944,
    effcat_pbs_sensors_aura = 1099511627776,
    effcat_pvp_flag = 8192,
    effcat_range_increasing = 32,
    effcat_resists = 8,
    effcat_safe_spot = 67108864,
    effcat_sensor_amplify = 2,
    effcat_sensor_supress = 4,
    effcat_shield = 16,
    effcat_speed_booster = 32768,
    effcat_tag = 131072,
    effcat_target_paint = 1073741824,
    effcat_teleport_effects = 134217728,
    effcat_terrain_object_effects = 8796093022208,
    effcat_zero_core_drop = 17179869184,
  }
}
