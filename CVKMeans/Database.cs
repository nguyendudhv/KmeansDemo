using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace KMeans
{
    /// <summary>
    /// Lớp cơ sở cung cấp các phương thức thao tác đến CSDL
    /// </summary>     
    public class Database
    {
        public static SqlConnection sqlCn;
        public Database()
        {
            //if(sqlCn==null)
            sqlCn = new SqlConnection("Data Source=NGUYENDU-PC;Initial Catalog=KmeansDemo;Integrated Security=True"); 
        }
        //Biến tĩnh kết nối tới CSDL
        
       
        /// <summary>
        /// Phưong thức thi hành một thủ tục SQL, có hai tham số truyền vào là
        /// tên thủ tục và điều kiện của thủ tục
        /// Kết quả trả về là một DattaTabe
        /// </summary>     
        public DataTable RunProcedureGet(string TenStoreProcedure, SqlParameter[] sqlParam)
        {           
            DataTable dtbTmp = new DataTable();
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCn;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.CommandText = TenStoreProcedure;

                for (int i = 0; i < sqlParam.Length; i++)
                {
                    sqlCmd.Parameters.Add(sqlParam[i]);
                }

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = sqlCmd;

                sqlDA.Fill(dtbTmp);
            }

            finally
            {
            }
            return dtbTmp;
        }

        /// <summary>
        /// Phưong thức thi hành một thủ tục SQL, có 1 tham số truyền vào là
        /// tên thủ tục
        /// Kết quả trả về là một DattaTabe
        /// </summary>     
        public DataTable RunProcedureGet(string TenStoreProcedure)
        {
            DataTable dtbTmp = new DataTable();
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = TenStoreProcedure;                
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = sqlCmd;
                sqlDA.Fill(dtbTmp);
            }
            finally
            {
            }
            return dtbTmp;
        }

        /// <summary>
        /// Phưong thức thi hành một thủ tục SQL, có 1 tham số truyền vào là
        /// tên thủ tục      
        /// </summary>     
        protected void RunProcedure(string TenStoreProcedure)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCn;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.CommandText = TenStoreProcedure;
                
                sqlCmd.ExecuteNonQuery();
            }
            finally
            {
            }
        }

        /// <summary>
        /// Phưong thức thi hành một thủ tục SQL, có 1 tham số truyền vào là
        /// tên thủ tục và điều kiện của thủ tục
        /// Kết quả trả về là một DattaTabe
        /// </summary>     
        public void RunProcedure(string TenStoreProcedure, SqlParameter[] sqlParam)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                if (sqlCn.State == ConnectionState.Closed)
                    sqlCn.Open();
                sqlCmd.Connection = sqlCn;
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.CommandText = TenStoreProcedure;

                for (int i = 0; i < sqlParam.Length; i++)
                {
                    sqlCmd.Parameters.Add(sqlParam[i]);
                }

                sqlCmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                
            }
        }
    }
}
