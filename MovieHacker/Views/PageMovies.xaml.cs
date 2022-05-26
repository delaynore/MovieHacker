using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using MovieHacker.Model;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageMovies.xaml
    /// </summary>
    public partial class PageMovies : Page
    {
        public PageMovies()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new MHDataBase())
            {
                //var films = db.Movies.Join(db.Genres, m => m.Genres, g => g., (m, g) => new
                //{
                //    Id = m.Id,
                //    Image = @"C:\Users\delay\source\repos\MovieHacker\MovieHacker\Resources\main_icon.ico",
                //    MovieName = m.Title,
                //    Genre = g.Name,
                //    DurationInMinutes = m.DurationInMinutes
                //}).ToArray();
                //listBox1.ItemsSource = films;
                //db.
               //var fi = from p in db.Movies
               //         join g in db.Genres on p.Genres.
            }
        }
    }
}
