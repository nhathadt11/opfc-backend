using System;
using System.Collections.Generic;

namespace OPFC.Models
{
    public class MenuCombo
    {
        public string Guid { get; set; }
        public List<Menu> Menus { get; set; }
        public double ComboTotal { get; set; }
        public List<long> ComboCategoryIds { get; set; }
    }
}
