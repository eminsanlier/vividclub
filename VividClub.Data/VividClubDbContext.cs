using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VividClub.Data.Configurations;
using VividClub.Data.Entities;

namespace VividClub.Data
{

    //public class VividClubDbContextFactory : IDesignTimeDbContextFactory<VividClubDbContext>
    //{
    //    public VividClubDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<VividClubDbContext>();

    //        return new VividClubDbContext(optionsBuilder.Options);
    //    }
    //}
    public class VividClubDbContext : IdentityDbContext<User>
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<ClubRequest> ClubRequests { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Club> Clubs { get; set; }



        public VividClubDbContext(DbContextOptions<VividClubDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClubConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new ClubRequestConfiguration());
            //builder.ApplyConfiguration(new EventUserConfiguration());
            //builder.ApplyConfiguration(new PostConfiguration());
            base.OnModelCreating(builder);

        }
    }
}