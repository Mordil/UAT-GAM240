using UnityEngine;
using System.Collections;
using System;

public abstract class SpellBase : MonoBehaviour
{
    public enum EffectType { Damage, Heal }

    public abstract EffectType Type { get; }

    // TODO: Fill out base spell stuff
    public T As<T>()
        where T : SpellBase
    {
        return (T)this;
    }
}

public class DamageSpell : SpellBase
{
    public int Damage;

    public override EffectType Type { get { return EffectType.Damage; } }

    // TODO: Add duration & maybe types?
}
