using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace CommonUtilities
{
    public class EventHandlers
    {
        [PluginEvent]
        public void OnPlayerInteractDoor(PlayerInteractDoorEvent ev)
        {
            if(Plugin.Singleton.Config.RemoteKeycardEnabled)
            {
                foreach (ItemBase item in ev.Player.Items)
                {
                    if(ev.Door.RequiredPermissions.CheckPermissions(item, ev.Player.ReferenceHub))
                    {
                        DoorEvents.TriggerAction(ev.Door, ev.Door.IsConsideredOpen() ? DoorAction.Closed : DoorAction.Opened , ev.Player.ReferenceHub);
                        break;
                    }
                }
            }
        }
    }
}
