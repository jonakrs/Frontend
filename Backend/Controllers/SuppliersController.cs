using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjectLab1.NewFolder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProjectLab1.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public SuppliersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select SuppliersId,ContactName,
                                   Address,City,Country,Phone
                              from  Suppliers ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Suppliers s)
        {
            string query = @"
                             insert into  Suppliers
                             values (@SuppliersName,@ContactName,@Address
                             @City,@Country,@Phone )";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@SuppliersName", s.SuppliersName);
                    myCommand.Parameters.AddWithValue("@ContactName", s.ContactName);
                    myCommand.Parameters.AddWithValue("@Address", s.Address);
                    myCommand.Parameters.AddWithValue("@City", s.City);
                    myCommand.Parameters.AddWithValue("@Country", s.Country);
                    myCommand.Parameters.AddWithValue("@Phone", s.Phone);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Suppliers s)
        {
            string query = @"Suppliers
                           update 
                           set SuppliersName= @SuppliersName,
                            ContactName= @ContactName,
                            Address= @Address,
                            City= @City,
                            Country= @Country,
                            Phone= @Phone,
                            where SuppliersId=@SuppliersId
                            ";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@SuppliersId", s.SuppliersId);
                    myCommand.Parameters.AddWithValue("@ContactName", .s.ContactName);
                    myCommand.Parameters.AddWithValue("@Address", s.Address);
                    myCommand.Parameters.AddWithValue("@City", s.City);
                    myCommand.Parameters.AddWithValue("Country", s.Country);
                    myCommand.Parameters.AddWithValue("@Phone", s.Phone);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from Suppliers
                            where SuppliersId=@SuppliersId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@SuppliersId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
