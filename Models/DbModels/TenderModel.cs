using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DbModels
{
    public class TenderModel
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StateId { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid OwnerId { get; set; }
        public Guid? ExecuterId { get; set; } = null;
        public List<OfferModel> Offers { get; set; }
        public TenderStateModel State { get; set; }
        public UserModel Owner { get; set; }
        public UserModel? Executer { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<TenderModel> builder)
        {
            builder.ToTable("Tender");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.Cost)
                .IsRequired(true)
                .HasPrecision(20, 2);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(512);

            builder.Property(x => x.CreationDate)
                .IsRequired(true);

            builder.Property(x => x.OwnerId)
                .IsRequired(true);

            builder.Property(x => x.ExecuterId)
                .IsRequired(false);

            builder.HasOne(x => x.Owner)
                .WithMany(o => o.Tenders)
                .HasForeignKey(x => x.OwnerId);

            builder.HasOne(x => x.Executer)
                .WithMany(o => o.CompletedTenders)
                .HasForeignKey(x => x.ExecuterId);

            builder.HasOne(x => x.State)
                .WithMany(s => s.Tenders)
                .HasForeignKey(x => x.StateId);
        }
    }
}
