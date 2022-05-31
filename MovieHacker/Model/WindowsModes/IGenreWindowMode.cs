using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public interface IGenreWindowMode
    {
        public string TitleText { get; }
        public string TextBlockText { get; }
        public string ButtonContent { get; }
        public string? GenreName { get; set; }
        public void Execute();
    }
}
