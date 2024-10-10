using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class DwarfAbility : PassiveAbility
    {
        public override string Name { get; set; } = "DwarfAbility";

        public override string Description { get; set; } =
            "Handles everything in regards to being a dwarf";

        public List<ItemType> RestrictedItems { get; set; } = new List<ItemType>()
        {
            ItemType.Adrenaline,
            ItemType.Painkillers,
            ItemType.SCP500
        };
            
        public List<Player> PlayersWithDwarfEffect = new List<Player>();
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithDwarfEffect.Add(player);
            Timing.CallDelayed(2.5f, () => player.Scale = new Vector3(0.75f, 0.75f, 0.75f));
            player.IsUsingStamina = false;
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithDwarfEffect.Remove(player);
            player.Scale = Vector3.one;
            player.IsUsingStamina = true;
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (PlayersWithDwarfEffect.Contains(ev.Player) && RestrictedItems.Contains(ev.Item.Type))
                ev.IsAllowed = false;
        }
        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (PlayersWithDwarfEffect.Contains(ev.Player) && RestrictedItems.Contains(ev.Pickup.Type))
                ev.IsAllowed = false;
        }
    }
}