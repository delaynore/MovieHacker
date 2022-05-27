using MovieHacker.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageCinema.xaml
    /// </summary>
    public partial class PageCinema : Page
    {
        public PageCinema()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new MHDataBase())
            {
                listBoCinemas.ItemsSource = db.Cinemas.Select(x => x.Name).ToArray();
            }
        }

        private void addCinema_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContentPresenter_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
