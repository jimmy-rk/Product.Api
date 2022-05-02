using Product.Api.Models.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Services.Product
{
    public interface IProductService
    {
        public Task<ServiceResponse<IEnumerable<SelectListItem>>> GetProductGroupTypes();
    }
}
