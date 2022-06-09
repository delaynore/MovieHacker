using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewCinemaMode : ICinemaWindowMode
    {
        public CinemaController CinemaController { get; }
        public Cinema Cinema { get; set; }
        public bool IsReadOnly => false;

        public string ButtonContent => "Добавить";
        public AddNewCinemaMode(CinemaController cC)
        {
            Cinema = new Cinema();
            CinemaController = cC;
        }
        public void Execute()
        {
            CinemaController.Add(Cinema);
        }
    }
}
