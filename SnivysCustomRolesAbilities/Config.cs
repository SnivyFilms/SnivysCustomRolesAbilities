using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SnivysCustomRolesAbilities
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug Printouts?")]
        public bool Debug { get; set; } = false;
    }
}