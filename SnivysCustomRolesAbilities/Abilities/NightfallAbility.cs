using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using InventorySystem.Items;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class NightfallAbility : PassiveAbility
    {
        public override string Name { get; set; } = "NightfallAbility";

        public override string Description { get; set; } =
            "Handles everything related to Nightfall";
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
            if (!PlayersWithNightfallEffect.Contains(ev.Player))
                return;
            if (Check(ev.Player) && ev.Item.Type == ItemType.Medkit)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.Painkillers)
                ev.IsAllowed = false;
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (!PlayersWithNightfallEffect.Contains(ev.Player))
                return;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.SCP500)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.Medkit)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.Painkillers)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.KeycardO5)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.GunE11SR)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.GunCrossvec)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.GunFSP9)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.GunLogicer)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.GunAK)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Pickup.Type == ItemType.GunShotgun)
                ev.IsAllowed = false;
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (!PlayersWithNightfallEffect.Contains(ev.Player))
                return;
            if (Check(ev.Player) && ev.Item.Type == ItemType.SCP500)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.Medkit)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.Painkillers)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.KeycardO5)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.GunE11SR)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.GunCrossvec)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.GunFSP9)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.GunLogicer)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.GunAK)
                ev.IsAllowed = false;
            if (Check(ev.Player) && ev.Item.Type == ItemType.GunShotgun)
                ev.IsAllowed = false;
        }
    }
}