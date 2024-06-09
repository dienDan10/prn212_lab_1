using BusinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO
    {

        private static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QHPDDOA\\SQLEXPRESS;Initial Catalog=MyStore;User ID=sa;Password=123;Trust Server Certificate=True");

        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                string query = "select * from Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category() {CategoryId = reader.GetInt32(0), CategoryName = reader.GetString(1) });
                }

            } catch (Exception ex)
            {

            } finally
            {
                conn.Close();
            }
            return categories;
        }
    }
}
