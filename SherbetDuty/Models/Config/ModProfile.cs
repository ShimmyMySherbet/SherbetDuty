using System.Xml.Serialization;
using Rocket.API;
using RocketExtensions.Models;

namespace SherbetDuty.Models.Config
{
    [XmlRoot]
    public class ModProfile
    {
        [XmlAttribute]
        public string ID = "Mod";

        public string OnDutyGroup = "OnDutyMod";
        public bool AutoVanish = false;
        public bool AutoGod = false;

        public bool HasPermission(LDMPlayer player)
            => player.UnturnedPlayer.HasPermission($"Mod.{ID}");
    }
}