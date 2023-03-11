using LayerDAL.Entities;
using LayerDAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository=repository;
        }
        [HttpGet]
        public async Task< ActionResult> ShowEmployees()
        {
            List<Employee> employees = await _repository.GetEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
             _repository.AddEmp(employee);
            return Ok(employee);
        }

    }
}
