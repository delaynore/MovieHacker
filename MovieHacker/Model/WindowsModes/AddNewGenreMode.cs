namespace MovieHacker.Model.WindowsModes
{
    public class AddNewGenreMode : IGenreWindowMode
    {
        public string TitleText => "";

        public string TextBlockText => "Введите название жанра: ";

        public string? GenreName { get; set; } = "";

        public string ButtonContent => "Добавить";

        public void Execute()
        {
            new GenreController().Add(new Tables.Genre { Name = GenreName! });
        }
    }
}
