

using CommandSystem;
using System;
using System.Collections.Generic;
using Utils;

namespace CommonUtilities.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ScaleCommand : ICommand
    {
        public string Command => "scale";

        public string[] Aliases => new string[] { };

        public string Description => "Scales players size (similar to size)";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 1)
            {
                float scale;
                if (float.TryParse(arguments.At(1), out scale))
                {
                    if (scale < 0.1f)
                    {
                        response = "Scale must be greater than 0.1";
                        return false;
                    }
                    bool playerFound = false;
                    List<ReferenceHub> list = RAUtils.ProcessPlayerIdOrNamesList(arguments, 0, out _, false);
                    foreach (ReferenceHub referenceHub in list)
                    {
                        playerFound = Helpers.ChangePlayerSize(referenceHub, scale, scale, scale) ? true : playerFound;
                    }
                    if (playerFound)
                    {
                        response = "Player size changed";
                        return true;
                    }
                    response = "Player not found " + arguments.At(0);
                    return false;
                }
                response = "Invalid scale";
                return false;
            }
            response = "Usage: scale <player> <scale>";
            return false;
        }
    }
}
