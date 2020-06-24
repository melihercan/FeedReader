using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedChannelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedChannelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/FeedChannels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedChannel>>> GetFeedChannels()
        {
            var user = await _userManager.GetUserAsync(User);
            //var feedChannelsx = _context.Users
              //  .Include(p => p.FeedChannelsLink)
                //.ThenInclude(p => p.FeedChannel)
                //.Single(p => p.UserName == user.UserName)
                //.FeedChannelsLink.Select(p => p.FeedChannel);

            var feedChannels = await _context.FeedChannels
                .Where(feedChannel => feedChannel.ApplicationUsersLink.Any(aufc => aufc.ApplicationUserId == user.Id))
                .Include(feedChannel => feedChannel.FeedItems)
                .ToListAsync();

            return feedChannels;

            ////return await _context.FeedChannels.ToListAsync();
        }

        // GET: api/FeedChannels/5
        [HttpGet("{id}")]
        public ActionResult<FeedChannel> GetFeedChannel(int id)
        {
            var feedChannel = _context.FeedChannels
                .Include(feedChannel => feedChannel.FeedItems)
                .SingleOrDefault(feedChannel => feedChannel.FeedChannelId == id);

            if (feedChannel == null)
            {
                return NotFound();
            }

            return feedChannel;
        }

        // PUT: api/FeedChannels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedChannel(int id, FeedChannel feedChannel)
        {
            var dbFeedChannel = _context.FeedChannels
                .Include(feedChannel => feedChannel.FeedItems)
                .Single(feedChannel => feedChannel.FeedChannelId == id);

            // Update parent.
            feedChannel.FeedChannelId = id;
            _context.Entry(dbFeedChannel).CurrentValues.SetValues(feedChannel);

            // Remove child items. Update of items is not possible.
            var dbFeedItems = dbFeedChannel.FeedItems.ToList();
            foreach (var dbFeedItem in dbFeedItems)
            {
                var feedItem = feedChannel.FeedItems.SingleOrDefault(fi => fi.Link == dbFeedItem.Link);
                if (feedItem == null)
                {
                    _context.Remove(dbFeedItem);
                }
            }

            // Add new items.
            foreach (var feedItem in feedChannel.FeedItems)
            {
                if(dbFeedItems.All(fi => fi.Link != feedItem.Link))
                {
                    dbFeedChannel.FeedItems.Add(feedItem);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/FeedChannels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostFeedChannel(FeedChannel feedChannel)
        {
            var user = await _userManager.GetUserAsync(User);

            // Check if channel is already created for that user.
            if(_context.FeedChannels
                .Where(fc => fc.ApplicationUsersLink.Any(aufc => aufc.ApplicationUserId == user.Id))
                .FirstOrDefault(fc => fc.Link == feedChannel.Link) != null)
            {
                return Conflict();
            }

            feedChannel.ApplicationUsersLink = new List<ApplicationUserFeedChannel>
            {
                new ApplicationUserFeedChannel
                {
                    ApplicationUser = user,
                    FeedChannel = feedChannel
                }
            };

            _context.FeedChannels.Add(feedChannel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FeedChannels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedChannel(int id)
        {
            var feedChannel = await _context.FeedChannels.FindAsync(id);
            if (feedChannel == null)
            {
                return NotFound();
            }

            _context.FeedChannels.Remove(feedChannel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
