using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrudOperation.Models
{
    public class DataAccessLayer
    {
        //public string InsertData(Employee obj)
        //{
        //    SqlConnection con = null;

        //    string result = "";
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect"].ToString());
        //        SqlCommand cmd = new SqlCommand("Crudoperation", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.Parameters.AddWithValue("@CustomerID", 0);    
        //        cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
        //        cmd.Parameters.AddWithValue("@MiddleName", obj.MiddleName);
        //        cmd.Parameters.AddWithValue("@LastName", obj.LastName);

        //        cmd.Parameters.AddWithValue("@Query", 1);
        //        con.Open();
        //        result = cmd.ExecuteScalar().ToString();
        //        return result;
        //    }
        //    catch
        //    {
        //        return result = "";
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        public string UpdateData(Employee obj)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect"].ToString());
                SqlCommand cmd = new SqlCommand("Crudoperation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", obj.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                cmd.Parameters.AddWithValue("@Query", 2);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
        public int DeleteData(string Id)
        {
            SqlConnection con = null;
            int result;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect"].ToString());
                SqlCommand cmd = new SqlCommand("Crudoperation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FirstName", null);
                cmd.Parameters.AddWithValue("@MiddleName", null);
                cmd.Parameters.AddWithValue("@LastName", null);
                cmd.Parameters.AddWithValue("@Query", 3);
                con.Open();
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return result = 0;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Employee> Selectalldata()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<Employee> empllist = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect"].ToString());
                SqlCommand cmd = new SqlCommand("Crudoperation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", null);
                cmd.Parameters.AddWithValue("@FirstName", null);
                cmd.Parameters.AddWithValue("@MiddleName", null);
                cmd.Parameters.AddWithValue("@LastName", null);
                cmd.Parameters.AddWithValue("@Query", 4);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                empllist = new List<Employee>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Employee eobj = new Employee();
                    eobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    eobj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                    eobj.MiddleName = ds.Tables[0].Rows[i]["MiddleName"].ToString();
                    eobj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();

                    empllist.Add(eobj);
                }
                return empllist;
            }
            catch
            {
                return empllist;
            }
            finally
            {
                con.Close();
            }
        }
        public Employee SelectDatabyID(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;
            Employee eobj = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect"].ToString());
                SqlCommand cmd = new SqlCommand("Crudoperation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FirstName", null);
                cmd.Parameters.AddWithValue("@MiddleName", null);
                cmd.Parameters.AddWithValue("@LastName", null);
                cmd.Parameters.AddWithValue("@Query", 5);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    eobj = new Employee();
                    eobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    eobj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                    eobj.MiddleName = ds.Tables[0].Rows[i]["MiddleName"].ToString();
                    eobj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();

                }
                return eobj;
            }
            catch
            {
                return eobj;
            }
            finally
            {
                con.Close();
            }
        }
    }
}