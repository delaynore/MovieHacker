using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewMovieMode : IMovieWindowMode
    {
        public Movie Movie { get; set; } 
        
        public bool IsReadOnly => false;

        public string ButtonContent => "Добавить";

        public AddNewMovieMode()
        {
            Movie = new Movie();
        }
        public void Execute()
        {

            new MovieController().Add(Movie);
        }
    }
}
