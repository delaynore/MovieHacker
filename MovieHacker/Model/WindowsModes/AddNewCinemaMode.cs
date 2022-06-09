using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewCinemaMode : IWindowMode<Cinema>
    {

        public AddNewCinemaMode(MHDataBase db)
        {
            Entity = new Cinema();
            Db = db;
        }
        public MHDataBase Db { get; }

        public Cinema Entity { get; }

        public bool IsReadOnly => false;

        public string ButtonContent => "Добавить";

        public void Execute()
        {
            Db.Add(Entity);
        }
    }
}
