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
        public void Add(T x);
        public void AddRange(params T[] x);
        public void Remove(T x);
        public void RemoveAt(int index);
        public T? Get(int id);
        public List<T> GetAll();

    }
}
