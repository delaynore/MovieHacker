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
        private AboutSessionWindow aboutSessionWindow;
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
                MovieName = m.Title,
                StartTime = s.StartTime.ToString("f"),
                Price = s.Price,
                CinemaName = s.FilmRoom.Cinema.Name,
                FreePlaces = s.NumberAvailableSeats
            }).ToArray();
            listBox1.ItemsSource = c1;

            FilterByFilm.ItemsSource = db.Movies.Select(x => x.Title).ToArray().Append("-").Reverse();
            FilterByCinema.ItemsSource = db.Cinemas.Select(x => x.Name).ToArray().Append("-").Reverse();

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
                MovieName = m.Title,
                StartTime = s.StartTime.ToString("f"),
                Price = s.Price,
                CinemaName = s.FilmRoom.Cinema.Name,
                FreePlaces = s.FilmRoom.Capacity

            }).ToArray();

            if (filterCinema != "-")
                c1 = c1.Where(x => x.CinemaName.Contains(filterCinema)).ToArray();

            if (filterFilm != "-")
                c1 = c1.Where(x => x.MovieName.Contains(filterFilm)).ToArray();
            find1.Text = c1.Length.ToString();
            listBox1.ItemsSource = c1;
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
