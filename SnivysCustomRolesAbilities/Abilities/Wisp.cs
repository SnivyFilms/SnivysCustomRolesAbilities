using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class Wisp : PassiveAbility
    {
        public override string Name { get; set; } = "MTF Wisp Effects.";

        public override string Description { get; set; } = "Enables walking through doors, Fog Control, Reduced Sprint";
        
        public List<Player> PlayersWithWispEffect = new List<Player>();
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithWispEffect.Add(player);
            Timing.CallDelayed(10f, () =>
            {
                player.EnableEffect(EffectType.Exhausted);
                player.EnableEffect(EffectType.Ghostly); 
                player.EnableEffect(EffectType.FogControl, 2);
            });
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        }

        protected override void AbilityRemoved(Player player)
        {
            PlayersWithWispEffect.Remove(player);
            player.DisableEffect(EffectType.Ghostly);
            player.DisableEffect(EffectType.FogControl);
            player.DisableEffect(EffectType.Exhausted);
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
        }
        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (PlayersWithWispEffect.Contains(ev.Player))
                if (ev.Item.Type == ItemType.Adrenaline)
                    ev.IsAllowed = false;
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (PlayersWithWispEffect.Contains(ev.Player))
                if (ev.Pickup.Type == ItemType.Adrenaline)
                    ev.IsAllowed = false;
        }
    }
}