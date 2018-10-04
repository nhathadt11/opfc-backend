using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.DTO
{
    public class PhotoDTO
    {
        /// <summary>
        /// Gets or sets photo identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets foreign key brand identifier
        /// </summary>
        public long? BrandId { get; set; }

        /// <summary>
        /// Gets or sets foreign key menu identifier
        /// </summary>
        public long? MenuId { get; set; }

        /// <summary>
        /// Gets or set photo ref
        /// </summary>
        public List<string> PhotoRef { get; set; }

        public PhotoDTO Clone()
        {
            return (PhotoDTO) this.MemberwiseClone();
        }
    }
}
