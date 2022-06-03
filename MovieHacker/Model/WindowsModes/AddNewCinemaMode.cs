using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewCinemaMode : ICinemaWindowMode
    {
        public string? CinemaName { get; set; } = "";
        public bool IsReadOnly => false;
        public string? DescriptionCinema { get; set; } = "";

        public string ButtonContent => "Добавить";

        public void Execute()
        {
            new CinemaController().Add(new Tables.Cinema { Name = CinemaName, Description = DescriptionCinema });
        }
    }
}
