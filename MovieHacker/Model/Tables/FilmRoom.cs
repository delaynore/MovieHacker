using System.Collections.Generic;

namespace MovieHacker.Model.Tables
{
    public class FilmRoom
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Session>? Sessions { get; set; }
    }
}
