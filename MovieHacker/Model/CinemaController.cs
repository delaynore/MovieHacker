using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieHacker.Model.Tables;

namespace MovieHacker.Model
{
    public class CinemaController : IDataBaseController<Cinema>
    {
        private MHDataBase db;
        public CinemaController()
        {
            db = new();
        }
        public override bool Add(Cinema x)
        {
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

        public override bool AddRange(params Cinema[] x)
        {
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

        public override Cinema? Get(int id)
        {
            var res = db.Cinemas.FirstOrDefault(x => x.Id == id);
            return res;
        }

        public override List<Cinema> GetAll()
        {
            return db.Cinemas.Include(x=>x.FilmRooms).ToList();
        }

        public override bool Remove(Cinema x)
        {
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

        public override bool RemoveAt(int index)
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

            return true;
        }
       
        public override void SaveChanges()
        {
            db.SaveChanges();
        }

        public override bool Update(params Cinema[] x)
        {
            try
            {
                db.Cinemas.UpdateRange(x);
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
