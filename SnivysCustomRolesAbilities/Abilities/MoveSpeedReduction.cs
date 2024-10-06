using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class MoveSpeedReduction : PassiveAbility
    {
        public override string Name { get; set; } = "Reduced movement speed.";

        public override string Description { get; set; } = "Reduces the player's movement speed.";

        protected override void AbilityAdded(Player player)
        {
            Timing.CallDelayed(2.5f, () => { player.EnableEffect(EffectType.Slowness, 30); });
        }

        protected override void AbilityRemoved(Player player)
        {
            player.DisableEffect(EffectType.Slowness);
        }
    }
}