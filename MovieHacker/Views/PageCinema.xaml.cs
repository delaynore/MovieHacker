using MovieHacker.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;
using System;

namespace MovieHacker.Views
{
    public partial class PageCinema : Page
    {
        private BoolEvent checker;
        private CinemaController cinemaController;
        public PageCinema()
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += UpdateCinemas;
            cinemaController = new();
        }
        private void UpdateCinemas()
        {
            var s = cinemaController.GetAll().Select(x => new { x.Id, x.Name }).Where(x => x.Name!.IsContains(textBoxFind.Text));
            listBoCinemas.ItemsSource = s.ToList();
            checker.Variable = false;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            checker.Variable = true;
        }

        private void addCinema_Click(object sender, RoutedEventArgs e)
        {
            new ActionsCinemaWindow(new AddNewCinemaMode(cinemaController)).ShowDialog();
            checker.Variable = true;
        }


        private void editCinema_Click(object sender, RoutedEventArgs e)
        {
            var id = GetIdFromSelectedItem();
            if (!id.HasValue) return;
            var selectedAsCinema = cinemaController.Get(id.Value);
            if (selectedAsCinema == null) return;
            new ActionsCinemaWindow(new EditCinemaMode(selectedAsCinema, cinemaController)).ShowDialog();
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
            var id = GetIdFromSelectedItem();
            if (!id.HasValue) return;
            var selected = cinemaController.Get(id.Value);
            if (selected == null) return;
            if (MessageBox.Show($"Вы действительное хотите удалить кинотеатр - {selected.Name}", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                cinemaController.Remove(selected);
                checker.Variable = true;
            }
        }
        private int? GetIdFromSelectedItem()
        {
            var str = listBoCinemas.SelectedItem.ToString();
            if (str == null) return null;
            var splited = str.Split(',')[0].Split(' ')[^1];
            return Convert.ToInt32(splited);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || btn.Tag == null) return;
            var id = Convert.ToInt32(btn.Tag);
            var cinema = cinemaController.Get(id);
            if (cinema == null) return;
            new ActionsCinemaWindow(new AboutCinemaMode(cinema)).ShowDialog();
        }
    }
}
