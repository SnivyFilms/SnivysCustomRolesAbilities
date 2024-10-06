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
    public class DwarfAbility : PassiveAbility
    {
        public override string Name { get; set; } = "DwarfAbility";

        public override string Description { get; set; } =
            "Handles everything in regards to being a dwarf";
        public List<Player> PlayersWithDwarfEffect = new List<Player>();
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithDwarfEffect.Add(player);
            Timing.CallDelayed(2.5f, () => player.Scale = new Vector3(0.75f, 0.75f, 0.75f));
            player.IsUsingStamina = false;
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithDwarfEffect.Remove(player);
            player.Scale = Vector3.one;
            player.IsUsingStamina = true;
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (PlayersWithDwarfEffect.Contains(ev.Player))
                if (ev.Item.Type is ItemType.Adrenaline or ItemType.Painkillers or ItemType.SCP500)
                    ev.IsAllowed = false;
        }
    }
}