using BAITAP.Models;
using BAITAP.Request;

namespace BAITAP.DTO
{
    public class ProfileCustomer
    {
        public Khachhang KhachHang { get; set; }
        public List<Cart> WistList { get; set; }
        public List<Diachi> Diachi { get; set; }
    }
}
