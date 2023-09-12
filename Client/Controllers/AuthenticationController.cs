using System;
using System.Collections.Generic;
using System.IO;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Socials.Client.Client.ClientService;
using Socials.Client.Client.Model;

namespace Socials.Client.Client.Controllers 
{
    public class AuthenticationController

    {

        public event Action UserLoggedOutOrIn; 
        private const string BASE_USER_URL = "user/";
        public enum LogInResult { Success, ServerNotFound, IncorrectCredentials,UnknownError}
        public enum LogoutResult { Success, ServerNotFound}
        public bool LoggedIn { get; private set; }

        public User? LoggedInUser { get; private set; }
        public bool UserTokenExists => GetUserToken() != null;
        private readonly IApiClientService ApiClient;
        private readonly IJSRuntime _jsRuntime;
        private string? _userBearerToken { get; set; }
        private readonly ILocalStorageService _localStorageService;
        public AuthenticationController(IApiClientService apiClient, ILocalStorageService localStorageService, IJSRuntime jsRuntime)
        {
            LoggedIn = false;
            LoggedInUser = null;
            ApiClient = apiClient;
            _jsRuntime = jsRuntime;
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


        public async Task<string?> GetUserToken()
        {

            bool contains = await _localStorageService.ContainKeyAsync("token");
            if(!contains)
            {
                return null;
            }

            string token = await _localStorageService.GetItemAsStringAsync("token");
            return token;

        }
        public async Task ReconnectUser(string token)
        {
            var user = await GetUserData(token);
            LoggedIn = true;
            LoggedInUser = user;
        }
        public async Task<User?> GetUserData(string token)
        {

            ApiClient.SetBearerToken(token);
            var content = await ApiClient.GetAsync(BASE_USER_URL);
            
            if (content.IsSuccessStatusCode)
            {
                string rawJson = await content.Content.ReadAsStringAsync();
                var json = JObject.Parse(rawJson);
                if (!json.ContainsKey("data"))
                {
                    return null;
                }
                var data = json["data"]?.ToString() ;
                if(data == null)
                {
                    return null;
                }
                User? user = JsonConvert.DeserializeObject<User>(data);
                return user;
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
                        if(_userBearerToken == null)
                        {
                            return LogInResult.UnknownError;
                        }
                        LoggedInUser = await GetUserData(_userBearerToken);
                        if(LoggedInUser != null)
                        {
                            LoggedIn = true;
                            await _localStorageService.SetItemAsStringAsync("token", _userBearerToken);
                        }
                        UserHasLoggedOutOrIn();
                        return LogInResult.Success;
                    }
                    return LogInResult.IncorrectCredentials;
                }
            }
            else
            {
                string message = await login.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            
            return LogInResult.ServerNotFound;
            
            
        }

        public async Task<LogoutResult> LogOut()
        {
            var logout = await ApiClient.PostAsync(BASE_USER_URL + "logout",null);
            if(!logout.IsSuccessStatusCode)
            {
                return LogoutResult.ServerNotFound;    
            }
            await _localStorageService.RemoveItemAsync("token");
            _userBearerToken = null;
            LoggedIn = false;
            UserHasLoggedOutOrIn();
            return LogoutResult.Success;
        }

        private void UserHasLoggedOutOrIn() => UserLoggedOutOrIn?.Invoke();
    }
}
