using ApiLogin.Data;
using ApiLogin.Models;
using ApiLogin.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiLogin.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LoginDbContext _dbContext;
        public EmployeeRepository(LoginDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
           return await _dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("EMPLOYEE NOT FOUND");
        }

        public async Task<EmployeeModel> AddNewEmployee(EmployeeModel newEmployee)
        {
            await _dbContext.Employees.AddAsync(newEmployee);
            await _dbContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel newEmployee, int id)
        {
            var employee = await GetEmployeeById(id) ??
                throw new Exception("EMPLOYEE NOT FOUND");

            employee.Name = newEmployee.Name;
            employee.Email = newEmployee.Email;
            employee.Password = newEmployee.Password;
            employee.Roles = newEmployee.Roles;

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await GetEmployeeById(id) ??
                throw new Exception("EMPLOYEE NOT FOUND");

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return true;
        } 
    }
}
