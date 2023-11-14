using ApiLogin.Application.Services;
using ApiLogin.Domain.Interface;
using ApiLogin.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    [ApiController]
    [Route("api/v1/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public AuthenticateController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpPost]
        public async Task<ActionResult<string>> AuthenticateToken(string name)
        {
           var employee = await _employeeRepository.GetEmployeeByName(name);

            if (employee.Roles == "DEVELOPER")
            {
                var token = TokenService.TokenGenerate(employee);
                return token;
            }

            return  BadRequest("EMPLOYEE IS NOT VALID");
           
        }
    }
}
