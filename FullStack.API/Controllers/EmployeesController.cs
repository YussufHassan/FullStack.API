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
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetEmployee([FromRoute] Guid id)
        {
            Employee employee = await _repository.EditEmp(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
             _repository.AddEmp(employee);

            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult> UpdateEmployee([FromRoute] Guid id, Employee employee)
        {
            if (employee == null)
            {
                return NotFound();
            }
            _repository.UpdateEmp(employee);

            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] Guid id)
        {

            Employee employee = await _repository.EditEmp(id);
            if (employee == null)
            {
                return NotFound();
            }
            _repository.DeleteEmp(id);
            return Ok();
        }
    }
}
