using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace External.WebServices.Marvel
{
    public class RestAPIClient
    {
        private string _baseUrl = $"https://gateway.marvel.com/v1/public";

        private readonly RestClient _restClient;

        public RestAPIClient()
        {
            _restClient = new RestClient(_baseUrl);
            _restClient.Authenticator = new MarvelAPIAuthenticator();
        }

        public async Task<object> SearchMarvelCharacters(string nameStartsWith)
        {
            var request = new RestRequest("characters", DataFormat.Json);
            request.AddParameter("nameStartsWith", nameStartsWith);

            return await _restClient.GetAsync<object>(request);
        }
    }

    class MarvelAPIAuthenticator : IAuthenticator
    {
        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var time = DateTime.Now.ToFileTime();
            var privateKey = "98bb3012551ab428361803397f86206ca5f3357d";
            var apiKey = "d03b9e87f48ec57f25076eb28e74785f";
            using (var md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, $"{time}{privateKey}{apiKey}");

                request.AddQueryParameter("ts", time.ToString());
                request.AddQueryParameter("apikey", apiKey);
                request.AddQueryParameter("hash", hash);
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
