using CityGuide.Models;
using System.Threading.Tasks;

namespace CityGuide.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName,string password);
        Task<bool> UserExist(string userName);
    }
}
