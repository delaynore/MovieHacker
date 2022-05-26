using System.Collections.Generic;

namespace MovieHacker.Model.Tables
{
    public class Cinema
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<FilmRoom>? FilmRooms { get; set; }
    }
}
