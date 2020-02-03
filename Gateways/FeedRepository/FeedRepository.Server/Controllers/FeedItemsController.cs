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
    public class FeedItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedItemEntity>>> GetFeedItemEntity()
        {
            return await _context.FeedItemEntity.ToListAsync();
        }

        // GET: api/FeedItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedItemEntity>> GetFeedItemEntity(int id)
        {
            var feedItemEntity = await _context.FeedItemEntity.FindAsync(id);

            if (feedItemEntity == null)
            {
                return NotFound();
            }

            return feedItemEntity;
        }

        // PUT: api/FeedItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedItemEntity(int id, FeedItemEntity feedItemEntity)
        {
            if (id != feedItemEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedItemEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedItemEntityExists(id))
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FeedItemEntity>> PostFeedItemEntity(FeedItemEntity feedItemEntity)
        {
            _context.FeedItemEntity.Add(feedItemEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedItemEntity", new { id = feedItemEntity.Id }, feedItemEntity);
        }

        // DELETE: api/FeedItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedItemEntity>> DeleteFeedItemEntity(int id)
        {
            var feedItemEntity = await _context.FeedItemEntity.FindAsync(id);
            if (feedItemEntity == null)
            {
                return NotFound();
            }

            _context.FeedItemEntity.Remove(feedItemEntity);
            await _context.SaveChangesAsync();

            return feedItemEntity;
        }

        private bool FeedItemEntityExists(int id)
        {
            return _context.FeedItemEntity.Any(e => e.Id == id);
        }
    }
}
