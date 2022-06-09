using MovieHacker.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace MovieHacker.Model.WindowsModes
{
    public class EditGenreMode : IWindowMode<Genre>
    {

        public string ButtonContent => "Сохранить";

        public Genre Entity { get; set; }

        public MHDataBase Db { get; }

        public bool IsReadOnly => false;

        public EditGenreMode(Genre genre, MHDataBase db)
        {
            Entity = genre;
            Db = db;
        }

        public void Execute()
        {
            Db.Entry(Entity).State = EntityState.Modified;
        }
    }
}
