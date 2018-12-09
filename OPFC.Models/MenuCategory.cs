using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPFC.Models
{
    public class MenuCategory
    {
        [Key, Column(Order = 0)]
        public long MenuId { get; set; }

        [Key, Column(Order = 1)]
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        public MenuCategory()
        {
        }

        public MenuCategory(long menuId, long categoryId)
        {
            MenuId = menuId;
            CategoryId = categoryId;
        }
    }
}
