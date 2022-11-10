using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VividClub.Data.Entities;

namespace VividClub.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
              .HasMany(u => u.MessagesReceived)
              .WithOne(m => m.Receiver)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasMany(u => u.MessagesSent)
              .WithOne(m => m.Sender)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}