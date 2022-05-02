using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Api.Constants;
using Product.Api.Extensions;
using Product.Api.Models.EF;
using Product.Api.Models.General;
using Product.Api.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Services.Product
{
    public class EfProductService : IProductService
    {
        private readonly ILogger<EfProductService> _logger;
        private readonly IMapper _mapper;
        private readonly ProductDbContext _dbContext;
        public EfProductService(ILogger<EfProductService> logger
            , IMapper mapper
            ,ProductDbContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ServiceResponse<ProductViewModel>> Create(ProductViewModel product)
        {
            try
            {
                var efProduct = _mapper.Map<Api.Models.EF.Product>(product);

                _dbContext.Add(efProduct);
                await _dbContext.SaveChangesAsync();


                if (efProduct == null || efProduct.Id.Equals(0) || efProduct.Guid == Guid.Empty)
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.ProductSaveError, StatusCodes.Status500InternalServerError);

                var result = _mapper.Map<ProductViewModel>(efProduct);

                return new ServiceResponse<ProductViewModel>(true, result, ServiceResponseMessages.ProductCreated, StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogException("Create", ex);
                return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.UnexpectedError, StatusCodes.Status500InternalServerError);
            }
            
        }

        public async Task<ServiceResponse<ProductViewModel>> GetProduct(string guid)
        {
            try
            {
                if (!Guid.TryParse(guid, out Guid productGuid))
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.InvalidGuid, StatusCodes.Status400BadRequest);

                var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Guid == productGuid);
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

        public async Task<ServiceResponse<IEnumerable<SelectListItem>>> GetProductGroupTypes()
        {
            try
            {
                var efRresult = await _dbContext.ProductGroups.ToListAsync();
                var mapperRes = _mapper.Map<IEnumerable<SelectListItem>>(efRresult);
                return new ServiceResponse<IEnumerable<SelectListItem>>(true, mapperRes, StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogException("Get Product Group Types", ex);
                return new ServiceResponse<IEnumerable<SelectListItem>>(false, "Unexpected_error", StatusCodes.Status500InternalServerError);

            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductViewModel>>> GetProducts()
        {
            try
            {
                var efRresult = await  _dbContext.Products.ToListAsync(); 
                var mapperRes = _mapper.Map<IEnumerable<ProductViewModel>>(efRresult);
                return new ServiceResponse<IEnumerable<ProductViewModel>>(true, mapperRes, StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogException("Get Products", ex);
                return new ServiceResponse<IEnumerable<ProductViewModel>>(false, "Unexpected_error", StatusCodes.Status500InternalServerError);

            }
        }

        public async Task<ServiceResponse<ProductViewModel>> Update(ProductViewModel product)
        {
            try
            {
                if (!Guid.TryParse(product.Guid, out Guid productGuid))
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.InvalidGuid, StatusCodes.Status400BadRequest); 

                var originalProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Guid == productGuid);
                if (originalProduct == null)
                {
                    return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.ProductDoesNotExist, StatusCodes.Status404NotFound);
                }

                originalProduct.Name = product.Name;
                originalProduct.ProductGroupNk = product.ProductGroupNk;
                originalProduct.Price = product.Price;
                originalProduct.Comments = product.Comments;
                
                _dbContext.Products.Update(originalProduct);

                await _dbContext.SaveChangesAsync();

                var result = _mapper.Map<ProductViewModel>(originalProduct);

                return new ServiceResponse<ProductViewModel>(true, result, ServiceResponseMessages.ProductUpdated, StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                _logger.LogException("Update", ex);
                return new ServiceResponse<ProductViewModel>(false, ServiceResponseMessages.UnexpectedError, StatusCodes.Status500InternalServerError);
            }
        }

    }
}
