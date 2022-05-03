using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.Tables
{
    public class Cinema
    {
        public int Id { get; set; }
        public string? CinemaName { get; set; }
        public List<FilmRoom>? FilmRooms { get; set; }
    }
}
