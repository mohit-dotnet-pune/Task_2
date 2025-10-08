using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_2.Model;
using Task_2.Repository;

namespace Task_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _repo = employeeRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById([FromRoute] int id)
        {
            var emp = _repo.GetEmployeeById(id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_repo.GetAllEmployees());
        }

        [HttpGet("byDept/{dept}")]
        public IActionResult GetEmployeesByDept([FromRoute] string dept)
        {
            return Ok(_repo.GetEmployeesByDept(dept));
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repo.AddEmploye(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            _repo.UpdateEmployee(employee);
            return Ok(employee);
        }

        [HttpPatch("{id}/email")]
        public IActionResult UpdateEmployeeEmail([FromRoute] int id, [FromQuery] string email)
        {
            _repo.UpdateEmployeeEmail(id, email);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            _repo.DeleteEmployee(id);
            return NoContent();
        }
    }
}
