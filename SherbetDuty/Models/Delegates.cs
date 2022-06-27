using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using SherbetDuty.Models.Config;

namespace SherbetDuty.Models
{
    public delegate void DutyToggleArgs(Player player, bool enabled, bool silent);
    public delegate void ModToggleArgs(Player player, bool enabled, ModProfile[] profiles, bool silent);
}
