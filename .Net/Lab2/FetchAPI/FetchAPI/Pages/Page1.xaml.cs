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
using HandyControl.Data;
using Newtonsoft.Json;

namespace FetchAPI.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Movie movies { get; set; }

        public Films films { get;}

        public Page1()
        {
            InitializeComponent();
            films = new Films();
            movies = new Movie();
        }

        private void ButtonShowDetailed_OnClick(object sender, RoutedEventArgs e)
        {
            gridFilms.ItemsSource = films.Show();
        }

        private void SearchBarMyList_OnSearchStarted(object? sender, FunctionEventArgs<string> e)
        {
            var myList = films.FindBy(ComboBoxMyList.Text, SearchBarMyList.Text);
            if (myList != null)
            {
                var serialized = JsonConvert.SerializeObject(myList);
                gridFilms.ItemsSource = JsonConvert.DeserializeObject<List<MovieSimplified>>(serialized);
            }
        }
    }
}
