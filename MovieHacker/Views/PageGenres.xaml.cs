using Microsoft.EntityFrameworkCore;
using MovieHacker.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MovieHacker.Model.WindowsModes;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageGenres.xaml
    /// </summary>
    public partial class PageGenres : Page
    {
        private BoolEvent checker;
        public PageGenres()
        {
            InitializeComponent();
            checker = new BoolEvent();
            checker.ForDo += UpdateGenres;
        }

        private void UpdateGenres()
        {
            using (var db = new MHDataBase())
            {
                db.Genres.Load();
                listBoGenres.ItemsSource = db.Genres.Local.Select(x => x.Name).ToList();
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            new CreateAndEditGenreWindow(new AddNewGenreMode()).ShowDialog();
        }

        private void editGenre_Click(object sender, RoutedEventArgs e)
        {
            var s = new MHDataBase().Genres.Where(x=>x.Name == listBoGenres.SelectedItem.ToString()).First();
            if (s == null) return;
            new CreateAndEditGenreWindow(new EditGenreMode(s.Id, s.Name)).ShowDialog();
        }
    }
}
