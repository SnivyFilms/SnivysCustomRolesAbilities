using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using PlayerRoles;
using YamlDotNet.Core.Events;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class GivingCandyAbility : PassiveAbility
    {
        public override string Name { get; set; } = "Giving Candy Ability";

        public override string Description { get; set; } =
            "Handles giving candy to a player at the beginning of the round";

        public List<CandyKindID> StartingCandy { get; set; } = new List<CandyKindID>()
        {
            CandyKindID.Pink
        };

        protected override void AbilityAdded(Player player)
        {
            Timing.CallDelayed(1.0f, () =>
            {
                foreach (var candy in StartingCandy)
                {
                    player.TryAddCandy(candy);
                }
            });
        }
    }
}