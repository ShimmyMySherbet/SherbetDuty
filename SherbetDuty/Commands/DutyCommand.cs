using Cysharp.Threading.Tasks;
using Rocket.API;
using RocketExtensions.Models;
using RocketExtensions.Plugins;

namespace SherbetDuty.Commands
{
    [AllowedCaller(AllowedCaller.Player)]
    public class DutyCommand : RocketCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            if (!Plugin.Config.DutySettings.EnableDuty)
            {
                await context.ReplyKeyAsync("Duty_Disabled", context.LDMPlayer.DisplayName);
                return;
            }

            await Plugin.Duty.ToggleDutyAsync(context, silent: false);
        }

        private new DutyPlugin Plugin =>
            base.Plugin as DutyPlugin;
    }
}