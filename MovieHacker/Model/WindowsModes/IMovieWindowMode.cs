using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public interface IMovieWindowMode : IMode
    {
        public MovieController MovieController { get; }
        public string ButtonContent { get; }
        public Tables.Movie Movie { get; set; }
        public bool IsReadOnly { get; }
    }
}
