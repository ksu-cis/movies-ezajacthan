using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public static List<Movie> All
        {
            get
            {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> Search (List<Movie> movies, string searchString)
        {
            //default search coverage
            List<Movie> returnList = new List<Movie>();
            foreach (Movie mov in movies)
            {
                if (mov.Title != null && mov.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                {
                    returnList.Add(mov);
                }
            }
            return returnList;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie mov in movies)
            {
                if(mpaa.Contains(mov.MPAA_Rating))
                {
                    results.Add(mov);
                }
            }
            return results;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float min)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie mov in movies)
            {
                if (mov.IMDB_Rating!= null && mov.IMDB_Rating >= min)
                {
                    results.Add(mov);
                }
            }
            return results;
        }
        public static List<Movie> FilterByMaxIMDB(List<Movie> movies, float max)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie mov in movies)
            {
                if (mov.IMDB_Rating != null && mov.IMDB_Rating <= max)
                {
                    results.Add(mov);
                }
            }
            return results;
        }
    }
}
