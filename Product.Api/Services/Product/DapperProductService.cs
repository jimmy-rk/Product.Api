using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Product.Api.Constants;
using Product.Api.Extensions;
using Product.Api.Models.General;
using Product.Api.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Services.Product
{
    public class DapperProductService : IProductService
    {
        private readonly ILogger<DapperProductService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DapperProductService(ILogger<DapperProductService> logger
            , IMediator mediator
            , IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<SelectListItem>>> GetProductGroupTypes()
        {
            try
            {
                var mediatorRes = await _mediator.Send(new Cqrs.Product.GetProductGroupTypes.Query());
                var mapperRes = _mapper.Map<IEnumerable<SelectListItem>>(mediatorRes);
                return new ServiceResponse<IEnumerable<SelectListItem>>(true, mapperRes, StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogException("Get Product Group Types", ex);
                return new ServiceResponse<IEnumerable<SelectListItem>>(false, "Unexpected_error", StatusCodes.Status500InternalServerError);

            }
        }

        public async Task<ServiceResponse<ProductViewModel>> GetProduct(string guid)
        {
            try
            {
                if (!Guid.TryParse(guid, out Guid productGuid))
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.InvalidGuid, StatusCodes.Status400BadRequest);
                var result = await _mediator.Send(new Cqrs.Product.GetByGuid.Query { Guid = productGuid });
                if (result == null)
                {
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.ProductDoesNotExist, StatusCodes.Status404NotFound);
                }
                var product = _mapper.Map<ProductViewModel>(result);
                return new ServiceResponse<ProductViewModel>(true, product, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogException("Get Product", ex);
                return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.UnexpectedError, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ServiceResponse<ProductViewModel>> Create(ProductViewModel product)
        {
            try
            {
                var createCmd = _mapper.Map<Cqrs.Product.Create.Command>(product);
                var result = await _mediator.Send(createCmd);
                product.Guid = result.Guid.ToString();

                if (result == null || result.Id.Equals(0) || result.Guid == Guid.Empty)
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.ProductSaveError, StatusCodes.Status500InternalServerError);

                return new ServiceResponse<ProductViewModel>(true, product, ServiceResponseMessages.ProductCreated, StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogException("Create", ex);
                return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.UnexpectedError, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
