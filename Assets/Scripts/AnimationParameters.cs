/// <summary>
/// Static reference for animation parameter names different animation controllers.
/// </summary>
public struct AnimationParameters
{
    /// <summary>
    /// Animation parameters for the Arissa AnimationController asset.
    /// </summary>
    public struct Arissa
    {
        /// <summary>
        /// Trigger parameter names.
        /// </summary>
        public struct Triggers
        {
            /// <summary>
            /// Trigger to play a death animation.
            /// </summary>
            public const string DEATH_ANIMATION = "Has Died";
            /// <summary>
            /// Trigger to play a jump animation.
            /// </summary>
            public const string JUMP = "Jump";

            /// <summary>
            /// Trigger names for spellcasting animations.
            /// </summary>
            public struct Spellcasting
            {
                /// <summary>
                /// Trigger for playing a fireball casting animation.
                /// </summary>
                public const string FIREBALL = "Spellcast Fireball";
            }

            /// <summary>
            /// Trigger names for melee attack animations.
            /// </summary>
            public struct MeleeAttacks
            {
                /// <summary>
                /// Trigger for playing a normal melee attack animation.
                /// </summary>
                public const string NORMAL = "Normal Attack";
            }
        }

        /// <summary>
        /// Integer parameter names.
        /// </summary>
        public struct Integers
        {
            /// <summary>
            /// The animation ID to play for the currently equipped weapon state.
            /// </summary>
            public const string WEAPON_ANIMATION = "Weapon Animation Index";
        }

        /// <summary>
        /// Float parameter names.
        /// </summary>
        public struct Floats
        {
            /// <summary>
            /// Forward/backward movement input value.
            /// </summary>
            public const string VERTICAL = "Vertical";
            /// <summary>
            /// Left/Right side movement input value;
            /// </summary>
            public const string HORIZONTAL = "Horizontal";
        }
    }
}
