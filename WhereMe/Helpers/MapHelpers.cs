using Android.Gms.Maps.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WhereMe.Helpers
{
    public class MapHelpers
    {
        public async Task<string> FindCoordinateAddress(LatLng position, string mapkey)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + position.Latitude.ToString() + "," + position.Longitude.ToString() + "&key=" + mapkey;
            string placeAddress = "";
            var handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);

            string result = await httpClient.GetStringAsync(url);

            if (!string.IsNullOrEmpty(result))
            {
                var geoCodeData = JsonConvert.DeserializeObject<GeocodingParser>(result);
                if (geoCodeData.status.Contains("OK"))
                {
                    placeAddress = geoCodeData.results[0].formatted_address;
                }
            }
            return placeAddress;
        }

    }
}