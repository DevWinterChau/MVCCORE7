using BAITAP.Models;

namespace BAITAP.DTO
{
    public class SanphamKhuyemMai
    {
        public Mh Mhs { get; set; }
        public Mathang mathang { get; set; }
        public Danhmuc danhmuc { get; set; }
        public double Giakhuyemai { get; set; }
        public double Phantramkhuyenmai { get; set; }
        public CtKhuyenMaiSanPham ctkm { get; set; }
    }
}
