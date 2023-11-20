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

        public async Task<EmployeeModel> AddNewEmployeeDTO(EmployeeDTORegisterLogin newEmployee)
        {
            //var employeeModel = _mapper.Map<EmployeeModel>(newEmployee);

            EmployeeModel employee = new EmployeeModel()
            {
                
                Name = newEmployee.Name,
                Email = newEmployee.Email,
                Password = newEmployee.Password,
                Roles = "BEGINNER"
                
            };
            await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel newEmployee, int id)
        {
            var employee = await GetEmployeeById(id) ??
                throw new Exception("EMPLOYEE NOT FOUND");

            employee.Name = newEmployee.Name;
            employee.Email = newEmployee.Email;
            employee.Password = newEmployee.Password;
          

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
