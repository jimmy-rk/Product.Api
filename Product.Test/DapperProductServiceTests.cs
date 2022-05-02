using Microsoft.AspNetCore.Http;
using Product.Api.Constants;
using Product.Api.Cqrs.Product;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Product.UnitTests
{
    public class DapperProductServiceTests
    {
        [Fact]
        public async Task GetProduct_InvalidGuid_Returns400Fail()
        {
            //Arrange
            var builder = new DapperProductServiceBuilder();
            var sut = builder.Build();

            //Act
            var serviceResponse = await sut.GetProduct("invalid_guid");

            //Assert
            Assert.False(serviceResponse.Success);
            Assert.Equal(StatusCodes.Status400BadRequest, serviceResponse.StatusCode);
            Assert.Equal(ServiceResponseMessages.InvalidGuid, serviceResponse.ResponseMessage);


        }

        [Fact]
        public async Task Create_AllGood_ReturnsSuccess()
        {
            // Arrange
            var response = new Create.Response()
            {
                Guid = Guid.NewGuid(),
                Id = 1
            };

            var builder = new DapperProductServiceBuilder();
            var sut = builder
                        .Mediator_Mock_CreateCommand(response)
                        .Build();
            var contract = builder.GetProductToCreate();

            //Act
            var serviceResponse = await sut.Create(contract);

            //Assert
            Assert.True(serviceResponse.Success);
            Assert.Equal(ServiceResponseMessages.ProductCreated, serviceResponse.ResponseMessage);
        }

        [Fact]
        public async Task Update_AllGood_ReturnsSuccess()
        {
            // Arrange
            var builder = new DapperProductServiceBuilder();
            var sut = builder
                        .Mediator_Mock_UpdateCommand(1)
                        .Build();
            var contract = builder.GetProductToUpdate();

            //Act
            var serviceResponse = await sut.Update(contract);

            //Assert
            Assert.True(serviceResponse.Success);
            Assert.Equal(ServiceResponseMessages.ProductUpdated, serviceResponse.ResponseMessage);
        }

        [Fact]
        public async Task Update_InvalidGuid_Returns_400Fail()
        {
            // Arrange
            var builder = new DapperProductServiceBuilder();
            var sut = builder
                        .Build();
            var contract = builder.GetProductToUpdate();
            contract.Guid = contract.Guid.Remove(3);

            //Act
            var serviceResponse = await sut.Update(contract);

            //Assert
            Assert.False(serviceResponse.Success);
            Assert.Equal(StatusCodes.Status400BadRequest, serviceResponse.StatusCode);
            Assert.Equal(ServiceResponseMessages.InvalidGuid, serviceResponse.ResponseMessage);
        }
    }
}
