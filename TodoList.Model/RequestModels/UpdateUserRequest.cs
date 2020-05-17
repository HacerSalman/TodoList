using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoList.Model.RequestModels
{
    public class UpdateUserRequest
    {

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("user_name")]
        public string Username { get; set; }
    }
}
