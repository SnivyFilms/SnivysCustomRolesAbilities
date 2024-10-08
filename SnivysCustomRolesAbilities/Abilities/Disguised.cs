﻿using Exiled.API.Features.Attributes;
using System.Collections.Generic;
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
                    if (Plugin.Instance.Config.DisguisedHintDisplay)
                        ev.Attacker.ShowHint(Plugin.Instance.Config.DisguisedCi, 5);
                    else
                        ev.Attacker.Broadcast(new Exiled.API.Features.Broadcast(Plugin.Instance.Config.DisguisedCi, 5));
                    ev.IsAllowed = false;
                }
                else if (ev.Player.IsCHI && (ev.Attacker.IsNTF || ev.Attacker.Role.Type == RoleTypeId.FacilityGuard || 
                                             ev.Attacker.Role.Type == RoleTypeId.Scientist))
                {
                    if (Plugin.Instance.Config.DisguisedHintDisplay)
                        ev.Attacker.ShowHint(Plugin.Instance.Config.DisguisedMtf, 5);
                    else
                        ev.Attacker.Broadcast(new Exiled.API.Features.Broadcast(Plugin.Instance.Config.DisguisedMtf, 5));
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