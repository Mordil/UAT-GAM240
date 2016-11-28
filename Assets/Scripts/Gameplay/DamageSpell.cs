using UnityEngine;

/// <summary>
/// The base class for all spells.
/// </summary>
public abstract class SpellBase : MonoBehaviour
{
    /// <summary>
    /// The intended way to handle who should receive the effect.
    /// </summary>
    public enum EffectType
    {
        /// <summary>
        /// Damages a target.
        /// </summary>
        Damage,
        /// <summary>
        /// Heals a target or self.
        /// </summary>
        Heal
    }

    /// <summary>
    /// The type of the spell.
    /// </summary>
    /// <seealso cref="EffectType"/>
    public abstract EffectType Type { get; }

    /// <summary>
    /// Casts the current object to the type provided.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>This cast as type T.</returns>
    public T As<T>()
        where T : SpellBase
    {
        return (T)this;
    }
}

/// <summary>
/// A spell that deals damage to targets.
/// </summary>
/// <seealso cref="SpellBase"/>
public class DamageSpell : SpellBase
{
    /// <summary>
    /// The amount of damage this spell does.
    /// </summary>
    public int Damage;

    /// <summary>
    /// Returns <see cref="SpellBase.EffectType.Damage"/>.
    /// </summary>
    /// <seealso cref="SpellBase.Type"/>
    public override EffectType Type { get { return EffectType.Damage; } }

    // TODO: Add duration & maybe damage types?
}
