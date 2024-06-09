using BusinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static SqlConnection con = new SqlConnection("Data Source=DESKTOP-QHPDDOA\\SQLEXPRESS;Initial Catalog=MyStore;User ID=sa;Password=123;Trust Server Certificate=True");

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string query = "select * from Products";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    products.Add(new Product()
                    {
                        ProductId = dr.GetInt32(0),
                        ProductName = dr.GetString(1),
                        CategoryId = dr.GetInt32(2),
                        UnitPrice = dr.GetDecimal(4),
                        UnitsInStock = dr.GetInt16(3)
                    });
                }


            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } finally { con.Close(); }

            return products;
        }

        public static void SaveProduct(Product p)
        {
            string query = "insert into Products (ProductName, CategoryID, UnitsInStock, UnitPrice) values (@name, @catId, @unit, @price)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", p.ProductName);
            cmd.Parameters.AddWithValue("@catId", p.CategoryId);
            cmd.Parameters.AddWithValue("@unit", p.UnitsInStock);
            cmd.Parameters.AddWithValue("@price", p.UnitPrice);
            try
            {
                con.Open();
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
        }

        public static void UpdateProduct(Product p)
        {

            string query = "update Products set ProductName= @name, CategoryID = @catId, UnitsInStock = @unit, UnitPrice= @price where ProductID = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", p.ProductName);
            cmd.Parameters.AddWithValue("@catId", p.CategoryId);
            cmd.Parameters.AddWithValue("@unit", p.UnitsInStock);
            cmd.Parameters.AddWithValue("@price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@id", p.ProductId);
            try
            {
                con.Open();
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
        }

        public static void DeleteProduct(Product p)
        {
            string query = "delete from Products where ProductID = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", p.ProductId);
            try
            {
                con.Open();
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }
        }

        public static Product GetProductById(int id)
        {
            List<Product> products = new List<Product>();
            string query = "select * from Products where ProductID = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            Product product = new Product();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.ProductId = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.CategoryId = reader.GetInt32(2);
                    product.UnitsInStock = reader.GetInt16(3);
                    product.UnitPrice = reader.GetDecimal(4);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { con.Close(); }

            return product;
        }
    }
}
