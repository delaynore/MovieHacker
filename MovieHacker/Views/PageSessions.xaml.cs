using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using MovieHacker.Model;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageSessions.xaml
    /// </summary>
    public partial class PageSessions : Page
    {
        private MHDataBase db;
        public PageSessions()
        {
            InitializeComponent();
            db = new MHDataBase();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var c1 = db.Sessions.Join(db.Movies, s => s.Movie.Id, m => m.Id, (s, m) => new
            {
                Id = s.Id,
                Image = @"C:\Users\delay\source\repos\MovieHacker\MovieHacker\Resources\main_icon.ico",
                MovieName = m.MovieName,
                StartTime = s.StartTime.Value.ToString("f"),
                Price = s.Price,
                CinemaName = s.FilmRoom.Cinema.CinemaName
            }).ToArray();
            listBox1.ItemsSource = c1;

            FilterByFilm.ItemsSource = db.Movies.Select(x => x.MovieName).ToArray().Append("-").Reverse();
            FilterByCinema.ItemsSource = db.Cinemas.Select(x => x.CinemaName).ToArray().Append("-").Reverse();

        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterByCinema.SelectedItem == null) return;
            if (FilterByFilm.SelectedItem == null) return;
            if (FilterByCinema.SelectedItem.ToString() == "-" && FilterByFilm.SelectedItem.ToString() == "-") return;

            string? filterCinema = FilterByCinema.SelectedItem.ToString();
            string? filterFilm = FilterByFilm.SelectedItem.ToString();
            if (filterCinema == null || filterFilm == null) return;


            var c1 = db.Sessions.Join(db.Movies, s => s.Movie.Id, m => m.Id, (s, m) => new
            {
                Id = s.Id,
                Image = @"C:\Users\delay\source\repos\MovieHacker\MovieHacker\Resources\main_icon.ico",
                MovieName = m.MovieName,
                StartTime = s.StartTime.Value.ToString("f"),
                Price = s.Price,
                CinemaName = s.FilmRoom.Cinema.CinemaName
            }).ToArray();

            if (filterCinema != "-")
                c1 = c1.Where(x => x.CinemaName.Contains(filterCinema)).ToArray();
            if (filterFilm != "-")
                c1 = c1.Where(x => x.MovieName.Contains(filterFilm)).ToArray();
            find1.Text = c1.Length.ToString();
            listBox1.ItemsSource = c1;
        }
    }
}
