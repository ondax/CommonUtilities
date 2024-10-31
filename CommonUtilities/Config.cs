using PlayerRoles;
using System.Collections.Generic;

namespace CommonUtilities
{
    public class Config
    {
        public bool RemoteKeycardEnabled = true;
        public bool ExtendedCuffedEscapeEnabled = true;
        public Dictionary<RoleTypeId, RoleTypeId> CuffedRolesEscape = new Dictionary<RoleTypeId, RoleTypeId>()
        {
            { RoleTypeId.ChaosConscript, RoleTypeId.NtfCaptain },
            { RoleTypeId.ChaosMarauder, RoleTypeId.NtfCaptain },
            { RoleTypeId.ChaosRepressor, RoleTypeId.NtfCaptain },
            { RoleTypeId.ChaosRifleman, RoleTypeId.NtfCaptain },
            { RoleTypeId.NtfCaptain, RoleTypeId.ChaosRifleman },
            { RoleTypeId.NtfPrivate, RoleTypeId.ChaosRifleman },
            { RoleTypeId.NtfSergeant, RoleTypeId.ChaosRifleman },
            { RoleTypeId.NtfSpecialist, RoleTypeId.ChaosRifleman },
            { RoleTypeId.FacilityGuard, RoleTypeId.ChaosConscript }
        };
        public bool CanGuardEscapeUncuffed = true;
        public RoleTypeId UncuffedGuardEscapeRole = RoleTypeId.NtfPrivate;
    }
}
