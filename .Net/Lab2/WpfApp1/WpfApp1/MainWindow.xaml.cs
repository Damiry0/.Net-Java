using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Student> students;
        public List<Beer> beers;

        public MainWindow()
        {
            InitializeComponent();
        }



        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await getResponceTask();
            /*
            string Json =
                File.ReadAllText("students.json");
                */

           // var students = JsonConvert.DeserializeObject<List<Student>>(Json);
            listBox1.Items.Clear();
            foreach (var beer in beers)
            {
                listBox1.Items.Add(beer);
            }
        }

        private async Task<string> getResponceTask()
        {
            var client = new HttpClient();
            string call = "https://rustybeer.herokuapp.com/hops";

            var responce = await client.GetStringAsync(call);
            beers = JsonConvert.DeserializeObject<List<Beer>>(responce);
            return responce;
        }
        
    }
}
