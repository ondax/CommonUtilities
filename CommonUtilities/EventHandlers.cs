using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace CommonUtilities
{
    public class EventHandlers
    {
        [PluginEvent]
        public void OnPlayerInteractDoor(PlayerInteractDoorEvent ev)
        {
            //TODO: make this work for lockboxes too
            if(Plugin.Singleton.Config.RemoteKeycardEnabled&&!ev.CanOpen)
            {
                foreach (ItemBase item in ev.Player.Items)
                {
                    if(ev.Door.RequiredPermissions.CheckPermissions(item, ev.Player.ReferenceHub))
                    {
                        ev.Door.NetworkTargetState = !ev.Door.NetworkTargetState;
                        break;
                    }
                }
            }
        }
    }
}
