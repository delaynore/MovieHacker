using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewGenreMode : IWindowMode<Genre>
    {
        public string ButtonContent => "Добавить";

        public MHDataBase Db { get; }

        public Genre Entity { get; set; }

        public bool IsReadOnly => false;

        public AddNewGenreMode(MHDataBase db)
        {
            Entity = new Genre();
            Db = db;  
        }
        public void Execute()
        {
            Db.Add(Entity);
        }
    }
}
