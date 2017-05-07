using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MovieBS
    {
        private MovieDb objDb;
        public MovieBS()
        {
            objDb = new MovieDb();
        }

        public IEnumerable<tbl_Movie> GetAll()
        {
        return objDb.GetAll();
        }

        public tbl_Movie GetById(int Id)
        {
            return objDb.GetById(Id);
        }

        public void Insert(tbl_Movie movie, String[] arrActorId)
        {
            objDb.Insert(movie, arrActorId);
        }

        public void Delete(int Id)
        {
            objDb.Delete(Id);
        }

        public tbl_Movie FindDelete(int Id)
        {
           return  objDb.FindDelete(Id);
        }
        public void Update(tbl_Movie movie, String[] arrActorId)
        {
            objDb.Update(movie,arrActorId);
        }
    }
}
