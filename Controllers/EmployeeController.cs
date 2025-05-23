using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        // Constructor: receives the database context so we can use it in our controller methods.
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployees = await dbContext.Employees.ToListAsync();

            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);

        }

        [HttpPost]
        //// We use AddEmployeeDto here instead of the Employee entity class
        // because we only want to accept the specific fields we need from the client.
        // This improves security by preventing users from accidentally or intentionally
        // setting fields like Id or DateCreated which should be controlled by the backend.
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };
            
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeEntity.Id }, employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeToUpdate = await dbContext.Employees.FindAsync(id);

            if (employeeToUpdate is null)
            {
                return NotFound();
            }

            employeeToUpdate.Name = updateEmployeeDto.Name;
            employeeToUpdate.Email = updateEmployeeDto.Email;
            employeeToUpdate.Phone = updateEmployeeDto.Phone;
            employeeToUpdate.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employeeToUpdate);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteEmployeeById(Guid id)
        {
            var employeeToDelete = await dbContext.Employees.FindAsync(id);
            if (employeeToDelete is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employeeToDelete);
            dbContext.SaveChanges();

            return Ok(new
            {
                message = "Employee deleted successfully",
                employee = employeeToDelete
            });

        }

    }
}
