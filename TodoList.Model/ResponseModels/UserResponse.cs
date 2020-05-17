using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoList.Model.ResponseModels
{
    public class UserResponse:BaseResponse
    {
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("created_date")]
        public long CreatedDate { get; set; }

        [JsonPropertyName("status")]
        public byte? Status { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
