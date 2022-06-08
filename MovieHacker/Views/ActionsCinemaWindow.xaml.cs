using MovieHacker.Model.WindowsModes;
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
        private ICinemaWindowMode _windowMode;
        private ICollection<FilmRoom>? filmRooms;
        private ICollection<FilmRoom>? filmRoomsToDelete;
        private BoolEvent checker;
        public ActionsCinemaWindow(ICinemaWindowMode windowMode)
        {
            InitializeComponent();
            checker = new BoolEvent();
            checker.ForDo += UpdateFilmRooms;

            _windowMode = windowMode;
            desc1.Text = _windowMode.Cinema.Description;
            name1.Text = _windowMode.Cinema.Name;
            button1.Content = _windowMode.ButtonContent;
            desc1.IsReadOnly = _windowMode.IsReadOnly;
            name1.IsReadOnly = _windowMode.IsReadOnly;
            addFilmRoom.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            delFilmRoom.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            filmRoomName.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            filmRoomCapacity.Visibility = _windowMode.IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
            if (_windowMode.Cinema.FilmRooms != null)
                filmRooms = _windowMode.Cinema.FilmRooms;
            checker.Variable = true;
        }
        private void UpdateFilmRooms()
        {
            listBoxFilmRooms.ItemsSource = filmRooms?.Select(x=> $"{x.Name} - {x.Capacity} м.");
            checker.Variable = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_windowMode.IsReadOnly)
            { 
                if(filmRoomsToDelete != null)
                {
                    using var db = new MHDataBase();
                    db.FilmRooms.RemoveRange(filmRoomsToDelete);
                    db.SaveChanges();
                }
                _windowMode.Cinema.FilmRooms = filmRooms;
                _windowMode.Cinema.Name = name1.Text;
                _windowMode.Cinema.Description = desc1.Text;
                _windowMode.Execute();
            }

            Close();
        }

        private void addFilmRoom_Click(object sender, RoutedEventArgs e)
        {
            if (filmRooms == null) filmRooms = new List<FilmRoom>();
            if (filmRooms.Select(x => x.Name).Contains(filmRoomName.Text))
            {
                MessageBox.Show($"{filmRoomName.Text} такой зал уже существует, введите другое имя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
                
            filmRooms.Add(new FilmRoom { Name = filmRoomName.Text, Capacity = int.Parse(filmRoomCapacity.Text), Cinema = _windowMode.Cinema });
            filmRoomName.Text = "";
            filmRoomCapacity.Text = "";
            checker.Variable = true;
        }

        private void delFilmRoom_Click(object sender, RoutedEventArgs e)
        {
            var name = listBoxFilmRooms.SelectedItem?.ToString()?.Split(' ')[0];
            if (name == null) return;
            var item = new MHDataBase().FilmRooms.FirstOrDefault(x => x.Name == name);
            if (filmRoomsToDelete == null) filmRoomsToDelete = new List<FilmRoom>();
            if(item != null)
                filmRoomsToDelete.Add(item);
            filmRooms?.Remove(filmRooms.First(x=>x.Name == name));
            checker.Variable = true;
        }

        private void filmRoomCapacity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
