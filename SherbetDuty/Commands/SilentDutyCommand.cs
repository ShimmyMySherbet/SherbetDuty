using Cysharp.Threading.Tasks;
using Rocket.Core.Logging;
using RocketExtensions.Models;
using RocketExtensions.Plugins;
using RocketExtensions.Utilities.ShimmyMySherbet.Extensions;
using SDG.Unturned;

namespace SherbetDuty.Commands
{
    public class SilentDutyCommand : RocketCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            if (!Plugin.Config.DutySettings.EnableDuty)
            {
                await context.ReplyKeyAsync("SilentDuty_Disabled", context.LDMPlayer.DisplayName);
                return;
            }

            await Plugin.Duty.ToggleDutyAsync(context, silent: true);
        }

        private new DutyPlugin Plugin =>
            base.Plugin as DutyPlugin;
    }
}