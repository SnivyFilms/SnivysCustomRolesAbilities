using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using UnityEngine;

namespace SnivysCustomRolesAbilities.Abilities
{
    [CustomAbility]
    public class Detect : ActiveAbility
    {
        public override string Name { get; set; } = "Detect";

        public override string Description { get; set; } = "Detects Hostiles Near By.";

        public override float Duration { get; set; } = 0f;

        public override float Cooldown { get; set; } = 120f;

        public string message;

        public float DetectRange { get; set; } = 30f;

        protected override void AbilityUsed(Player player)
        {
            ActivateDetect(player);
            DisplayHint(player);
        }

        private void ActivateDetect(Player ply)
        {
            List<Player> detectedPlayers = new List<Player>();

            foreach (Player p in Player.List)
            {
                if (ply.IsCHI)
                {
                    if (Vector3.Distance(ply.Position, p.Position) <= DetectRange &&
                        (p.Role == RoleTypeId.Scientist || p.Role == RoleTypeId.NtfCaptain ||
                         p.Role == RoleTypeId.NtfPrivate || p.Role == RoleTypeId.NtfSergeant
                         || p.Role == RoleTypeId.NtfSpecialist || p.Role == RoleTypeId.FacilityGuard ||
                         p.Role == RoleTypeId.Scp049 || p.Role == RoleTypeId.Scp0492 || p.Role == RoleTypeId.Scp096 ||
                         p.Role == RoleTypeId.Scp106
                         || p.Role == RoleTypeId.Scp173 || p.Role == RoleTypeId.Scp939 ||
                         p.Role == RoleTypeId.Scp3114 ||
                         p.Role == RoleTypeId.Tutorial))
                    {
                        detectedPlayers.Add(p);
                    }
                }
                else if (ply.IsNTF)
                {
                    if (Vector3.Distance(ply.Position, p.Position) <= DetectRange &&
                        (p.Role == RoleTypeId.ChaosConscript || p.Role == RoleTypeId.ChaosMarauder ||
                         p.Role == RoleTypeId.ChaosRepressor || p.Role == RoleTypeId.ChaosRifleman
                         || p.Role == RoleTypeId.Scientist ||
                         p.Role == RoleTypeId.Scp049 || p.Role == RoleTypeId.Scp0492 || p.Role == RoleTypeId.Scp096 ||
                         p.Role == RoleTypeId.Scp106
                         || p.Role == RoleTypeId.Scp173 || p.Role == RoleTypeId.Scp939 ||
                         p.Role == RoleTypeId.Scp3114 ||
                         p.Role == RoleTypeId.Tutorial))
                    {
                        detectedPlayers.Add(p);
                    }
                }
            }

            if (detectedPlayers.Count > 0)
            {
                message = "Detected Targets Near By: \n";
                foreach (Player detectedPlayer in detectedPlayers)
                {
                    message += $"{GetRoleName(detectedPlayer.Role)}\n";
                }
            }
            else
            {
                message = "There is no detected targets near you";
            }
        }

        public void DisplayHint(Player pl)
        {
            pl.ShowHint(message, 10f);
        }

        private string GetRoleName(RoleTypeId role)
        {
            switch (role)
            {
                case RoleTypeId.Scientist:
                    return "Scientist";
                case RoleTypeId.NtfCaptain:
                    return "MTF Captain";
                case RoleTypeId.NtfPrivate:
                    return "MTF Private";
                case RoleTypeId.NtfSergeant:
                    return "MTF Sergeant";
                case RoleTypeId.NtfSpecialist:
                    return "MTF Specialist";
                case RoleTypeId.FacilityGuard:
                    return "Facility Guard";
                case RoleTypeId.Scp049:
                    return "SCP-049";
                case RoleTypeId.Scp0492:
                    return "SCP-049-2";
                case RoleTypeId.Scp096:
                    return "SCP-096";
                case RoleTypeId.Scp106:
                    return "SCP-106";
                case RoleTypeId.Scp173:
                    return "SCP-173";
                case RoleTypeId.Scp939:
                    return "SCP-939";
                case RoleTypeId.Scp3114:
                    return "SCP-3114";
                case RoleTypeId.Tutorial:
                    return "Serpents Hand";
                default:
                    return "Unknown Role";
            }
        }
    }
}