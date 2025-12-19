using ApplicationLayer.DTOs;
using ApplicationLayer.Services;
using AutoMapper;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;
    private readonly IMapper _mapper;

    public EmployeeController(IMapper mapper, EmployeeService service)
    {
        _service = service;
        _mapper = mapper;

    }

    // GET: api/employees
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _service.GetAllAsync();
        var employeesDto = _mapper.Map<List<GetAllEmployeeDto>>(employees);

        // 3️⃣ Return 200 OK with the DTO list
        return Ok(employeesDto);
    }

    // GET: api/employees/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _service.GetByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    // POST: api/employees
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Create(EmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 1️⃣ Check if email already exists
        bool emailExists = await _service.EmailExistsAsync(dto.Email, 0);
        if (emailExists)
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return BadRequest(ModelState);
        }

        // 2️⃣ Map DTO -> Entity using AutoMapper
        var employee = _mapper.Map<Employee>(dto);

        // 3️⃣ Save to database via service
        await _service.AddAsync(employee);

        // 4️⃣ Map back to DTO if needed
        var resultDto = _mapper.Map<EmployeeDto>(employee);

        // 5️⃣ Return 201 Created with Location header
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, resultDto);
    }



    // PUT: api/employees/{id}
    // PUT: api/employees/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 1️⃣ Check if employee exists
        var existingEmployee = await _service.GetByIdAsync(id);
        if (existingEmployee == null)
            return NotFound($"Employee with ID {id} not found.");

        bool emailExists = await _service.EmailExistsAsync(dto.Email, id);
        if (emailExists)
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return BadRequest(ModelState);
        }

        _mapper.Map(dto, existingEmployee);

        await _service.UpdateAsync(existingEmployee);

        var resultDto = _mapper.Map<EmployeeDto>(existingEmployee);

        return Ok(resultDto);
    }


    // DELETE: api/employees/{id}  
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _service.SoftDeleteAsync(id);
        return NoContent();
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] DomainLayer.Entities.filters filters)
    {
        // You can implement filtering and sorting logic inside repository
        var result = await _service.SearchAsync(filters);
        return Ok(result);

        // 3️⃣ Return 200 OK with the DTO list
    }


}