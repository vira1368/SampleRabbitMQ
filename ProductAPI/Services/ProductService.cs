using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _database;

        public ProductService(DatabaseContext database)
        {
            _database = database;
        }

        public Product AddProduct(Product product)
        {
            var result = _database.Products.Add(product);
            _database.SaveChanges();
            return result.Entity;
        }

        public bool DeleteProduct(int id)
        {
            var productInDb = _database.Products.SingleOrDefault(product => product.Id == id);
            var result = _database.Remove(productInDb);
            _database.SaveChanges();
            return result != null;
        }

        public Product GetProductById(int id)
        {
            return _database.Products.SingleOrDefault(product => product.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _database.Products.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            var result = _database.Products.Update(product);
            _database.SaveChanges();
            return result.Entity;
        }
    }
}
