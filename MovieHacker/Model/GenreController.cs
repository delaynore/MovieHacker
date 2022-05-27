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
        public void Add(Genre x)
        {
            using(var db = new MHDataBase())
            {
                db.Genres.Add(x);
                db.SaveChanges();
            }
        }

        public void AddRange(params Genre[] x)
        {
            using (var db = new MHDataBase())
            {
                db.Genres.AddRange(x);
                db.SaveChanges();
            }
        }

        public Genre? Get(int id)
        {
            using (var db = new MHDataBase())
            {
                var res = db.Genres.FirstOrDefault(x => x.Id == id);
                return res;
            }
        }

        public List<Genre> GetAll()
        {
            using (var db = new MHDataBase())
            {
                db.Genres.Load();
                return db.Genres.ToList();
            }
        }

        public void Remove(Genre x)
        {
            using (var db = new MHDataBase())
            {
                db.Genres.Remove(x);
                db.SaveChanges();
            }
        }

        public void RemoveAt(int index)
        {
            using (var db = new MHDataBase())
            {
                try
                {
                    db.Genres.Remove(db.Genres.First(x => x.Id == index));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    db.SaveChanges();
                }
            }
        }
    }
}
