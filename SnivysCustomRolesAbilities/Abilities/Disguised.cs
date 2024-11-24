using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using YamlDotNet.Core.Events;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class Disguised : PassiveAbility
    {
        public override string Name { get; set; } = "Disguised";

        public override string Description { get; set; } =
            "Handles everything with being disguised";
        
        [Description("What should the notification says if the player is a disguised for the CI side")]
        public string DisguisedCi { get; set; } = "That MTF is actually on the CI side";
        [Description("What should the notification says if the player is a disguised for the MTF side")]
        public string DisguisedMtf { get; set; } = "That CI is actually on the MTF side";

        [Description("Should the disguised notifcation be a hint? (True = Hint, False = Broadcast)")]
        public bool DisguisedHintDisplay { get; set; } = true;
        
        [Description("How long should the text displayed to the player should last")]
        public float DisguisedTextDisplayTime { get; set; } = 5f;
        
        public List<Player> PlayersWithDisguisedEffect = new List<Player>();
        protected override void AbilityAdded(Player player)
        {
            PlayersWithDisguisedEffect.Add(player);
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithDisguisedEffect.Remove(player);
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null)
                return;
            if (PlayersWithDisguisedEffect.Contains(ev.Player))
            {
                if (ev.Player.IsNTF && (ev.Attacker.IsCHI || ev.Attacker.Role.Type == RoleTypeId.ClassD))
                {
                    if (DisguisedHintDisplay)
                        ev.Attacker.ShowHint(DisguisedCi, DisguisedTextDisplayTime);
                    else
                        ev.Attacker.Broadcast(new Exiled.API.Features.Broadcast(DisguisedCi, (ushort)DisguisedTextDisplayTime));
                    ev.IsAllowed = false;
                }
                else if (ev.Player.IsCHI && (ev.Attacker.IsNTF || ev.Attacker.Role.Type == RoleTypeId.FacilityGuard || 
                                             ev.Attacker.Role.Type == RoleTypeId.Scientist))
                {
                    if (DisguisedHintDisplay)
                        ev.Attacker.ShowHint(DisguisedMtf, DisguisedTextDisplayTime);
                    else
                        ev.Attacker.Broadcast(new Exiled.API.Features.Broadcast(DisguisedMtf, (ushort)DisguisedTextDisplayTime));
                    ev.IsAllowed = false;
                }
            }
        }

        private void OnShooting(ShootingEventArgs ev)
        {
            if (PlayersWithDisguisedEffect.Contains(ev.Player))
            {
                Player target = Player.Get(ev.TargetNetId);
                if (ev.Player.IsNTF)
                {
                    if (target != null && Check(ev.Player) && (target.Role == RoleTypeId.ClassD || target.IsCHI))
                        ev.IsAllowed = false;
                }
                else if (ev.Player.IsCHI)
                {
                    if (target != null && Check(ev.Player) && (target.Role == RoleTypeId.Scientist || target.IsNTF || target.Role == RoleTypeId.FacilityGuard))
                        ev.IsAllowed = false;
                }
            }
        }
    }
}