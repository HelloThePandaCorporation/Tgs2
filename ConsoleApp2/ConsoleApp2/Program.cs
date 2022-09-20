using System;
using System.Data.SqlClient;


namespace ConsoleApp2
{
    class Program
    {
        public SqlConnection sqlConn = new SqlConnection();
        public SqlCommand sqlcom = new SqlCommand();
        public SqlParameter sqlpar = new SqlParameter();
        public string connectionString = "Data Source= LAPTOP-S368EH41;" +
                                       "Initial Catalog= Pekerja;" +
                                       "User Id= HelloPanda;" +
                                       "Password= hello123;";


        static void Main(string[] args)
        {
            string connectionString = "Data Source= LAPTOP-S368EH41;" +
                                           "Initial Catalog= Pekerja;" +
                                           "User Id= HelloPanda;" +
                                           "Password= hello123;";

            Program CRUD = new Program();
            CRUD.koneksi();
            Console.WriteLine("==================================");
            CRUD.selectall();
            Console.WriteLine("==================================");
            Console.WriteLine("Masukkan id data karyawan yang ingin dicari :");
            string dataid = Console.ReadLine();
            int id = Convert.ToInt32(dataid);
            CRUD.get_id(id);
            Console.WriteLine("==================================");
            Console.WriteLine("Masukkan id karyawan baru    :");
            dataid = Console.ReadLine();
            id = Convert.ToInt32(dataid);
            Console.WriteLine("Masukkan nama karyawan baru  :");
            string name = Console.ReadLine();
            
            CRUD.insert(id, name);
            Console.WriteLine("==================================");
            Console.WriteLine("Masukkan id karyawan yang mau diganti    :");
            dataid = Console.ReadLine();
            id = Convert.ToInt32(dataid);
            Console.WriteLine("Masukkan nama baru karyawan              :");
            name = Console.ReadLine();
            CRUD.update(id, name);
        }    


            void koneksi()
            {
                try
                {
                    this.sqlConn.ConnectionString = "Data Source= LAPTOP-S368EH41;" +
                                               "Initial Catalog= Pekerja;" +
                                               "User Id= HelloPanda;" +
                                               "Password= hello123;";

                    sqlConn.Open();
                    Console.WriteLine("Connection successfully!");
                    sqlConn.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ups..something wrong, ");
                    Console.WriteLine(ex.InnerException);
                }
            }

            void selectall()
            {
                sqlcom.Connection = sqlConn;
                sqlcom.CommandText = "SELECT * FROM tbl_karyawan " +
                                     "join tbl_position " +
                                     "ON tbl_karyawan.id_position = tbl_position.id_position";

                try
                {
                    this.sqlConn.Open();
                    using (SqlDataReader sqlDataReader = sqlcom.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                Console.WriteLine("id   : " + sqlDataReader[0] +
                                                  "| nama  : " + sqlDataReader[1] +
                                                  "| Position" + sqlDataReader[6]);

                            }

                        }
                        else
                        {
                            Console.WriteLine("No Data Found!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }

            }

            void get_id(int id)
            {
            
                string query = "SELECT " +
                    "* " +
                    "from tbl_karyawan " +
                    " Where id = @id";

                sqlConn = new SqlConnection(connectionString);
                sqlcom = new SqlCommand(query, sqlConn);
                sqlpar.ParameterName = "@id";
                sqlpar.Value = id;

                sqlcom.Parameters.Add(sqlpar);

                try
                {
                    sqlConn.Open();
                    using (SqlDataReader sqlDataReader = sqlcom.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                Console.WriteLine("id   : " + sqlDataReader[0] +
                                               "| nama  : " + sqlDataReader[1] +
                                               "| Position : " + sqlDataReader[6]);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("No Data Rows");
                        }
                        sqlDataReader.Close();
                    }
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

            void insert(int id_karyawan, string name_karyawan)
            {
            

            using (sqlConn = new SqlConnection(connectionString)) {
                sqlConn.Open();
                SqlTransaction sqlTransaction = sqlConn.BeginTransaction();

                sqlcom = sqlConn.CreateCommand();
                sqlcom.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = id_karyawan;

                try
                {
                    sqlcom.CommandText = "INSERT INTO tbl_karyawan " +
                        "(id) Values (@id)";
                    sqlcom.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
                

            }

            void update(int id, string name)
            {
            using (sqlConn = new SqlConnection(connectionString))
            {
                string query = "UPDATE tbl_karyawan set name = '" +
                    name +"' " +
                    " Where id = @id";

                sqlConn = new SqlConnection(connectionString);
                sqlcom = new SqlCommand(query, sqlConn);
                sqlpar.ParameterName = "@id";
                sqlpar.Value = id;

            }
            
            }
        
    }
}
