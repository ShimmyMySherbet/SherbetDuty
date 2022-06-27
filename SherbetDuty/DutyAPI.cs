using Rocket.Unturned.Player;
using SDG.Unturned;
using SherbetDuty.Models;
using SherbetDuty.Models.Config;

namespace SherbetDuty
{
    public static class DutyAPI
    {
        public static DutyPlugin Plugin { get; private set; }

        public static event DutyToggleArgs DutyToggled;

        public static event ModToggleArgs ModToggled;

        internal static void Init(DutyPlugin plugin) =>
            Plugin = plugin;

        internal static void RaiseDuty(Player player, bool enabled, bool silent)
        {
            DutyToggled.Invoke(player, enabled, silent);
        }

        internal static void RaiseMod(Player player, bool enabled, ModProfile[] profiles, bool silent)
        {
            ModToggled?.Invoke(player, enabled, profiles, silent);
        }

        public static bool IsOnDuty(UnturnedPlayer player)
        {
            if (player.IsAdmin)
            {
                return true;
            }

            var activeMods = Plugin.Mod.GetActiveProfiles(player);

            return activeMods.Length > 0;
        }
    }
}