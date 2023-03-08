using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.WebControls;

namespace prjMvcDemo.Models
{
    public class CustomerFactory
    {
        const string _tableName = "tCustomer";


        public List<Customer> QueryAll()
        {
            string sql = $"SELECT * FROM {_tableName}";

            List<Customer> customers = QueryBySql(sql, null);

            return customers;
        }


        public Customer QueryById(int fId)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE fId=@K_FID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("K_FID", fId));

            List<Customer> customers = QueryBySql(sql, parameters);
            if (customers == null)
            {
                return null;
            }
            return customers.Single();
        }


        public void Delete(int fId)
        {
            string sql = $"DELETE FROM {_tableName} WHERE fId=@K_FID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("K_FID",(object)fId));

            ExecuteSql(sql, parameters);
        }


        public void Create(Customer p)
        {
            string sql = $@"INSERT INTO {_tableName} (
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


        //----------------------------------------------------------//
        //----------------------------------------------------------//
        //----------------------------------------------------------//


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

        
        private List<Customer> QueryBySql(string sql, List<SqlParameter> parameters)
        {
            List<Customer> customers = new List<Customer>();

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

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer customer = new Customer();

                        customer.fId = (int)reader["fId"];

                        if (reader["fName"].Equals(DBNull.Value) == false)
                        {
                            customer.fName = reader["fName"].ToString();
                        }

                        if (reader["fPhone"].Equals(DBNull.Value) == false)
                        {
                            customer.fPhone = reader["fPhone"].ToString();
                        }

                        if (reader["fAddress"].Equals(DBNull.Value) == false)
                        {
                            customer.fAddress = reader["fAddress"].ToString();
                        }

                        if (reader["fEmail"].Equals(DBNull.Value) == false)
                        {
                            customer.fEmail = reader["fEmail"].ToString();
                        }

                        if (reader["fPassword"].Equals(DBNull.Value) == false)
                        {
                            customer.fPassword = reader["fPassword"].ToString();
                        }

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        // end of class CustomerFactory
    }
}