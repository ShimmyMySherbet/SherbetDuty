using System.Linq;
using Rocket.API;
using Rocket.Core.Plugins;
using RocketExtensions.Models;
using RocketExtensions.Utilities;
using SDG.Unturned;
using SherbetDuty.Models;
using SherbetDuty.Models.Config;
using UnityEngine;

namespace SherbetDuty
{
    public partial class DutyPlugin : RocketPlugin<DutyConfig>
    {
        public DutyConfig Config => Configuration.Instance;

        public ModTool Mod { get; private set; }
        public DutyTool Duty { get; private set; }

        public override void LoadPlugin()
        {
            Mod = new ModTool(this);
            Duty = new DutyTool(this);
            DutyAPI.Init(this);

            Provider.onEnemyConnected += OnPlayerJoin;

            base.LoadPlugin();
        }

        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            Provider.onEnemyConnected -= OnPlayerJoin;
            base.UnloadPlugin(state);
        }

        private void OnPlayerJoin(SteamPlayer player)
        {
            var ldm = LDMPlayer.FromPlayer(player.player);
            var active = Mod.GetActiveProfiles(ldm.UnturnedPlayer);

            var enableGod = false;
            var enableVanish = false;
                

            if (active.Length > 0)
            {
                enableVanish = active.Any(x => x.AutoVanish);
                enableGod = active.Any(x => x.AutoGod);
                Tell(player, "Mod_Reminder");
            }
            
            if (ldm.IsAdmin)
            {
                Tell(player, "Duty_Reminder");

                if (!enableGod)
                    enableGod = Config.DutySettings.AutoGod;

                if (!enableVanish)
                    enableVanish = Config.DutySettings.AutoGod;
            }

            if (enableVanish)
            {
                ldm.UnturnedPlayer.Features.VanishMode = true;
                Tell(player, "Vanish_AutoEnabled");

                // Vanish only kicks in once the player has moved
                // So we need to update their pos to hide them
                ldm.Player.teleportToLocationUnsafe(ldm.Position, ldm.Rotation);
            }

            if (enableGod)
            {
                ldm.UnturnedPlayer.Features.GodMode = true;
                Tell(player, "God_AutoEnabled");
            }
        }

        private void Tell(SteamPlayer player, string key)
        {
            ChatManager.say(player.playerID.steamID, Translate(key).ReformatColor(), Color.green, true);
        }
    }
}