using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class CinemaController : IDataBaseController<Cinema>
    {
        public bool Add(Cinema x)
        {
            using var db = new MHDataBase();
            try
            {
                db.Cinemas.Add(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddRange(params Cinema[] x)
        {
            using var db = new MHDataBase();
            try
            {
                db.Cinemas.AddRange(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Cinema? Get(int id)
        {
            using var db = new MHDataBase();
            var res = db.Cinemas.FirstOrDefault(x => x.Id == id);
            return res;
        }

        public List<Cinema> GetAll()
        {
            return GetAll(x => true);
        }

        public List<Cinema> GetAll(Func<Cinema, bool> selector)
        {
            using var db = new MHDataBase();
            db.Cinemas.Load();
            return db.Cinemas.Local.Where(x => selector(x)).ToList();
        }

        public bool Remove(Cinema x)
        {
            using var db = new MHDataBase();
            try
            {
                db.Cinemas.Remove(x);
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
                    db.Cinemas.Remove(db.Cinemas.First(x => x.Id == index));
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

        public bool Update(Cinema x)
        {
            using var db = new MHDataBase();
            try
            {
                var c = db.Cinemas.First(c => c.Id == x.Id);
                c.Name = x.Name;
                c.Description = x.Description;
                c.FilmRooms = x.FilmRooms;
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
