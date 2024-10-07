using Exiled.API.Features.Attributes;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using MEC;
using UnityEngine;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class Flipped : PassiveAbility
    {
        public override string Name { get; set; } = "FlippedAbility";

        public override string Description { get; set; } =
            "Handles being upside down";
        
        protected override void AbilityAdded(Player player)
        {
            Timing.CallDelayed(2.5f, () => player.Scale = new Vector3(1.0f, -1.0f, 1.0f));
        }
        protected override void AbilityRemoved(Player player)
        {
            player.Scale = Vector3.one;
        }
    }
}