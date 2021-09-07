using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace SpeedandReachFixes.GMST
{
    /// <summary>
    /// Contains game settings related to base reach multipliers, specifically fCombatDistance and fCombatBashReach.
    /// </summary>
    public class GameSettingsCombatReach
    {
        [MaintainOrder]
        
        [SettingName("Activé")]
        [Tooltip("Active cette catégorie. Il est fortement recommandé de laisser cette option activée !")]
        public bool Enabled = true;

        [SettingName("Distance de combat")]
        [Tooltip("Le multiplicateur de portée de base utilisé pour toutes les attaques, à l'exception des coups de bouclier.")]
        public float fCombatDistance = 141F;

        [SettingName("Portée des coups")]
        [Tooltip("Le multiplicateur de portée de base utilisé pour les attaques de type "coup de bouclier".")]
        public float fCombatBashReach = 61F;

        public int AddGameSettings(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            if (!Enabled) return 0;
            state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
            {
                EditorID = "fCombatDistance",
                Data = fCombatDistance
            });
            state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
            {
                EditorID = "fCombatBashReach",
                Data = fCombatBashReach
            });
            return 2;
        }
    }
}
