using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Rocket.API;
using Rocket.Core;
using RocketExtensions.Models;
using RocketExtensions.Plugins;

namespace SherbetDuty.Commands
{
    [AllowedCaller(AllowedCaller.Player)]
    public class SilentModCommand : RocketCommand
    {
        public override async UniTask Execute(CommandContext context)
        {
            await Plugin.Mod.ToggleMod(context, silent: true);
        }
        private new DutyPlugin Plugin =>
            base.Plugin as DutyPlugin;
    }
}
