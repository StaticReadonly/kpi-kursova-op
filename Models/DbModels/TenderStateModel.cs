using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.DbModels
{
    public class TenderStateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();

        public static void ConfigureEntity(EntityTypeBuilder<TenderStateModel> builder)
        {
            builder.ToTable("TenderState");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique(true);
        }
    }
}
