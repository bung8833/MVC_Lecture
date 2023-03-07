using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CustomerFactory
    {
        public void Create(Customer p)
        {
            string sql = $@"INSERT INTO tCustomer (
fName, fPhone, fAddress, fEmail, fPassword)
VALUES(
'{p.fName}', '{p.fPhone}', '{p.fAddress}', '{p.fEmail}', '{p.fPassword}'
)";

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=P215-2203-NB01;Initial Catalog=dbDemo;Integrated Security=True";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}