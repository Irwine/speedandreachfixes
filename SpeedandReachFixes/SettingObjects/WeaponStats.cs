using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Noggog;
using System.Linq;

namespace SpeedandReachFixes.SettingObjects
{
    /// <summary>
    /// Represents the stats associated with a given weapon keyword, as well as a priority level which determines the winning category if a weapon has multiple keywords.
    /// </summary>
    [ObjectNameMember(nameof(Keyword))]
    public class WeaponStats
    {
        [MaintainOrder]
        
        [SettingName("Mot clé")]
        [Tooltip("Le mot clé attaché à ce type d'arme.")]
        public FormLink<IKeywordGetter> Keyword;

        [SettingName("Priorité")]
        [Tooltip("Lorsque plusieurs types d'armes s'appliquent à la même catégorie, la priorité la plus élevée l'emporte.")]
        public int Priority;

        [SettingName("Est un modificateur additionnel.")]
        [Tooltip("Lorsque cette case est cochée, ajoute les valeurs spécifiées plutôt que de les écraser. Les valeurs négatives seront soustraites.")]
        public bool IsAdditiveModifier;

        [SettingName("Portée")]
        [Tooltip("La portée de cette arme. Une valeur de 0 signifie qu'elle est inchangée.")]
        public float Reach;

        [SettingName("Vitesse")]
        [Tooltip("La vitesse de cette arme. Une valeur de 0 signifie qu'elle est inchangée.")]
        public float Speed;

        // Default Constructor
        public WeaponStats()
        {
            Priority = 0;
            IsAdditiveModifier = false;
            Keyword = new();
            Keyword.SetToNull(); // set keyword to null (all 0s)
            Reach = Constants.NullFloat;
            Speed = Constants.NullFloat;
        }

        // Constructor
        public WeaponStats(int priority, bool modifier, FormLink<IKeywordGetter> keyword, float speed = Constants.NullFloat, float reach = Constants.NullFloat)
        {
            Priority = priority;
            IsAdditiveModifier = modifier;
            Keyword = keyword;
            Reach = reach;
            Speed = speed;
        }

        /// <summary>
        /// Takes the current & member values for Reach / Speed, returns their sum if IsModifier is true, the member value if
        /// Private function, only usable within WeaponStats
        /// See GetReach() & GetSpeed() for public access functions.
        /// </summary>
        /// <param name="current">The current value of any given weapon's speed or reach stat.</param>
        /// <param name="local">The member value of either Speed or Reach depending on which stat is being requested.</param>
        /// <param name="changed">When true, the return value != current, else the returned value is equal to current.</param>
        /// <returns>float</returns>
        private float GetFloat(float current, float local, out bool changed)
        {
            changed = !local.EqualsWithin(current) && !local.EqualsWithin(Constants.NullFloat); // if current != local and local is set to a valid number
            if (changed) // return sum if additive modifier is true, else return local
                return IsAdditiveModifier ? (current + local) : local;
            return current;
        }

        /// <summary>
        /// Takes a weapon record's current reach value and calculates the final value using this category's configured stats.
        /// </summary>
        /// <param name="current">Current reach value</param>
        /// <param name="changed">Set to true if the return value does not equal current</param>
        /// <returns>float</returns>
        public float GetReach(float current, out bool changed)
        {
            return GetFloat(current, Reach, out changed);
        }

        /// <summary>
        /// Takes a weapon record's current speed value and calculates the final value using this category's configured stats.
        /// </summary>
        /// <param name="current">Current speed value</param>
        /// <param name="changed">Set to true if the return value does not equal current</param>
        /// <returns>float</returns>
        public float GetSpeed(float current, out bool changed)
        {
            return GetFloat(current, Speed, out changed);
        }

        /// <summary>
        /// Check if this WeaponStats object is not null, and not using default values.
        /// </summary>
        /// <returns>bool</returns>
        public bool ShouldSkip()
        {
            return (Keyword.IsNull) || (Reach.Equals(Constants.DefaultPriority) && Speed.Equals(Constants.DefaultPriority));
        }

        /// <summary>
        /// Retrieve the priority level of this WeaponStats instance, if it is not null and contains at least one valid value.
        /// </summary>
        /// <param name="keywords">List of keywords currently applied to a weapon.</param>
        /// <returns>int</returns>
        public int GetPriority(ExtendedList<IFormLinkGetter<IKeywordGetter>>? keywords)
        {
            if ((keywords != null) && (!ShouldSkip()) && keywords.Any(kywd => Keyword.Equals(kywd)))
                return Priority;
            return Constants.DefaultPriority;
        }
    }
}
