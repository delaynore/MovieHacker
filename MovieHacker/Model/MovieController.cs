using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class MovieController : IDataBaseController<Movie>
    {
        private MHDataBase db;
        public MovieController()
        {
            db = new MHDataBase();
        }
        public override bool Add(Movie x)
        {
            try
            {
                db.Movies.Add(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool AddRange(params Movie[] x)
        {;
            try
            {
                db.Movies.AddRange(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override Movie? Get(int id)
        {
            var res = db.Movies.Include(x=>x.Genres).FirstOrDefault(x => x.Id == id);
            return res;
        }

        public override List<Movie> GetAll()
        {
            return db.Movies.Include(x=>x.Genres).ToList();
        }

        public override bool Remove(Movie x)
        {
            try
            {
                db.Movies.Remove(x);
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
                db.Movies.Remove(db.Movies.First(x => x.Id == index));
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

        public override bool Update(params Movie[] x)
        {
            try
            {
                db.Movies.UpdateRange(x);
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
