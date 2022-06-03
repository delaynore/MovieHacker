using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public interface ICinemaWindowMode : IMode
    {
        public string? CinemaName { get; set; }
        public string? DescriptionCinema { get; set; }
        public string ButtonContent { get; }
        public bool IsReadOnly { get; }
    }
}
