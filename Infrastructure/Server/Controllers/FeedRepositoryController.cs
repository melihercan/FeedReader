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

namespace Infrastructure.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedRepositoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedRepositoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/FeedChannels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedChannel>>> GetFeedChannels()
        {
            var user = await _userManager.GetUserAsync(User);
            ///            var feedChannels = _context.Users
            ///             .Include(p => p.FeedChannelsLink)
            ///          .ThenInclude(p => p.FeedChannel)
            ///       .Single(p => p.UserName == user.UserName);
            ///       

            var feedChannels = await _context.FeedChannels
                .Where(feedChannel => feedChannel.ApplicationUsersLink
                .Any(aufc => aufc.ApplicationUserId == user.Id)).ToListAsync();

            return feedChannels;

            ////return await _context.FeedChannels.ToListAsync();
        }

        // GET: api/FeedChannels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedChannel>> GetFeedChannel(int id)
        {
            var feedChannel = await _context.FeedChannels.FindAsync(id);

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
            if (id != feedChannel.FeedChannelId)
            {
                return BadRequest();
            }

            _context.Entry(feedChannel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedChannelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FeedChannels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FeedChannel>> PostFeedChannel(FeedChannel feedChannel)
        {
            _context.FeedChannels.Add(feedChannel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedChannel", new { id = feedChannel.FeedChannelId }, feedChannel);
        }

        // DELETE: api/FeedChannels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedChannel>> DeleteFeedChannel(int id)
        {
            var feedChannel = await _context.FeedChannels.FindAsync(id);
            if (feedChannel == null)
            {
                return NotFound();
            }

            _context.FeedChannels.Remove(feedChannel);
            await _context.SaveChangesAsync();

            return feedChannel;
        }

        private bool FeedChannelExists(int id)
        {
            return _context.FeedChannels.Any(e => e.FeedChannelId == id);
        }
    }
}
