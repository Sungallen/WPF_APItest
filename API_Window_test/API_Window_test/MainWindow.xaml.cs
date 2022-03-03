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
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace API_Window_test
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
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string project = await GetAPIAsync("http://localhost:3000/api/article");
            string[] charstoremove = new string[] { "[", "]" };
            foreach (var c in charstoremove)
            {
                project = project.Replace(c, string.Empty);
            }
            Project ans = JsonConvert.DeserializeObject<Project>(project);
            String sResult = "API Result on api/article" + Environment.NewLine + "Title"+ ans.article_title + Environment.NewLine + "tag" + ans.article_tag
                + Environment.NewLine + "content" + ans.article_content;

            apitext.Text = sResult;
        }
        public class Project
        {
            public int article_id { get; set; }
            public int user_id { get; set; }
            public string article_title { get; set; }
            public string article_tag { get; set; }
            public string article_content { get; set; }
            public string article_created_time { get; set; }
            public string article_updated_time { get; set; }

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
