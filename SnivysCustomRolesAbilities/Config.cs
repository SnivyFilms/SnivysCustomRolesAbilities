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

        [Description("What should the notification says if the player is a disguised for the CI side")]
        public string DisguisedCi { get; set; } = "That MTF is actually on the CI side";
        [Description("What should the notification says if the player is a disguised for the MTF side")]
        public string DisguisedMtf { get; set; } = "That CI is actually on the MTF side";

        [Description("Should the disguised notifcation be a hint? (True = Hint, False = Broadcast)")]
        public bool DisguisedHintDisplay { get; set; } = true;
    }
}