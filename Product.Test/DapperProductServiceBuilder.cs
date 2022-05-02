using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Product.Api.Cqrs.Product;
using Product.Api.Models.Product;
using Product.Api.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.UnitTests
{
    public class DapperProductServiceBuilder
    {
        public Mock<IMediator> MediatorMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<ILogger<DapperProductService>> LoggerMock { get; }

        public DapperProductServiceBuilder()
        {
            MediatorMock = new Mock<IMediator>();
            MapperMock = new Mock<IMapper>();
            LoggerMock = new Mock<ILogger<DapperProductService>>();
        }

        public DapperProductService Build()
        {
            return new DapperProductService(LoggerMock.Object, MediatorMock.Object, MapperMock.Object);
        }

        #region MediatorMocks   

        public DapperProductServiceBuilder Mediator_Mock_GetProduct()
        {
            MediatorMock.Setup(mock => mock.Send(It.IsAny<GetByGuid.Query>(),
                            It.IsAny<CancellationToken>()));
            return this;
        }

        public DapperProductServiceBuilder Mediator_Mock_CreateCommand(Create.Response res)
        {
            MediatorMock.Setup(mock => mock.Send(It.IsAny<Create.Command>(),
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(res);
            return this;
        }
        

        public DapperProductServiceBuilder Mediator_Mock_UpdateCommand(int res)
        {
            MediatorMock.Setup(mock => mock.Send(It.IsAny<Update.Command>(),
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Update.Response() { UpdateCount = res, Guid = Guid.NewGuid()});
            return this;
        }


        #endregion

        #region MapperMocks

        public DapperProductServiceBuilder Mapper_Mock_ProductViewModel(ProductViewModel result)
        {
            MapperMock.Setup(mock => mock.Map<ProductViewModel>(It.IsAny<GetByGuid.Response>())).Returns(result);
            return this;
        }

        #endregion

        #region TestData

        public ProductViewModel GetProductToCreate()
        {
            return new ProductViewModel()
            {
                Guid = null,
                Name = "product",
                ProductGroupNk = "COM",
                Price = 120.34m
            };

        }

        public ProductViewModel GetProductToUpdate()
        {
            return new ProductViewModel()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = "product",
                ProductGroupNk = "COM",
                Price = 120.34m
            };

        }


        #endregion
    }
}
