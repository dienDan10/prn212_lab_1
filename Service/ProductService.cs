using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository iproductRepository;

        public ProductService()
        {
            this.iproductRepository = new ProductRepository();
        }

        public void DeleteProduct(Product product)
        {
            iproductRepository.DeleteProduct(product);
        }

        public Product GetProductById(int id)
        {
            return iproductRepository.GetProductById(id);
        }

        public List<Product> GetProducts()
        {
            return iproductRepository.GetProducts();
        }

        public void SaveProduct(Product product)
        {
            iproductRepository.SaveProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            iproductRepository.UpdateProduct(product);
        }
    }
}
