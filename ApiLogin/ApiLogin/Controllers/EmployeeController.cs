using ApiLogin.Models;
using ApiLogin.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<EmployeeModel>>> GetAll()
        {
            return await _employeeRepository.GetAllEmployees();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> Register(EmployeeModel newEmployee)
        {
            return await _employeeRepository.AddNewEmployee(newEmployee);
        }
       
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeModel>> Update(EmployeeModel newEmployee, int id)
        {
            newEmployee.Id = id;
            return await _employeeRepository.UpdateEmployee(newEmployee, id);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

    }
}
