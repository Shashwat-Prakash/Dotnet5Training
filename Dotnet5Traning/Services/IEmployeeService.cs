using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet5Traning.Models;

namespace Dotnet5Traning.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDetails> CreateEmployee(EmployeeDetails emp);
        Task<EmployeeDetails> GetEmployee(string id);
        Task<List<EmployeeDetails>> GetAllEmployees();
        Task<bool> UpdateEmployee(EmployeeDetails emp, string id);
        Task<bool> DeleteEmployee(string id);
    }
}
