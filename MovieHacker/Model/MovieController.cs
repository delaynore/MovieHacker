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
        public bool Add(Movie x)
        {
            using var db = new MHDataBase();
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

        public bool AddRange(params Movie[] x)
        {
            using var db = new MHDataBase();
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

        public Movie? Get(int id)
        {
            using var db = new MHDataBase();
            var res = db.Movies.Include(x=>x.Genres).ThenInclude(x=>x.Genre).FirstOrDefault(x => x.Id == id);
            return res;
        }

        public List<Movie> GetAll()
        {
            return GetAll(x => true);
        }

        public List<Movie> GetAll(Func<Movie, bool> selector)
        {
            using var db = new MHDataBase();
            db.Movies.Load();
            return db.Movies.Local.Where(x => selector(x)).ToList();
        }

        public bool Remove(Movie x)
        {
            using var db = new MHDataBase();
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

        public bool RemoveAt(int index)
        {
            using (var db = new MHDataBase())
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
            }
            return true;
        }

        public bool Update(Movie x)
        {
            using var db = new MHDataBase();
            try
            {
                db.Movies.Update(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(int id)
        {
            return Update(Get(id));
        }
    }
}
