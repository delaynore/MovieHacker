using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class GenreController : IDataBaseController<Genre>
    {
        public bool Add(Genre x)
        {
            using var db = new MHDataBase();
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

        public bool AddRange(params Genre[] x)
        {
            using var db = new MHDataBase();
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

        public Genre? Get(int id)
        {
            using var db = new MHDataBase();
            var res = db.Genres.FirstOrDefault(x => x.Id == id);
            return res;
        }
        public Genre? Get(string name)
        {
            using var db = new MHDataBase();
            var res = db.Genres.FirstOrDefault(x => x.Name == name);
            return res;
        }
        public List<Genre> GetAll()
        {
            return GetAll(x => true);
        }

        public List<Genre> GetAll(Func<Genre, bool> selector)
        {
            using var db = new MHDataBase();
            db.Genres.Load();
            return db.Genres.Local.Where(x => selector(x)).ToList();
        }

        public bool Remove(Genre x)
        {
            using var db = new MHDataBase();
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

        public bool RemoveAt(int index)
        {
            using (var db = new MHDataBase())
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
            }
            return true;
        }

        public bool Update(Genre x)
        {
            using var db = new MHDataBase();
            try
            {
                db.Genres.Update(x);
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
