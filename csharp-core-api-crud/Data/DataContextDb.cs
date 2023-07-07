using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DataContextDb: DbContext
    {
        public DataContextDb(DbContextOptions<DataContextDb> options) : base(options) { }

        public DbSet<UsersModel> Users { get; set; }
    }
}
