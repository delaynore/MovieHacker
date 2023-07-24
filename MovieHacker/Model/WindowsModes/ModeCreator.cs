using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public class ModeCreator<TEntity> : IMode<TEntity>
    {
        public ModeCreator()
        {

        }

        public TEntity Entity => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public string ButtonContent => throw new NotImplementedException();

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
