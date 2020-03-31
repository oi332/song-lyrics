using SongLyrics.Application;
using System;
using Xunit;

namespace SongLyrics.Tests
{
    public class SongNormalizeTest
    {
        [Fact]
        public void SpotifyNormalize_ShouldReturnNormalizedSongName_WhenSongNameIsNotValid()
        {
            var name1 = "American Boy (feat. Kanye West)";
            var name2 = "I Wanna Sex You Up - Single Mix";
            var name3 = "Until The End Of Time (with Beyonce)";

            var normalizedName1 = SongNormalizer.SpotifyNormalize(name1);
            var normalizedName2 = SongNormalizer.SpotifyNormalize(name2);
            var normalizedName3 = SongNormalizer.SpotifyNormalize(name3);

            Assert.Matches("American Boy", normalizedName1);
            Assert.Matches("I Wanna Sex You Up", normalizedName2);
            Assert.Matches("Until The End Of Time", normalizedName3);
        }

        [Fact]
        public void Normalize_ShouldReturnNormalizedSongName_WhenSongNameIsNotValid()
        {
            var name1 = "American Boy";
            var name2 = "Can We Kiss Forever?";
            var name3 = "13";
            var name4 = "Me & U";

            var normalizedName1 = SongNormalizer.Normalize(name1);
            var normalizedName2 = SongNormalizer.Normalize(name2);
            var normalizedName3 = SongNormalizer.Normalize(name3);
            var normalizedName4 = SongNormalizer.Normalize(name4);

            Assert.Matches("americanboy", normalizedName1);
            Assert.Matches("canwekissforever", normalizedName2);
            Assert.Matches("13", normalizedName3);
            Assert.Matches("meu", normalizedName4);
        }
    }
}
