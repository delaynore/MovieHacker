using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
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

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageGenres.xaml
    /// </summary>
    public partial class PageGenres : Page
    {
        public PageGenres()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new MHDataBase())
            {
                listBoGenres.ItemsSource = db.Genres.Select(x => x.Name).ToArray();
            }
        }
               
    }
}
