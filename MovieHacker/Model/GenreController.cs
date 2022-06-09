using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class GenreController : IDataBaseController<Genre>
    {
        private MHDataBase db;
        public GenreController()
        {
            db = new();
        }
        public override bool Add(Genre x)
        {
            try
            {
                db.Genres.Add(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool AddRange(params Genre[] x)
        {
            try
            {
                db.Genres.AddRange(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override Genre? Get(int id)
        {
            var res = db.Genres.FirstOrDefault(x => x.Id == id);
            return res;
        }
        public Genre? Get(string name)
        {
            var res = db.Genres.FirstOrDefault(x => x.Name == name);
            return res;
        }

        public override List<Genre> GetAll()
        {
            return db.Genres.Include(x=>x.Movies).ToList();
        }

        public override bool Remove(Genre x)
        {
            try
            {
                db.Genres.Remove(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool RemoveAt(int index)
        {
            try
            {
                db.Genres.Remove(db.Genres.First(x => x.Id == index));
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                db.SaveChanges();

            }
            return true;
        }

        public override void SaveChanges()
        {
            db.SaveChanges();
        }

        public override bool Update( params Genre[] x)
        {
            try
            {
                db.Genres.UpdateRange(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool Update(int id)
        {
            var x = Get(id);
            if (x == null) return false;
            return Update(x);
        }
    }
}
