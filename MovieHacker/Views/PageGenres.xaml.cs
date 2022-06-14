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
        private readonly MHDataBase db;
        public PageGenres()
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += UpdateGenres;
            db = new();

            addGenre.Visibility = Users.ToVisibility();
            deleteGenre.Visibility = Users.ToVisibility();
            editGenre.Visibility = Users.ToVisibility();
        }

        private void UpdateGenres()
        {
            listBoGenres.ItemsSource = db.Genres.AsEnumerable().Select(x => x.Name).Where(x => x.IsContains(textBoxFind.Text)).ToList();
            checker.Variable = false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            new CreateAndEditGenreWindow(new AddNewGenreMode(db)).ShowDialog();
            checker.Variable = true;
        }

        private void editGenre_Click(object sender, RoutedEventArgs e)
        {
            var genre = GetGenreByName();
            if (genre == null) return;
            new CreateAndEditGenreWindow(new EditGenreMode(genre, db)).ShowDialog();
            checker.Variable = true;
        }

        private void deleteGenre_Click(object sender, RoutedEventArgs e)
        {
            var s = GetGenreByName();
            if (s == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить жанр - {s.Name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Remove(s);
                db.SaveChanges();
                checker.Variable = true;
            }
                
        }
        private Genre? GetGenreByName()
        {
            var genreName = listBoGenres.SelectedItem.ToString();
            if (genreName == null) return null;
            return db.Genres.FirstOrDefault(x=>x.Name.CompareTo(genreName) == 0);
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
