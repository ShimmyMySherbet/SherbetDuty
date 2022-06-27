using Rocket.API.Collections;

namespace SherbetDuty
{
    public partial class DutyPlugin
    {
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "Duty_Disabled", "[color=red]The duty command is disabled[/color]"},
            { "SilentDuty_Disabled", "[color=red]The silent duty command is disabled[/color]"},

            { "On_Duty", "[color=red]{0} is now on duty![/color]"},
            { "Off_Duty", "[color=red]{0} is now off duty![/color]"},
            { "On_Duty_Silent", "[color=yellow]You are now off duty[/color]"},
            { "Off_Duty_Silent", "[color=yellow]You are now on duty[/color]"},

            { "On_ModDuty", "[color=red]{0} is now on duty![/color]"},
            { "Off_ModDuty", "[color=red]{0} is now off duty![/color]"},
            { "On_ModDuty_Silent", "[color=yellow]You are now off duty[/color]"},
            { "Off_ModDuty_Silent", "[color=yellow]You are now on duty[/color]"},

            { "Mod_NoProfiles", "[color=yellow]You don't have access to any mod profiles.[/color]"},
            { "Mod_Broken", "[color=yellow]Failed to go on duty: {0}[/color]"},

            { "Vanish_AutoEnabled", "[color=cyan]Vanish auto enabled[/color]"},
            { "Vanish_AutoDisabled", "[color=cyan]Vanish auto disabled[/color]"},

            { "God_AutoEnabled", "[color=cyan]Godmode auto enabled[/color]"},
            { "God_AutoDisabled", "[color=cyan]Godmode auto disabled[/color]"},
            { "Duty_Reminder", "[color=red]Reminder:[/color][color=yellow] You're on duty![/color]"},
            { "Mod_Reminder", "[color=red]Reminder:[/color][color=yellow] You're on duty![/color]"},
        };
    }
}