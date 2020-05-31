using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Server.Data;

namespace Infrastructure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedItem>>> GetFeedItem()
        {
            return await _context.FeedItems.ToListAsync();
        }

        // GET: api/FeedItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedItem>> GetFeedItem(int id)
        {
            var feedItem = await _context.FeedItems.FindAsync(id);

            if (feedItem == null)
            {
                return NotFound();
            }

            return feedItem;
        }

        // PUT: api/FeedItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedItem(int id, FeedItem feedItem)
        {
            if (id != feedItem.FeedItemId)
            {
                return BadRequest();
            }

            _context.Entry(feedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedItemExists(id))
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

        // POST: api/FeedItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FeedItem>> PostFeedItem(FeedItem feedItem)
        {
            _context.FeedItems.Add(feedItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedItem", new { id = feedItem.FeedItemId }, feedItem);
        }

        // DELETE: api/FeedItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedItem>> DeleteFeedItem(int id)
        {
            var feedItem = await _context.FeedItems.FindAsync(id);
            if (feedItem == null)
            {
                return NotFound();
            }

            _context.FeedItems.Remove(feedItem);
            await _context.SaveChangesAsync();

            return feedItem;
        }

        private bool FeedItemExists(int id)
        {
            return _context.FeedItems.Any(e => e.FeedItemId == id);
        }
    }
}
