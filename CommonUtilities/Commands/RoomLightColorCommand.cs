using CommandSystem;
using MapGeneration;
using RemoteAdmin;
using System;
using UnityEngine;

namespace CommonUtilities.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RoomLightColorCommand : ICommand
    {
        public string Command => "roomlightcolor";

        public string[] Aliases => new string[] { };

        public string Description => "Changes light color in current room";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 3)
            {
                response = "Usage: roomlightcolor <color> or roomlightcolor <r> <g> <b>";
                return false;
            }
            float r;
            float g;
            float b;
            if (!float.TryParse(arguments.At(0), out r) || !float.TryParse(arguments.At(1), out g) || !float.TryParse(arguments.At(2), out b))
            {
                response = "Invalid color";
                return false;
            }
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                response = "Invalid color";
                return false;
            }
            Color color = new Color(r, g, b);
            PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;
            if (playerCommandSender == null)
            {
                response = "Only players can use this command";
                return false;
            }
            RoomIdentifier room = RoomIdUtils.RoomAtPosition(playerCommandSender.ReferenceHub.transform.position);
            if (room == null)
            {
                response = "Not in a room";
                return false;
            }
            foreach (RoomLightController controller in RoomLightController.Instances)
            {
                if (controller.Room == room)
                {
                    controller.NetworkOverrideColor = color;
                }
            }
            response = "Color changed";
            return true;
        }
    }
}
