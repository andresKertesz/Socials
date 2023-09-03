using System;
using System.Collections.Generic;
using System.IO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Socials.Client.Client.ClientService;
using Socials.Client.Client.Model;

namespace Socials.Client.Client.Controllers 
{
    public class AuthenticationController

    {
        private const string BASE_USER_URL = "user/";
        public enum LogInResult { Success, ServerNotFound, IncorrectCredentials }
        public bool LoggedIn { get; private set; }

        public User? LoggedInUser { get; private set; }
        private readonly IApiClientService ApiClient;
        private string? _userBearerToken { get; set; }
        private readonly ILocalStorageService _localStorageService;
        public AuthenticationController(IApiClientService apiClient, ILocalStorageService localStorageService)
        {
            LoggedIn = false;
            LoggedInUser = null;
            ApiClient = apiClient;
            this._localStorageService = localStorageService;
        }

        private async Task RefreshCurrentUserData()
        {
            string? token = await GetUserToken();
            if(token != null)
            {
                var u = await GetUserData(token);
                return;
            }


        }


        private async Task<string?> GetUserToken()
        {

            bool contains = await _localStorageService.ContainKeyAsync("token");
            if(!contains)
            {
                return null;
            }

            string token = await _localStorageService.GetItemAsStringAsync("token");
            return token;

        }

        public async Task<User> GetUserData(string token)
        {

            ApiClient.SetBearerToken(token);
            var content = await ApiClient.GetAsync(BASE_USER_URL);
            
            if (content.IsSuccessStatusCode)
            {
                string rawJson = await content.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(rawJson);
                return new("user");
            }
            else
            {
                throw new Exception("Couldn't contact server");
            }
        }

        public async Task<LogInResult> LogInAsync(string username, string password)
        {
            // Logic to set LoggedIn to true
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            HttpContent httpContent = new FormUrlEncodedContent(parameters);
            var login = await ApiClient.PostAsync(BASE_USER_URL + "login", httpContent);
            if (login.IsSuccessStatusCode)
            {
                var content = await login.Content.ReadAsStringAsync();
                if(content != null)
                {
                    JObject response = JObject.Parse(content);
                    if (response.ContainsKey("token"))
                    {
                        _userBearerToken = response["token"]?.ToObject<string>();
                        return LogInResult.Success;
                    }
                    return LogInResult.IncorrectCredentials;
                }
            }
            
            return LogInResult.ServerNotFound;
            
            
        }

        public void LogOut()
        {
            // Logic to set LoggedIn to false
            LoggedIn = false;
        }
    }
}
