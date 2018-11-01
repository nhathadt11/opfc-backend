using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OPFC.Models
{
    public class Tag
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
