using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class TagRepository : EFRepository<Tag>
    {
        public TagRepository(DbContext dbContext) : base(dbContext) { }
    }
}
