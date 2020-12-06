using Dotnet5Traning.Controllers;
using Dotnet5Traning.Models;
using Dotnet5Traning.Services;
using Moq;
using System;
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
        public async Task CreateEmployee_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;

            // Act
            var result = await employeeController.CreateEmployee(
                emp);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetEmployee_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            string id = null;

            // Act
            var result = await employeeController.GetEmployee(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAllEmployees_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();

            // Act
            var result = await employeeController.GetAllEmployees();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateEmployee_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            EmployeeDetails emp = null;
            string id = null;

            // Act
            var result = await employeeController.UpdateEmployee(
                emp,
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteEmployee_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            string id = null;

            // Act
            var result = await employeeController.DeleteEmployee(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
