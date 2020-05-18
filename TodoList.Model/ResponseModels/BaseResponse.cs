using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoList.Model.ResponseModels
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Message = "";
        }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
