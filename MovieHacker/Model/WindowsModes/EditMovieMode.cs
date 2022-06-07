using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class EditMovieMode : IMovieWindowMode
    {
        public Movie Movie { get; set; }
        public EditMovieMode(Movie movie)
        {
            Movie = movie;
        }

        public bool IsReadOnly => false;

        public string ButtonContent => "Сохранить";

        public void Execute()
        {
            new MovieController().Update(Movie);
        }
    }
}
