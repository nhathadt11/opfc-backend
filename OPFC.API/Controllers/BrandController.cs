using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.Services.Interfaces;
using ServiceStack;

namespace OPFC.API.Controllers
{
    //[ServiceStack.Route("api/[controller]")]
    //[ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandService _brandService = AppHostBase.Instance.Resolve<IBrandService>();

        //[ServiceStack.Route("/CreateBrand/", "POST")]
        //public bool CreateBrand()
        //{

        //}
    }
}