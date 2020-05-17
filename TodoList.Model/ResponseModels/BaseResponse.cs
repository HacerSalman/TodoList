using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoList.Model.ResponseModels
{
    public class BaseResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
