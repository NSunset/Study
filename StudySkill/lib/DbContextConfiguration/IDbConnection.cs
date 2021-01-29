using Microsoft.EntityFrameworkCore;

namespace lib.DbContextConfiguration
{
    public interface IDbConnection
    {
        public string WriteConnectionStr { get; }


        public string[] ReadConnectionStr { get; }

        public DbContextOptionsBuilder DbContextOptionsBuilder { get; }

        public Strategy Strategy { get; }
    }
}
