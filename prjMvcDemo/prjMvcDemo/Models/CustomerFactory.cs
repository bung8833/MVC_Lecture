using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
            if (customers == null || customers.Count == 0)
            {
                return null;
            }
            return customers.Single();
        }


        public Customer QueryByEmail(string email)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE fEmail=@K_FEMAIL";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("K_FEMAIL", email));

            List<Customer> customers = QueryBySql(sql, parameters);
            if (customers == null || customers.Count == 0)
            {
                return null;
            }
            return customers.Single();
        }


        public List<Customer> QueryByKeyword(string keyword)
        {
            string sql = $@"SELECT * FROM {_tableName} WHERE 
fName LIKE @K_KEYWORD
OR fPhone LIKE @K_KEYWORD
OR fEmail LIKE @K_KEYWORD
OR fAddress LIKE @K_KEYWORD";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("K_KEYWORD", "%" + keyword + "%"));

            List<Customer> customers = QueryBySql(sql, parameters);
            return customers;
        }


        public void Delete(int fId)
        {
            string sql = $"DELETE FROM {_tableName} WHERE fId=@K_FID";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("K_FID",(object)fId));

            ExecuteSql(sql, parameters);
        }

        //----------------------------------------------------------//
        //----------------------------------------------------------//
        //----------------------------------------------------------//
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
        public void create(Customer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer (";
            if (!string.IsNullOrEmpty(p.fName))
                sql += " fName, ";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += " fPhone, ";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += " fEmail, ";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += " fAddress, ";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += " fPassword,";
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += " )VALUES( ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " @K_FNAME, ";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " @K_FPHONE, ";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " @K_FEMAIL, ";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " @K_FADDRESS, ";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " @K_FPASSWORD, ";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += ")";
            ExecuteSql(sql, paras);
        }
        public void update(Customer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "UPDATE tCustomer SET ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " fName=@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " fPhone=@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " fEmail=@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " fAddress=@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " fPASSWORD=@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += " WHERE fId=@K_FID";
            paras.Add(new SqlParameter("K_FID", (object)p.fId));
            ExecuteSql(sql, paras);
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