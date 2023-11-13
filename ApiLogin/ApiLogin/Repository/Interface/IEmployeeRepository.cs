using ApiLogin.Models;

namespace ApiLogin.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<EmployeeModel> AddNewEmployee(EmployeeModel newEmployee);
        Task<EmployeeModel> UpdateEmployee(EmployeeModel newEmployee, int id);
        Task<bool> DeleteEmployee(int id); 

    }
}
