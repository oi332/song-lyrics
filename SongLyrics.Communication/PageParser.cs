using HtmlAgilityPack;
using SongLyrics.Communication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SongLyrics.Communication
{
    public static class PageParser
    {
        public static LyricsResponse Parse(Stream content)
        {
            var xPath = "/html/body[@class='az-song-text']/div[@class='container main-page']/div[@class='row']/div[@class='col-xs-12 col-lg-8 text-center']/div[5]";

            var pageDocument = new HtmlDocument();
            pageDocument.Load(content);

            var lyrics = pageDocument.DocumentNode.SelectSingleNode(xPath).InnerText;

            return new LyricsResponse { Lyrics = lyrics };
        }
    }
}
