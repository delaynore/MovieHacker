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
using MovieHacker.Model;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageSessions.xaml
    /// </summary>
    public partial class PageSessions : Page
    {
        public PageSessions()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new MHDataBase())
            {
                var c1 = db.Sessions.Join(db.Movies, s => s.Movie.Id, m => m.Id, (s, m) => new
                {
                    Id = s.Id,
                    Image = @"C:\Users\delay\source\repos\MovieHacker\MovieHacker\Resources\main_icon.ico",
                    MovieName = m.MovieName,
                    StartTime = s.StartTime,
                    Price = s.Price,
                    CinemaName = s.FilmRoom.Cinema.CinemaName
                }).ToArray();
                listBox1.ItemsSource = c1;
            }
        }
    }
}
