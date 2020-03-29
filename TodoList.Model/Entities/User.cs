using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Model.Entities
{
    public class User:BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_name")]
        [Required, StringLength(80)]
        public string UserName { get; set; }

        [Column("full_name")]
        [Required, StringLength(80)]
        public string FullName { get; set; }

        [Column("password")]
        [Required, StringLength(80)]
        public string Password { get; set; }
    }
}
