using System.Collections.Generic;
using Rocket.API;

namespace SherbetDuty.Models.Config
{
    public class DutyConfig : IRocketPluginConfiguration
    {
        public DutySettings DutySettings = new DutySettings();

        public List<ModProfile> ModProfiles = new List<ModProfile>();

        public void LoadDefaults()
        {
            ModProfiles.Add(new ModProfile() { ID = "Mod", OnDutyGroup = "OnDutyMod" });
            ModProfiles.Add(new ModProfile() { ID = "TMod", OnDutyGroup = "OnDutyTMod" });
        }
    }
}