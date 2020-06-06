using Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Domain.Entities.FeedChannel> FeedChannels { get; set; }

        public DbSet<Domain.Entities.FeedItem> FeedItems { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserFeedChannel>()
                .HasKey(t => new { t.ApplicationUserId, t.FeedChannelId });

            modelBuilder.Entity<ApplicationUserFeedChannel>()
                .HasOne(aufc => aufc.ApplicationUser)
                .WithMany(aufc => aufc.FeedChannelsLink)
                .HasForeignKey(aufc => aufc.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserFeedChannel>()
                .HasOne(aufc => aufc.FeedChannel)
                .WithMany(aufc => aufc.ApplicationUsersLink)
                .HasForeignKey(aufc => aufc.FeedChannelId);
        }
    }
}
