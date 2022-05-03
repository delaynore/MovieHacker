using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.Tables
{
    public class FilmRoom
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string? NameRoom { get; set; }
        public Cinema? Cinema { get; set; }
    }
}
