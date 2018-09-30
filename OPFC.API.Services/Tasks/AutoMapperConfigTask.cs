using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OPFC.API.DTO;
using OPFC.Models;


namespace OPFC.API.ServiceModel.Tasks
{
    /// <summary>
    /// The auto mapper config
    /// </summary>
    public class AutoMapperConfigTask : Profile, ITask
    {
        /// <summary>
        /// Create mapping on startup
        /// </summary>
        public void Execute()
        {
            //IDBSET to MODEL
            CreateMap<OPFC.Models.Brand, BrandDTO>();
            CreateMap<OPFC.Models.Caterer, CatererDTO>();
            CreateMap<OPFC.Models.User, UserDTO>();
            
            Mapper.Initialize(x => x.AddProfile<AutoMapperConfigTask>());
        }
    }
}
