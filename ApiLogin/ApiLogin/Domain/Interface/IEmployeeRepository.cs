using ApiLogin.Domain.DTOs;
using ApiLogin.Domain.Models;

namespace ApiLogin.Domain.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeDTOById(int id);
        Task<EmployeeModel> GetEmployeeById (int id);
        Task<EmployeeModel> GetEmployeeByName(string name);
        Task<EmployeeModel> AddNewEmployeeDTO(EmployeeDTORegisterLogin newEmployee);
        Task<EmployeeModel> UpdateEmployee(EmployeeModel newEmployee, int id);
        Task<bool> DeleteEmployee(int id);

    }
}
