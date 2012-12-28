using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace KMeans
{
   public class Datasevice
    {
       string str = ConfigurationSettings.AppSettings["Con"].ToString();
       SqlConnection cn;
       void MoKetNoi()
       {
           cn = new SqlConnection(str);
           cn.Open();
       }
       void DongKetNoi()
       {
           cn.Close();
       }
       public DataTable LoadDL(string sql, params SqlParameter[] Thamso)
       {
           MoKetNoi();
           SqlCommand cm = new SqlCommand(sql,cn);
           cm.Parameters.AddRange(Thamso);
               SqlDataReader dr=cm.ExecuteReader();
           DataTable Bang=new DataTable();
           Bang.Load(dr);
           DongKetNoi();
           return Bang;
       }
       public void CapNhatDL_Proc(string sql, params SqlParameter[] ThamSo)
       {
           MoKetNoi();
           SqlCommand cm = new SqlCommand(sql,cn);
           cm.Parameters.AddRange(ThamSo);
           cm.CommandType = CommandType.StoredProcedure;
           cm.ExecuteNonQuery();
           DongKetNoi();
       }

       public void CapNhatDL(string sql, params SqlParameter[] ThamSo)
       {
           MoKetNoi();
           SqlCommand cm = new SqlCommand(sql, cn);
           cm.Parameters.AddRange(ThamSo);
           cm.ExecuteNonQuery();
           DongKetNoi();
       }

       
       public string Lay_String(string sql)
       {
           MoKetNoi();
           SqlCommand cm = new SqlCommand(sql,cn);
           string Xau = "";
           Xau = cm.ExecuteScalar().ToString();
           DongKetNoi();
           return Xau;
       }
       public int Lai_int(string sql)
       {
           MoKetNoi();
           SqlCommand cm = new SqlCommand(sql,cn);
           int So;
           So = (int)cm.ExecuteScalar();
           DongKetNoi();
           return So;
           
       }
       public bool KiemTra_DangNhap(string sql)
       {
           bool ok=false;
           if (Lai_int(sql)>0)
           {
               ok = true;
           }
           return ok;
       }

       public DataSet ThucThi_Proc_DataSet(string Proc, params SqlParameter[] param)
       {
           cn = new SqlConnection(str);
           SqlCommand cm = new SqlCommand(Proc,cn);
           if(param!=null)
           {
               cm.Parameters.Clear();
              cm.Parameters.AddRange(param);
           }
           DataSet ds = new DataSet();
           SqlDataAdapter da = new SqlDataAdapter(Proc,cn);
           cm.CommandType = CommandType.StoredProcedure;
           da.SelectCommand = cm;
           try
           {
               cn.Open();
               da.Fill(ds);
           }
           finally
           {
               if (cn.State == ConnectionState.Open)
                   cn.Close();
               cn.Dispose();
           }
           return ds;
       }
       
    }
}
