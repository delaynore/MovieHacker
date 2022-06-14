using MovieHacker.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;
using System;
using MovieHacker.Model.Tables;

namespace MovieHacker.Views
{
    public partial class PageCinema : Page
    {
        private readonly BoolEvent checker;
        private readonly MHDataBase db;
        public PageCinema()
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += UpdateCinemas;
            db = new();

            addCinema.Visibility = Users.ToVisibility();
            deleteCinema.Visibility = Users.ToVisibility();
            editCinema.Visibility = Users.ToVisibility();
        }
        private void UpdateCinemas()
        {
            var s = db.Cinemas.Include(x=>x.FilmRooms).AsEnumerable().Select(x => new { x.Id, x.Name }).Where(x => x.Name!.IsContains(textBoxFind.Text));
            listBoCinemas.ItemsSource = s.ToList();
            checker.Variable = false;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void addCinema_Click(object sender, RoutedEventArgs e)
        {
            new ActionsCinemaWindow(new AddNewCinemaMode(db)).ShowDialog();
            checker.Variable = true;
        }


        private void editCinema_Click(object sender, RoutedEventArgs e)
        {
            var cinema = GetIdFromSelectedItem();
            if (cinema == null) return;
            new ActionsCinemaWindow(new EditCinemaMode(cinema, db)).ShowDialog();
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

        private void deleteCinema_Click(object sender, RoutedEventArgs e)
        {
            var cinema = GetIdFromSelectedItem();
            if (cinema == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить кинотеатр - {cinema.Name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Remove(cinema);
                db.SaveChanges();
                checker.Variable = true;
            }
        }
        private Cinema? GetIdFromSelectedItem()
        {
            var str = listBoCinemas.SelectedItem.ToString();
            if (str == null) return null;
            var splited = str.Split(',')[0].Split(' ')[^1];
            var id = Convert.ToInt32(splited);
            return db.Cinemas.Find(id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.Tag == null) return;
            var id = Convert.ToInt32(btn.Tag);
            var cinema = db.Cinemas.Find(id);
            if (cinema == null) return;
            new ActionsCinemaWindow(new AboutCinemaMode(cinema)).ShowDialog();
        }
    }
}
