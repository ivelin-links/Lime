using System;

namespace LimeAcademy.MovieTitles.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var movieSearchResult = new MovieTitlesCommand().GetMovieTitles("spiderman").Result;
            System.Console.WriteLine(string.Join(Environment.NewLine, movieSearchResult));
        }
    }
}
