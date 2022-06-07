using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>
    /// Логика взаимодействия для ActionsMovieWindow.xaml
    /// </summary>
    public partial class ActionsMovieWindow : Window
    {
        private IMovieWindowMode _windowMode;
        private BoolEvent checker;
        private List<MovieToGenre>? _genres;
        private BitmapImage? image1;
        private string? path;
        private bool imageChanged;
        public ActionsMovieWindow(IMovieWindowMode windowMode)
        {
            InitializeComponent();
            checker = new BoolEvent();
            checker.ForDo += GenresUpdater;
            _windowMode = windowMode;
            if (windowMode.Movie != null) {
                image1 = ImageBase64Converter.ToXAMLView(_windowMode.Movie?.Picture);
                movieName.Text = _windowMode.Movie!.Title;
                duration.Text = _windowMode.Movie.DurationInMinutes.ToString();
                isActual.IsChecked = _windowMode.Movie.IsActual;
                desc1.Text = _windowMode.Movie.Description;
                //listBox1.ItemsSource = _windowMode.Movie.Genres != null ? _windowMode.Movie.Genres.Select(x => x.Genre.Name).ToList() : null;
                _genres = new List<MovieToGenre>();// null изиенить добавить провкрке
                if(_windowMode.Movie.Genres != null)
                    _genres.AddRange(_windowMode.Movie.Genres);
            }
            changeImg.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            delImg.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            addGenre.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            delGenre.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            movieName.IsReadOnly = _windowMode.IsReadOnly;
            duration.IsReadOnly = _windowMode.IsReadOnly;
            isActual.IsEnabled = !_windowMode.IsReadOnly;
            desc1.IsReadOnly = _windowMode.IsReadOnly;
            button1.Content = _windowMode.ButtonContent;
            checker.Variable = true;
            imageChanged = false;
        }
        private void GenresUpdater()
        {
            image.Source = image1;
            comboBoxGenres.SelectedIndex = -1;
            var genres = _genres.Select(x => x.Genre.Name).ToList();
            listBox1.ItemsSource = genres;
            comboBoxGenres.ItemsSource = new GenreController().GetAll(x => !genres.Contains(x.Name)).Select(x=>x.Name);
            checker.Variable = false;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (!_windowMode.IsReadOnly)
            {
               
                _windowMode.Movie.Genres = _genres;
                if (imageChanged)
                {
                    if (path == null)
                        _windowMode.Movie.Picture = null;
                    else _windowMode.Movie.Picture = ImageBase64Converter.ImageToBase64(path);
                }
                    
                _windowMode.Movie.Description = desc1.Text;
                _windowMode.Movie.Title = movieName.Text;
                _windowMode.Movie.DurationInMinutes = int.Parse(duration.Text);
                _windowMode.Movie.IsActual = (bool)(isActual.IsChecked == null ? false : isActual.IsChecked);
                _windowMode.Execute();
            }
            Close();
        }

        private void delGenre_Click(object sender, RoutedEventArgs e)
        {
            
            var selected = new GenreController().Get(listBox1.SelectedItem.ToString()!);
            var del = _genres.FirstOrDefault(x => x.Genre.Id == selected.Id);
            if (del == null) return;
            _genres.Remove(del);
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
            _genres.Add(new MovieToGenre { Movie = _windowMode.Movie, Genre = new GenreController().Get(comboBoxGenres.SelectedItem.ToString())});
            checker.Variable = true;
        }
    }
}
