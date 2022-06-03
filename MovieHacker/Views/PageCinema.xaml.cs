using MovieHacker.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.WindowsModes;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для PageCinema.xaml
    /// </summary>
    public partial class PageCinema : Page
    {
        private BoolEvent checker;
        public PageCinema()
        {
            InitializeComponent();
            checker = new BoolEvent();
            checker.ForDo += UpdateCinemas;
        }
        private void UpdateCinemas()
        {
            using (var db = new MHDataBase())
            {
                db.Cinemas.Load();
                var s = db.Cinemas.Local.Select(x => new { x.Id, x.Name }).Where(x=>x.Name.ToLower().Contains(textBoxFind.Text.ToLower()));
                listBoCinemas.ItemsSource = s.ToList();
                checker.Variable = false;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void addCinema_Click(object sender, RoutedEventArgs e)
        {
            new ActionsCinemaWindow(new AddNewCinemaMode()).ShowDialog();
            checker.Variable = true;
        }


        private void editCinema_Click(object sender, RoutedEventArgs e)
        {
            if (listBoCinemas.SelectedItem == null) return;
            var selected = listBoCinemas.SelectedItem.ToString();
            if (selected == null) return;
            var id = int.Parse(selected.Split(',')[0].Split(' ')[^1]);

            using var db = new MHDataBase();
            var selectedAsCinema = db.Cinemas.Where(x => x.Id == id).First();
            new ActionsCinemaWindow(new EditCinemaMode(selectedAsCinema)).ShowDialog();
            checker.Variable = true;
        }

        private void updateCinema_Click(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }


        private void textBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            checker.Variable = true;
        }

        private void deleteGenre_Click(object sender, RoutedEventArgs e)
        {
            if (listBoCinemas.SelectedItem == null) return;
            var id = int.Parse(listBoCinemas.SelectedItem.ToString().Split(',')[0].Split(' ')[^1]);
            var selected = new MHDataBase().Cinemas.Where(x => x.Id == id).First();
            if (selected == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить кинотеатр - {selected.Name}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                new CinemaController().RemoveAt(selected.Id);
                checker.Variable = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.Tag == null) return;
            var tag = int.Parse(btn.Tag.ToString());
            new ActionsCinemaWindow(new AboutCinemaMode(new MHDataBase().Cinemas.Where(x=>x.Id == tag).First())).ShowDialog();
        }
    }
}
