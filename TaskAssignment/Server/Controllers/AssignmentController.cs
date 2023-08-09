using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAssignment.Shared.Models;
using TaskAssignment.Shared.DataContexts;

namespace TaskAssignment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly SQLDBContext _dbContext;

        public AssignmentController(SQLDBContext context)
        {
            _dbContext = context;
        }

        [Route("GetAssignments")]
        [HttpGet]
        public async Task<IList<Assignment>> GetAssignments()
        {
            try
            {
                var _data = await _dbContext.Assignments.ToListAsync();
                return _data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("GetAssignment/{id}")]
        [HttpGet]
        public async Task<Assignment> GetAssignment(int id)
        {
            try
            {
                var _data = await _dbContext.Assignments.FindAsync(id);
                return _data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("SaveAssignment")]
        [HttpPost]
        public async Task<IActionResult> SaveAssignment(Assignment assignment)
        {
            try
            {
                if (_dbContext.Assignments == null)
                {
                    return Problem("Entity set 'SQLDBContext.Assignment' is null.");
                }

                if (assignment != null)
                {
                    _dbContext.Add(assignment);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Save Successfully!!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        [Route("UpdateAssignment")]
        [HttpPost]
        public async Task<IActionResult> UpdateAssignment(Assignment assignment)
        {
            _dbContext.Entry(assignment).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok("Update Successfully!!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(assignment.Id))
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

        [HttpDelete("DeleteAssignment/{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            if (_dbContext.Assignments == null)
            {
                return NotFound();
            }
            var assignment = await _dbContext.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _dbContext.Assignments.Remove(assignment);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool AssignmentExists(int id)
        {
            return (_dbContext.Assignments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
