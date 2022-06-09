using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model.WindowsModes
{
    public interface IWindowMode<TEntity> where TEntity : class
    {
        public MHDataBase? Db { get; }
        public TEntity Entity { get; }
        public bool IsReadOnly { get; }
        public string ButtonContent { get; }
        public void Execute();
    }
}
