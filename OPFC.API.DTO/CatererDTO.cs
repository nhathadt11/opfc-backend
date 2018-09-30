using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.DTO
{
    public class CatererDTO
    {
        /// <summary>
        /// The object user
        /// </summary>
        public UserDTO User { get; set; }

        /// <summary>
        /// The object brand
        /// </summary>
        public BrandDTO Brand { get; set; }
    }
}
