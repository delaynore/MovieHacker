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
        public CinemaController CinemaController { get; }
        public Cinema Cinema { get; set; }
        public bool IsReadOnly => false;
        public string ButtonContent => "Сохранить";
        public EditCinemaMode(Cinema cinema, CinemaController cC)
        {
            Cinema = cinema;
            CinemaController = cC;
        }
        public void Execute()
        {

            CinemaController.Update(Cinema);
        }
    }
}
