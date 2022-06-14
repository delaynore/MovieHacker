using MovieHacker.Model.WindowsModes;
using MovieHacker.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MovieHacker.Model.Tables;
using MovieHacker.Model;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для ActionsCinemaWindow.xaml
    /// </summary>
    public partial class ActionsCinemaWindow : Window
    {
        private IWindowMode<Cinema> mode;
        private readonly BoolEvent checker;
        public ActionsCinemaWindow(IWindowMode<Cinema> windowMode)
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += UpdateFilmRooms;
            mode = windowMode;

            desc1.Text = mode.Entity.Description;
            name1.Text = mode.Entity.Name;

            button1.Content = mode.ButtonContent;
            desc1.IsReadOnly = mode.IsReadOnly;
            name1.IsReadOnly = mode.IsReadOnly;
            addFilmRoom.Visibility = mode.IsReadOnly.BoolToVisibility();
            delFilmRoom.Visibility = mode.IsReadOnly.BoolToVisibility();
            filmRoomName.Visibility = mode.IsReadOnly.BoolToVisibility();
            filmRoomCapacity.Visibility = mode.IsReadOnly.BoolToVisibility();
            checker.Variable = true;
        }
        private void UpdateFilmRooms()
        {
            listBoxFilmRooms.ItemsSource = mode.Entity.FilmRooms?.Select(x=> $"{x.Name} - {x.Capacity} м.");
            checker.Variable = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!mode.IsReadOnly)
            {
                mode.Entity.Name = name1.Text;
                mode.Entity.Description = desc1.Text;
                mode.Execute();
                mode.Db.SaveChanges();
            }

            Close();
        }

        private void addFilmRoom_Click(object sender, RoutedEventArgs e)
        {
            if (mode.Entity.FilmRooms == null) mode.Entity.FilmRooms = new List<FilmRoom>();
            if (mode.Entity.FilmRooms.Select(x => x.Name).IsAlreadyExistInEnumerable(filmRoomName.Text))
            {
                MessageBox.Show($"{filmRoomName.Text} такой зал уже существует, введите другое имя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            mode.Entity.FilmRooms.Add(new FilmRoom { Name = filmRoomName.Text, Capacity = int.Parse(filmRoomCapacity.Text), Cinema = mode.Entity });
            filmRoomName.Text = "";
            filmRoomCapacity.Text = "";
            checker.Variable = true;
        }

        private void delFilmRoom_Click(object sender, RoutedEventArgs e)
        {
            var filmRoom = GetNameFromSelected();
            if (filmRoom == null) return;
            mode.Entity.FilmRooms?.Remove(filmRoom);
            checker.Variable = true;
        }

        private void filmRoomCapacity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private FilmRoom? GetNameFromSelected()
        {
            var selected = listBoxFilmRooms.SelectedItem.ToString();
            if (selected == null) return null;
            var splited = selected.Split(' ')[0];
            return mode.Db.FilmRooms.FirstOrDefault(x=>x.Name.CompareTo(splited) == 0);
        }
    }
}
