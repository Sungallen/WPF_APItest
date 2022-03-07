using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace TimeReq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(5);
            dt.Tick += Dt_TickAsync;
            dt.Start();
        }

        private async void Dt_TickAsync(object? sender, EventArgs e)
        {
            string project = await GetAPIAsync("http://localhost:3000/api/history/1");
            string[] charstoremove = new string[] { "[", "]" };
            foreach (var c in charstoremove)
            {
                project = project.Replace(c, string.Empty);
            }
            History ans = JsonConvert.DeserializeObject<History>(project);
            String sResult = "API Result on api/article" + Environment.NewLine + "Time" + ans.time + Environment.NewLine + "temp" + ans.temp
                + Environment.NewLine + "HR" + ans.SPO2 + Environment.NewLine + "mouse_id" + ans.u_mouse_id;

            apitext.Text = sResult;
        }
        // time temp HR SPO2 u_mouse_id
        public class History
        {
            public string time { set; get; }
            public int temp { set; get; }
            public int HR { set; get; }
            public int SPO2 { set; get; }
            public int u_mouse_id { set; get; } 
        }
        static async Task<string> GetAPIAsync(string path)

        {
            string project = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)

            {
                project = await response.Content.ReadAsStringAsync();
            }

            return project;

        }
    }
}
