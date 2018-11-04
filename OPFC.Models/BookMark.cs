using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OPFC.Models
{
    public class BookMark
    {
        [Key, Column(Order = 0)]
        public long UserId { get; set; }

        [Key, Column(Order = 1)]
        public long MenuId { get; set; }
    }
}
