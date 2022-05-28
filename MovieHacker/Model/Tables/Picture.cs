using MovieHacker.Model.Tables;

namespace MovieHacker.Model.Tables
{
    public class Picture
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
