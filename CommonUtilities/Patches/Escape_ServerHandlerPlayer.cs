using HarmonyLib;
using InventorySystem.Disarming;
using PlayerRoles;
using PluginAPI.Events;

namespace CommonUtilities.Patches
{
    [HarmonyPatch(typeof(Escape), nameof(Escape.ServerHandlePlayer))]
    public class Escape_ServerHandlerPlayer
    {
        static bool Prefix(ReferenceHub hub)
        {
            HumanRole humanRole = hub.roleManager.CurrentRole as HumanRole;
            if (humanRole == null || (humanRole.FpcModule.Position - Escape.WorldPos).sqrMagnitude > 156.5f || humanRole.ActiveTime < 10f) return true;
            if(Plugin.Singleton.Config.ExtendedCuffedEscapeEnabled&&hub.inventory.IsDisarmed())
            {
                if(Plugin.Singleton.Config.CuffedRolesEscape.TryGetValue(humanRole.RoleTypeId, out RoleTypeId newRole))
                {
                    if (!EventManager.ExecuteEvent(new PlayerEscapeEvent(hub, newRole))) return true;
                    hub.roleManager.ServerSetRole(newRole, RoleChangeReason.Escaped, RoleSpawnFlags.All);
                    return false;
                }
            }
            else if (Plugin.Singleton.Config.CanGuardEscapeUncuffed&&humanRole.RoleTypeId==RoleTypeId.FacilityGuard)
            {
                RoleTypeId newRole = Plugin.Singleton.Config.UncuffedGuardEscapeRole;
                if (!EventManager.ExecuteEvent(new PlayerEscapeEvent(hub, newRole))) return true;
                hub.roleManager.ServerSetRole(newRole, RoleChangeReason.Escaped, RoleSpawnFlags.All);
                return false;
            }
            return true;
        }
    }
}
