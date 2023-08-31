using CityGuide.Models;
using System;
using System.Threading.Tasks;

namespace CityGuide.Data
{
    public class AuthRepository : IAuthRepository
    {
       private DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public Task<User> Login(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, paswordSalt;
            CreatePasswordHash(password,out passwordHash,out paswordSalt);
            user.PasswordSalt = paswordSalt;
            user.PasswordHash= passwordHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] paswordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                paswordSalt = hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<bool> UserExist(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
