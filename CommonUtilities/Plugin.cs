using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
