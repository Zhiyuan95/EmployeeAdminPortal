using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

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
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };
            
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeToUpdate = dbContext.Employees.Find(id);

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

        public IActionResult DeleteEmployeeById(Guid id)
        {
            var employeeToDelete = dbContext.Employees.Find(id);
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
