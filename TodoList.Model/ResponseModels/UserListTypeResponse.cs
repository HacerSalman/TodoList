using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoList.Model.Entities;

namespace TodoList.Model.ResponseModels
{
    public class UserListTypeResponse
    {
        [JsonPropertyName("list")]
        public List<ListType> List { get; set; }

        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }
    }
}
