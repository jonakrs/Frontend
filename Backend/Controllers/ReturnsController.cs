using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;                        
using System.Data;
using System.Data.SqlClient;
using shoeshop.Model;
using System.IO;


namespace shoeshop.Controllers     

    {
        [Route("api/[controller]")]
        [ApiController]
        public class ReturnsController : ControllerBase
        {
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env;
            public ReturnsController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                            select ReturnId,OrderId,
                                   CustomerId,ReturnDate,Reason,RefundAmount
                              from Returns ";

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
            public JsonResult Post(Returns r)
            {
                string query = @"
                             insert into Returns
                             values (@OrderId, @CustomerId,@ReturnDate,@Reason,@RefundAmount)";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@OrderId", r.OrderId);
                        myCommand.Parameters.AddWithValue("@CustomerId", r.CustomerId);
                        myCommand.Parameters.AddWithValue("@ReturnDate", r.ReturnDate);
                        myCommand.Parameters.AddWithValue("@Reason", r.Reason);
                        myCommand.Parameters.AddWithValue("@RefundAmount", r.RefundAmount);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }

            [HttpPut]
            public JsonResult Put(Returns r)
            {
                string query = @"
                           update Returns
                           set OrderId= @OrderId,
                            CustomerId=@CustomerId,
                            ReturnDate=@ReturnDate,
                            Reason=@Reason,
                            RefundAmount=@RefundAmount,
                            where ReturnId=@ReturnId
                            ";


                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ReturnId", r.ReturnId);
                        myCommand.Parameters.AddWithValue("@OrderId", r.OrderId);
                        myCommand.Parameters.AddWithValue("@CustomerId", r.CustomerId);
                        myCommand.Parameters.AddWithValue("@ReturnDate", r.ReturnDate);
                        myCommand.Parameters.AddWithValue("@Reason", r.Reason);
                    myCommand.Parameters.AddWithValue("@RefundAmount", r.RefundAmount);
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
                           delete from Returns
                            where ReturnId=@ReturnId
                            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ShoeshopAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ReturnId", id);

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
