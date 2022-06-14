using MovieHacker.Model.Tables;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MovieHacker.Model.WindowsModes
{
    public class EditMovieMode : IWindowMode<Movie>
    {
        public MHDataBase Db { get; }
        public Movie Entity { get; }
        public EditMovieMode(Movie movie, MHDataBase db)
        {
            Entity = movie;
            Db = db;
        }

        public bool IsReadOnly => false;

        public string ButtonContent => "Сохранить";

        public void Execute()
        {
            Db.Movies.Update(Entity);
        }
    }
}
