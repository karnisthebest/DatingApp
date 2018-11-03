using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    //this AuthRepository will be responsible for querying from our database
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            // get user from database that matches the what the user types in
            // FirstOrDefaultAsync returns null if it doesn't find anything that matches 
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            //if they dont find a user 
            if (user == null)
                return null;
            //if they do find a user
            //verify the password 
            // ! if this function returns false 
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user; 
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                
                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false; 
                }
                 return true;
            }
            
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            // out +> reference to the passwordHash, passwordSalt 
            // but doesnt put in the value 
            // if passwordHash, passwordSalt updates in this function then
            // the variable outside this function also updates 
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            //save changes to user table after adding passwordHash, passwordSalt 
            //user is the instance of User we created in this function 
            //so we just add a new person user into the table User
            await _context.Users.AddAsync(user);
            //save the changes back to the database
            await _context.SaveChangesAsync();

            return user;

        }

        //recieves password from user whem register 
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

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;  
                
            return false;
        }
    }//end class
}//end namespace