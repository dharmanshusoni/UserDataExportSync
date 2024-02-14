using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Adapter;
using UserSync.Entities;
using UserSync.Entities.PlaceholderUser;
using UserSync.Entities.RandomUser;

namespace UserSync.Utility
{
    public static class Mapper
    {
        public static User Map(this Result result)
        {
            return new User
            {
                FirstName = result.name.first,
                LastName = result.name.last,
                Email = result.email
            };
        }

        public static User Map(this PlaceholderResult result)
        {
            return new User
            {
                FirstName = result.name.Split(" ")[0],
                LastName = result.name.Split(" ")[1],
                Email = result.email
            };
        }
    }
}
