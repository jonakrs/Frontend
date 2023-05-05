

namespace ProjectLab1.Model
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
