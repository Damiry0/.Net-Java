using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HandyControl.Controls;
using HandyControl.Themes;
using HandyControl.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using TextBox = HandyControl.Controls.TextBox;

namespace FetchAPI
{
    public partial class MainWindow
    {

        public Movie movies { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Change Theme
        private void ButtonConfig_OnClick(object sender, RoutedEventArgs e) => PopupConfig.IsOpen = true;

        private void ButtonSkins_OnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button)
            {
                PopupConfig.IsOpen = false;
                if (button.Tag is ApplicationTheme tag)
                {
                    ((App)Application.Current).UpdateTheme(tag);
                }
                else if (button.Tag is Brush accentTag)
                {
                    ((App)Application.Current).UpdateAccent(accentTag);
                }
                else if (button.Tag is "Picker")
                {
                    var picker = SingleOpenHelper.CreateControl<ColorPicker>();
                    var window = new PopupWindow
                    {
                        PopupElement = picker,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        AllowsTransparency = true,
                        WindowStyle = WindowStyle.None,
                        MinWidth = 0,
                        MinHeight = 0,
                        Title = "Select Accent Color"
                    };

                    picker.SelectedColorChanged += delegate
                    {
                        ((App)Application.Current).UpdateAccent(picker.SelectedBrush);
                        window.Close();
                    };
                    picker.Canceled += delegate { window.Close(); };
                    window.Show();
                }
            }
        }
        #endregion

        private async void ButtonSearchFilm_OnClick(object sender, RoutedEventArgs e)
        {
            var searchItem = HttpUtility.UrlEncode(TextBoxMain.Text);
            await getResponceTask(searchItem);
            if (movies != null)
            {
                listBoxMain.Items.Clear();
                listBoxMain.Items.Add(movies);
                DisplayPoster();
            }
        }

        private async Task<string> getResponceTask(string request)
        {
            var client = new HttpClient();
            string call = $"http://www.omdbapi.com/?t={request}&apikey=82c88151";

            var responce = await client.GetStringAsync(call);
            movies = JsonConvert.DeserializeObject<Movie>(responce);
            return responce;
        }

        public void DisplayPoster()
        {
            var image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(movies.Poster, UriKind.Absolute);
            bitmap.EndInit();
            ImagePoster.Source = bitmap;
        }
        
    }
}
