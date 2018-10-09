using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;
namespace OPFC.API.ServiceModel.BookMark
{
    public class CreateBookMarkResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BookMarkDTO BookMark { get; set; }
    }

    public class GetAllBookMarkResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<BookMarkDTO> BookMarks { get; set; }
    }

    public class UpdateBookMarkResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BookMarkDTO BookMark { get; set; }
    }

    public class DeleteBookMarkResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BookMarkDTO BookMark { get; set; }
    }


}
