using CommandSystem;
using MapGeneration;
using RemoteAdmin;
using System;
using UnityEngine;

namespace CommonUtilities.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class LightColorCommand : ICommand
    {
        public string Command => "lightcolor";

        public string[] Aliases => new string[] { };

        public string Description => "Changes color of facility lights";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 3)
            {
                response = "Usage: lightcolor <color> or lightcolor <r> <g> <b>";
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
            foreach (RoomLightController controller in RoomLightController.Instances)
            {
                controller.NetworkOverrideColor = color;
            }
            response = "Color changed";
            return true;
        }
    }
}
