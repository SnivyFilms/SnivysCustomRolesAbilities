using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.CustomRoles.API.Features;
using SnivysCustomRolesAbilities.Abilities;

namespace SnivysCustomRolesAbilities
{
    public class Plugin : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Higher;
        public static Plugin Instance;
        public override string Name { get; } = "Snivy's Custom Role Abilities";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVCRAbilities";
        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 12, 2);

        public override void OnEnabled()
        {
            CustomAbility.RegisterAbilities();
            /*ActiveCamo.RegisterAbilities();
            ChargeAbility.RegisterAbilities();
            Detect.RegisterAbilities();
            Disguised.RegisterAbilities();
            DwarfAbility.RegisterAbilities();
            Flipped.RegisterAbilities();
            HealingMist.RegisterAbilities();
            HealOnKill.RegisterAbilities();
            Martyrdom.RegisterAbilities();
            MoveSpeedReduction.RegisterAbilities();
            ReactiveHume.RegisterAbilities();
            RemoveDisguise.RegisterAbilities();
            RestrictedEscape.RegisterAbilities();
            SpeedOnKill.RegisterAbilities();
            Wisp.RegisterAbilities();*/
            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomAbility.UnregisterAbilities();
            Instance = null;
            base.OnDisabled();
        }
    }
}