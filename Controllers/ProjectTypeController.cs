using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DevHouse1.Models;

namespace DevHouse1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectTypeController(AppDbContext context)
        {
            _context = context;
        }

        // Get all project types
        [HttpGet]
        public async Task<ActionResult<List<ProjectType>>> GetProjectTypes()
        {
            if (_context.ProjectTypes == null)
            {
                return NotFound();
            }
            var projectTypes = await _context.ProjectTypes.ToListAsync();
            return Ok(projectTypes);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProjectType>> GetProjectType(int Id)
        {
            if (_context.ProjectTypes == null)
            {
                return NotFound(new { Message = "The projecttype you search is not found" });
            }
            var projectType = await _context.ProjectTypes.FindAsync(Id);
            if (projectType == null)
            {
                return NotFound(new { Message = "Project Type is not found" });
            }
            return Ok(projectType);

        }

        //add a project type 
        //add authorize later 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ProjectType>> AddProjectType([FromBody] ProjectType projectType)
        {
            var existedProjectType = await _context.ProjectTypes.FirstOrDefaultAsync(p => p.Name == projectType.Name);
            if (existedProjectType != null)
            {
                return BadRequest(new { Message = $"this project type with name {projectType.Name} already exist" });
            }
            await _context.ProjectTypes.AddAsync(projectType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectType), new { Id = projectType.Id }, projectType);
        }

        //update a projecttype 
        [HttpPut("updateprojecttype/{Id:int}")]
        public async Task<ActionResult<ProjectType>> UpdateProjectType(int Id, ProjectType projectType)
        {
            if (Id != projectType.Id)
            {
                return BadRequest();
            }
            _context.Update(projectType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTypeExists(Id))
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

        //helper for update
        private bool ProjectTypeExists(int id)
        {
            return (_context.ProjectTypes?.Any(ProjectType => ProjectType.Id == id)).GetValueOrDefault();
        }


        //delete /id 
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ProjectType>> DeleteProjectType(int Id)
        {
            if (_context.ProjectTypes == null)
            {
                return NotFound();
            }
            var existedProject = await _context.ProjectTypes.FindAsync(Id);
            if (existedProject is null)
            {
                return NotFound(new { Message = "The Project Type is not found" });
            }
            //delete
            _context.ProjectTypes.Remove(existedProject);
            await _context.SaveChangesAsync();
            return NoContent();
        }






    }
}
