using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SongLyrics.Application
{
    public static class AccessTokenRenewer
    {
        public static void Renew(string accessToken)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();

            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C dotnet user-secrets set \"SpotifySettings:AccessToken\" \"{accessToken}\" --project \"{Path.GetFullPath(@"..\..\..\")}";

            process.StartInfo = startInfo;

            process.Start();
        }
    }
}
