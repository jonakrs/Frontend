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
namespace shoeshop.Model
{
    public class Returns
{
    public int ReturnId { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Reason { get; set; }
    public Decimal RefundAmount { get; set; }
  

}
}
