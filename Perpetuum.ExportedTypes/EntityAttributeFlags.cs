// Decompiled with JetBrains decompiler
// Type: Perpetuum.ExportedTypes.EntityAttributeFlags
// Assembly: Perpetuum.ExportedTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 32C89C4C-6C49-453C-B906-BA0A54641350
// Assembly location: C:\PerpetuumServer\Perpetuum.ExportedTypes.dll

namespace Perpetuum.ExportedTypes
{
  public struct EntityAttributeFlags
  {
    public EntityAttributeFlags(ulong flags)
    {
      this = new EntityAttributeFlags();
      this.Flags = flags;
    }

    public ulong Flags { get; private set; }

    public bool HasFlag(AttributeFlags flag)
    {
      return (this.Flags & (ulong) (1L << (int) flag)) > 0UL;
    }

    public void SetFlag(AttributeFlags flag)
    {
      this.Flags = this.Flags | (ulong) (1L << (int) flag);
    }

    public bool ActiveModule
    {
      get
      {
        return this.HasFlag(AttributeFlags.activeModule);
      }
    }

    public bool AlwaysStackable
    {
      get
      {
        return this.HasFlag(AttributeFlags.alwaysStackable);
      }
    }

    public bool Consumable
    {
      get
      {
        return this.HasFlag(AttributeFlags.consumable);
      }
    }

    public bool ForceOneCycle
    {
      get
      {
        return this.HasFlag(AttributeFlags.forceOneCycle);
      }
    }

    public bool Invulnerable
    {
      get
      {
        return this.HasFlag(AttributeFlags.invulnerable);
      }
    }

    public bool MainBase
    {
      get
      {
        return this.HasFlag(AttributeFlags.mainbase);
      }
    }

    public bool NonAttackable
    {
      get
      {
        return this.HasFlag(AttributeFlags.nonattackable);
      }
    }

    public bool NonLockable
    {
      get
      {
        return this.HasFlag(AttributeFlags.nonlockable);
      }
    }

    public bool NonRecyclable
    {
      get
      {
        return this.HasFlag(AttributeFlags.nonRecyclable);
      }
    }

    public bool NonRelocatable
    {
      get
      {
        return this.HasFlag(AttributeFlags.nonrelocatable);
      }
    }

    public bool NonStackable
    {
      get
      {
        return this.HasFlag(AttributeFlags.nonStackable);
      }
    }

    public bool OffensiveModule
    {
      get
      {
        return this.HasFlag(AttributeFlags.offensive_module);
      }
    }

    public bool PassiveModule
    {
      get
      {
        return this.HasFlag(AttributeFlags.passiveModule);
      }
    }

    public bool PrimaryLockedTarget
    {
      get
      {
        return this.HasFlag(AttributeFlags.primary_locked_target);
      }
    }

    public bool PvpSupport
    {
      get
      {
        return this.HasFlag(AttributeFlags.pvp_support);
      }
    }

    public bool SelfEffect
    {
      get
      {
        return this.HasFlag(AttributeFlags.self_effect);
      }
    }

    public bool TargetIsRobot
    {
      get
      {
        return this.HasFlag(AttributeFlags.targetIsRobot);
      }
    }

    public bool Repackable
    {
      get
      {
        if (!this.AlwaysStackable)
          return !this.NonStackable;
        return false;
      }
    }

    public bool InstantActivate
    {
      get
      {
        return this.HasFlag(AttributeFlags.instantActivate);
      }
    }
  }
}
