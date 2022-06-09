using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class AboutCinemaMode : IWindowMode<Cinema>
    {
        public MHDataBase? Db { get; }

        public Cinema Entity { get; }

        public bool IsReadOnly => true;

        public string ButtonContent => "Закрыть";
        public AboutCinemaMode(Cinema cinema)
        {
            Entity = cinema;
        }
        public void Execute() { }
    }
}
