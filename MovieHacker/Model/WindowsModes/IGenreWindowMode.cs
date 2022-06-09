using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public interface IGenreWindowMode : IMode
    {
        public GenreController GenreController { get; }
        public Genre Genre { get; set; }
        public string TextBlockText { get; }
        public string ButtonContent { get; }
    }
}
