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
public class PunetoriController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public PunetoriController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select PunetoriId, PunetoriName, Departament,
                             convert(varchar(10),DateOfJoining,120) as DateOfJoining, PhotoFileName
                            from 
                            dbo.Punetori
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PunetoriAppCon");
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
        public JsonResult Post(Punetori pun)
        {
            string query = @"
                            insert into   dbo.Punetori
                            (PunetoriName,Departament,DateOfJoining,PhotoFileName)
                            values  (@PunetoriName,@Departament,@DateOfJoining,@PhotoFileName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PunetoriAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PunetoriName", pun.PunetoriName);
                    myCommand.Parameters.AddWithValue("@Departament", pun.Departament);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", pun.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", pun.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Punetori pun)
        {
            string query = @"
                            update  dbo.Punetori

                                set PunetoriName=@Punetori
                                Departament=@Departament,
                                DateOfJoining=@DateOfJoining,
                                PhotoFileName=@PhotoFileName
                            where PunetoriId=@PunetoriId    
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PunetoriAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PunetoriId", pun.PunetoriId);
                    myCommand.Parameters.AddWithValue("@PunetoriName", pun.PunetoriName);
                    myCommand.Parameters.AddWithValue("@Departament", pun.Departament);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", pun.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", pun.PhotoFileName);
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
                            delete from dbo.Punetori
                            where PunetoriId=@PunetoriId    
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PunetoriAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PunetoriId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Deleted Successfully");

        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }

            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

    }
}