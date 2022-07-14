using CrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudOperation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee model)
        {


            string mainconn = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand sqlcomm = new SqlCommand("Crudoperation", sqlconn);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@FirstName", model.FirstName);
            sqlcomm.Parameters.AddWithValue("@MiddleName", model.MiddleName);
            sqlcomm.Parameters.AddWithValue("@LastName", model.LastName);
            
            sqlcomm.Parameters.AddWithValue("@Query", 1);
            sqlcomm.ExecuteNonQuery().ToString();
            sqlconn.Close();


            return RedirectToAction("List");

        }

        [HttpGet]
        public ActionResult List()
        {
            List<Employee> mo = new List<Employee>();
            string mainconn = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand sqlcomm = new SqlCommand("Crudoperation", sqlconn);
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@FirstName", null);
            sqlcomm.Parameters.AddWithValue("@MiddleName", null);
            sqlcomm.Parameters.AddWithValue("@LastName", null);
            sqlcomm.Parameters.AddWithValue("@Query", 4);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Employee obj = new Employee();
                obj.Id = Convert.ToInt32(dr["Id"]);
                obj.FirstName = Convert.ToString(dr["FirstName"]);
                obj.MiddleName = Convert.ToString(dr["MiddleName"]);
                obj.LastName = Convert.ToString(dr["LastName"]);

                mo.Add(obj);

            }
            return View(mo);
        }
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            Employee objEmployee = new Employee();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata    
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {

            if (ModelState.IsValid) //checking model is valid or not    
            {
                DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata    
                string result = objDB.UpdateData(model);
                //ViewData["result"] = result;    
                TempData["result2"] = result;
                ModelState.Clear(); //clearing model    
                                    //return View();    
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(String ID)
        {
            DataAccessLayer objDB = new DataAccessLayer();
            int result = objDB.DeleteData(ID);
            TempData["result3"] = result;
            ModelState.Clear(); //clearing model    
                                //return View();    
            return RedirectToAction("List");
        }
    }
}



