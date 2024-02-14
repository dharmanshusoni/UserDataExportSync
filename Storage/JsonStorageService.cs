using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;

namespace UserSync.Storage
{
    public class JsonStorageService : IStorageService
    {
        public async Task SaveAsync(IEnumerable<User> users, string path)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            await File.WriteAllTextAsync(path, json);
        }
    }
}
