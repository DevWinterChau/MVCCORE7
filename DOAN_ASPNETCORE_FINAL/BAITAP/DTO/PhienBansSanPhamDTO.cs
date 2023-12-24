using BAITAP.Models;

namespace BAITAP.DTO
{
    public class PhienBansSanPhamDTO
    {
        public Mh mathang { get; set; }
        public List<Mathang> PhienBansanphamList { get; set; } = new List<Mathang>();
        public string JsonData { get; set; }

    }
}
