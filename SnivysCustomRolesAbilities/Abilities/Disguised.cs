using Exiled.API.Features.Attributes;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

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
                if (ev.Player.IsNTF)
                {
                    if (ev.Attacker.IsCHI || ev.Attacker.Role.Type == RoleTypeId.ClassD)
                    {
                        ev.Attacker.ShowHint(Plugin.Instance.Config.DisguisedCi, 5);
                        ev.IsAllowed = false;
                    }
                }
            }
            else if (ev.Player.IsCHI)
            {
                if (ev.Player.IsCHI)
                {
                    if (ev.Attacker.IsNTF || ev.Attacker.Role.Type == RoleTypeId.FacilityGuard ||
                        ev.Attacker.Role.Type == RoleTypeId.Scientist)
                    {
                        ev.Attacker.ShowHint(Plugin.Instance.Config.DisguisedMtf, 5);
                        ev.IsAllowed = false;
                    }
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
                    if (target != null && target.Role == RoleTypeId.ClassD && Check(ev.Player) ||
                        target != null && target.Role == RoleTypeId.ChaosConscript && Check(ev.Player)
                        || target != null && target.Role == RoleTypeId.ChaosRepressor && Check(ev.Player) ||
                        target != null && target.Role == RoleTypeId.ChaosMarauder && Check(ev.Player)
                        || target != null && target.Role == RoleTypeId.ChaosRifleman && Check(ev.Player))
                    {
                        ev.IsAllowed = false;
                    }
                }
                else if (ev.Player.IsCHI)
                {
                    if (target != null && target.Role == RoleTypeId.Scientist && Check(ev.Player) ||
                        target != null && target.Role == RoleTypeId.NtfCaptain && Check(ev.Player)
                        || target != null && target.Role == RoleTypeId.NtfPrivate && Check(ev.Player) ||
                        target != null && target.Role == RoleTypeId.NtfSergeant && Check(ev.Player)
                        || target != null && target.Role == RoleTypeId.NtfSergeant && Check(ev.Player) ||
                        target != null && target.Role == RoleTypeId.FacilityGuard && Check(ev.Player))
                    {
                        ev.IsAllowed = false;
                    }
                }
            }
        }
    }
}