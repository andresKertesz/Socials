using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Socials.Client.Client.Model;

namespace Socials.Client.Client.Controllers 
{
    public class AuthenticationController 
    {
        public enum LogInResult { Success,ServerNotFound, IncorrectCredentials}
        public bool LoggedIn { get; private set; }
        public User? LoggedInUser { get; private set; }

        public AuthenticationController()
        {
            LoggedIn = false;
            LoggedInUser = null;
        }

        public async LogInResult LogInAsync(string username, string password)
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
