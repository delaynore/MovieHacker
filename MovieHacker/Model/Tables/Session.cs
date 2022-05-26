using System;

namespace MovieHacker.Model.Tables
{
    public class Session
    {

        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int FilmRoomId { get; set; }
        public FilmRoom FilmRoom { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberAvailableSeats { get; set; }
        public decimal Price { get; set; }
    }
}
