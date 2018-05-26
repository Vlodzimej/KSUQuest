using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KSUQuest.Model;

namespace KSUQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/Groups")]
    public class GroupsController : Controller
    {
        private readonly DataContext _context;

        public GroupsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public IEnumerable<Group> GetGroups()
        {
            return _context.Groups;
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @group = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);

            if (@group == null)
            {
                return NotFound();
            }

            return Ok(@group);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup([FromRoute] Guid id, [FromBody] Group @group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @group.Id)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] Group @group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            group.Id = Guid.NewGuid();

            _context.Groups.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.Id }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @group = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return Ok(@group);
        }

        private bool GroupExists(Guid id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}