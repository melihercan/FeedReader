using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedRepository.Server.Data;
using FeedRepository.Shared.Models;

namespace FeedRepository.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedChannelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedChannelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedChannels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedChannelEntity>>> GetFeedChannelEntity()
        {
            return await _context.FeedChannelEntity.ToListAsync();
        }

        // GET: api/FeedChannels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedChannelEntity>> GetFeedChannelEntity(int id)
        {
            var feedChannelEntity = await _context.FeedChannelEntity.FindAsync(id);

            if (feedChannelEntity == null)
            {
                return NotFound();
            }

            return feedChannelEntity;
        }

        // PUT: api/FeedChannels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedChannelEntity(int id, FeedChannelEntity feedChannelEntity)
        {
            if (id != feedChannelEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedChannelEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedChannelEntityExists(id))
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FeedChannelEntity>> PostFeedChannelEntity(FeedChannelEntity feedChannelEntity)
        {
            _context.FeedChannelEntity.Add(feedChannelEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedChannelEntity", new { id = feedChannelEntity.Id }, feedChannelEntity);
        }

        // DELETE: api/FeedChannels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedChannelEntity>> DeleteFeedChannelEntity(int id)
        {
            var feedChannelEntity = await _context.FeedChannelEntity.FindAsync(id);
            if (feedChannelEntity == null)
            {
                return NotFound();
            }

            _context.FeedChannelEntity.Remove(feedChannelEntity);
            await _context.SaveChangesAsync();

            return feedChannelEntity;
        }

        private bool FeedChannelEntityExists(int id)
        {
            return _context.FeedChannelEntity.Any(e => e.Id == id);
        }
    }
}
