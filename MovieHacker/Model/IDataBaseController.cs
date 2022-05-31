using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model
{
    interface IDataBaseController<T> 
        where T: class
    {
        public bool Add(T x);
        public bool AddRange(params T[] x);
        public bool Update(T x);
        public bool Update(int id);
        public bool Remove(T x);
        public bool RemoveAt(int index);
        public T? Get(int id);
        public List<T> GetAll();
        public List<T> GetAll(Func<T,bool> selector);

    }
}
