// Decompiled with JetBrains decompiler
// Type: vMixControlLibrary.ScoreboardControl
// Assembly: vMixScoreboardLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21DC743A-1377-49AD-8A30-439EAF611035
// Assembly location: C:\Users\Theo van Drimmelen\Documents\vmix titles & layout\SetupHome.dll

using DigitsViewLib;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
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
            string configFilename = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Scoreboard.config";
            try
            {
                Configuration configuration = new Configuration(configFilename);
                this.homePrimaryColor = configuration[nameof(homePrimaryColor)];
                this.homeSecundaryColor = configuration[nameof(homeSecundaryColor)];
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
