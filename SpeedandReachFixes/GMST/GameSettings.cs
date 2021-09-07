using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace SpeedandReachFixes.GMST
{
    /// <summary>
    /// Contains settings related to GMST (Game Setting) records.
    /// </summary>
    public class GameSettings
    {
        [MaintainOrder]
        // Combat reach
        [SettingName("Multiplicateurs de la portée de base en combat.")]
        [Tooltip("Formule de la portée de mêlée : ( reach = ( fCombatDistance | fCombatBashReach ) * NPCRaceScale * WeaponReach + ( fObjectHitWeaponReach | fObjectHitTwoHandReach | fObjectHitH2HReach ) )")]
        public GameSettingsCombatReach CombatReach = new();
        // Weapon type reach
        [SettingName("Modificateurs de la portée selon le type d'arme")]
        [Tooltip("Formule de la portée de mêlée : ( reach = ( fCombatDistance | fCombatBashReach ) * NPCRaceScale * WeaponReach + ( fObjectHitWeaponReach | fObjectHitTwoHandReach | fObjectHitH2HReach ) )")]
        public GameSettingsWeaponTypeReach WeaponTypeReach = new();

        public int AddGameSettingsToPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            var count = 0;
            // add game settings from weapon type reach category
            count += WeaponTypeReach.AddGameSettings(state);

            // add game settings from combat reach category
            count += CombatReach.AddGameSettings(state);
            return count;
        }
    }
}
