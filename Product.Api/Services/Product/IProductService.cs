namespace Product.Api.Services.Product
{
    public interface IProductService
    {
        public Task<ServiceResponse<IEnumerable<SelectListItem>>> GetProductGroupTypes();
    }
}
