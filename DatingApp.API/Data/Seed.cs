using System.Collections.Generic;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            // read jason file
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            // convert json file into c# list of users
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            // save data into data base the same way we did in our login
            foreach (var user in users)
            {
             byte[] passwordHash, passwordSalt;
             CreatePasswordHash("password", out passwordHash, out passwordSalt);

             user.PasswordHash = passwordHash;
             user.PasswordSalt = passwordSalt;
             user.Username = user.Username.ToLower();

             _context.Users.Add(user);
            }

            _context.SaveChanges();

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //generate password salt from the user's new password
                // to unloack password hash 
                passwordSalt = hmac.Key;
                //gen hash from password 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}