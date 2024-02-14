using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;
using UserSync.Utility;

namespace UserSync.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<UserEntity>> FetchUsersAsync(string apiUrl, int sourceId)
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            var data = JToken.Parse(response);

            var parsers = new Dictionary<string, IUserParser>
            {
                { "data", new DataKeyParser() },
                { "users", new UsersKeyParser() },
                { "array", new ArrayParser() },
                { "results", new ResultsKeyParser() }
            };

            IUserParser parser = null;
            if (data.Type == JTokenType.Object)
            {
                var key = parsers.Keys.FirstOrDefault(k => data[k] != null);
                if (key != null)
                {
                    parser = parsers[key];
                }
            }
            else if (data.Type == JTokenType.Array)
            {
                parser = parsers["array"];
            }

            return parser != null ? parser.Parse(data, sourceId) : Enumerable.Empty<UserEntity>();
        }
    }
}
