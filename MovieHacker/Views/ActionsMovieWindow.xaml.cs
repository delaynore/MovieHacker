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
using Microsoft.EntityFrameworkCore;

namespace MovieHacker.Views
{
    public partial class ActionsMovieWindow : Window
    {
        private readonly IWindowMode<Movie> mode;
        private readonly BoolEvent checker;

       // private ICollection<Genre>? genres;
        private BitmapImage? image1;
        private string? path;
        private bool imageChanged;
        
        public ActionsMovieWindow(IWindowMode<Movie> windowMode)
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += GenresUpdater;
            mode = windowMode;

            image1 = ImageBase64Converter.ToXAMLView(mode.Entity.Picture);
            movieName.Text = mode.Entity!.Title;
            duration.Text = mode.Entity.DurationInMinutes.ToString();
            isActual.IsChecked = mode.Entity.IsActual;
            desc1.Text = mode.Entity.Description;

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

            var genresName = mode.Entity.Genres?.Select(x => x.Name).ToList();
            listBox1.ItemsSource = genresName;
            if (genresName != null)
                comboBoxGenres.ItemsSource = mode.Db.Genres.Where(x => !genresName.Contains(x.Name))?.Select(x => x.Name).ToList();
            else
                comboBoxGenres.ItemsSource = mode.Db.Genres.Select(x => x.Name).ToList();

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
                    mode.Entity.Picture = ImageBase64Converter.ImageToBase64(path);
                mode.Entity.Description = desc1.Text;
                mode.Entity.Title = movieName.Text;
                mode.Entity.DurationInMinutes = int.Parse(duration.Text);
                mode.Entity.IsActual = (bool)(isActual.IsChecked == null ? false : isActual.IsChecked);
                mode.Execute();
                mode.Db.SaveChanges();
            }
            Close();

        }

        private void delGenre_Click(object sender, RoutedEventArgs e)
        {
            var genre = GetGenreFromSelected(listBox1.SelectedItem.ToString());
            if (genre == null) return;
            if (mode.Entity.Genres == null) return;
            var del = mode.Entity.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (del == null) return;
            mode.Entity.Genres.Remove(del);
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
            if (mode.Entity.Genres == null) mode.Entity.Genres = new List<Genre>();
            mode.Entity.Genres.Add(genre);
            checker.Variable = true;
        }
        private Genre? GetGenreFromSelected(string? str)
        {
            if (str == null) return null;
            return mode.Db.Genres.FirstOrDefault(x=> x.Name.CompareTo(str) == 0);
        }
    }
}
