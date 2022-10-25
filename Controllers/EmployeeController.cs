using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Employees";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }
        [Produces("application/json")]
        [HttpPost]
        public JsonResult Post(Employees emp)
        {

            string query = @"insert into dbo.Employees (employee_name,employee_department,date_of_joining)
                            values (@employeeName,@employeeDepartment,@dateofJoin)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@employeeName", emp.employeeName);
                    myCommand.Parameters.AddWithValue("@employeeDepartment", emp.employeeDept);
                    myCommand.Parameters.AddWithValue("@dateofJoin", DateTime.Now.ToString());
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpDelete]
        public JsonResult Delete(Employees emp)
        {
            string query = @"delete from dbo.Employees
                            where employee_name = @admin_name";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@admin_name", emp.employeeName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}

