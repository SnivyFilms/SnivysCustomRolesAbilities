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
        public override string Name { get; set; } = "Wisp";

        public override string Description { get; set; } = "Enables walking through doors, Fog Control, Reduced Sprint";

        public Dictionary<EffectType, byte> EffectsToApply { get; set; } = new Dictionary<EffectType, byte>()
        {
            {EffectType.Exhausted, 1},
            {EffectType.Ghostly, 1},
            {EffectType.FogControl, 5},
        };

        public List<ItemType> RestrictedItems { get; set; } = new List<ItemType>()
        {
            ItemType.Adrenaline,
            ItemType.SCP500
        };
        
        public List<Player> PlayersWithWispEffect = new List<Player>();
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithWispEffect.Add(player);
            Timing.CallDelayed(10f, () =>
            {
                foreach (var effect in EffectsToApply)
                {
                    player.EnableEffect(effect.Key, effect.Value, 0);
                }
            });
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        }

        protected override void AbilityRemoved(Player player)
        {
            PlayersWithWispEffect.Remove(player);
            foreach (var effect in EffectsToApply)
            {
                player.DisableEffect(effect.Key);
            }
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
        }
        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (PlayersWithWispEffect.Contains(ev.Player) && RestrictedItems != null && RestrictedItems.Contains(ev.Item.Type))
                ev.IsAllowed = false;
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (PlayersWithWispEffect.Contains(ev.Player) && RestrictedItems != null && RestrictedItems.Contains(ev.Pickup.Type))
                ev.IsAllowed = false;
        }
    }
}