using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewMovieMode : IWindowMode<Movie>
    {
        public MHDataBase Db { get; }
        public Movie Entity { get; set; } 
        
        public bool IsReadOnly => false;

        public string ButtonContent => "Добавить";

        public AddNewMovieMode(MHDataBase db)
        {
            Db = db;
            Entity = new Movie();
        }
        public void Execute()
        {
            Db.Add(Entity);
        }
    }
}
