using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VividClub.Data.Entities;

namespace VividClub.Data.Configurations
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.HasMany(x => x.Members).WithMany("Clubs");
            builder.HasOne(x => x.Admin).WithMany();
        }
    }
}