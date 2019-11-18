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
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> SearchAndFilter (string searchString, List<string> rating)
        {
            //default search coverage
            if (searchString == null && rating.Count == 0) return All;

            List<Movie> returnList = new List<Movie>();
            foreach(Movie mov in movies)
            {
                //Search with ratings
                if (searchString != null && rating.Count > 0)
                {
                    if (mov.Title != null && mov.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) && rating.Contains(mov.MPAA_Rating))
                    {
                        returnList.Add(mov);
                    }
                }
                //Search only
                else if (searchString != null)
                {
                    if (mov.Title != null && mov.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        returnList.Add(mov);
                    }
                }
                //Ratings only
                else if (rating.Count > 0)
                {
                    if (rating.Contains(mov.MPAA_Rating))
                    {
                        returnList.Add(mov);
                    }
                }

            }

            return returnList;
        }
    }
}
