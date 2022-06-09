using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;
using MovieHacker.Model.Tables;

namespace MovieHacker.Views
{
    public partial class PageGenres : Page
    {
        private readonly BoolEvent checker;
        private readonly GenreController genreController;
        public PageGenres()
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += UpdateGenres;
            genreController = new();
        }

        private void UpdateGenres()
        {
            listBoGenres.ItemsSource = genreController.GetAll().Select(x => x.Name).Where(x => x.IsContains(textBoxFind.Text)).ToList();
            checker.Variable = false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            new CreateAndEditGenreWindow(new AddNewGenreMode(genreController)).ShowDialog();
            checker.Variable = true;
        }

        private void editGenre_Click(object sender, RoutedEventArgs e)
        {
            var genre = GetGenreByName();
            if (genre == null) return;
            new CreateAndEditGenreWindow(new EditGenreMode(genre, genreController)).ShowDialog();
            checker.Variable = true;
        }

        private void deleteGenre_Click(object sender, RoutedEventArgs e)
        {
            var s = GetGenreByName();
            if (s == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить жанр - {s.Name}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                genreController.Remove(s);
                checker.Variable = true;
            }
                
        }
        private Genre? GetGenreByName()
        {
            var genreName = listBoGenres.SelectedItem.ToString();
            if (genreName == null) return null;
            return genreController.Get(genreName);
        }
        private void updateGenre_Click(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void TextBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            checker.Variable = true;
        }
    }
}
