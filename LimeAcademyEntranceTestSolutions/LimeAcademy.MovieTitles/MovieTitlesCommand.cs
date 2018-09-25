using LimeAcademy.MovieTitles.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace LimeAcademy.MovieTitles
{
    public class MovieTitlesCommand
    {
        private const string MovieTitlesBaseUrl = "https://jsonmock.hackerrank.com/api/movies/search/";

        private Uri GetMoviesSearchUrl(string substr) =>
            new Uri(MovieTitlesBaseUrl)
                .AddParameter("Title", substr);

        private Uri GetPagedMoviesSearchUrl(string substr, int pageNumber) =>
            GetMoviesSearchUrl(substr)
                .AddParameter("page", pageNumber.ToString("D"));

        // Used for mocking
        public Func<Uri, Task<Stream>> MoviesJsonStreamAsyncCallback { get; private set; }

        public MovieTitlesCommand()
        {
            MoviesJsonStreamAsyncCallback = RetrieveMoviesStreamFromUriAsync;
        }

        public MovieTitlesCommand(Func<Uri, Task<Stream>> moviesStreamRetriever)
        {
            MoviesJsonStreamAsyncCallback = moviesStreamRetriever;
        }

        public async Task<ICollection<string>> GetMovieTitles(string movieName)
        {
            var titles = new List<string>();

            var searchUrl = GetMoviesSearchUrl(movieName);
            var movieSearchResults = await RetrieveMoviesFromUriAsync(searchUrl);
            titles.AddRange(movieSearchResults.data.Select(m => m.Title));

            for (var currentPageNumber = 2; currentPageNumber <= movieSearchResults.total_pages; currentPageNumber++)
            {
                searchUrl = GetPagedMoviesSearchUrl(movieName, currentPageNumber);
                movieSearchResults = await RetrieveMoviesFromUriAsync(searchUrl);
                titles.AddRange(movieSearchResults.data.Select(m => m.Title));
            }

            titles.Sort();

            return titles;
        }

        private async Task<MovieSearchResultsDto> RetrieveMoviesFromUriAsync(Uri uri)
        {
            using (var jsonSearchStream = await MoviesJsonStreamAsyncCallback(uri))
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
