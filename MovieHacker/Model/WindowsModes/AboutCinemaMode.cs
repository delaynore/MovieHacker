using MovieHacker.Model.Tables;
namespace MovieHacker.Model.WindowsModes
{
    public class AboutCinemaMode : ICinemaWindowMode
    {
        public Cinema Cinema { get; set; }
        public bool IsReadOnly => true;
        public string ButtonContent => "Закрыть";
        public AboutCinemaMode(Cinema cinema)
        {
            Cinema = cinema;
        }
        public void Execute()
        {

        }
    }
}
