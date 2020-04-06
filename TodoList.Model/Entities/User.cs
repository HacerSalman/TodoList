using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoList.Model.Entities
{
    [Table("user")]
    public class User:BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_name")]
        [Required(ErrorMessage = "Please provide user name", AllowEmptyStrings = false), StringLength(80)]
        public string UserName { get; set; }

        [Column("full_name")]
        [Required(ErrorMessage = "Please provide full name", AllowEmptyStrings = false), StringLength(80)]
        public string FullName { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false), StringLength(80)]
        [JsonIgnore]
        public string Password { get; set; }
    }
}
