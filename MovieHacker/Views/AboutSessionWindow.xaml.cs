using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MovieHacker.Model;
using Microsoft.EntityFrameworkCore;

namespace MovieHacker.Views
{
    /// <summary>
    /// Логика взаимодействия для AboutSessionWindow.xaml
    /// </summary>
    public partial class AboutSessionWindow : Window
    {
        public AboutSessionWindow(int id)
        {
            InitializeComponent();
            FillWindow(id);
        }
        
        private void FillWindow(int id)
        {
            using (var db = new MHDataBase())
            {
                var session = db.Sessions.Include(x=>x.Movie).Include(x=>x.FilmRoom).Include(x=>x.FilmRoom.Cinema).FirstOrDefault(x => x.Id == id);
                if (session == null) return;
                movieName.Text = session.Movie.Title;
                cinemaName.Text = session.FilmRoom.Cinema.Name;
                filmRoom.Text = session.FilmRoom.Name;
                startTime.Text = session.StartTime.ToLocalTime().ToString("f");
                price.Text = session.Price.ToString("C");
                freePlaces.Text = session.NumberAvailableSeats.ToString() + "/" + session.FilmRoom.Capacity.ToString();
                toBookATicket.Tag = session.Id;
                descriptionMovie.Text = session.Movie.Description;
            }
        }

        private void toBookATicket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
