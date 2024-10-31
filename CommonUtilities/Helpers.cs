using PlayerRoles;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
