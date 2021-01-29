using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace lib.DbContextConfiguration
{
    public interface ICustomDbContextFactory
    {
        DbContext GetDbContext(WriteAndRead writeAndRead);
    }
}
