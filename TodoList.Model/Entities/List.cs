using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Model.Entities
{
    public class List:BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        [Required, StringLength(200)]
        public string Title { get; set; }

        [Column("description")]
        [Required, StringLength(5000)]
        public string Description { get; set; }

        [Column("starts_at")]
        public long StartsAt { get; set; }

        [Column("ends_at")]
        public long? EndsAt { get; set; }

        [Column("type")]
        public int Type { get; set; }

        public ListType ListType { get; set; }
    }
}
