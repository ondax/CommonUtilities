using CommandSystem;
using UnityEngine;
using PluginAPI.Core;
using Utils;
using System;
using System.Collections.Generic;

namespace CommonUtilities.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SizeCommand : ICommand
    {
        public string Command => "size";

        public string[] Aliases => new string[] { };

        public string Description => "Change player size";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 3)
            {
                float x;
                float y;
                float z;
                if (float.TryParse(arguments.At(1), out x) && float.TryParse(arguments.At(2), out y) && float.TryParse(arguments.At(3), out z))
                {
                    if(x < 0.1f || y < 0.1f || z < 0.1f)
                    {
                        response = "Size must be greater than 0.1";
                        return false;
                    }
                    bool playerFound = false;
                    List<ReferenceHub> list = RAUtils.ProcessPlayerIdOrNamesList(arguments, 0, out _, false);
                    foreach (ReferenceHub referenceHub in list)
                    {
                        playerFound = Helpers.ChangePlayerSize(referenceHub, x, y, z) ? true : playerFound;
                    }
                    if (playerFound)
                    {
                        response = "Player size changed";
                        return true;
                    }
                    response = "Player not found " + arguments.At(0);
                    return false;
                }
                response = "Invalid size";
                return false;
            }
            response = "Usage: size <player> <x> <y> <z>";
            return false;
        }
    }
}