using CRUDusingSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

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

		// GET: Values/GetAllEmployee
		public dynamic GetAllEmployee()
		{
			SqlDataAdapter da = new SqlDataAdapter("usp_GetAllEmployee", con);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataTable dt = new DataTable();
			da.Fill(dt);
			List<Employee> listEmployee = new List<Employee>();
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					Employee emp = new Employee();
					emp.Name = dt.Rows[i]["Name"].ToString();
					emp.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
					emp.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
					emp.Active = Convert.ToInt32(dt.Rows[i]["Active"]);
					listEmployee.Add(emp);
				}
			}
			if (listEmployee.Count > 0)
			{
				return JsonConvert.SerializeObject(listEmployee);
			}
			else
			{
				return null;
			}
		}


		// GET: Values/GetEmployeeByID
		public dynamic GetEmployeeByID(int Id)
		{
			SqlDataAdapter da = new SqlDataAdapter("usp_GetEmployeeByID", con);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.AddWithValue("@Id", Id);
			DataTable dt = new DataTable();
			da.Fill(dt);
			Employee emp = new Employee();
			if (dt.Rows.Count > 0)
			{
				emp.Name = dt.Rows[0]["Name"].ToString();
				emp.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
				emp.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
				emp.Active = Convert.ToInt32(dt.Rows[0]["Active"]);

			}
			if (emp != null)
			{
				return JsonConvert.SerializeObject(emp);
			}
			else
			{
				return null;
			}
		}


		// GET: Values/Edit/5
		[HttpPost]
		public string Edit(int Id, Employee employee)
		{
			string msg = "";
			if (employee != null)
			{
				SqlCommand cmd = new SqlCommand("usp_UpdateEmployee", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", Id);
				cmd.Parameters.AddWithValue("@Name", employee.Name);
				cmd.Parameters.AddWithValue("@Age", employee.Age);
				cmd.Parameters.AddWithValue("@Active", employee.Active);
				con.Open();
				int i = cmd.ExecuteNonQuery();
				con.Close();
				if (i > 0)
				{
					msg = "Data Updated Successfully";
				}
				else
				{
					msg = "Error";
				}
			}
			return msg;
		}


		// GET: Values/Delete/5
		public string Delete(int Id)
		{
			string msg = "";
			SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Id", Id);
			con.Open();
			int i = cmd.ExecuteNonQuery();
			con.Close();
			if (i > 0)
			{
				msg = "Data Deleted Successfully";
			}
			else
			{
				msg = "Error";
			}
			return msg;
		}


	}
}
