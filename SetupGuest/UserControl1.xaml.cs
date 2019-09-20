
using DigitsViewLib;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using vMixInterop;

namespace vMixControlLibrary
{
    public partial class ScoreboardControl : UserControl, vMixWPFUserControl, IComponentConnector
    {
        private string homePrimaryColor;
        private string homeSecundaryColor;

        public ScoreboardControl()
        {
            this.InitializeComponent();
            Logger.Source = "Scoreboard";
            string configFilename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Scoreboard.config";
            try
            {
                Configuration configuration = new Configuration(configFilename);
                this.homePrimaryColor = configuration["guestPrimaryColor"];
                this.homeSecundaryColor = configuration["guestSecundaryColor"];
            }
            catch (Exception ex)
            {
                Logger.Log("Could not read config file or property not present'" + configFilename + "': " + ex.Message, EventLogEntryType.Error, 1);
            }
            if (this.homePrimaryColor != "")
                this.TEAMCOLOR.Fill = (Brush)new BrushConverter().ConvertFrom((object)this.homePrimaryColor);
            if (!(this.homeSecundaryColor != ""))
                return;
            this.TEAM2NDCOLOR.Background = (Brush)new BrushConverter().ConvertFrom((object)this.homeSecundaryColor);
        }

        public void Close()
        {
        }

        public TimeSpan GetPosition()
        {
            return new TimeSpan(0, 0, 0);
        }

        public TimeSpan GetDuration()
        {
            return new TimeSpan(0, 0, 0);
        }

        public void Load(int width, int height)
        {
        }

        public void Pause()
        {
        }

        public void Play()
        {
        }

        public void SetPosition(TimeSpan position)
        {
        }

        public void ShowProperties()
        {
        }


    }
}
