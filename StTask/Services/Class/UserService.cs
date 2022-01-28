using System;
using System.Threading.Tasks;
using StTask.Helper;
using StTask.Model;
using StTask.Services.Interface;

namespace StTask.Services.Class
{
    public class UserService : IUserService
    {
        public async Task<Tuple<Users, bool, string>> GetUsersData()
        {
            var response = await HttpManage.GetAsync<Users>("https://reqres.in/api/users").ConfigureAwait(false);
            return response;
        }
    }
}
