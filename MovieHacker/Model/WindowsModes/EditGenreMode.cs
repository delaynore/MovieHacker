namespace MovieHacker.Model.WindowsModes
{
    public class EditGenreMode : IGenreWindowMode
    {
        public string TitleText => "";

        public string TextBlockText => "Введите новое название:";

        public string? GenreName { get; set; }

        public string ButtonContent => "Изменить";

        private int _id;
        public EditGenreMode(int id, string textContent)
        {
            _id = id;
            GenreName = textContent;
        }

        public void Execute()
        {
            var gc = new GenreController();
            var genre = gc.Get(_id);
            if (genre == null) return;
            genre.Name = GenreName;
            gc.Update(genre);
        }
    }
}
