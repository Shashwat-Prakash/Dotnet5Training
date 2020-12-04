using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dotnet5Traning.Models
{
    public class EmployeeDetails
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmpId { get; set; }
        [BsonElement]
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpCompany { get; set; }
        public int EmpSalary { get; set; }
        public string EmpContact { get; set; }
        public string EmpCity { get; set; }
        public string EmpState { get; set; }
        public string EmpCountry { get; set; }
    }
}
