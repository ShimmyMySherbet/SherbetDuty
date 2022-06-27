using System.Linq;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core;
using Rocket.Core.Logging;
using RocketExtensions.Models;
using SherbetDuty.Models.Config;

namespace SherbetDuty.Models
{
    public class ModTool
    {
        public DutyPlugin Plugin { get; }

        public ModTool(DutyPlugin plugin)
        {
            Plugin = plugin;
        }

        public ModProfile[] GetActiveProfiles(IRocketPlayer player)
        {
            var groups = R.Permissions.GetGroups(player, false).Select(x => x.Id);
            var modGroups = Plugin.Config.ModProfiles;

            var activeGroups = modGroups.Where(x => groups.Contains(x.OnDutyGroup)).ToArray();

            return activeGroups;
        }

        public async Task ToggleMod(CommandContext context, bool silent = false)
        {
            var activeGroups = GetActiveProfiles(context.Player);

            if (activeGroups.Length > 0)
            {
                await DisableMod(context, activeGroups, silent);
                return;
            }

            await EnableMod(context, silent);
        }

        private async Task EnableMod(CommandContext context, bool silent)
        {
            var highestModProfile = Plugin.Config.ModProfiles.FirstOrDefault
                (x => x.HasPermission(context.LDMPlayer));

            if (highestModProfile == null)
            {
                await context.ReplyKeyAsync("Mod_NoProfiles");
                return;
            }

            var result = R.Permissions.AddPlayerToGroup(highestModProfile.OnDutyGroup, context.Player);

            if (result != RocketPermissionsProviderResult.Success)
            {
                await context.ReplyKeyAsync("Mod_Broken", result);
                return;
            }

            Logger.Log($"Player [{context.LDMPlayer.DisplayName} ({context.PlayerID})] has gone on mod [{highestModProfile.ID}].");

            if (silent)
            {
                await context.ReplyKeyAsync("On_ModDuty_Silent", context.LDMPlayer.DisplayName, highestModProfile.ID);
            }
            else
            {
                await context.AnnounceKeyAsync("On_ModDuty", context.LDMPlayer.DisplayName, highestModProfile.ID);
            }

            DutyAPI.RaiseMod(context.LDMPlayer.Player, true, new[] { highestModProfile }, silent);

            if (highestModProfile.AutoGod && !context.UnturnedPlayer.Features.GodMode)
            {
                context.UnturnedPlayer.Features.GodMode = true;
                await context.ReplyKeyAsync("God_AutoEnabled");
            }

            if (highestModProfile.AutoVanish && !context.UnturnedPlayer.Features.VanishMode)
            {
                context.UnturnedPlayer.Features.VanishMode = true;
                await context.ReplyKeyAsync("Vanish_AutoEnabled");
            }
        }

        private async Task DisableMod(CommandContext context, ModProfile[] activeGroups, bool silent)
        {
            var removeVanish = false;
            var removeGod = false;

            foreach (var dutyGroup in activeGroups)
            {
                if (dutyGroup.AutoGod)
                    removeGod = true;

                if (dutyGroup.AutoVanish)
                    removeVanish = true;

                R.Permissions.RemovePlayerFromGroup(dutyGroup.OnDutyGroup, context.Player);
            }

            Logger.Log($"Player [{context.LDMPlayer.DisplayName} ({context.PlayerID})] has gone off mod [{string.Join(", ", activeGroups.Select(x => x.ID))}].");

            DutyAPI.RaiseMod(context.LDMPlayer.Player, false, activeGroups, silent);

            if (silent)
            {
                await context.ReplyKeyAsync("Off_ModDuty_Silent", context.LDMPlayer.DisplayName);
            }
            else
            {
                await context.AnnounceKeyAsync("Off_ModDuty", context.LDMPlayer.DisplayName);
            }

            if (removeVanish && context.UnturnedPlayer.Features.VanishMode)
            {
                context.UnturnedPlayer.Features.VanishMode = false;
                await context.ReplyKeyAsync("Vanish_AutoDisabled");
            }

            if (removeGod && context.UnturnedPlayer.Features.GodMode)
            {
                context.UnturnedPlayer.Features.GodMode = false;
                await context.ReplyKeyAsync("God_AutoDisabled");
            }
        }
    }
}