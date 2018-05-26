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
    [Route("api/Questions")]
    public class QuestionsController : Controller
    {
        private readonly DataContext _context;

        public QuestionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            return _context.Questions;
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }


        // GET: api/Questions/5
        [HttpGet("group/{id}")]
        public async Task<IActionResult> GetQuestionsByGroupId([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Questions.Where(q => q.GroupId == id).OrderBy(q => q.Number).ToListAsync();

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }


        // GET: api/Questions/5
        [HttpGet("safebygroup/{id}")]
        public async Task<IActionResult> GetSafeQuestionsByGroupId([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Questions
                .Where(q => q.GroupId == id)
                .OrderBy(q => q.Number)
                .Select(q => new {
                    q.Id,
                    q.Content
                })
                .ToListAsync();

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // GET: api/Questions/answer&value=...
        [HttpGet("answer")]
        public async Task<IActionResult> CheckAnswer([FromQuery] Guid questionId, [FromQuery] string value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Questions.Where(q => q.Id == questionId && q.Answer == value.ToLower()).FirstOrDefaultAsync();

            if (question == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion([FromRoute] Guid id, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            question.Answer = question.Answer.ToLower();
            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions
        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lastQuestion = _context.Questions.Where(q => q.GroupId == question.GroupId).OrderBy(q => q.Number).LastOrDefault();
            int lastNumber = lastQuestion != null ? lastQuestion.Number : 0;

            question.Id = Guid.NewGuid();
            question.Number = lastNumber + 1;
            question.Answer = question.Answer.ToLower();

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return Ok(question);
        }

        private bool QuestionExists(Guid id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}