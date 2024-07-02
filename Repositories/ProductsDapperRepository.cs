using System.Data.SqlClient;
using AzurePractice.Models;
using AzurePractice.Repositories.Base;
using Dapper;

namespace AzurePractice.Repositories;


public class ProductsDapperRepository : IProductsRepository
{
    
    private string _connectionString;
    public ProductsDapperRepository()
    {
        this._connectionString = "Server=tcp:practicemssqldevserver.database.windows.net,1433;Initial Catalog=practicemssqldev;Persist Security Info=False;User ID=azureuser;Password=UserPw03;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }

    public async Task CreateProduct(Product newProduct)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)", newProduct);
        }
    }

    public async Task<Product> GetProductByName(string name)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefault("SELECT FROM Products WHERE Name = @Name", new { Name = name });
        }
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<Product>("SELECT * FROM Products");
        }
    }

    public async Task UpdateProduct(Product newProduct)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("UPDATE Products SET Name = @Name, Price = @Price", newProduct);
        }
    }

    public async Task DeleteProduct(Guid productId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            await connection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = productId });
        }
    }

}

