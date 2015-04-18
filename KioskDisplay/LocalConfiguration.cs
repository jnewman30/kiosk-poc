using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Westwind.Utilities.Configuration;

namespace KioskDisplay
{
    public class LocalConfiguration : AppConfiguration
    {
        public string SettingsCode { get; set; }
        public double InactivityTimerInterval { get; set; }
        public double AutoScrollInterval { get; set; }
        public double VideoTransitionDelay { get; set; }
        public double InactiveVolume { get; set; }
        public double ActiveVolume { get; set; }
        public bool ReplaceWindowsShell { get; set; }

        public LocalConfiguration()
        {
            // Defaults
            SettingsCode = "1234";
            InactivityTimerInterval = 1.0d;
            AutoScrollInterval = 0.5d;
            VideoTransitionDelay = 3;
            InactiveVolume = 0.25d;
            ActiveVolume = 0.5d;
            ReplaceWindowsShell = false;
        }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            return new ConfigurationFileConfigurationProvider<LocalConfiguration>()
            {
                ConfigurationSection = "LocalConfiguration",
                ConfigurationFile = "./Settings.xml"
            };
        }

        private static LocalConfiguration _configuration;
        private static object _lockObj = new object();

        internal static LocalConfiguration Settings
        {
            get
            {
                lock (_lockObj)
                {
                    if (_configuration == null)
                    {
                        _configuration = new LocalConfiguration();
                        _configuration.Initialize();
                    }
                    return _configuration;
                }
            }
        }
    }
}
