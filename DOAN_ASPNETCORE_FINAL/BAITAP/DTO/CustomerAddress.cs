using BAITAP.Common;
using BAITAP.Models;

namespace BAITAP.DTO
{
    public class CustomerAddress
    {
        public KhachHangLogin KhachhangLogin { get; set; }
        public Diachi Diachiganday { get; set; }
        public List<Diachi> Diachis { get; set; }
    }
}
