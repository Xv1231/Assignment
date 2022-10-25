using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly IConfiguration _configuration;
        public TaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Task";

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
        public JsonResult Post([FromBody]Models.Task tsk)
        {
            Console.WriteLine(tsk.taskName);

            string query = @"insert into dbo.Task 
                            values ('"+tsk.taskName+"','"+tsk.completionTime+"','"+tsk.assignedTo +"')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                SqlCommand myCommand = new SqlCommand(query, mycon) ;
                

                    /*myCommand.Parameters.AddWithValue("@taskName", tsk.taskName);
                    myCommand.Parameters.AddWithValue("@completiontTime", "30 Days");
                    myCommand.Parameters.AddWithValue("@assignedTo", "ASAD");*/
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                
            }
            return new JsonResult(table);
        }
        [HttpPut]
        public JsonResult Put([FromBody] Models.Task tsk)
        {
            Console.WriteLine(tsk.taskName);

            string query = " UPDATE dbo.Task SET task_name = '" + tsk.taskName + "', completion_time = '" + tsk.completionTime + "', assigned_to = '" + tsk.assignedTo + "' Where task_name ='" + tsk.taskName + "' )";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                SqlCommand myCommand = new SqlCommand(query, mycon);


                /*myCommand.Parameters.AddWithValue("@taskName", tsk.taskName);
                myCommand.Parameters.AddWithValue("@completiontTime", "30 Days");
                myCommand.Parameters.AddWithValue("@assignedTo", "ASAD");*/
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                mycon.Close();

            }
            return new JsonResult(table);
        }
        [HttpDelete]
        public JsonResult Delete(Models.Task tsk)
        {
            string query = @"delete from dbo.Task
                            where task_name = @taskName";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@taskName", tsk.taskName);
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
