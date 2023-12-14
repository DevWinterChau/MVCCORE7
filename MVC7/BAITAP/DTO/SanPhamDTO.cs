using BAITAP.Models;

namespace BAITAP.DTO
{
    public class SanPhamDTO
    {
        public Mh mh {  get; set; }   
        public int giacaonhat { get; set; }
        public string GiaGiaoDong {  get; set; }    
        public float DanhGiaTrungBinh { get; set; }
        public bool NoiBat {  get; set; }
        public bool KhuyenMai { get; set; }
    }
}
