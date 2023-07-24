using MovieHacker.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class EditSessionMode : IWindowMode<Session>
    {
        public EditSessionMode(MHDataBase db, Session session)
        {
            Db = db;
            Entity = session;
        }

        public MHDataBase Db { get; }

        public Session Entity { get; }

        public bool IsReadOnly => false;

        public string ButtonContent => "Сохранить";

        public void Execute()
        {
            Db.Sessions.Update(Entity);
        }
    }
}
