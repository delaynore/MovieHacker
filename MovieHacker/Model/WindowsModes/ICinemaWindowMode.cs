using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public interface ICinemaWindowMode : IMode
    {
        public Cinema Cinema { get; set; }
        public string ButtonContent { get; }
        public bool IsReadOnly { get; }
    }
}
