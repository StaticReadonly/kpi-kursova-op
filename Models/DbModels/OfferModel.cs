using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DbModels
{
    public class OfferModel
    {
        public Guid Id { get; set; }
        public Guid TenderId { get; set; }
        public Guid OffererId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public int StateId { get; set; }
        public string Description { get; set; } = string.Empty;
        public OfferStateModel State { get; set; }
        public UserModel Offerer { get; set; }
        public TenderModel Tender { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<OfferModel> builder)
        {
            builder.ToTable("Offer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.TenderId)
                .IsRequired(true);

            builder.Property(x => x.OffererId)
                .IsRequired(true);

            builder.Property(x => x.CreationDate)
                .IsRequired(true);

            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasPrecision(20, 2);

            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(512)
                .IsRequired(true);

            builder.HasOne(x => x.State)
                .WithMany(s => s.Offers)
                .HasForeignKey(x => x.StateId);

            builder.HasOne(x => x.Offerer)
                .WithMany(o => o.Offers)
                .HasForeignKey(x => x.OffererId);

            builder.HasOne(x => x.Tender)
                .WithMany(t => t.Offers)
                .HasForeignKey(x => x.TenderId);
        }
    }
}
