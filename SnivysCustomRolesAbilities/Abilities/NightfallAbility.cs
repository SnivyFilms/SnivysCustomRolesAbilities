using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class NightfallAbility : PassiveAbility
    {
        public override string Name { get; set; } = "NightfallAbility";

        public override string Description { get; set; } =
            "Handles everything related to Nightfall";

        public List<ItemType> RestrictedItems { get; set; } = new List<ItemType>()
        {
            ItemType.Medkit,
            ItemType.Painkillers,
            ItemType.SCP500,
            ItemType.KeycardO5,
            ItemType.GunE11SR,
            ItemType.GunCrossvec,
            ItemType.GunFSP9,
            ItemType.GunLogicer,
            ItemType.GunAK,
            ItemType.GunShotgun
        };
        public List<Player> PlayersWithNightfallEffect = new List<Player>();
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithNightfallEffect.Add(player);
            Exiled.Events.Handlers.Player.DroppingItem += OnDroppingItem;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
            Exiled.Events.Handlers.Player.UsingItem += OnUsingMedicalItem;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithNightfallEffect.Remove(player);
            Exiled.Events.Handlers.Player.DroppingItem -= OnDroppingItem;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingMedicalItem;
        }

        private void OnUsingMedicalItem(UsingItemEventArgs ev)
        {
            if (PlayersWithNightfallEffect.Contains(ev.Player) && RestrictedItems.Contains(ev.Item.Type))
                ev.IsAllowed = false;
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (PlayersWithNightfallEffect.Contains(ev.Player) && RestrictedItems.Contains(ev.Pickup.Type))
                ev.IsAllowed = false;
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (PlayersWithNightfallEffect.Contains(ev.Player) && RestrictedItems.Contains(ev.Item.Type))
                ev.IsAllowed = false;
        }
    }
}