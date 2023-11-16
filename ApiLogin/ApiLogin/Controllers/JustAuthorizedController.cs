using ApiLogin.Domain.Interface;
using ApiLogin.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogin.Controllers
{
    [ApiController]
    [Route("api/v1/JusAuthorized")]
    public class JustAuthorizedController : ControllerBase
    {
        private readonly IJustAuthorizedRepository _justAuthorized;

        public JustAuthorizedController(IJustAuthorizedRepository justAuthorized)
        {
            _justAuthorized = justAuthorized;
        }
        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult<EmployeeModel>> Register(EmployeeModel newEmployee)
        {
           var employee = await _justAuthorized.RegisterEmployee(newEmployee);

            if (newEmployee == null) BadRequest("THE EMPLOYEE CAN'T BE NULL");

            return employee;
        }
        [Authorize]
        [HttpPatch("updaterole/{id}")]
        public async Task<ActionResult<EmployeeModel>> UpdateRole(int id, string newRole)
        {
            return await _justAuthorized.UpdateEmployeeRole(id, newRole);
        }
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _justAuthorized.DeleteEmployee(id);
        }
    }
}
