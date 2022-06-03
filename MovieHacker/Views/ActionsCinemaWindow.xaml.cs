using MovieHacker.Model.WindowsModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для ActionsCinemaWindow.xaml
    /// </summary>
    public partial class ActionsCinemaWindow : Window
    {
        private ICinemaWindowMode _windowMode;
        public ActionsCinemaWindow(ICinemaWindowMode windowMode)
        {
            InitializeComponent();
            _windowMode = windowMode;
            desc1.Text = _windowMode.DescriptionCinema;
            name1.Text = _windowMode.CinemaName;
            button1.Content = _windowMode.ButtonContent;
            desc1.IsReadOnly = _windowMode.IsReadOnly;
            name1.IsReadOnly = _windowMode.IsReadOnly;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _windowMode.CinemaName = name1.Text;
            _windowMode.DescriptionCinema = desc1.Text;
            _windowMode.Execute();
            Close();
        }
    }
}
