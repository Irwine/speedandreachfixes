// GameSettings subsection containing reach modifiers.
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace SpeedandReachFixes.GMST
{
    /// <summary>
    /// Contains game settings related to weapon type reach modifiers, specifically:
    ///		fObjectHitWeaponReach,
    ///		fObjectHitTwoHandReach,
    ///	  & fObjectHitH2HReach.
    /// </summary>
    public class GameSettingsWeaponTypeReach
    {
        [MaintainOrder]
        [SettingName("Activé")]
        [Tooltip("Active cette catégorie. Il est fortement recommandé de laisser cette option activée !")]
        public bool Enabled = true;

        [SettingName("Portée des armes à une main")]
        [Tooltip("Valeur ajoutée à la portée des armes à une main.")]
        public float fObjectHitWeaponReach = 81F;

        [SettingName("Portée des armes à deux mains")]
        [Tooltip("Valeur ajouté à la portée des armes à deux mains.")]
        public float fObjectHitTwoHandReach = 135F;

        [SettingName("Portée à mains nues")]
        [Tooltip("Valeur ajouté à la portée à mains nues.")]
        public float fObjectHitH2HReach = 61F;

        // Adds the game settings from this class to the current patcher state
        public int AddGameSettings(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            if (!Enabled) return 0;
            state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
            {
                EditorID = "fObjectHitWeaponReach",
                Data = fObjectHitWeaponReach
            });
            state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
            {
                EditorID = "fObjectHitTwoHandReach",
                Data = fObjectHitTwoHandReach
            });
            state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
            {
                EditorID = "fObjectHitH2HReach",
                Data = fObjectHitH2HReach
            });
            return 3;
        }
    }
}
