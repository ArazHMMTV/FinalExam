using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IEmployeeService
    {
        Task AddEmployee(Employee employee);
        void DeleteEmployee(int id);
        void UpdateEmployee(int id, Employee newEmployee);
        Employee GetEmployee(Func<Employee, bool>? func = null);
        List<Employee> GetAllEmployee(Func<Employee, bool>? func = null);
    }
}
