using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;
using UserSync.Entities.RandomUser;
using UserSync.Utility;

namespace UserSync.Adapter
{
    public class RandomUserAdapter : IAdapter
    {
        private const string url = "https://randomuser.me/api/?results=500";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<List<User>> GetAllUsers()
        {
            var json = await httpClient.GetStringAsync(url);
            var root = JsonConvert.DeserializeObject<Rootobject>(json);
            return root.results.Select(Mapper.Map).ToList();
        }
    }
}
