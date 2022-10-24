using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVideoStreaming.Models
{
   public interface IUserRepository
    {
        User GetUserById(int id);
        User GetExistingUser(string Email);
        User GetUser(string Email,string Password);
        IEnumerable<User> GetAllUsers();
        User Add(User User);
        User Update(User UserChanges);
        User Delete(int Id);
    }
}
