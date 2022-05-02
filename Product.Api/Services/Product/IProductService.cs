using Product.Api.Models.General;
using Product.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Api.Services.Product
{
    public interface IProductService
    {
        public Task<ServiceResponse<IEnumerable<SelectListItem>>> GetProductGroupTypes();
        public Task<ServiceResponse<ProductViewModel>> GetProduct(string guid);
        public Task<ServiceResponse<ProductViewModel>> Create(ProductViewModel product);
        public Task<ServiceResponse<ProductViewModel>> Update(ProductViewModel product);
    }
}
