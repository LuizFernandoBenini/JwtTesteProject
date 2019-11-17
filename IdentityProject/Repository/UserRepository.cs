using System.Collections.Generic;
using System.Linq;
using IdentityProject.Models;

public static class UserRepository
{
    public static User Get(string username, string password)
    {
        var users = new List<User>();
        users.Add(new User { Id = 1, UserName = "batman", Password = "batmanPass", Role = "manager" });
        users.Add(new User { Id = 2, UserName = "robin", Password = "robinPass", Role = "employee" });
        return users.Where(w => w.UserName.ToLower() == username.ToLower() && w.Password == password).FirstOrDefault();
    }
}