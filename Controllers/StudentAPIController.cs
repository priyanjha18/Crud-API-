using AspCoreWebApiUsingCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebApiUsingCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MasterContext context;

        public StudentAPIController(MasterContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudent()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var data = await context.Students.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student Std)
        {
            if (id != Std.Id)
            {
                return BadRequest();
            }
            context.Entry(Std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(Std);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std=await context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            if (std.Id != id)
            {
                return BadRequest();
            }
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
    }
}
