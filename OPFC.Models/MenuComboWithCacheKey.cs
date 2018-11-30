using System;
using System.Collections.Generic;

namespace OPFC.Models
{
    public class MenuComboWithCacheKey
    {
        public string CacheKey { get; set; }
        public List<MenuCombo> MenuComboList { get; set; }
    }
}
