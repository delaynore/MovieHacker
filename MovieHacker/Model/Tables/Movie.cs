using System.Collections.Generic;

namespace MovieHacker.Model.Tables
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }
        public string? Picture { get; set; }
        public bool IsActual { get; set; }
        public virtual ICollection<Genre>? Genres { get; set; }
        public virtual ICollection<Session>? Sessions { get; set; }
    }
}
