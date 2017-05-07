using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MovieDb
    {
        private MovieDbEntities db;
        public MovieDb()
        {
            db = new MovieDbEntities();
        }
        public IEnumerable<tbl_Movie> GetAll()
        {
            return db.tbl_Movie.ToList();
        }

        public tbl_Movie GetById(int Id)
        {
            return db.tbl_Movie.Find(Id);
        }

        public void Insert(tbl_Movie movie, String[] arrActorId)
        {          
            db.tbl_Movie.Add(movie);
            Save();
            int movieId = movie.MovieId;
            tbl_ActorMovie tblActorMovieObj = new tbl_ActorMovie();
            foreach(String actorIdString in arrActorId)
            {
                tblActorMovieObj.ActorId =Convert.ToInt32(actorIdString);
                tblActorMovieObj.MovieId = movieId;
                db.tbl_ActorMovie.Add(tblActorMovieObj);
                Save();
            }            

        }

        public tbl_Movie FindDelete(int Id)
        {           
            return db.tbl_Movie.Find(Id);
        }

        public void Delete(int Id)
        {
            tbl_Movie movie = db.tbl_Movie.Find(Id);
            IEnumerable<tbl_ActorMovie> iEnumActorMovie =  movie.tbl_ActorMovie;
            db.tbl_ActorMovie.RemoveRange(iEnumActorMovie);
            Save();            
            db.tbl_Movie.Remove(movie);
            Save();
        }

        public void Update(tbl_Movie movie, String[] arrActorId)
        {
            db.Entry(movie).State =EntityState.Modified;
            db.SaveChanges();           


            
            int movieId = movie.MovieId;
            tbl_ActorMovie tblActorMovieObj = new tbl_ActorMovie();
            //-----------------------------------------------------------
            var deleteActorMovieRows =  from rowActorMovie in db.tbl_ActorMovie
                                        where rowActorMovie.MovieId == movie.MovieId                                      
                                        select rowActorMovie;
           
            db.tbl_ActorMovie.RemoveRange(deleteActorMovieRows);
            Save();
            
            //add the new records.
            foreach (String actorIdString in arrActorId)
            {               
                tblActorMovieObj.ActorId = Convert.ToInt32(actorIdString);
                tblActorMovieObj.MovieId = movieId;
                db.tbl_ActorMovie.Add(tblActorMovieObj);
                Save();
            }


        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
