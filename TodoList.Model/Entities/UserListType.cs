using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Model.Entities
{
    [Table("user_list_type")]
    public class UserListType:BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        public User User { get; set; }

        [Column("list_type_id")]
        public int ListTypeId { get; set; }

        public ListType ListType { get; set; }
    }
}
