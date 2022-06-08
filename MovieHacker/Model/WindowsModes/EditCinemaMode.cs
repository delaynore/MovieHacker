using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class EditCinemaMode : ICinemaWindowMode
    {
        public Cinema Cinema { get; set; }
        public bool IsReadOnly => false;
        public string ButtonContent => "Сохранить";
        public EditCinemaMode(Cinema cinema)
        {
            Cinema = cinema;
        }
        public void Execute()
        {

            new CinemaController().Update(Cinema);
        }
    }
}
