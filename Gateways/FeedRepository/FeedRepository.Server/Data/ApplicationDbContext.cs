using FeedRepository.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace FeedRepository.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FeedUrlEntity> FeedUrlEntity { get; set; }
        public DbSet<FeedChannelEntity> FeedChannelEntity { get; set; }
        public DbSet<FeedItemEntity> FeedItemEntity { get; set; }


    }
}
