using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class AddNewSessionMode : IWindowMode<Session>
    {
        public AddNewSessionMode(MHDataBase db)
        {
            Db = db;
            Entity = new Session();
        }

        public MHDataBase Db { get; }

        public Session Entity { get; }

        public bool IsReadOnly => false;

        public string ButtonContent => "Добавить";

        public void Execute()
        {
            Db.Sessions.Add(Entity);
        }
    }
}
