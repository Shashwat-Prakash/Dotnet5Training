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

        /*[Fact]
        public async Task CreateEmployee_SuccesfulyAdded_Error()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;
                *//*new EmployeeDetails()
            {
                EmpName = "Shashwat",
                EmpEmail = "",
                EmpCity = "Chapra",
                EmpCompany = "Agenty Analytics",
                EmpContact = "9066740766",
                EmpSalary = 20000
            };*//*
            //mockEmployeeService.Setup(i => i.CreateEmployee(emp)).ReturnsAsync(emp);
            mockEmployeeService.Setup(x => x.CreateEmployee(emp)).ReturnsAsync(emp);
            // Act
            var result = (OkObjectResult) await employeeController.CreateEmployee(
               emp);
            // Assert           
            Assert.Equal(400, result.StatusCode);
            this.mockRepository.VerifyAll();
        }*/

        [Fact]
        public async Task GetEmployeeByID_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            mockEmployeeService
                .Setup(x => x.GetEmployee(It.IsAny<string>()))
                .ReturnsAsync(new EmployeeDetails() {
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
        public async Task GetAllEmployees_StateUnderTest_ExpectedBehavior()
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
        public async Task UpdateEmployee_StateUnderTest_ExpectedBehavior()
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
        public async Task DeleteEmployee_StateUnderTest_ExpectedBehavior()
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
            Assert.NotNull(data.Value);
            this.mockRepository.VerifyAll();
        }
    }
}
