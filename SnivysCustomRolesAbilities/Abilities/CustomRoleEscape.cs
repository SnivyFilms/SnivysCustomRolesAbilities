using System;
using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomRoles;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class CustomRoleEscape : PassiveAbility
    {
        public override string Name { get; set; } = "Custom Role Escape";

        public override string Description { get; set; } =
            "Handles if you are a custom role, if you escape are you given another custom role";
        public List<Player> PlayersWithCustomRoleEscape = new List<Player>();
        public String UncuffedEscapeCustomRole { get; set; } = String.Empty;
        public String CuffedEscapeCustomRole { get; set; } = String.Empty;
        public bool AllowUncuffedCustomRoleChange { get; set; } = true;
        public bool AllowCuffedCustomRoleChange { get; set; } = true;
        public bool SaveInventory { get; set; } = true;

        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithCustomRoleEscape.Add(player);
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithCustomRoleEscape.Remove(player);
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
        }

        private void OnEscaping(EscapingEventArgs ev)
        {
            if (!PlayersWithCustomRoleEscape.Contains(ev.Player))
                return;
            List<Item> storedInventory = ev.Player.Items.ToList();
            
            if (ev.Player.IsCuffed && AllowCuffedCustomRoleChange && CuffedEscapeCustomRole != String.Empty)
            {
                ev.IsAllowed = false;
                CustomRole.Get(CuffedEscapeCustomRole).AddRole(ev.Player);
                storedInventory.Clear();
            }
            else if (AllowUncuffedCustomRoleChange && UncuffedEscapeCustomRole != String.Empty)
            {
                ev.IsAllowed = false;
                CustomRole.Get(UncuffedEscapeCustomRole).AddRole(ev.Player);
                if (SaveInventory)
                {
                    Timing.CallDelayed(1f, () =>
                    {
                        foreach (Item item in storedInventory)
                        {
                            item.CreatePickup(ev.Player.Position);
                        }
                        storedInventory.Clear();
                    });
                }
                else
                    storedInventory.Clear();
            }
        }
    }
}