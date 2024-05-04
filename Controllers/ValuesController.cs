using CRUDusingSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUDusingSP.Controllers
{
	public class ValuesController : Controller
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
		Employee emp = new Employee();
		// GET: Values
		public ActionResult Index()
		{
			return View();
		}

		// GET: Values/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Values/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Values/Create
		[HttpPost]
		public string Create(Employee employee)
		{
			string msg = "";
			if (employee != null)
			{
				SqlCommand cmd = new SqlCommand("usp_addEmployee", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Name", employee.Name);
				cmd.Parameters.AddWithValue("@Age", employee.Age);
				cmd.Parameters.AddWithValue("@Active", employee.Active);
				con.Open();
				int i = cmd.ExecuteNonQuery();
				con.Close();
				if (i > 0)
				{
					msg = "Data Inserted Successfully";
				}
				else
				{
					msg = "Error";
				}
			}
			return msg;
		}

		// GET: Values/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Values/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Values/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Values/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
