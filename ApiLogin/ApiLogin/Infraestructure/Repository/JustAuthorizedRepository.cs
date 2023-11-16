using ApiLogin.Domain.Interface;
using ApiLogin.Domain.Models;
using ApiLogin.Infraestructure.Data;

namespace ApiLogin.Infraestructure.Repository
{
    public class JustAuthorizedRepository : IJustAuthorizedRepository
    {
        private readonly LoginDbContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;

        public JustAuthorizedRepository(LoginDbContext dbContext, IEmployeeRepository employeeRepository)
        {
            _dbContext = dbContext;
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeModel> RegisterEmployee(EmployeeModel employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel employee, int id)
        {
            var updateEmployee = await _employeeRepository.GetEmployeeById(id) ??
                throw new Exception("EMPLOYEE NOT FOUND");

            updateEmployee.Name = employee.Name;
            updateEmployee.Email = employee.Email;
            updateEmployee.Password = employee.Password;

            _dbContext.Employees.Update(updateEmployee);
            await _dbContext.SaveChangesAsync();

            return updateEmployee;
        }

        public async Task<EmployeeModel> UpdateEmployeeRole(int id, string newRole)
        {
            var updateEmployee = await _employeeRepository.GetEmployeeById(id) ??
               throw new Exception("EMPLOYEE NOT FOUND");

            updateEmployee.Roles = newRole.ToUpper();

            _dbContext.Employees.Update(updateEmployee);
            await _dbContext.SaveChangesAsync();

            return updateEmployee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var deleteEmployee = await _employeeRepository.GetEmployeeById(id) ??
               throw new Exception("EMPLOYEE NOT FOUND");

            _dbContext.Employees.Remove(deleteEmployee);
            await _dbContext.SaveChangesAsync();

            return true;

        }

    }
}
