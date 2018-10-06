using System;
namespace OPFC.API.DTO
{
    public class MenuDTO
    {
        public long Id { get; set; }

        public string MenuName { get; set; }

        public string Description { get; set; }

        public int ServingNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long BrandId { get; set; }
    }
}
