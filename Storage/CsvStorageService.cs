using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;

namespace UserSync.Storage
{
    public class CsvStorageService : IStorageService
    {
        public async Task SaveAsync(IEnumerable<User> users, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("FirstName,LastName,Email,SourceId");
            foreach (var user in users)
            {
                sb.AppendLine($"{user.FirstName},{user.LastName},{user.Email},{user.SourceId}");
            }
            await File.WriteAllTextAsync(path, sb.ToString());
        }
    }
}
