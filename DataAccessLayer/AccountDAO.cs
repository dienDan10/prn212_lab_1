using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class AccountDAO
    {

        private static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QHPDDOA\\SQLEXPRESS;Initial Catalog=MyStore;User ID=sa;Password=123;Trust Server Certificate=True");

        public static AccountMember GetAccountById(string accountID)
        {
            string query = "select * from AccountMember where MemberID = @accountID";
            SqlCommand cmd = new SqlCommand(query, conn);
            AccountMember accountMember = null;
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@accountID", accountID);
                SqlDataReader reader =  cmd.ExecuteReader();
                if (reader.Read())
                {
                    accountMember = new AccountMember();
                    accountMember.MemberId = reader.GetString(0);
                    accountMember.MemberPassword = reader.GetString(1);
                    accountMember.FullName = reader.GetString(2);
                    accountMember.EmailAddress = reader.GetString(3);
                    accountMember.MemberRole = reader.GetInt32(4);
                }
                
            } catch { 
                conn.Close();
            }
           
            return accountMember;
        }
    }
}
