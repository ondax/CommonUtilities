using Footprinting;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using InventorySystem.Items.Keycards;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace CommonUtilities
{
    public class EventHandlers
    {
        [PluginEvent]
        public bool OnPlayerInteractDoor(PlayerInteractDoorEvent ev)
        {
            if(Plugin.Singleton.Config.RemoteKeycardEnabled&&!ev.CanOpen)
            {
                foreach (ItemBase item in ev.Player.Items)
                {
                    if(ev.Door.RequiredPermissions.CheckPermissions(item, ev.Player.ReferenceHub))
                    {
                        ev.Door.NetworkTargetState = !ev.Door.NetworkTargetState;
                        return false;
                    }
                }
            }
            return true;
        }
        [PluginEvent]
        public bool OnPlayerInteractLocker(PlayerInteractLockerEvent ev)
        {
            if (Plugin.Singleton.Config.RemoteKeycardEnabled && !ev.CanOpen)
            {
                foreach(ItemBase item in ev.Player.Items)
                {
                    KeycardItem keycard = item as KeycardItem;
                    if (keycard != null && keycard.Permissions.HasFlagFast(ev.Chamber.RequiredPermissions))
                    {
                        ev.Chamber.SetDoor(!ev.Chamber.IsOpen, ev.Locker._grantedBeep);
                        ev.Locker.RefreshOpenedSyncvar();
                        return false;
                    }   
                }
            }
            return true;
        }
        [PluginEvent]
        public bool OnPlayerInteractGenerator(PlayerInteractGeneratorEvent ev)
        {
            if(Plugin.Singleton.Config.RemoteKeycardEnabled&&!ev.Generator.IsUnlocked)
            {
                foreach (ItemBase item in ev.Player.Items)
                {
                    KeycardItem keycard = item as KeycardItem;
                    if (keycard != null && keycard.Permissions.HasFlagFast(ev.Generator._requiredPermission)&&EventManager.ExecuteEvent(new PlayerUnlockGeneratorEvent(ev.Player.ReferenceHub, ev.Generator)))
                    {
                        ev.Generator.ServerSetFlag(MapGeneration.Distributors.Scp079Generator.GeneratorFlags.Unlocked, true);
                        ev.Generator.ServerGrantTicketsConditionally(new Footprint(ev.Player.ReferenceHub), 0.5f);
                        return false;
                    }
                }
            }
            return true;
        } 
    }
}
