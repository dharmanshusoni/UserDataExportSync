using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSync.Entities;

namespace UserSync.Storage
{
    public interface IStorageService
    {
        Task SaveAsync(IEnumerable<UserEntity> users, string path);
    }
}
