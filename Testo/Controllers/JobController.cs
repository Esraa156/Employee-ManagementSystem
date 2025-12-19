using ApplicationLayer.Services;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly JobTitleService _service;
    public JobController(JobTitleService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<JobTitle>> Create(JobTitle job)
    {
        if (!await _service.IsJobNameUniqueAsync(job.Name))
            return BadRequest("Job name already exists.");

        await _service.CreateAsync(job);
        return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<JobTitle>> GetById(int id)
    {
        var job = await _service.GetByIdAsync(id);
        if (job == null) return NotFound();
        return Ok(job);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] JobTitle job)
    {
        job.Id = id;
        var updated = await _service.UpdateAsync(job);
        if (!updated) return NotFound();
        return NoContent();
    }
    // GET: api/jobs/is-unique?name=Developer&id=1
    [HttpGet("is-unique")]
    public async Task<ActionResult<bool>> IsUnique([FromQuery] string name, [FromQuery] int id = 0)
    {
        var isUnique = await _service.IsJobNameUniqueAsync(name, id);
        return Ok(isUnique);
    }

}
