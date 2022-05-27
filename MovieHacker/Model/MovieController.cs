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
        public void Add(Movie x)
        {
            using (var db = new MHDataBase())
            {
                db.Movies.Add(x);
                db.SaveChanges();
            }
        }

        public void AddRange(params Movie[] x)
        {
            using (var db = new MHDataBase())
            {
                db.Movies.AddRange(x);
                db.SaveChanges();
            }
        }

        public Movie? Get(int id)
        {
            using (var db = new MHDataBase())
            {
                var res = db.Movies.FirstOrDefault(x => x.Id == id);
                return res;
            }
        }

        public List<Movie> GetAll()
        {
            using (var db = new MHDataBase())
            {
                db.Movies.Load();
                return db.Movies.ToList();
            }
        }

        public void Remove(Movie x)
        {
            using (var db = new MHDataBase())
            {
                db.Movies.Remove(x);
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
