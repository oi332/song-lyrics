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
    }
}
