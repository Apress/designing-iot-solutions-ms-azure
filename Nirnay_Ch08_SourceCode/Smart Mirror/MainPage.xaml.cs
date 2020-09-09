using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace IoT.Solutions.SmartIndustrialApplications.SmartMirror
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Events> EventList { get; } = new ObservableCollection<Events>();

        public MainPage()
        {
            this.InitializeComponent();

            txtClock.Text = DateTime.Now.ToString("hh:mm");

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://sports.yahoo.com/nfl/teams/cin/ical.ics");
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponseAsync().Result;
            using (StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                Ical.Net.Calendar calendar = Ical.Net.Calendar.Load(sr.ReadToEnd());
                foreach (var events in calendar.Events)
                {
                    EventList.Add(new Events() { Datetime = events.DtStart.ToString("dd-MM hh:mm", null), Summary = events.Summary });
                }
            }
        }
    }
}
