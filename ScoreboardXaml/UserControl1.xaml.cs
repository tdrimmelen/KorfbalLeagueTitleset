
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

namespace ScoreboardV2Xaml
{
    public partial class MainControl : UserControl, IComponentConnector
    {
        private long theAttentionTime = 5;
        private string theScoreboardUrl;
        private string theTimeclockUrl;
        private string theShotclockUrl;
        private string homePrimaryColor;
        private string homeSecundaryColor;
        private string guestPrimaryColor;
        private string guestSecundaryColor;
        private ScoreboardRetriever theScoreboardRetriever;
        private TimeclockRetriever theTimeclockRetriever;
        private ShotclockRetriever theShotclockRetriever;

        public MainControl()
        {
            long aTime = 50;
            this.InitializeComponent();
            Logger.Source = "Scoreboard";
            string configFilename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Scoreboard.config";
            try
            {
                Configuration configuration = new Configuration(configFilename);
                this.theScoreboardUrl = configuration["ScoreboardURL"];
                this.theTimeclockUrl = configuration["TimeclockURL"];
                this.theShotclockUrl = configuration["ShotclockURL"];
                aTime = long.Parse(configuration["refreshTime"]);
                this.theAttentionTime = long.Parse(configuration["attentionTime"]);
                this.homePrimaryColor = configuration[nameof(homePrimaryColor)];
                this.homeSecundaryColor = configuration[nameof(homeSecundaryColor)];
                this.guestPrimaryColor = configuration[nameof(guestPrimaryColor)];
                this.guestSecundaryColor = configuration[nameof(guestSecundaryColor)];
            }
            catch (Exception ex)
            {
                Logger.Log("Could not read config file or property not present'" + configFilename + "': " + ex.Message, EventLogEntryType.Error, 1);
            }
            if (this.theScoreboardUrl != "")
                this.theScoreboardRetriever = new ScoreboardRetriever(this.Dispatcher, new Action<ScoreboardResponse>(this.ScoreboardUpdate), aTime, this.theScoreboardUrl);
            if (this.theTimeclockUrl != "")
                this.theTimeclockRetriever = new TimeclockRetriever(this.Dispatcher, new Action<TimeclockResponse>(this.TimeclockUpdate), aTime, this.theTimeclockUrl);
            if (this.theShotclockUrl != "")
                this.theShotclockRetriever = new ShotclockRetriever(this.Dispatcher, new Action<ShotclockResponse>(this.ShotclockUpdate), aTime, this.theShotclockUrl);
            if (this.homePrimaryColor != "")
                this.HOMETEAMCOLOR.Fill = (Brush)new BrushConverter().ConvertFrom((object)this.homePrimaryColor);
            if (this.homeSecundaryColor != "")
                this.HOMETEAM2NDCOLOR.Background = (Brush)new BrushConverter().ConvertFrom((object)this.homeSecundaryColor);
            if (this.guestPrimaryColor != "")
                this.GUESTTEAMCOLOR.Fill = (Brush)new BrushConverter().ConvertFrom((object)this.guestPrimaryColor);
            if (!(this.guestSecundaryColor != ""))
                return;
            this.GUESTTEAM2NDCOLOR.Background = (Brush)new BrushConverter().ConvertFrom((object)this.guestSecundaryColor);
        }

        private void ScoreboardUpdate(ScoreboardResponse aResponse)
        {
            if (aResponse != null && aResponse.Status == "OK")
            {
                this.HOMESCORE.Text = string.Format("{0}", (object)aResponse.Home);
                this.GUESTSCORE.Text = string.Format("{0}", (object)aResponse.Guest);
            }
            else
            {
                this.HOMESCORE.Text = "-";
                this.GUESTSCORE.Text = "-";
            }
        }

        private void TimeclockUpdate(TimeclockResponse aResponse)
        {
            if (aResponse != null && aResponse.Status == "OK")
                this.TIME.Text = string.Format("{0}", (object)aResponse.Minute) + ":" + string.Format("{0:00}", (object)aResponse.Second);
            else
                this.TIME.Text = "-";
        }

        private void ShotclockUpdate(ShotclockResponse aResponse)
        {
            if (aResponse != null && aResponse.Status == "OK")
            {
                this.SHOTCLOCK.Text = string.Format("{0:00}", (object)aResponse.Time);
                if (aResponse.Time <= this.theAttentionTime)
                    this.SHOTCLOCK.Foreground = (Brush)new SolidColorBrush(Colors.Yellow);
                else
                    this.SHOTCLOCK.Foreground = (Brush)new SolidColorBrush(Colors.White);
            }
            else
                this.SHOTCLOCK.Text = "-";
        }
    }
}
