using BAITAP.Models;

namespace BAITAP.DTO
{
    public class DetailMhs
    {
        public Mh MatHangChinh { get; set; }
        public List<Mathang> DanhSachPhienBan { get; set; }
    }
}
