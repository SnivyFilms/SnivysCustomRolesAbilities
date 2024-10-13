using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PluginAPI.Events;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class DoorPicking : ActiveAbility
    {
        public override string Name { get; set; } = "Door Picking Ability";
        public override string Description { get; set; } = "Allows you to open any door for a short period of time, but limited by some external factors";
        public override float Duration { get; set; } = 15f;
        public override float Cooldown { get; set; } = 180f;
        public float TimeToDoorPickMin { get; set; } = 3f;
        public float TimeToDoorPickMax { get; set; } = 6f;
        public float TimeForDoorToBeOpen { get; set; } = 5f;
        public string BeforePickingDoorText { get; set; } = "Interact with a door to start to pick it";
        public string PickingDoorText { get; set; } = "Picking door...";
        public Dictionary<EffectType, byte> EffectsToApply { get; set; } = new Dictionary<EffectType, byte>()
        {
            {EffectType.Ensnared, 1},
            {EffectType.Slowness, 255},
        };
        public List<Player> PlayersWithPickingDoorAbility = new List<Player>();
        
        protected override void AbilityUsed(Player player)
        {
            player.ShowHint(BeforePickingDoorText, 5f);
            PlayersWithPickingDoorAbility.Add(player);
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            base.UnsubscribeEvents();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!PlayersWithPickingDoorAbility.Contains(ev.Player))
                return;
            
            if (ev.Door.IsOpen)
                return;
            
            ev.IsAllowed = false;
            int randomTime = new Random().Next((int)TimeToDoorPickMin, (int)TimeToDoorPickMax);
            ev.Player.ShowHint(PickingDoorText, randomTime);
            foreach (var effect in EffectsToApply)
            {
                ev.Player.EnableEffect(effect.Key, effect.Value, randomTime);
            }

            Timing.CallDelayed(randomTime, () =>
            {
                ev.Door.IsOpen = true;
                PlayersWithPickingDoorAbility.Remove(ev.Player);
                Timing.CallDelayed(TimeForDoorToBeOpen, () =>
                {
                    ev.Door.IsOpen = false;
                });
            });
        }
    }
}