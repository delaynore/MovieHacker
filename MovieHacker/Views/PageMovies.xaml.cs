using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageMovies.xaml
    /// </summary>
    public partial class PageMovies : Page
    {
        private BoolEvent checker;
        public PageMovies()
        {
            InitializeComponent();
            checker = new BoolEvent();
            checker.ForDo += UpdateMovies;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }
        private void UpdateMovies()
        {
            using (var db = new MHDataBase())
            {
                var selected = db.Movies
                    .Include(x => x.Genres)
                    .Select(x => new
                    {
                        Id = x.Id,
                        MovieName = x.Title,
                        Image = ImageBase64Converter.ToXAMLView(x.Picture),
                        Genre = string.Join(", ", x.Genres.Select(x => x.Genre.Name).ToList()),
                        DurationInMinutes = x.DurationInMinutes,
                        IsActual = x.IsActual ? "Да" : "Нет",
                        Description = x.Description
                    })
                    .AsEnumerable()
                    .Where(x => x.MovieName.IsContains(finder.Text))
                    .ToList();
                listBox1.ItemsSource = selected;
                checker.Variable = false;
            }
        }
        private void addMovie_Click(object sender, RoutedEventArgs e)
        {
            new ActionsMovieWindow(new AddNewMovieMode()).ShowDialog();
        }

        private void updateMovie_Click(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void editMovie_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(listBox1.SelectedItem.ToString().Split(',')[0].Split(' ')[^1]);
            new ActionsMovieWindow(new EditMovieMode(new MovieController().Get(id))).ShowDialog();
            checker.Variable = true;
        }

        private void finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            checker.Variable = true;
        }

        private void deleteMovie_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(listBox1.SelectedItem.ToString().Split(',')[0].Split(' ')[^1]);
            var moviecontroller = new MovieController();
            moviecontroller.RemoveAt(id);
            checker.Variable = true;
        }
    }
    public static class Extension
    {
        public static bool IsContains(this string str, string containableString)
        {
            if (str == null || containableString == null) return true;
            if (str.Length == 0 || containableString.Length == 0) return true;
            var str1 = str.ToLower();
            var str2 = containableString.ToLower();
            return str1.Contains(str2);
        }
    }
}
