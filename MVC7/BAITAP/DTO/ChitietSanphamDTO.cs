using BAITAP.Models;

namespace BAITAP.DTO
{
    public class ChitietSanphamDTO
    {
        public Mh mathangchinh { get; set; }
        public List<MathangKM> DanhsachMathang { get; set; }
        public CtKhuyenMai CtKhuyenMai { get; set; }
        public Danhmuc danhmuc { get; set; }

        public CtKhuyenMaiSanPham ctkm { get; set; }
    }
    public class MathangKM
    {
        public Mathang mathang { get; set; }
        public double Giakhuyemai { get; set; }
        public int Phantramkhuyenmai { get; set; }
    }

}
