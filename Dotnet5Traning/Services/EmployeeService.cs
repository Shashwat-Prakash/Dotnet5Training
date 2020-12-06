using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet5Traning.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Dotnet5Traning.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IMongoCollection<EmployeeDetails> _empCollection { get; }
        public EmployeeService()
        {
            BsonClassMap.RegisterClassMap<EmployeeDetails>(t =>
            {
                t.AutoMap();
                t.SetIdMember(t.GetMemberMap(t => t.EmpId));
            });
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("agentytask");
            _empCollection = db.GetCollection<EmployeeDetails>("employees");
        }

        public async Task<EmployeeDetails> CreateEmployee(EmployeeDetails emp)
        {
            await _empCollection.InsertOneAsync(emp);      
            return emp;      
        }

        public async Task<EmployeeDetails> GetEmployee(string id)
        {
            var employee = await _empCollection.Find(emp => emp.EmpId.Equals(id)).FirstOrDefaultAsync();
            return employee;
        }

        public async Task<List<EmployeeDetails>> GetAllEmployees()
        {
            var employees = await _empCollection.Find(_=> true).ToListAsync();
            return employees;
        }

        public async Task<bool> UpdateEmployee(EmployeeDetails employee, string id)
        {
            var isEmployeeUpdated = await _empCollection.ReplaceOneAsync(emp => emp.EmpId.Equals(id), employee);
            return isEmployeeUpdated.IsAcknowledged && isEmployeeUpdated.ModifiedCount > 0;
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            var isEmployeeDeleted = await _empCollection.DeleteOneAsync(emp => emp.EmpId.Equals(id));
            return isEmployeeDeleted.IsAcknowledged && isEmployeeDeleted.DeletedCount > 0;
        }
    }
}
