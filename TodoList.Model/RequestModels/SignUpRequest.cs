﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoList.Model.RequestModels
{
    public class SignUpRequest
    {
        [JsonPropertyName("user_name")]
        [Required]
        public string UserName { get; set; }

        [JsonPropertyName("full_name")]
        [Required]
        public string FullName { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
    }
}
