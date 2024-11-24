using PlayerRoles;
using PluginAPI.Core;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonUtilities
{
    public class Helpers
    {
        public static SpawnableTeamType GetSpawnableTeam(RoleTypeId role)
        {
            if (role == RoleTypeId.ChaosConscript||role==RoleTypeId.ChaosMarauder||role==RoleTypeId.ChaosRepressor||role==RoleTypeId.ChaosRifleman||role==RoleTypeId.ClassD)
            {
                return SpawnableTeamType.ChaosInsurgency;
            }
            else if (role == RoleTypeId.NtfCaptain||role==RoleTypeId.NtfPrivate||role==RoleTypeId.NtfSergeant||role==RoleTypeId.NtfSpecialist||role==RoleTypeId.FacilityGuard||role==RoleTypeId.Scientist)
            {
                return SpawnableTeamType.NineTailedFox;
            }
            else return SpawnableTeamType.None;
        }
        public static bool ChangePlayerSize(ReferenceHub referenceHub, float x, float y, float z)
        {
            if (referenceHub != null)
            {
                referenceHub.transform.localScale = new Vector3(x, y, z);
                foreach (Player player in Server.GetPlayers())
                {
                    Plugin.SendSpawnMessage.Invoke(null, new object[] { referenceHub.networkIdentity, player.Connection });
                }
                return true;
            }
            return false;
        }
    }
}
