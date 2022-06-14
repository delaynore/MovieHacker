using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MovieHacker.Model;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Extensions;
using System.Drawing;
using MovieHacker.Model.Tables;

namespace MovieHacker.Views
{
    public partial class AboutSessionWindow : Window
    {
        private MHDataBase db;
        private Session session;
        public AboutSessionWindow(int id, MHDataBase mHDataBase)
        {
            InitializeComponent();
            db = mHDataBase;
            FillWindow(id);
        }
        
        private void FillWindow(int id)
        {
                session = db.Sessions.Include(x=>x.Movie).Include(x=>x.FilmRoom).Include(x=>x.FilmRoom.Cinema).FirstOrDefault(x => x.Id == id)!;
                if (session == null) return;
                movieName.Text = session.Movie.Title;
                cinemaName.Text = session.FilmRoom.Cinema.Name;
                filmRoom.Text = session.FilmRoom.Name;
                startTime.Text = session.StartTime.ToLocalTime().ToString("f");
                price.Text = session.Price.ToString("C");
                freePlaces.Text = session.NumberAvailableSeats.ToString() + "/" + session.FilmRoom.Capacity.ToString();
                toBookATicket.Tag = session.Id;
                descriptionMovie.Text = session.Movie.Description;
                image1.Source = ImageBase64Converter.ToXAMLView(session.Movie.Picture);
        }

        private void toBookATicket_Click(object sender, RoutedEventArgs e)
        {
            var countInt = Convert.ToInt32(count.Text);
            
            if (countInt < 1 || countInt > 5) 
            {
                MessageBox.Show("Забронировать можно от 1 до 5 билетов!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (session.NumberAvailableSeats < countInt)
            {
                MessageBox.Show("Столько мест уже нет!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            session.NumberAvailableSeats -= countInt;
            freePlaces.Text = session.NumberAvailableSeats.ToString() + "/" + session.FilmRoom.Capacity.ToString();
            db.SaveChanges();
            MessageBox.Show($"Места в количестве {countInt} шт. забронированы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void count_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
