using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace Kursova.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TenderStateModel> TenderStates { get; set; }
        public DbSet<TenderModel> TenderModels { get; set; }
        public DbSet<OfferStateModel> OfferStates { get; set; }
        public DbSet<OfferModel> OfferModels { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(UserModel.ConfigureEntity);
            modelBuilder.Entity<OfferModel>(OfferModel.ConfigureEntity);
            modelBuilder.Entity<TenderStateModel>(TenderStateModel.ConfigureEntity);
            modelBuilder.Entity<TenderModel>(TenderModel.ConfigureEntity);
            modelBuilder.Entity<OfferStateModel>(OfferStateModel.ConfigureEntity);
        }
    }
}
