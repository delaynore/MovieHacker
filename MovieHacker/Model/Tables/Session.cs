using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.Tables
{
    public class Session
    {
        public int Id { get; set; }
        public Movie? Movie { get; set; }
        public Cinema? Cinema { get; set; }
        public FilmRoom? FilmRoom { get; set; }
        public DateTime? StartTime { get; set; }
        public int NumberAvailableSeats { get; set; }
        public decimal Price { get; set; }
    }
}
