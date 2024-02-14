using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;

namespace UserSync.Utility
{
    public interface IUserParser
    {
        IEnumerable<User> Parse(JToken data, int sourceId);
    }

    public class DataKeyParser : IUserParser
    {
        public IEnumerable<User> Parse(JToken data, int sourceId)
        {
            return data["data"].ToObject<List<JObject>>().Select(item =>
            {
                return new User
                {
                    FirstName = (string)item["first_name"],
                    LastName = (string)item["last_name"],
                    Email = (string)item["email"],
                    SourceId = sourceId
                };
            });
        }
    }

    // Parser for the 'users' key structure
    public class UsersKeyParser : IUserParser
    {
        public IEnumerable<User> Parse(JToken data, int sourceId) =>
            data["users"].ToObject<List<User>>().Select(user => { user.SourceId = sourceId; return user; });
    }

    // Parser for the array structure
    public class ArrayParser : IUserParser
    {
        public IEnumerable<User> Parse(JToken data, int sourceId)
        {
            return data.ToObject<List<JObject>>().Select(item =>
            {
                var fullName = (string)item["name"];
                var splitName = fullName.Split(new[] { ' ' }, 2);
                return new User
                {
                    FirstName = splitName.Length > 0 ? splitName[0] : "",
                    LastName = splitName.Length > 1 ? splitName[1] : "",
                    Email = (string)item["email"],
                    SourceId = sourceId
                };
            });
        }
    }

    // Parser for the 'results' key structure
    public class ResultsKeyParser : IUserParser
    {
        public IEnumerable<User> Parse(JToken data, int sourceId)
        {
            return data["results"].ToObject<List<JObject>>().Select(item =>
            {
                return new User
                {
                    FirstName = (string)item["name"]["first"],
                    LastName = (string)item["name"]["last"],
                    Email = (string)item["email"],
                    SourceId = sourceId
                };
            });
        }
    }
}
