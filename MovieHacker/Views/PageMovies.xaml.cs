using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using MovieHacker.Model.Extensions;

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
                var s = from p in db.Movies
                        select new
                        {
                            Id = p.Id,
                            MovieName = p.Title,
                            Image = ImageBase64Converter.ToXAMLView(p.Picture.Path),
                            Genre = string.Join(", ", p.Genres.Select(x=>x.Genre.Name).ToList()),
                            DurationInMinutes = p.DurationInMinutes
                        };
                listBox1.ItemsSource = s.ToArray();
            }
        }

        private void addMovie_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
