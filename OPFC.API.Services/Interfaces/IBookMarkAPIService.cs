using System;
using OPFC.API.ServiceModel.BookMark;
namespace OPFC.API.Services.Interfaces
{
    public interface IBookMarkAPIService
    {
        CreateBookMarkResponse Post(CreateBookMarkRequest request);

        DeleteBookMarkResponse Post(DeleteBookMarkRequest request);
    }
}
