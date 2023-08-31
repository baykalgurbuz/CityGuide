using CityGuide.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName == userName);
            if (user == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
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

        public async Task<bool> UserExist(string userName)
        {
            if (await _context.Users.AnyAsync(x=>x.UserName == userName))
            {
                return true;
            }
            return false;
        }
    }
}
