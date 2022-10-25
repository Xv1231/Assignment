using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Assignment.Models;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select name from dbo.Admin";

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
        public JsonResult Post(Admin ad)
        {

            string query = @"insert into dbo.Admin (name,password)
                            values (@adminName,@adminPassword)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                   
                    myCommand.Parameters.AddWithValue("@adminName", ad.adminName);
                    myCommand.Parameters.AddWithValue("@adminPassword", ad.adminPassword);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpDelete]
        public JsonResult Delete(Admin ad)
        {
            string query = @"delete from dbo.Admin
                            where name = @admin_name";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AssignmentConn");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@admin_name", ad.adminName);
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
