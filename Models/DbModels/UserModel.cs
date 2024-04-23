using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DbModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronimyc { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
        public List<TenderModel> CompletedTenders { get; set; } = new List<TenderModel>();
        public List<OfferModel> Offers { get; set; } = new List<OfferModel>();

        public static void ConfigureEntity(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Surname)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Patronimyc)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Password)
                .HasColumnType("varchar")
                .HasMaxLength(256)
                .IsRequired(true);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Address)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);
        }
    }
}
