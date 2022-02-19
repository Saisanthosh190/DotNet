using DotNetExam.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetExam.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True";
            conn.Open();

            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = conn;
            cmdSelect.CommandType = System.Data.CommandType.StoredProcedure;
            cmdSelect.CommandText = "SelectProducts";


            List<Products> lstProducts = new List<Products>();
            SqlDataReader dr = cmdSelect.ExecuteReader();
            while(dr.Read())
            {
                lstProducts.Add(new Products { ProductId = dr.GetInt32(0), ProductName = dr.GetString(1), Rate = dr.GetDecimal(2), Description = dr.GetString(3), CategoryName = dr.GetString(4) });
            }

            dr.Close();
            conn.Close();
            return View(lstProducts);
        }

     

       

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True";
            conn.Open();

            SqlCommand selectcmd = new SqlCommand();
            selectcmd.Connection = conn;
            selectcmd.CommandType = System.Data.CommandType.StoredProcedure;
            selectcmd.CommandText = "SelectOneProduct";

            selectcmd.Parameters.AddWithValue("@ProductId", id);

            SqlDataReader dr = selectcmd.ExecuteReader();
            Products p = null;
            if(dr.Read())
            {
                p = new Products { ProductId = id, ProductName = dr.GetString(1), Rate = dr.GetDecimal(2), Description = dr.GetString(3), CategoryName = dr.GetString(4) };
            }
            dr.Close();
            conn.Close();
            return View(p);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Products prod)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString= @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True";
            conn.Open();

            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = conn;
            cmdUpdate.CommandType = System.Data.CommandType.StoredProcedure;
            cmdUpdate.CommandText = "UpdateProduct";

            Products obj = new Products { ProductId = prod.ProductId, ProductName = prod.ProductName, Rate = prod.Rate, Description = prod.Description, CategoryName = prod.CategoryName };


            cmdUpdate.Parameters.AddWithValue("@ProductId", obj.ProductId);
            cmdUpdate.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmdUpdate.Parameters.AddWithValue("@Rate", obj.Rate);
            cmdUpdate.Parameters.AddWithValue("@Description", obj.Description);
            cmdUpdate.Parameters.AddWithValue("@CategoryName", obj.CategoryName);

            try
            {

                cmdUpdate.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Error=e.Message;
                return View();
            }
           
        }

     
    }
}
