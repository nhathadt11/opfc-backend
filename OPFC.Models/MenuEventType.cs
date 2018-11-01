using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class MenuEventType
    {
        [Key]
        public long MenuId { get; set; }
        [Key]
        public long EventTypeId { get; set; }
    }
}