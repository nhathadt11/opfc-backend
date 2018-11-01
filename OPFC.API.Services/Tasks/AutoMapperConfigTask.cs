using AutoMapper;
using OPFC.API.DTO;
using System.Collections.Generic;
using OPFC.API.ServiceModel.Menu;
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

            CreateMap<CreateMenuRequest, Models.Menu>().ReverseMap();

            CreateMap<Models.Rating, RatingDTO>().ReverseMap();

            CreateMap<BrandOrder, BrandOrderDTO>().ReverseMap();
            
            CreateMap<BrandOrderLine, BrandOrderLineDTO>().ReverseMap();

            CreateMap<EventPlannerOrder, EventPlannerOrderDTO>().ReverseMap();

            CreateMap<EventPlannerOrderLine, EventPlannerOrderLineDTO>().ReverseMap();

            #region List mapping

            CreateMap<List<OPFC.Models.Event>, List<EventDTO>>();
            CreateMap<List<EventDTO>, List<OPFC.Models.Event>>();

            CreateMap<List<OPFC.Models.BookMark>, List<BookMarkDTO>>();
            CreateMap<List<BookMarkDTO>, List<OPFC.Models.BookMark>>();


            CreateMap<List<OPFC.Models.Meal>, List<MealDTO>>();
            CreateMap<List<MealDTO>, List<OPFC.Models.Meal>>();

            CreateMap<List<OPFC.Models.Rating>, List<RatingDTO>>().ReverseMap();

            CreateMap<List<BrandOrder>, List<BrandOrderDTO>>().ReverseMap();
            
            CreateMap<List<BrandOrderLine>, List<BrandOrderLineDTO>>().ReverseMap();

            CreateMap<List<EventPlannerOrder>, List<EventPlannerOrderDTO>>().ReverseMap();

            #endregion

            Mapper.Initialize(x =>
            {
                x.AddProfile<AutoMapperConfigTask>();
                x.ValidateInlineMaps = false;
            });


        }
    }
}
