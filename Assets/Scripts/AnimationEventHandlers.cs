/// <summary>
/// A handler object for Spellcasting Animation events.
/// </summary>
public interface ISpellcastingAnimationHandler
{
    /// <summary>
    /// Animation event fired on the frame the spell should be instantiated.
    /// </summary>
    /// <param name="spellName">The spell's name that should be instantiated.</param>
    void SpawnSpell(string spellName);
}

/// <summary>
/// A handler object for Melee Attack Animation events.
/// </summary>
public interface IMeleeAttackAnimationHandler
{
    /// <summary>
    /// Animation event fired on the frame that melee attack hit checks should happen.
    /// </summary>
    void MeleeAttackHitCheck();
}
