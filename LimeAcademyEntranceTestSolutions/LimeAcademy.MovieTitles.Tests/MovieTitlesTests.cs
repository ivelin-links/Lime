using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace LimeAcademy.MovieTitles.Tests
{
    public class MovieTitlesTests
    {
        [Fact]
        public async Task SpidermanTest()
        {
            var movieName = "Spiderman";
            var pageParamName = "page";
            var expectedResults = new string[] {
                "Amazing Spiderman Syndrome",
                "Fighting, Flying and Driving: The Stunts of Spiderman 3",
                "Hollywood's Master Storytellers: Spiderman Live",
                "Italian Spiderman",
                "Spiderman",
                "Spiderman",
                "Spiderman 5",
                "Spiderman and Grandma",
                "Spiderman in Cannes",
                "Superman, Spiderman or Batman",
                "The Amazing Spiderman T4 Premiere Special",
                "The Death of Spiderman",
                "They Call Me Spiderman"
            };

            var command = new MovieTitlesCommand(
                uri =>
                {
                    if (!uri.ToString().Contains($"{pageParamName}="))
                        return Task.FromResult(GetStreamFromString(Resource.SpidermanResultsPage1));
                    else if (uri.ToString().Contains($"{pageParamName}=2"))
                        return Task.FromResult(GetStreamFromString(Resource.SpidermanResultsPage2));
                    else
                        throw new InvalidDataException("Page was not expected");
                });
            var movieResults = await command.GetMovieTitles(movieName);

            Assert.Equal(expectedResults, movieResults);
        }

        [Fact]
        public async Task JohnyJohnyYesPappaTest()
        {
            var movieName = "JohnyJohnyYesPappa";
            var expectedResults = new string[0];

            var command = new MovieTitlesCommand(
                uri => Task.FromResult(GetStreamFromString(Resource.JohnyJohnyYesPappaResults)));
            var movieResults = await command.GetMovieTitles(movieName);

            Assert.Equal(expectedResults, movieResults);
        }

        private Stream GetStreamFromString(string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
