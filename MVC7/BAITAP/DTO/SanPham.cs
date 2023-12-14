using BAITAP.Models;

namespace BAITAP.DTO
{
    public class SanPham
    {
        public List<SanPhamDTO> ListSanPhamDTOs { get; set; }
        public List<SanPhamHot> ListSanPhamHot { get; set; }
        public List<Mh> ListSanPhamGoiY {  get; set; }  
    }
}
