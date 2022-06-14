using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;
using MovieHacker.Model.Tables;

namespace MovieHacker.Views
{
    public partial class PageMovies : Page
    {
        private readonly BoolEvent checker;
        private readonly MHDataBase db;
        public PageMovies()
        {
            InitializeComponent();
            db = new();
            checker = new();
            checker.ForDo += UpdateMovies;

            addMovie.Visibility = Users.ToVisibility();
            deleteMovie.Visibility = Users.ToVisibility();
            editMovie.Visibility = Users.ToVisibility();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }
        private void UpdateMovies()
        {
            var movies = db.Movies.Include(x => x.Genres).AsEnumerable().Where(x=>x.Title.IsContains(finder.Text))
                .Select(x => new
                {
                    Id = x.Id,
                    MovieName = x.Title,
                    Image = ImageBase64Converter.ToXAMLView(x.Picture!),
                    Genre = string.Join(", ", x.Genres!.Select(x => x.Name).ToList()),
                    DurationInMinutes = x.DurationInMinutes,
                    IsActual = x.IsActual.FromBoolToString(),
                    Description = x.Description
                });
                listBox1.ItemsSource = movies.ToList();
                checker.Variable = false;
            
        }
        private void addMovie_Click(object sender, RoutedEventArgs e)
        {
            new ActionsMovieWindow(new AddNewMovieMode(db)).ShowDialog();
            checker.Variable = true;
        }

        private void updateMovie_Click(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void editMovie_Click(object sender, RoutedEventArgs e)
        {
            var movie = GetMovieFromSelectedItem();
            if (movie == null) return;
            new ActionsMovieWindow(new EditMovieMode(movie, db)).ShowDialog();
            checker.Variable = true;
        }

        private void finder_TextChanged(object sender, TextChangedEventArgs e)
        {
            checker.Variable = true;
        }

        private void deleteMovie_Click(object sender, RoutedEventArgs e)
        {
            var movie = GetMovieFromSelectedItem();
            if (movie == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить кино - {movie.Title}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
                checker.Variable = true;
            } 
        }

        private Movie? GetMovieFromSelectedItem()
        {
            var str = listBox1.SelectedItem.ToString();
            if(str == null) return null;
            var splited = str.Split(',')[0].Split(' ')[^1];
            var id = Convert.ToInt32(splited);
            return db.Movies.Find(id);
        }
    }
}
