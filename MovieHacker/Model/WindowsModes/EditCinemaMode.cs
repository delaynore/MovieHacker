using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model.WindowsModes
{
    public class EditCinemaMode : IWindowMode<Cinema>
    {
        public MHDataBase Db { get; }

        public Cinema Entity { get; }

        public bool IsReadOnly => false;

        public string ButtonContent => "Сохранить";

        public EditCinemaMode(Cinema cinema, MHDataBase db)
        {
            Entity = cinema;
            Db = db;
        }

        public void Execute()
        {

            Db.Entry(Entity).State = EntityState.Modified;
        }
    }
}
