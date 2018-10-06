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

            CreateMap<OPFC.Models.Menu, MenuDTO>();
            CreateMap<MenuDTO, OPFC.Models.Menu>();

            CreateMap<OPFC.Models.Meal, MealDTO>();
            CreateMap<MealDTO, OPFC.Models.Meal>();

            CreateMap<OPFC.Models.Order, OrderDTO>();
            CreateMap<OrderDTO, OPFC.Models.Order>();

            CreateMap<OPFC.Models.BookMark, BookMarkDTO>();
            CreateMap<BookMarkDTO, OPFC.Models.BookMark>();

            CreateMap<OPFC.Models.Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, OPFC.Models.Transaction>();

            Mapper.Initialize(x => x.AddProfile<AutoMapperConfigTask>());
        }
    }
}
