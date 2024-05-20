using Flavory.Services.OrderAPI.Models.Dto;

namespace Flavory.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        //This will be responsible to load all the products from ProductAPI

        Task<IEnumerable<ProductDto>> GetProducts();

    }
}
