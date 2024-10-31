using HarmonyLib;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace CommonUtilities
{
    public class Plugin
    {
        [PluginConfig]
        public Config Config;
        public static Plugin Singleton { get; private set; }
        [PluginPriority(PluginAPI.Enums.LoadPriority.Medium)]
        [PluginEntryPoint("CommonUtilities", "1.0", "Common stuff", "ondax")]
        void LoadPlugin()
        {
            Singleton = this;
            EventManager.RegisterEvents<EventHandlers>(this);
            Harmony harmony = new Harmony("commonutilities");
            harmony.PatchAll();
        }
    }
}
