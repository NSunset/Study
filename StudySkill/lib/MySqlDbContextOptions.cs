using lib.DbContextConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace lib
{
    public class MySqlDbContextOptions : IDbContextOptions
    {
        public DbContextOptions Configure<T>(DbContextOptionsBuilder builder, string connectionString) where T : DbContext
        {
            return builder.UseMySQL(connectionString).Options;
        }

        public DbContextOptions Configure<T>(DbContextOptionsBuilder builder, DbConnection connection) where T : DbContext
        {
            return builder.UseMySQL(connection).Options;
        }
    }
}
