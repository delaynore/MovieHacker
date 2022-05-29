using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using MovieHacker.Model.Extensions;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageSessions.xaml
    /// </summary>
    public partial class PageSessions : Page
    {
        private MHDataBase db;
        private AboutSessionWindow? aboutSessionWindow;
        public PageSessions()
        {
            InitializeComponent();
            db = new MHDataBase();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var allMovies = db.Movies.Select(x => x.Title).Distinct().ToList();
            allMovies.Insert(0, "Все фильмы");
            var allCinemas = db.Cinemas.Select(x => x.Name).Distinct().ToList();
            allCinemas.Insert(0, "Все кинотеатры");
            FilterByCinema.ItemsSource = allCinemas;
            FilterByFilm.ItemsSource = allMovies;
            FilterByFilm.SelectedIndex = 0;
            FilterByCinema.SelectedIndex = 0;
            //var sessions = db.Sessions.Include(x => x.FilmRoom).Include(x => x.FilmRoom.Cinema).Include(x => x.Movie).Include(x => x.Movie.Picture);
            ////var c1 = db.Sessions.Join(db.Movies, s => s.Movie.Id, m => m.Id, (s, m) => new
            ////{
            ////    Id = s.Id,
            ////    Image = m.Picture,
            ////    MovieName = m.Title,
            ////    StartTime = s.StartTime.ToLocalTime().ToString("f"),
            ////    Price = s.Price,
            ////    CinemaName = s.FilmRoom.Cinema.Name,
            ////    FreePlaces = s.NumberAvailableSeats
            ////}).ToArray();
            //var c1 = from s in sessions
            //         select new
            //         {
            //             Id = s.Id,
            //             Image = ImageBase64Converter.ToXAMLView(s.Movie.Picture.Path),
            //             MovieName = s.Movie.Title,
            //             StartTime = s.StartTime.ToLocalTime().ToString("f"),
            //             Price = s.Price,
            //             CinemaName = s.FilmRoom.Cinema.Name,
            //             FreePlaces = s.NumberAvailableSeats
            //         };
            //listBox1.ItemsSource = c1.ToArray();

        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterByCinema.SelectedItem == null) return;
            if (FilterByFilm.SelectedItem == null) return;
            if (FilterByCinema.SelectedItem.ToString() == "-" && FilterByFilm.SelectedItem.ToString() == "-") return;

            string? filterCinema = FilterByCinema.SelectedItem.ToString();
            string? filterFilm = FilterByFilm.SelectedItem.ToString();
            if (filterCinema == null || filterFilm == null) return;

            var sessions = db.Sessions.Include(x => x.FilmRoom).Include(x => x.FilmRoom.Cinema).Include(x => x.Movie).Include(x => x.Movie.Picture);
            var c1 = from s in sessions
                     select new
                     {
                         Id = s.Id,
                         Image = ImageBase64Converter.ToXAMLView(s.Movie.Picture.Path),
                         MovieName = s.Movie.Title,
                         StartTime = s.StartTime.ToLocalTime().ToString("f"),
                         Price = s.Price,
                         CinemaName = s.FilmRoom.Cinema.Name,
                         FreePlaces = s.NumberAvailableSeats
                     };
            if (filterCinema != "Все кинотеатры")
                c1 = c1.Where(x => x.CinemaName.Contains(filterCinema));

            if (filterFilm != "Все фильмы")
                c1 = c1.Where(x => x.MovieName.Contains(filterFilm));
            find1.Text = c1.Count().ToString();
            listBox1.ItemsSource = c1.ToArray();
        }

        private void listBox1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (listBox1.SelectedItem == null) return;
            //var se = listBox1.SelectedItem;
            //MessageBox.Show(se.Tag.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && int.TryParse(button.Tag.ToString(), out var tag))
            {
                aboutSessionWindow = new AboutSessionWindow(tag);
                aboutSessionWindow.ShowDialog();
            }
            
           // MessageBox.Show((sender as Button).Tag.ToString());
        }
    }
}
