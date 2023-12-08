using BAITAP.Models;

namespace BAITAP.Request
{
    public class TotalRequestModel
    {
        public List<Cart> cartItems { get; set; }
        public Diachi1 diachi { get; set; }
        public Info info { get; set; }
    }
    public class Info
    {
        public string hoten { get; set; }
        public string sodienthoai { get; set; }

    }
    public class Diachi1
    {
        public int id { get; set; }
        public string Diachi { get; set; }
        public int macdinh { get; set; }
        public string phuongxa { get; set; }
        public string quanhuyen { get; set; }
        public string tinhthanh { get; set; }
    }

}
