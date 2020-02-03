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
    public class FeedUrlsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedUrlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedUrls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedUrlEntity>>> GetFeedUrlEntity()
        {
            return await _context.FeedUrlEntity.ToListAsync();
        }

        // GET: api/FeedUrls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedUrlEntity>> GetFeedUrlEntity(int id)
        {
            var feedUrlEntity = await _context.FeedUrlEntity.FindAsync(id);

            if (feedUrlEntity == null)
            {
                return NotFound();
            }

            return feedUrlEntity;
        }

        // PUT: api/FeedUrls/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedUrlEntity(int id, FeedUrlEntity feedUrlEntity)
        {
            if (id != feedUrlEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedUrlEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedUrlEntityExists(id))
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

        // POST: api/FeedUrls
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FeedUrlEntity>> PostFeedUrlEntity(FeedUrlEntity feedUrlEntity)
        {
            _context.FeedUrlEntity.Add(feedUrlEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedUrlEntity", new { id = feedUrlEntity.Id }, feedUrlEntity);
        }

        // DELETE: api/FeedUrls/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeedUrlEntity>> DeleteFeedUrlEntity(int id)
        {
            var feedUrlEntity = await _context.FeedUrlEntity.FindAsync(id);
            if (feedUrlEntity == null)
            {
                return NotFound();
            }

            _context.FeedUrlEntity.Remove(feedUrlEntity);
            await _context.SaveChangesAsync();

            return feedUrlEntity;
        }

        private bool FeedUrlEntityExists(int id)
        {
            return _context.FeedUrlEntity.Any(e => e.Id == id);
        }
    }
}
