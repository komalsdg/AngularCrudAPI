using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularCrudAPI.Models;
using Newtonsoft.Json;
using AngularCrudAPI.Interface.Services;
using System.Web;

namespace AngularCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly CrudDbContext _context;

        public ProjectsController(CrudDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] Paging<Project> pagingmodel)
        {
            var isNumeric = int.TryParse(pagingmodel.filter, out int filterValue);
            var isDateTime = DateTime.TryParse(pagingmodel.filter, out DateTime dtFilterValue);

            var query = (from p in _context.projects
                         join g in _context.groups on p.Group equals g.GroupId
                         where pagingmodel.filter != null ?
                               (p.Area.ToLower().Contains(pagingmodel.filter.ToLower()) ||
                          p.PracticeType.ToLower().Contains(pagingmodel.filter.ToLower()) ||
                          p.ProjectSize.ToLower().Contains(pagingmodel.filter.ToLower()) ||
                          g.GroupName.ToLower().Contains(pagingmodel.filter.ToLower()) ||
                          (isDateTime ? (p.StartDate.CompareTo(dtFilterValue) >= 0) : false) ||
                          (isNumeric ? p.Quantity == filterValue : p.Quantity.Equals(pagingmodel.filter.ToLower())))
                          : true
                         select new
                         {
                             ProjectId = p.ProjectId,
                             GroupId = g.GroupId,
                             Group = g.GroupName,
                             PracticeType = p.PracticeType,
                             Area = p.Area,
                             ProjectSize = p.ProjectSize,
                             Quantity = p.Quantity,
                             StartDate = p.StartDate
                         }).AsQueryable();

            var list = await PagingService.GetPagination(query, pagingmodel.PageNumber, pagingmodel.OrderBy, pagingmodel.OrderByDesc, pagingmodel.PageSize);

            

            return new JsonResult(list);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            var project = await _context.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        private bool ProjectExists(int id)
        {
            return _context.projects.Any(e => e.ProjectId == id);
        }


        
        [HttpGet]
        [Route("GetByAutoSearch/{search}")]
        public async Task<IActionResult> GetByAutoSearch(string search) {
            var project =  (from p in _context.projects
                           join g in _context.groups on p.Group equals g.GroupId
                           where search != "all" ?
                            p.PracticeType.ToLower().Contains(search.ToLower()) : true
                           select new
                           {
                               ProjectId = p.ProjectId,
                               GroupId = g.GroupId,
                               Group = g.GroupName,
                               PracticeType = p.PracticeType,
                               Area = p.Area,
                               ProjectSize = p.ProjectSize,
                               Quantity = p.Quantity,
                               StartDate = p.StartDate
                           }).ToListAsync();

            return new JsonResult(await project);
        }
    }
}
