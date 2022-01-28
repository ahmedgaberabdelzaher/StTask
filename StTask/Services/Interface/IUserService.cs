using System;
using System.Threading.Tasks;
using StTask.Model;

namespace StTask.Services.Interface
{
    public interface IUserService
    {
        Task<Tuple<Users, bool, string>> GetUsersData();
    }
}
