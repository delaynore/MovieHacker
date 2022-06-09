using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewGenreMode : IGenreWindowMode
    {

        public string TextBlockText => "Введите название жанра: ";

        public string ButtonContent => "Добавить";
        public GenreController GenreController { get; }
        public Genre Genre { get; set; }
        public AddNewGenreMode(GenreController gC)
        {
            Genre = new Genre();
            GenreController = gC;  
        }
        public void Execute()
        {
            GenreController.Add(Genre);
        }
    }
}
