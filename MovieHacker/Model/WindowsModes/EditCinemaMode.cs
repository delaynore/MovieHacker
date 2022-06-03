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
        public string? CinemaName { get; set; }

        public string? DescriptionCinema { get; set; }
        public bool IsReadOnly => false;
        public string ButtonContent => "Изменить";
        private Cinema _cinema;
        public EditCinemaMode(Cinema cinema)
        {
            _cinema = cinema;
            CinemaName = cinema.Name;
            DescriptionCinema = cinema.Description;
        }
        public void Execute()
        {
            _cinema.Name = CinemaName;
            _cinema.Description = DescriptionCinema;
            new CinemaController().Update(_cinema);
        }
    }
}
