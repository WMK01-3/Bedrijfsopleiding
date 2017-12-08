using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json.Linq;

namespace BedrijfsOpleiding.API.GoogleMaps
{
    class AutoCompleteLocations
    {
        private const string Key = "AIzaSyDO05fNSgYF4gyWd8As-oZGLsvbPb567Xg";
        private const string Language = "nl";
        private const string Type = "geocode";


        public static List<string> FetchLocations(string input)
        {
            string dataString = new WebClient().DownloadString(UrlBuilder(input));

            JObject json = JObject.Parse(dataString);
            IEnumerable<JToken> tokens = json.SelectTokens("$.predictions[*].description");
            List<string> list = new List<string>(tokens.Values<string>());
            return list;
        }

        private static string UrlBuilder(string input)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://maps.googleapis.com/maps/api/place/autocomplete/json?input=");
            sb.Append(input);
            sb.Append("&types=");
            sb.Append(Type);
            sb.Append("&language=");
            sb.Append(Language);
            sb.Append("&key=");
            sb.Append(Key);
            string url = sb.ToString();
            return url;
        }
    }
}