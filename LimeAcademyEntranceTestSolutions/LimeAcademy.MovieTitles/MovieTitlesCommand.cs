using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using LimeAcademy.MovieTitles.DataContracts;

namespace LimeAcademy.MovieTitles
{
    public class MovieTitlesCommand
    {
        private const string MovieTitlesBaseUrl = "https://jsonmock.hackerrank.com/api/movies/search/";

        // Used for mocking
        public Func<Uri, Task<Stream>> MoviesJsonStreamCallback { get; private set; }

        public MovieTitlesCommand()
        {
            MoviesJsonStreamCallback = RetrieveMoviesStreamFromUriAsync;
        }

        public MovieTitlesCommand(Func<Uri, Task<Stream>> moviesStreamRetriever)
        {
            MoviesJsonStreamCallback = moviesStreamRetriever;
        }

        public async Task<ICollection<string>> GetMovieTitles(string movieName)
        {
            var titles = new List<string>();

            var currentPageNumber = 1;
            MovieSearchResultsDto movieSearchResults;
            do
            {
                var searchUrl = GetMoviesSearchUrl(movieName, currentPageNumber);
                movieSearchResults = await RetrieveMoviesFromUriAsync(searchUrl);

                titles.AddRange(movieSearchResults.data.Select(m => m.Title));
            } while (++currentPageNumber <= movieSearchResults.total_pages);

            titles.Sort();

            return titles;
        }

        private Uri GetMoviesSearchUrl(string substr, int pageNumber)
        {
            var uri = new Uri(MovieTitlesBaseUrl).AddParameter("Title", substr);
            if (pageNumber > 1)
                uri = uri.AddParameter("page", pageNumber.ToString("D"));

            return uri;
        }

        private async Task<MovieSearchResultsDto> RetrieveMoviesFromUriAsync(Uri uri)
        {
            using (var jsonSearchStream = await MoviesJsonStreamCallback(uri))
            {
                var serializer = new DataContractJsonSerializer(typeof(MovieSearchResultsDto));
                var movieSearchResults = (MovieSearchResultsDto)serializer.ReadObject(jsonSearchStream);

                return movieSearchResults;
            }
        }

        private async Task<Stream> RetrieveMoviesStreamFromUriAsync(Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                var textStream = await httpClient.GetStreamAsync(uri);
                return textStream;
            }
        }
    }
}