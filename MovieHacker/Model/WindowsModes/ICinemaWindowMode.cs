using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public interface ICinemaWindowMode : IMode
    {
        public CinemaController? CinemaController { get; }
        public Cinema Cinema { get; set; }
        public string ButtonContent { get; }
        public bool IsReadOnly { get; }
    }
}
