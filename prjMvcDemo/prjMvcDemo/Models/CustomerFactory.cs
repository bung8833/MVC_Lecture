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
        public void Delete(int fId)
        {
            string sql = "DELETE FROM tCustomer WHERE fId=@K_FID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("K_FID",(object)fId));

            ExecuteSql(sql, parameters);
        }



        public void Create(Customer p)
        {
            string sql = $@"INSERT INTO tCustomer (
fName, fPhone, fAddress, fEmail, fPassword)
VALUES(
@fName, @fPhone, @fAddress, @fEmail, @fPassword
)";

            List<SqlParameter> parameters= new List<SqlParameter>();
            parameters.Add(new SqlParameter("fName", p.fName));
            parameters.Add(new SqlParameter("fPhone", p.fPhone));
            parameters.Add(new SqlParameter("fAddress", p.fAddress));
            parameters.Add(new SqlParameter("fEmail", p.fEmail));
            parameters.Add(new SqlParameter("fPassword", p.fPassword));

            ExecuteSql(sql, parameters);
        }


        private void ExecuteSql(string sql, List<SqlParameter> parameters)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // end of class CustomerFactory
    }
}