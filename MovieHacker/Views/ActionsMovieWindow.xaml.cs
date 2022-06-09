using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;
using MovieHacker.Model.Tables;
using MovieHacker.Model;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing.Imaging;

namespace MovieHacker.Views
{
    public partial class ActionsMovieWindow : Window
    {
        private readonly IMovieWindowMode mode;
        private readonly BoolEvent checker;

        private ICollection<Genre> genres;
        private BitmapImage? image1;
        private string? path;
        private bool imageChanged;
        private GenreController genreController;
        public ActionsMovieWindow(IMovieWindowMode windowMode)
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += GenresUpdater;
            mode = windowMode;
            genreController = new();
            if (mode.Movie.Picture != null)
                image1 = ImageBase64Converter.ToXAMLView(mode.Movie.Picture);
            movieName.Text = mode.Movie!.Title;
            duration.Text = mode.Movie.DurationInMinutes.ToString();
            isActual.IsChecked = mode.Movie.IsActual;
            desc1.Text = mode.Movie.Description;
            genres = mode.Movie.Genres;
            changeImg.Visibility = mode.IsReadOnly.BoolToVisibility();
            delImg.Visibility = mode.IsReadOnly.BoolToVisibility();
            addGenre.Visibility = mode.IsReadOnly.BoolToVisibility();
            delGenre.Visibility = mode.IsReadOnly.BoolToVisibility();
            movieName.IsReadOnly = mode.IsReadOnly;
            duration.IsReadOnly = mode.IsReadOnly;
            isActual.IsEnabled = !mode.IsReadOnly;
            desc1.IsReadOnly = mode.IsReadOnly;
            button1.Content = mode.ButtonContent;
            checker.Variable = true;
            imageChanged = false;
        }
        private void GenresUpdater()
        {
            image.Source = image1;
            comboBoxGenres.SelectedIndex = -1;

            var genresName = genres?.Select(x => x.Name).ToList();
            listBox1.ItemsSource = genresName;
            comboBoxGenres.ItemsSource = genreController.GetAll().Where(x =>
            {
                if (genresName == null) return true;
                return !genresName.Contains(x.Name);
            }).Select(x=>x.Name);

            checker.Variable = false;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (!mode.IsReadOnly)
            {
                if(imageChanged)
                    mode.Movie.Picture = ImageBase64Converter.ImageToBase64(path);
                mode.Movie.Genres = null;
                mode.Movie.Description = desc1.Text;
                mode.Movie.Title = movieName.Text;
                mode.Movie.DurationInMinutes = int.Parse(duration.Text);
                mode.Movie.IsActual = (bool)(isActual.IsChecked == null ? false : isActual.IsChecked);
                mode.Execute();
                Movie movie;
                if (mode.Movie.Id == 0)
                {
                    movie = mode.MovieController.GetAll().Last();
                }
                else
                {
                    movie = mode.MovieController.Get(mode.Movie.Id);
                }
                foreach (var item in genres)
                {
                    if (item.Movies == null) item.Movies = new List<Movie>();
                    item.Movies.Add(movie);
                }
                mode.MovieController.SaveChanges();
            }
            Close();

        }

        private void delGenre_Click(object sender, RoutedEventArgs e)
        {
            if (genres == null) return;
            var genre = GetGenreFromSelected(listBox1.SelectedItem.ToString());
            if (genre == null) return;
            var del = genres.FirstOrDefault(x => x.Id == genre.Id);
            if (del == null) return;
            genres.Remove(del);
            checker.Variable = true;
        }

        private void delImg_Click(object sender, RoutedEventArgs e)
        {
            image1 = null;
            path = null;
            imageChanged = true;
            checker.Variable = true;
        }

        private void changeImg_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Image (*.jpg)|*.jpg"
            };
            if(ofd.ShowDialog()== true)
            {
                image1 = new BitmapImage(new Uri(ofd.FileName));
                path = ofd.FileName;
                imageChanged = true;
                checker.Variable = true;
            }
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            var genre = GetGenreFromSelected(comboBoxGenres.SelectedItem.ToString());
            if (genre == null) return;
            if (genres == null) genres = new List<Genre>();
            genres.Add(genre);
            checker.Variable = true;
        }
        private Genre? GetGenreFromSelected(string? str)
        {
            if (str == null) return null;
            return genreController.Get(str);
        }
    }
}
