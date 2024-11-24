using HarmonyLib;
using Mirror;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using System.Reflection;

namespace CommonUtilities
{
    public class Plugin
    {
        private static MethodInfo sendSpawnMessage;
        public static MethodInfo SendSpawnMessage => sendSpawnMessage ??= typeof(NetworkServer).GetMethod("SendSpawnMessage", BindingFlags.NonPublic | BindingFlags.Static);
        [PluginConfig]
        public Config Config;
        public static Plugin Singleton { get; private set; }
        [PluginPriority(PluginAPI.Enums.LoadPriority.Medium)]
        [PluginEntryPoint("CommonUtilities", "1.0", "Common stuff", "ondax")]
        public void LoadPlugin()
        {
            Singleton = this;
            EventManager.RegisterEvents<EventHandlers>(this);
            Harmony harmony = new Harmony("commonutilities");
            harmony.PatchAll();
        }
    }
}
