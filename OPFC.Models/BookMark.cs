using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OPFC.Models
{
    public class BookMark
    {
        [Key]
        public long BookMarkId { get; set; }

        public long UserId { get; set; }

        public long MenuId { get; set; }
    }
}
