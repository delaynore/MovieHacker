using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class AboutCinemaMode : ICinemaWindowMode
    {
        public string? CinemaName { get; set; }
        public string? DescriptionCinema { get; set; }
        public bool IsReadOnly => true;
        public string ButtonContent => "Закрыть";
        public AboutCinemaMode(Tables.Cinema cinema)
        {
            CinemaName = cinema.Name;
            DescriptionCinema = cinema.Description;
        }
        public void Execute()
        {

        }
    }
}
