using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewCinemaMode : ICinemaWindowMode
    {
        public Cinema Cinema { get; set; }
        public bool IsReadOnly => false;

        public string ButtonContent => "Добавить";
        public AddNewCinemaMode()
        {
            Cinema = new Cinema();
        }
        public void Execute()
        {
            new CinemaController().Add(Cinema);
        }
    }
}
