using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public List<Movie> Movies;

        [BindProperty]
        public string Search { get; set; }
        [BindProperty]
        public List<string> Rating { get; set; } = new List<string>();
        [BindProperty]
        public float? MinIMDB { get; set; }
        [BindProperty]
        public float? MaxIMDB { get; set; }


        public void OnGet()
        {
            Movies = MovieDatabase.All;
        }

        public void OnPost()
        {
            //default with the entire database
            Movies = MovieDatabase.All;
            //filter if the search terms exist and pare down the movie list
            if(Search !=null) Movies = MovieDatabase.Search(Movies, Search);
            if (Rating.Count != 0) Movies = MovieDatabase.FilterByMPAA(Movies, Rating);
            if (MinIMDB != null) Movies = MovieDatabase.FilterByMinIMDB(Movies, (float)MinIMDB);
            if (MaxIMDB != null) Movies = MovieDatabase.FilterByMaxIMDB(Movies, (float)MaxIMDB);
        }
    }
}
