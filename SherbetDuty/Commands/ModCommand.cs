using Cysharp.Threading.Tasks;
using Rocket.API;
using RocketExtensions.Models;
using RocketExtensions.Plugins;

namespace SherbetDuty.Commands
{
    [AllowedCaller(AllowedCaller.Player)]
    public class ModCommand : RocketCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            await Plugin.Mod.ToggleMod(context, silent: false);
        }

        private new DutyPlugin Plugin =>
            base.Plugin as DutyPlugin;
    }
}