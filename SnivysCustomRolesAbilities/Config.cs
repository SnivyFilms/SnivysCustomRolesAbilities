using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SnivysCustomRolesAbilities
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug Printouts? *Attention this WILL flood your console with debug statements*")]
        public bool Debug { get; set; } = false;

        [Description("What does the hint say if the player is not allowed to escape")]
        public string EscapeRestricted { get; set; } =
            "You're not allowed to escape normally, you can escape if cuffed though";

        [Description("What the hint says if the player is a disguised for the CI side")]
        public string DisguisedCi { get; set; } = "That MTF is actually on the CI side";
        [Description("What the hint says if the player is a disguised for the MTF side")]
        public string DisguisedMtf { get; set; } = "That CI is actually on the MTF side";
    }
}