using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class MenuEventType
    {
        [Key]
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long EventTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}