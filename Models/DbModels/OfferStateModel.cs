using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DbModels
{
    public class OfferStateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<OfferModel> Offers { get; set; } = new List<OfferModel>();

        public static void ConfigureEntity(EntityTypeBuilder<OfferStateModel> builder)
        {
            builder.ToTable("OffersState");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique(true);
        }
    }
}
