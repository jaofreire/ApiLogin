using ApiLogin.Domain.DTOs;
using ApiLogin.Domain.Interface;
using ApiLogin.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    
    [ApiController]
    [Route("api/v1/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAll()
        {
            return await _employeeRepository.GetAllEmployees();
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            return await _employeeRepository.GetEmployeeDTOById(id);
        }
        [Authorize]
        [HttpPost("Register")]
        public async Task<ActionResult<EmployeeModel>> Register(EmployeeModel newEmployee)
        {
            return await _employeeRepository.AddNewEmployee(newEmployee);
        }
        [Authorize]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<EmployeeModel>> Update(EmployeeModel newEmployee, int id)
        {
            newEmployee.Id = id;
            return await _employeeRepository.UpdateEmployee(newEmployee, id);
        }
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

    }
}
