using HandyControl.Controls;
using HandyControl.Themes;
using HandyControl.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Dynamic;
using TextBox = HandyControl.Controls.TextBox;

namespace GUI
{
    public partial class MainWindow
    {
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Items.Clear();
            Seed.Clear();
            Capacity.Clear();
            RichTextBox.Document.Blocks.Clear();
            RichTextBoxItems.Document.Blocks.Clear();
        }


        private void ButtonCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            if (Items.Text == "" || Seed.Text == "" || Capacity.Text == "") return;
            var bagTarget = new Bag(Capacity.Text.ConvertToInt());
            var bagSource = new Bag(Items.Text.ConvertToInt());
            bagSource.GenerateRandomBackpack(Seed.Text.ConvertToInt());
            bagTarget.KnapSack(bagSource);
            RichTextBox.Document.Blocks.Clear();
            RichTextBoxItems.Document.Blocks.Clear();
            RichTextBoxItems.AppendText(bagSource.ToString());
            RichTextBox.AppendText(bagTarget.ToString());
        }
    }
}
