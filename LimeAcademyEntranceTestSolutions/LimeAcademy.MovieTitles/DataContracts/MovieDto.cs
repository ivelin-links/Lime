using System;
using System.Collections.Generic;
using System.Text;

namespace LimeAcademy.MovieTitles.DataContracts
{
    public class MovieDto
    {
        public string Poster { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string imdbID { get; set; }
    }
}
