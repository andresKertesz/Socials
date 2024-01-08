using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Socials.Client.Client.ClientService;
using Socials.Client.Client.Helpers;
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

        public async Task<ResponseMessage> RegisterUser(User user)
        {

            var rm = new ResponseMessage()
            {
                OK = false
            };
            Dictionary<string, string?> userData = new Dictionary<string, string?>(){
                { "username", user.Username },
                { "password", user.Password },
                { "email", user.Email },
                {"name",user.Name },
                {"verifiedId","1" },
                {"hidden",user.Hidden.ToString() },
                { "birthdate",user.Birthdate.Value.ToString("yyyy-MM-dd hh:mm:ss")}
            };

            HttpContent httpContent = new FormUrlEncodedContent(userData);
            var content = await ApiClient.PostAsync(BASE_USER_URL+"register",httpContent);
            if (content.IsSuccessStatusCode)
            {
                string rawJson = await content.Content.ReadAsStringAsync();
                var json = JObject.Parse(rawJson);
                if (!json.ContainsKey("data"))
                {
                    if (json.ContainsKey("errorInfo"))
                    {
                        var errorMessage = json["errorInfo"]?.Value<string>();
                        rm.Message = errorMessage;
                    }

                    return rm;
                }
                var data = json["data"]?.ToString();
                if (data == null)
                {
                    rm.Message = "No se pudo crear la cuenta correctamente. Por favor intentarlo en otro momento.";
                    return rm;
                }
                rm.OK = true;
                rm.Message = "Usuario creado correctamente";
                return rm;
            }

            rm.Message = "No se pudo comunicar con el servidor.";
            return rm;

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


        public async Task<User?> GetUserDataByUsername(string username)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["username"] = username;

            var userJson = await ApiClient.GetAsync(BASE_USER_URL +"?"+ query.ToString()); ;
            if (userJson.IsSuccessStatusCode)
            {
                var content = await userJson.Content.ReadAsStringAsync();
                User? userData = GetUserFromJson(content); 
                return userData;
            }
            else
            {
                return null;
            }

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


        private User? GetUserFromJson(string content)
        {
            string rawJson = content;
            var json = JObject.Parse(rawJson);
            if (!json.ContainsKey("data"))
            {
                return null;
            }
            var data = json["data"]?[0]?.ToString();
            if (data == null)
            {
                return null;
            }
            User? user = JsonConvert.DeserializeObject<User>(data);
            return user;
        }

    }
}
