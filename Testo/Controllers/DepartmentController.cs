using Microsoft.AspNetCore.Mvc;
using DomainLayer.Entities;
using ApplicationLayer.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _service;

        public DepartmentController(DepartmentService service)
        {
            _service = service;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetAll()
        {
            var depts = await _service.GetAllAsync();
            return Ok(depts);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var dept = await _service.GetByIdAsync(id);
            if (dept == null) return NotFound();
            return Ok(dept);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult<Department>> Create([FromBody] Department dept)
        {
            await _service.CreateAsync(dept);
            return CreatedAtAction(nameof(GetById), new { id = dept.Id }, dept);
        }
        [HttpGet("check-name")]
        public async Task<IActionResult> CheckNameUnique([FromQuery] string name)
        {
            var exists = await _service.IsNameUniqueAsync(name); 
            return Ok(!exists); 
        }
        // PUT: api/Department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Department dept)
        {
            dept.Id = id;
            var updated = await _service.UpdateAsync(dept);
            if (!updated) return NotFound();
            return NoContent();
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
