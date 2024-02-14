using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;
using UserSync.Entities.PlaceholderUser;
using UserSync.Utility;

namespace UserSync.Adapter
{
    internal class PlaceholderUserAdapter : IAdapter
    {
        private const string url = "https://jsonplaceholder.typicode.com/users";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<List<User>> GetAllUsers()
        {
            var json = await httpClient.GetStringAsync(url);
            var root = JsonConvert.DeserializeObject<List<PlaceholderResult>>(json);
            return root.Select(Mapper.Map).ToList();
        }
    }
}
