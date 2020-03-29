using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Model.Entities
{
    public class BaseEntity
    {
        [Column("created_date")]
        public long CreatedDate { get; set; }

        [Column("updated_date")]
        public long UpdatedDate { get; set; }

        [Column("deleted_date")]
        public long? DeletedDate { get; set; }

        [Column("owner_by")]
        [StringLength(80)]
        public string OwnerBy { get; set; }

        [Column("modifier_by")]
        [StringLength(80)]
        public string ModifierBy { get; set; }

        [Column("status")]
        public byte? Status { get; set; }
    }
}
