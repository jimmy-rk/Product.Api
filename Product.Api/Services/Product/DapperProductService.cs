using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Api.Models.General;
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
    }
}
