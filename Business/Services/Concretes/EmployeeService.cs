using Business.Exceptions;
using Business.Excetions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeService(IEmployeeRepository employeeRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _env = env;
        }

        public async Task AddEmployee(Employee employee)
        {
            if (employee.ImageFile == null)
                throw new FileNullReferenceException("File bosdur");


            employee.ImageUrl=  Helper.SaveFile(_env.WebRootPath,@"uploads\employees",employee.ImageFile);   
 
           await _employeeRepository.AddAsync(employee);
            await _employeeRepository.CommitAsync();
        }

        public void DeleteEmployee(int id)
        {
           var existEmployee = _employeeRepository.Get(x=>x.Id == id);
            if (existEmployee == null)
                throw new EntityNotFound("Bele Employee yoxdur");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\employees", existEmployee.ImageUrl);

            _employeeRepository.Delete(existEmployee);
            _employeeRepository.Commit();
        }

        public List<Employee> GetAllEmployee(Func<Employee, bool>? func = null)
        {
           return  _employeeRepository.GetAll();
        }

        public Employee GetEmployee(Func<Employee, bool>? func = null)
        {
           return _employeeRepository.Get(func);
        }

        public void UpdateEmployee(int id, Employee newEmployee)
        {
           var oldEmployee = _employeeRepository.Get(x=>x.Id == id);
            if (oldEmployee == null)
                throw new EntityNotFound("Employee yoxdur");

            if (newEmployee.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\employees", oldEmployee.ImageUrl);
                oldEmployee.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\employees", newEmployee.ImageFile);
            }
            oldEmployee.Name = newEmployee.Name;    
            oldEmployee.Description = newEmployee.Description;
            oldEmployee.Field = newEmployee.Field;
            _employeeRepository.Commit();
        }
    }
}
