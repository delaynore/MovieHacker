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
    public partial class PageMovies : Page
    {
        private readonly BoolEvent checker;
        private readonly MovieController movieController;
        public PageMovies()
        {
            InitializeComponent();
            movieController = new();
            checker = new();
            checker.ForDo += UpdateMovies;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }
        private void UpdateMovies()
        {
            var movies = movieController
                .GetAll()
                .Select(x => new
                        {
                            Id = x.Id,
                            MovieName = x.Title,
                            Image = ImageBase64Converter.ToXAMLView(x.Picture!),
                            Genre = string.Join(", ", x.Genres.Select(x => x.Name).ToList()),
                            DurationInMinutes = x.DurationInMinutes,
                            IsActual = x.IsActual.FromBoolToString(),
                            Description = x.Description
                         })
                    .Where(x => x.MovieName.IsContains(finder.Text))
                    .ToList();
                listBox1.ItemsSource = movies;
                checker.Variable = false;
            
        }
        private void addMovie_Click(object sender, RoutedEventArgs e)
        {
            new ActionsMovieWindow(new AddNewMovieMode(movieController)).ShowDialog();
            checker.Variable = true;
        }

        private void updateMovie_Click(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void editMovie_Click(object sender, RoutedEventArgs e)
        {
            var id = GetIdFromSelectedItem();
            if (!id.HasValue) return;
            var selectedMovie = movieController.Get(id.Value);
            if (selectedMovie == null) return;
            new ActionsMovieWindow(new EditMovieMode(selectedMovie, movieController)).ShowDialog();
            checker.Variable = true;
        }

        private void finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            checker.Variable = true;
        }

        private void deleteMovie_Click(object sender, RoutedEventArgs e)
        {
            var id = GetIdFromSelectedItem();
            if (!id.HasValue) return;
            movieController.RemoveAt(id.Value);
            checker.Variable = true;
        }

        private int? GetIdFromSelectedItem()
        {
            var str = listBox1.SelectedItem.ToString();
            if(str == null) return null;
            var splited = str.Split(',')[0].Split(' ')[^1];
            return Convert.ToInt32(splited);
        }
    }
}
