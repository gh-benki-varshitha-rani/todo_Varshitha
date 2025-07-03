using TodoApi.Models;

namespace TodoApi.Services
{
    public interface IUserService
    {
        bool RegisterUser(string username, string password, string role = "User");
        User? Authenticate(string username, string password);
    }
}
