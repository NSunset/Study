using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace lib.DbContextConfiguration
{
    public interface IDbContextOptions
    {
        DbContextOptions Configure<T>(DbContextOptionsBuilder builder, string connectionString) where T : DbContext;

        DbContextOptions Configure<T>(DbContextOptionsBuilder builder, DbConnection connection) where T : DbContext;
    }
}
