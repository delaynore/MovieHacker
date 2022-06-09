using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model
{
     public abstract class IDataBaseController<T> 
        where T: class
    {
        public abstract void SaveChanges();
        public abstract bool Add(T x);
        public abstract bool AddRange(params T[] x);
        public abstract bool Update(params T[] x);
        public abstract bool Update(int id);
        public abstract bool Remove(T x);
        public abstract bool RemoveAt(int index);
        public abstract T? Get(int id);
        public abstract List<T> GetAll();

    }
}
