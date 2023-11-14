using ApiLogin.Domain.DTOs;
using ApiLogin.Domain.Interface;
using ApiLogin.Domain.Models;
using ApiLogin.Infraestructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ApiLogin.Infraestructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LoginDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(LoginDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            return await _dbContext.Employees
                .Select(x => new EmployeeDTO()
                {
                    id = x.Id,
                    name = x.Name,
                    role = x.Roles
                })
                .ToListAsync();
        }

        public async Task<EmployeeDTO> GetEmployeeDTOById(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("EMPLOYEE NOT FOUND");

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return employeeDTO;
        }

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            return await _dbContext.Employees.FirstAsync(x => x.Id == id) ??
                throw new Exception("EMPLOYEE NOT FOUND");
        }


        public async Task<EmployeeModel> GetEmployeeByName(string name)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Name == name) ??
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
