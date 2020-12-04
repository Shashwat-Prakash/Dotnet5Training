using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dotnet5Traning.Models;
using Dotnet5Traning.Services;

namespace Dotnet5Traning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpPost("create-employee")]
        public async Task<ActionResult<EmployeeDetails>> CreateEmployee([FromBody] EmployeeDetails emp)
        {
            if (emp != null)
            {                
                var employee = await _service.CreateEmployee(emp);
                return Ok("Employee is succesfully added. "+employee.EmpId);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var emp = await _service.GetEmployee(id);
            if (emp != null)
            {
                return Ok(emp);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var emp = await _service.GetAllEmployees();
            if (emp != null)
            {
                return Ok(emp);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDetails emp, string id)
        {
            var employee = await _service.GetEmployee(id);
            if (employee != null)
            {
                await _service.UpdateEmployee(emp, id);
                return Ok(emp);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _service.GetEmployee(id);
            if (employee != null)
            {
                await _service.DeleteEmployee(id);
                return Ok("Employee deleted.");
            }
            return NotFound();
        }

    }
}
