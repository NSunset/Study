using lib.DbContextConfiguration;
using Microsoft.EntityFrameworkCore;
using System;

namespace lib.EFDbContext.Default
{
    public class DefaultDbConnection : IDbConnection
    {
        public string WriteConnectionStr { get => "server=101.37.116.76;uid=test;pwd=Test1996!!;database=Default;AllowUserVariables=true"; }
        public string[] ReadConnectionStr { get => new[] { "server=127.0.0.1;uid=root;pwd=root;database=Default;AllowUserVariables=true" }; }
        public Strategy Strategy { get => Strategy.Polling; }

        public DbContextOptionsBuilder DbContextOptionsBuilder { get => new DbContextOptionsBuilder<DefaultDbContext>(); }
    }
}
