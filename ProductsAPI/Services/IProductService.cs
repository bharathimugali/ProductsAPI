using ProductsAPI.Data.Entities;
using ProductsAPI.Models.RequestDto;
using ProductsAPI.Models.ResponseDto;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(AddProductDto addProductDto);
        Task UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
        Task<bool> DecrementStockAsync(int id, int quantity);
        Task<bool> AddToStockAsync(int id, int quantity);
    }
}
