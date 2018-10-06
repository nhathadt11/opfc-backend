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
            CreateMap<BrandDTO, OPFC.Models.Brand>();

            CreateMap<OPFC.Models.Caterer, CatererDTO>();
            CreateMap<CatererDTO, OPFC.Models.Caterer>();

            CreateMap<OPFC.Models.User, UserDTO>();
            CreateMap<UserDTO, OPFC.Models.User>();

            CreateMap<OPFC.Models.Photo, PhotoDTO>();
            CreateMap<PhotoDTO, OPFC.Models.Photo>();

            CreateMap<OPFC.Models.Event, EventDTO>();
            CreateMap<EventDTO, OPFC.Models.Event>();

            Mapper.Initialize(x => x.AddProfile<AutoMapperConfigTask>());
        }
    }
}
