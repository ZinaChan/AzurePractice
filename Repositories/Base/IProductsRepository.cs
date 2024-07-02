using AzurePractice.Models;

namespace AzurePractice.Repositories.Base;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProducts();

    Task<Product> GetProductByName(string name);
    Task CreateProduct(Product newProduct);
    Task UpdateProduct(Product newProduct);
    Task DeleteProduct(int productId); 
}

