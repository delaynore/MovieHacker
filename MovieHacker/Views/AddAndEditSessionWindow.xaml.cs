using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;
using MovieHacker.Model.WindowsModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditSessionWindow.xaml
    /// </summary>
    public partial class AddAndEditSessionWindow : Window
    {
        private List<Cinema> cinemas;
        private List<Movie> movies;
        private List<FilmRoom>? filmRooms;
        private readonly IWindowMode<Session> mode;
        private bool isEdit;
        public AddAndEditSessionWindow(IWindowMode<Session> windowMode)
        {
            InitializeComponent();
            mode = windowMode;
            var min = new DateTime(1970, 1, 1, 0, 0, 0);
            dateTime.Minimum = min;
            button.Content = mode.ButtonContent;
            price.Text = mode.Entity.Price.ToString();
            if (mode.Entity.StartTime < min)
            {
                dateTime.Value = DateTime.Now;
                isEdit = false;
            }
                
            else
            {
                dateTime.Value = mode.Entity.StartTime;
                isEdit = true;
            }       
            cinemas = new List<Cinema>(mode.Db.Cinemas.Include(x => x.FilmRooms).Where(x=>x.FilmRooms != null || x.FilmRooms.Count() != 0));
            movies = new List<Movie>(mode.Db.Movies.Where(x => x.IsActual));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(dateTime.Value == null)
            {
                MessageBox.Show("Введите дату показа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(Convert.ToInt32(price.Text) < 1)
            {
                MessageBox.Show("Введите стоимость билета больше 0!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            mode.Entity.StartTime =(DateTime)dateTime.Value!;
            mode.Entity.Price = Convert.ToInt32(price.Text);
            mode.Entity.Movie = movies[movie.SelectedIndex];
            mode.Entity.FilmRoom = filmRooms![filmRoom.SelectedIndex];
            mode.Entity.NumberAvailableSeats = filmRooms[filmRoom.SelectedIndex].Capacity;
            mode.Execute();
            mode.Db.SaveChanges();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            cinema.ItemsSource = cinemas.Select(x => x.Name);
            movie.ItemsSource = movies.Select(x => x.Title);
            if (isEdit)
            {
                cinema.SelectedIndex = cinemas.IndexOf(mode.Entity.FilmRoom.Cinema);
                movie.SelectedIndex = movies.IndexOf(mode.Entity.Movie);
                filmRoom.ItemsSource = cinemas[cinema.SelectedIndex].FilmRooms?.Select(x=>x.Name);
                filmRoom.SelectedIndex = filmRooms.IndexOf(mode.Entity.FilmRoom);
            }
            else
            {
                filmRoom.IsEnabled = false;
            }
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void cinema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cinema.SelectedIndex == -1) return;
            filmRoom.IsEnabled = true;
            filmRooms = cinemas[cinema.SelectedIndex].FilmRooms?.ToList();
            filmRoom.ItemsSource = cinemas[cinema.SelectedIndex].FilmRooms?.Select(x => x.Name);
        }
    }
}
