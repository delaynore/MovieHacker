using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MovieHacker.Model;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для AboutSessionWindow.xaml
    /// </summary>
    public partial class AboutSessionWindow : Window
    {
        public AboutSessionWindow(int id)
        {
            InitializeComponent();
            using (var db = new MHDataBase())
            {
                var x = from s in db.Sessions
                        where s.Id == id
                        select new
                        {
                            MovieName = s.Movie.MovieName,
                            CinemaName = s.FilmRoom.Cinema.CinemaName,
                            StartTime = s.StartTime,
                            Price = s.Price,
                            FreePlaces = s.NumberAvailableSeats
                        };
                listBox1.ItemsSource = x.ToArray();
            }
        }
    }
}
