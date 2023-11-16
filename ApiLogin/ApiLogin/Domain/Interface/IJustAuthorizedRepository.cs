using ApiLogin.Domain.Models;

namespace ApiLogin.Domain.Interface
{
    public interface IJustAuthorizedRepository
    {
        Task<EmployeeModel> RegisterEmployee(EmployeeModel employee);
        Task<EmployeeModel> UpdateEmployee(EmployeeModel employee, int id);
        Task<EmployeeModel> UpdateEmployeeRole(int id, string newRole);
        Task<bool> DeleteEmployee(int id);
        
    }
}
