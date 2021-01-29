using lib.DbContextConfiguration;
using Microsoft.EntityFrameworkCore;
using System;

namespace lib.EFDbContext.Default
{
    public class DefaultDbContextFactory : CustomDbContextFactoryBase<DefaultDbContext>
    {
        public DefaultDbContextFactory(DefaultDbConnection dbConnection
            , MySqlDbContextOptions mySqlDbContextOptions
            ,IServiceProvider serviceProvider):base(dbConnection, mySqlDbContextOptions, serviceProvider)
        {
        }

        public DefaultDbContext GetDefaultDbContext(WriteAndRead writeAndRead)
        {
            return base.GetDbContext(writeAndRead) as DefaultDbContext;
        }
    }
}
