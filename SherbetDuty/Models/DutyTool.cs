using System.Threading.Tasks;
using Rocket.Core.Logging;
using RocketExtensions.Models;
using RocketExtensions.Utilities.ShimmyMySherbet.Extensions;
using SDG.Unturned;

namespace SherbetDuty.Models
{
    public class DutyTool
    {
        public DutyPlugin Plugin { get; }

        public DutyTool(DutyPlugin plugin)
        {
            Plugin = plugin;
        }

        public async Task ToggleDutyAsync(CommandContext context, bool silent = false)
        {
            var isOnDuty = context.Player.IsAdmin;

            if (isOnDuty)
            {
                await DisableDuty(context, silent);
                return;
            }
            await EnableDuty(context, silent);
        }

        private async Task EnableDuty(CommandContext context, bool silent)
        {
            await ThreadTool.RunOnGameThreadAsync(SteamAdminlist.admin, context.LDMPlayer.CSteamID, context.LDMPlayer.CSteamID);

            if (silent)
            {
                await context.ReplyKeyAsync("On_Duty_Silent", context.LDMPlayer.DisplayName);
            }
            else
            {
                await context.AnnounceKeyAsync("On_Duty", context.LDMPlayer.DisplayName);
            }

            Logger.Log($"Player [{context.LDMPlayer.DisplayName} ({context.PlayerID})] has gone on duty.");

            if (Plugin.Config.DutySettings.AutoGod && !context.UnturnedPlayer.Features.GodMode)
            {
                context.UnturnedPlayer.Features.GodMode = true;
                await context.ReplyKeyAsync("God_AutoEnabled");
            }

            if (Plugin.Config.DutySettings.AutoVanish && !context.UnturnedPlayer.Features.VanishMode)
            {
                context.UnturnedPlayer.Features.VanishMode = true;
                await context.ReplyKeyAsync("Vanish_AutoEnabled");
            }

            DutyAPI.RaiseDuty(context.LDMPlayer.Player, true, silent);
        }

        private async Task DisableDuty(CommandContext context, bool silent)
        {
            await ThreadTool.RunOnGameThreadAsync(SteamAdminlist.unadmin, context.LDMPlayer.CSteamID);

            if (silent)
            {
                await context.ReplyKeyAsync("Off_Duty_Silent", context.LDMPlayer.DisplayName);
            }
            else
            {
                await context.AnnounceKeyAsync("Off_Duty", context.LDMPlayer.DisplayName);
            }
            DutyAPI.RaiseDuty(context.LDMPlayer.Player, false, silent);

            Logger.Log($"Player [{context.LDMPlayer.DisplayName} ({context.PlayerID})] has gone off duty.");

            if (Plugin.Config.DutySettings.AutoGod && context.UnturnedPlayer.Features.GodMode)
            {
                context.UnturnedPlayer.Features.GodMode = false;
                await context.ReplyKeyAsync("God_AutoDisabled");
            }

            if (Plugin.Config.DutySettings.AutoVanish && context.UnturnedPlayer.Features.VanishMode)
            {
                context.UnturnedPlayer.Features.VanishMode = false;
                await context.ReplyKeyAsync("Vanish_AutoDisabled");
            }
        }
    }
}