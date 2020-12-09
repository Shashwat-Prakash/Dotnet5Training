using Dotnet5Traning.Controllers;
using Dotnet5Traning.Models;
using Dotnet5Traning.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dotnet5TrainingTest.Controllers
{
    public class EmployeeControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IEmployeeService> mockEmployeeService;

        public EmployeeControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockEmployeeService = this.mockRepository.Create<IEmployeeService>();
        }

        private EmployeeController CreateEmployeeController()
        {
            return new EmployeeController(
                this.mockEmployeeService.Object);
        }

        [Fact]
        public async Task CreateEmployee_SuccesfulyAdded_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = new EmployeeDetails() { 
                EmpName = "Shashwat", EmpEmail="", EmpCity = "Chapra", 
                EmpCompany="Agenty Analytics", EmpContact="9066740766", EmpSalary = 20000
            };
            mockEmployeeService.Setup(i => i.CreateEmployee(emp)).ReturnsAsync(emp);

            // Act
             var result = (OkObjectResult) await employeeController.CreateEmployee(
                emp);
            // Assert           
            Assert.Equal("Employee is succesfully added. ", result.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CreateEmployee_SuccesfulyAdded_Error()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;            
            // Act
            var result = await employeeController.CreateEmployee(
               emp);
            var data = result as NotFoundObjectResult;
            // Assert           
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee details cannot be empty.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetEmployeeByID_StateUnderTest_EmployeeFound()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            mockEmployeeService
                .Setup(x => x.GetEmployee(It.IsAny<string>()))
                .ReturnsAsync(new EmployeeDetails()
                {
                    EmpName = "Shashwat",
                    EmpEmail = "",
                    EmpCity = "Chapra",
                    EmpCompany = "Agenty Analytics",
                    EmpContact = "9066740766",
                    EmpSalary = 20000
                });
            string id = "hb9dabh4js87dfjksf32h32sjkdhc";

            // Act
            var result = await employeeController.GetEmployee(
                id);
            var data = result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, data.StatusCode);
            Assert.NotNull(data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetEmployeeByID_StateUnderTest_NotFound()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;
            mockEmployeeService
                .Setup(x => x.GetEmployee(It.IsAny<string>()))
                .ReturnsAsync(emp);
            string id = "hb9dabh4js87dfjksf32h32sjkdhc";

            // Act
            var result = await employeeController.GetEmployee(
                id);
            var data = result as NotFoundObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee not found.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetEmployeeByID_StateUnderTest_EmpIdNull()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();            
            string id = null;

            // Act
            var result = await employeeController.GetEmployee(
                id);
            var data = result as NotFoundObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee Id is required.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAllEmployees_StateUnderTest_EmployeeFound()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();            
            mockEmployeeService
                .Setup(i => i.GetAllEmployees())
                .ReturnsAsync(new List<EmployeeDetails>() { new EmployeeDetails {
                    EmpName = "Shashwat", EmpEmail="shashwat@agenty.com", EmpCity = "Chapra",
                EmpCompany="Agenty Analytics", EmpContact="9066740766", EmpSalary = 20000
                }});
            // Act
            var result = await employeeController.GetAllEmployees();
            var data = result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, data.StatusCode);
            Assert.NotNull(data.Value);            
            this.mockRepository.VerifyAll();           
        }

        [Fact]
        public async Task GetAllEmployees_StateUnderTest_EmployeeNotFound()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            List<EmployeeDetails> emp = null;
            mockEmployeeService
                .Setup(i => i.GetAllEmployees())
                .ReturnsAsync(emp);
            // Act
            var result = await employeeController.GetAllEmployees();
            var data = result as NotFoundObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Zero Employee Found.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateEmployee_StateUnderTest_SuccesfullyUpdated()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = new EmployeeDetails()
            {
                EmpName = "Shashwat",
                EmpEmail = "",
                EmpCity = "Chapra",
                EmpCompany = "Agenty Analytics",
                EmpContact = "9066740766",
                EmpSalary = 20000
            };
            string id = "sdhgu87432hb28hr8392rf923cff94";
            mockEmployeeService
                .Setup(i => i.UpdateEmployee(emp, It.IsAny<string>()))
                .ReturnsAsync(true); 
            mockEmployeeService.Setup(i => i.GetEmployee(It.IsAny<string>()))
                .ReturnsAsync(emp);
            // Act
            var result = await employeeController.UpdateEmployee(
                emp,
                id);
            var data = result as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, data.StatusCode);
            Assert.NotNull(data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateEmployee_StateUnderTest_EmployeeIdNull()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = new EmployeeDetails()
            {
                EmpName = "Shashwat",
                EmpEmail = "",
                EmpCity = "Chapra",
                EmpCompany = "Agenty Analytics",
                EmpContact = "9066740766",
                EmpSalary = 20000
            };
            string id = null; ;
            
            // Act
            var result = await employeeController.UpdateEmployee(
                emp,
                id);
            var data = result as NotFoundObjectResult;
            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee Id cannot be empty.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateEmployee_StateUnderTest_EmployeeNotFound()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;
            string id = "89783hf89735849fh0129837hfhf39";
            mockEmployeeService.Setup(i => i.GetEmployee(It.IsAny<string>())).ReturnsAsync(emp);

            // Act
            var result = await employeeController.UpdateEmployee(
                emp,
                id);
            var data = result as NotFoundObjectResult;
            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee not found.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteEmployee_StateUnderTest_SuccesfullyDeleted()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            string id = "yf78345hrf348534hrfd92dj298";
            mockEmployeeService
                .Setup(i => i.DeleteEmployee(It.IsAny<string>()))
                .ReturnsAsync(true);
            mockEmployeeService
                .Setup(i => i.GetEmployee(It.IsAny<string>()))
                .ReturnsAsync(new EmployeeDetails()
                {
                    EmpName = "Shashwat",
                    EmpEmail = "",
                    EmpCity = "Chapra",
                    EmpCompany = "Agenty Analytics",
                    EmpContact = "9066740766",
                    EmpSalary = 20000
                });
            // Act
            var result = await employeeController.DeleteEmployee(
                id);
            var data = result as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, data.StatusCode);
            Assert.Equal("Employee deleted successfully.", data.Value);
            Assert.NotNull(data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteEmployee_StateUnderTest_EmployeeIdNull()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            string id = null;            
            // Act
            var result = await employeeController.DeleteEmployee(
                id);
            var data = result as NotFoundObjectResult;
            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee Id cannot be null.", data.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteEmployee_StateUnderTest_EmployeeNotFound()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;
            string id = "yf78345hrf348534hrfd92dj298";            
            mockEmployeeService
                .Setup(i => i.GetEmployee(It.IsAny<string>()))
                .ReturnsAsync(emp);
            // Act
            var result = await employeeController.DeleteEmployee(
                id);
            var data = result as NotFoundObjectResult;
            // Assert
            Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Equal("Employee not found.", data.Value);
            this.mockRepository.VerifyAll();
        }
    }
}
