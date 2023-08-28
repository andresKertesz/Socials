
using Socials.Client.Client.Model;

namespace Socials.Client.Client.Controllers
{
    public class UserController
    {
        public bool LoggedIn { get; private set; }
        public User? LoggedInUser { get; private set; }
        public void LogIn(string username, string password)
        {
            // Logic to set LoggedIn to true
            User loggedIn_user = new(username);
            LoggedInUser = loggedIn_user;
            LoggedIn = true;
            
        }

        public void LogOut()
        {
            // Logic to set LoggedIn to false
            LoggedIn = false;
        }
    }
}
