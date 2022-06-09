using MovieHacker.Model.Tables;
namespace MovieHacker.Model.WindowsModes
{
    public class AboutCinemaMode : ICinemaWindowMode
    {
        public CinemaController CinemaController { get; }
        public Cinema Cinema { get; set; }
        public bool IsReadOnly => true;
        public string ButtonContent => "Закрыть";
        public AboutCinemaMode(Cinema cinema)
        {
            Cinema = cinema;
            CinemaController = null!;
        }
        public void Execute()
        {

        }
    }
}
