using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;

namespace UserSync.Adapter
{
    public interface IAdapter
    {
        Task<List<User>> GetAllUsers();
    }
}
