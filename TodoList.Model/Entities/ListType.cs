﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Model.Entities
{
    public class ListType:BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Column("description")]
        [Required, StringLength(500)]
        public string Description { get; set; }
      
    }
}
