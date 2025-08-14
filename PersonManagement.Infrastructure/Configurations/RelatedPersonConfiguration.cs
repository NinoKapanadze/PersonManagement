using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain;

namespace PersonManagement.Infrastructure.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.HasKey(rp => rp.Id);

            builder.HasOne(rp => rp.RelatedTo)
                 .WithMany()
                 .HasForeignKey(rp => rp.RelatedToId);

            builder.Property(rp => rp.RelationshipType)
                .HasConversion<string>();

            builder.HasQueryFilter(rp => !rp.IsDeleted);
        }
    }
}
