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
        private ICinemaWindowMode mode;
        private ICollection<FilmRoom>? filmRooms;
        //private ICollection<FilmRoom>? filmRoomsToDelete;
        private CinemaController cinemaController;
        private BoolEvent checker;
        public ActionsCinemaWindow(ICinemaWindowMode windowMode)
        {
            InitializeComponent();
            checker = new();
            checker.ForDo += UpdateFilmRooms;
            cinemaController = new();
            mode = windowMode;
            desc1.Text = mode.Cinema.Description;
            name1.Text = mode.Cinema.Name;
            button1.Content = mode.ButtonContent;
            desc1.IsReadOnly = mode.IsReadOnly;
            name1.IsReadOnly = mode.IsReadOnly;
            addFilmRoom.Visibility = mode.IsReadOnly.BoolToVisibility();
            delFilmRoom.Visibility = mode.IsReadOnly.BoolToVisibility();
            filmRoomName.Visibility = mode.IsReadOnly.BoolToVisibility();
            filmRoomCapacity.Visibility = mode.IsReadOnly.BoolToVisibility();
            filmRooms = mode.Cinema.FilmRooms;
            checker.Variable = true;
        }
        private void UpdateFilmRooms()
        {
            listBoxFilmRooms.ItemsSource = filmRooms?.Select(x=> $"{x.Name} - {x.Capacity} м.");
            checker.Variable = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!mode.IsReadOnly)
            {
                //if(filmRoomsToDelete != null)
                //{
                //    using var db = new MHDataBase();
                //    db.FilmRooms.RemoveRange(filmRoomsToDelete);
                //    db.SaveChanges();
                //}
                if (mode.Cinema.Id == 0)
                    mode.Cinema.FilmRooms = filmRooms;
                mode.Cinema.Name = name1.Text;
                mode.Cinema.Description = desc1.Text;
                mode.Execute();
            }

            Close();
        }

        private void addFilmRoom_Click(object sender, RoutedEventArgs e)
        {
            if (filmRooms == null) filmRooms = new List<FilmRoom>();
            if (filmRooms.Select(x => x.Name).IsAlreadyExistInEnumerable(filmRoomName.Text))
            {
                MessageBox.Show($"{filmRoomName.Text} такой зал уже существует, введите другое имя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
                
            filmRooms.Add(new FilmRoom { Name = filmRoomName.Text, Capacity = int.Parse(filmRoomCapacity.Text), Cinema = mode.Cinema });
            filmRoomName.Text = "";
            filmRoomCapacity.Text = "";
            checker.Variable = true;
        }

        private void delFilmRoom_Click(object sender, RoutedEventArgs e)
        {
            var name = GetNameFromSelected();
            if (name == null) return;
            filmRooms?.Remove(filmRooms.First(x=>x.Name == name));
            checker.Variable = true;
        }

        private void filmRoomCapacity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private string GetNameFromSelected()
        {
            var selected = listBoxFilmRooms.SelectedItem.ToString();
            if (selected == null) return null;
            var splited = selected.Split(' ')[0];
            return splited;
        }
    }
}
