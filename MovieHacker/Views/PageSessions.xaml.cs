using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using MovieHacker.Model.Extensions;
using MovieHacker.Model.Tables;
using MovieHacker.Model.WindowsModes;

namespace MovieHacker.Views
{
    public partial class PageSessions : Page
    {
        private readonly MHDataBase db;
        private AboutSessionWindow? aboutSessionWindow;
        private BoolEvent checker;
        public PageSessions()
        {
            InitializeComponent();
            db = new MHDataBase();
            checker = new BoolEvent();
            checker.ForDo += SessionUpdater;
            gridAdmin.Visibility = Users.ToVisibility();
            if (Users.Role == UsersRoles.User) rowAdmin.Height = new GridLength(0);
            else rowAdmin.Height = new GridLength(50);
        }
        private void SessionUpdater()
        {
           
            if (FilterByCinema.SelectedItem == null) return;
            if (FilterByFilm.SelectedItem == null) return;

            string? filterCinema = FilterByCinema.SelectedItem.ToString();
            string? filterFilm = FilterByFilm.SelectedItem.ToString();
            if (filterCinema == null || filterFilm == null) return;
            var date = FilterByDate.SelectedDate;
            var sessions = db.Sessions.Include(x => x.FilmRoom).Include(x => x.FilmRoom.Cinema).Include(x => x.Movie).AsEnumerable();
            if (actualToSee.IsChecked == false)
                sessions = sessions.Where(x => date == null || x.StartTime.Date == date.Value.Date);
            else
                sessions = sessions.Where(x => x.StartTime >= DateTime.Now && x.NumberAvailableSeats > 0);
            var c1 = from s in sessions
                     select new
                     {
                         Id = s.Id,
                         Image = ImageBase64Converter.ToXAMLView(s.Movie.Picture),
                         MovieName = s.Movie.Title,
                         StartTime = s.StartTime.ToString("f"),
                         Price = s.Price.ToString("C"),
                         CinemaName = s.FilmRoom.Cinema.Name,
                         FreePlaces = s.NumberAvailableSeats
                     };
            if (filterCinema != "Все кинотеатры")
                c1 = c1.Where(x => x.CinemaName.Contains(filterCinema));

            if (filterFilm != "Все фильмы")
                c1 = c1.Where(x => x.MovieName.Contains(filterFilm));
            listBox1.ItemsSource = c1.ToArray();
            checker.Variable = false;
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
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checker.Variable = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && int.TryParse(button.Tag.ToString(), out var tag))
            {
                aboutSessionWindow = new AboutSessionWindow(tag, db);
                aboutSessionWindow.ShowDialog();
                checker.Variable = true;
            }
        }

        private void addSession_Click(object sender, RoutedEventArgs e)
        {
            new AddAndEditSessionWindow(new AddNewSession(db)).ShowDialog();
            checker.Variable = true;
        }

        private void editSession_Click(object sender, RoutedEventArgs e)
        {
            var session = GetSessionFromString();
            if (session == null) return;
            new AddAndEditSessionWindow(new EditSession(db, session)).ShowDialog();
            checker.Variable = true;

        }

        private void delSession_Click(object sender, RoutedEventArgs e)
        {
            var session = GetSessionFromString();
            if (session == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить сеанс?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Sessions.Remove(session);
                db.SaveChanges();
                checker.Variable = true;
            }
        }

        private Session? GetSessionFromString()
        {
            var selected = listBox1.SelectedItem.ToString();
            if (selected == null) return null;
            var strId = selected.Split(',')[0].Split(' ').Last();
            int id = Convert.ToInt32(strId);
            return db.Sessions.Find(id);
        }

        private void FilterByDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            checker.Variable = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FilterByDate.SelectedDate = null;
            FilterByFilm.SelectedIndex = 0;
            FilterByCinema.SelectedIndex = 0;
            actualToSee.IsChecked = true;
            checker.Variable = true;
        }

        private void actualToSee_Click(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }
    }
}
