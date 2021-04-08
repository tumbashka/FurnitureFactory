using Microsoft.EntityFrameworkCore;
using FurnitureFactoryDatabaseImplement.Models;

namespace FurnitureFactoryDatabaseImplement
{
    public class FurnitureFactoryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-98L3NOK\NEWMSSQLSERVER;
                                            Initial Catalog=FurnitureFactoryDatabase;
                                            Integrated Security=True;
                                            MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<FurnitureModel> FurnitureModels { set; get; }
        public virtual DbSet<FurnitureType> FurnitureTypes { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
        public virtual DbSet<Position> Positions { set; get; }


    }
}
