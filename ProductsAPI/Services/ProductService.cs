using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using ProductsAPI.Data.Entities;
using ProductsAPI.Infrastructure.Exceptions;
using ProductsAPI.Models.RequestDto;
using ProductsAPI.Models.ResponseDto;
using ProductsAPI.Repositories;

namespace ProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<Product> repository, IMapper mapper, ILogger<ProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                if (products == null || !products.Any())
                {
                    throw new NotFoundException("No products found.");
                }

                List<ProductDto> productDtos = new List<ProductDto>();
                foreach (var product in products)
                {
                    ProductDto productDto = _mapper.Map<ProductDto>(product);
                    productDtos.Add(productDto);
                }
                return productDtos;
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching the product.");
                throw;
            }

        }
        //check for required filed alternative
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);

                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {id} not found.");
                }
                return _mapper.Map<ProductDto>(product);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching the product.");
                throw; 
            }
        }

        public async Task<ProductDto> AddProductAsync(AddProductDto addProductDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(addProductDto);
                await _repository.AddAsync(product);
                return _mapper.Map<ProductDto>(product);
            }
            catch (ValidationException ex)
            {
                throw new Exception("Error occurred while adding product", ex);
            }
        }

        public async Task UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);

                if (product == null)
                {
                    throw new ArgumentException($"Product with Id {id} does not exist.");
                }

                product.Name = updateProductDto.Name;
                product.Stock = updateProductDto.Stock;

                await _repository.UpdateAsync(product);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex,"Error occurred while updating product");
                throw;
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);

                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {id} not found.");
                }
                await _repository.DeleteAsync(id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting the product.");
                throw;
            }
        }


        public async Task<bool> DecrementStockAsync(int id, int quantity)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);

                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {id} does not exist.");
                }

                if (product.Stock < quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for product with ID {id}. Current stock: {product.Stock}, requested: {quantity}.");
                }

                product.Stock -= quantity;
                await _repository.UpdateAsync(product);
                return true;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error occurred while decrementing stock");
                throw;
            }
        }

        public async Task<bool> AddToStockAsync(int id, int quantity)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                {
                    throw new NotFoundException($"Product with ID {id} does not exist.");
                }
                product.Stock += quantity;
                await _repository.UpdateAsync(product);
                return true;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error occurred while adding stock");
                throw;
            }
        }
    }
}
