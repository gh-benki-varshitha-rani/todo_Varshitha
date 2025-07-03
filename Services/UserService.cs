using System.Linq;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class UserService : IUserService
    {
        private readonly TodoContext _context;

        public UserService(TodoContext context)
        {
            _context = context;
        }

        public bool RegisterUser(string username, string password, string role = "User")
        {
            if (_context.Users.Any(u => u.Username == username))
                return false;

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }
    }
}
