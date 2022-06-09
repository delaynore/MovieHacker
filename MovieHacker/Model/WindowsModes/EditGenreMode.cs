using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class EditGenreMode : IGenreWindowMode
    {
        public string TextBlockText => "Введите новое название:";

        public string ButtonContent => "Сохранить";

        public Genre Genre { get; set; }
        public GenreController GenreController { get; }
        public EditGenreMode(Genre genre, GenreController gC)
        {
            Genre = genre;
            GenreController = gC;
        }

        public void Execute()
        {
            GenreController.Update(Genre);
        }
    }
}
