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
using PlayerRoles;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class RestrictedEscape : PassiveAbility
    {
        public override string Name { get; set; } = "Restricted Escape";

        public override string Description { get; set; } =
            "Prevents players from escaping regularly (can still escape while detained)";
        public List<Player> PlayersWithRestrictedEscapeEffect = new List<Player>();
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithRestrictedEscapeEffect.Add(player);
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithRestrictedEscapeEffect.Remove(player);
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
        }

        private void OnEscaping(EscapingEventArgs ev)
        {
            if (PlayersWithRestrictedEscapeEffect.Contains(ev.Player))
                if (!ev.Player.IsCuffed)
                {
                    ev.IsAllowed = false;
                    ev.Player.ShowHint(Plugin.Instance.Config.EscapeRestricted, 5);
                }
        }
    }
}