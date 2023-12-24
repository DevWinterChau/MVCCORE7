using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BAITAP.Data;
using BAITAP.Models;
using BAITAP.Request;
using BAITAP.Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Diachi = BAITAP.Models.Diachi;
using Microsoft.EntityFrameworkCore.Internal;
using BAITAP.Other;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BAITAP.DTO;
using NuGet.Packaging;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Elfie.Model.Map;
using Humanizer.Localisation.TimeToClockNotation;
using BAITAP.MailService;

namespace BAITAP.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public IHttpContextAccessor HttpContextAccessor { get; }
        private readonly IMailLogic _mailLogic;


        // Inject the IHttpContextAccessor to access the session
        public CustomerController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMailLogic mailLogic)
        {
            _context = context;
            HttpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _mailLogic = mailLogic;

        }

        // GET: Customer
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false, string khuyenmai = "0")
        {
            DateTime currentDate = DateTime.Now;

            var query = from mathang in _context.Mathangs
                        join ctkm in _context.CtKhuyenMaiSanPhams
                        on mathang.MaMh equals ctkm.Mamh into ctkmGroup
                        from ctkm in ctkmGroup.DefaultIfEmpty()
                        join danhmuc in _context.Danhmucs
                        on mathang.MaDm equals danhmuc.MaDm
                        //where ctkm.MaCtkmNavigation.NgayBatDau <= currentDate && ctkm.MaCtkmNavigation.NgayKetThuc <= currentDate
                        select new SanphamKhuyemMai
                        {
                            mathang = mathang,
                            danhmuc = danhmuc,
                            ctkm = ctkm,
                            Phantramkhuyenmai = ctkm != null ? ctkm.Phantramkhuyenmai != null ? ctkm.Phantramkhuyenmai : 0 : 0,
                            Giakhuyemai = ctkm != null ? mathang.GiaBan - mathang.GiaBan * (ctkm.Phantramkhuyenmai / 100.0) : 0
                        };




            // Filter by category name if provided
            if (!string.IsNullOrEmpty(category) && category != "Tất cả")
            {
                query = query.Where(m => m.danhmuc.Ten == category);
                ViewBag.category = category;
            }
            else
            {
                ViewBag.category = "Tất cả";
            }


            // Filter by category name if provided
            if (khuyenmai == "1")
            {
                query = query.Where(m => m.ctkm.MaCtkmNavigation.NgayKetThuc <= DateTime.Now && m.ctkm.MaCtkmNavigation.NgayBatDau <= DateTime.Now);
                ViewBag.khuyenmai = "1";
            }
            else
            {
                ViewBag.khuyenmai = "0";
            }


            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.mathang.Ten.Contains(keyword.Trim())
                                                           || x.danhmuc.Ten.Contains(keyword.Trim())
                                                            || x.mathang.MoTa.Contains(keyword.Trim()));
            }

            // Sort by price
            if (sort == "priceHighToLow")
            {
                query = query.OrderByDescending(m => m.mathang.GiaBan);
                ViewBag.sort = sort;

            }
            else if (sort == "priceLowToHigh")
            {
                query = query.OrderBy(m => m.mathang.GiaBan);
                ViewBag.sort = sort;

            }
            else
            {
                ViewBag.sort = "Không sắp xếp";
            }
            // Add more sorting options if needed

            // Apply pagination
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Tính toán các thông tin phân trang
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.keyword = keyword;
            ViewBag.Fill = Fill;
            // Populate the dropdown with categories
            ViewBag.ListDanhMuc = await _context.Danhmucs.ToListAsync();

            return View(items);
        }

        // Tính giá giao động của sản phẩm giá thấp nhất - giá cao nhất
        public string tinhgiagiaodong(List<Mathang> lstmh)
        {
            if (lstmh == null || lstmh.Count == 0)
            {
                return "Danh sách trống.";
            }

            decimal minValue = lstmh.Min(x => x.GiaBan);
            decimal maxValue = lstmh.Max(x => x.GiaBan);

            if (minValue.Equals(maxValue))
            {
                return minValue.ToString("N0") + "đ";
            }

            return minValue.ToString("N0") + "đ - " + maxValue.ToString("N0") + " đ";
        }
        // Lượt mua trung bình
        public int TinhLuotMuaTrungBinh(List<Mathang> lstmh)
        {
            if (lstmh == null || lstmh.Count == 0)
            {
                return 0;
            }
            int tong = 0;
            foreach (var item in lstmh)
            {
                tong += item.LuotMua;
            }
            return tong != 0 ? tong : 0;
        }
        // Tính giá cao nhất
        public int tinhgiacaonhat(List<Mathang> lstmh)
        {
            if (lstmh == null || lstmh.Count == 0)
            {
                return 0;
            }

            return lstmh.Max(x => x.GiaBan);
        }
        // Sở thích danh mục
        public async Task<List<string>> SothichDanhMuc(int IDKH)
        {
            List<string> items = new List<string>();
            var listsothich = await _context.Danhmucsothiches.Where(x => x.Makh.Equals(IDKH)).ToListAsync();
            foreach (var sothich in listsothich)
            {
                items.Add(sothich.Loaisanphamyeuthich);
            }
            return items;
        }
        // Sở thích Thương hiệu
        public async Task<List<string>> SothichThuongHieu(int IDKH)
        {
            List<string> items = new List<string>();
            var listsothich = await _context.Thuonghieusothiches.Where(x => x.Makh.Equals(IDKH)).ToListAsync();
            foreach (var sothich in listsothich)
            {
                items.Add(sothich.Thuonghieuyeuthich);
            }
            return items;
        }
        //Danh sách sản phẩm hot
        public async Task<List<SanPhamHot>> DanhSachSanPhamHOT()
        {
            var lismh = await _context.Mhs.Include(m => m.Danhmucs).ToListAsync();
            List<SanPhamHot> ListSanPhamHot = new List<SanPhamHot>();
            foreach (var mh in lismh)
            {
                var itemhot = new SanPhamHot();
                var listSanpham = await _context.Mathangs.Where(x => x.MaMhchinh.Equals(mh.Id)).ToListAsync();
                itemhot.mathangchinh = mh;
                itemhot.LuotMuatrungbinh = TinhLuotMuaTrungBinh(listSanpham);
                ListSanPhamHot.Add(itemhot);
            }
            return ListSanPhamHot.OrderByDescending(x => x.LuotMuatrungbinh).ToList();
        }
        // DS tương tự
        public async Task<List<SanphamKhuyemMai>> DanhSachSanPhamTuongtu(int IdDanhMuc, string thuonghieu, string size, string mausac)
        {
            List<SanphamKhuyemMai> ListSanPhamHot = new List<SanphamKhuyemMai>();

            DateTime currentDate = DateTime.Now;

            var query = from mathang in _context.Mathangs
                        join ctkm in _context.CtKhuyenMaiSanPhams
                        on mathang.MaMh equals ctkm.Mamh into ctkmGroup
                        from ctkm in ctkmGroup.DefaultIfEmpty()
                        join danhmuc in _context.Danhmucs
                        on mathang.MaDm equals danhmuc.MaDm
                        //where ctkm.MaCtkmNavigation.NgayBatDau <= currentDate && ctkm.MaCtkmNavigation.NgayKetThuc <= currentDate
                        select new SanphamKhuyemMai
                        {
                            mathang = mathang,
                            danhmuc = danhmuc,
                            ctkm = ctkm,
                            Phantramkhuyenmai = ctkm != null ? ctkm.Phantramkhuyenmai != null ? ctkm.Phantramkhuyenmai : 0 : 0,
                            Giakhuyemai = ctkm != null ? mathang.GiaBan - mathang.GiaBan * (ctkm.Phantramkhuyenmai / 100.0) : 0
                        };

            ListSanPhamHot = await query.ToListAsync();

            return ListSanPhamHot.OrderByDescending(x => x.mathang.MaDm.Equals(IdDanhMuc) && x.mathang.Thuonghieu.Equals(thuonghieu) && x.mathang.Kichco.Equals(size) && x.mathang.Mausac.Equals(mausac)).ToList();
        }
        // Gợi ý dựa vào sở thích
        public async Task<List<SanphamKhuyemMai>> DanhSachSanGoiYDuaVaoSoThich(int IDKH)
        {
            List<SanphamKhuyemMai> ListSanPhamHot = new List<SanphamKhuyemMai>();
            DateTime currentDate = DateTime.Now;
            var query = from mathang in _context.Mathangs
                        join ctkm in _context.CtKhuyenMaiSanPhams
                        on mathang.MaMh equals ctkm.Mamh into ctkmGroup
                        from ctkm in ctkmGroup.DefaultIfEmpty()
                        join danhmuc in _context.Danhmucs
                        on mathang.MaDm equals danhmuc.MaDm
                        //where ctkm.MaCtkmNavigation.NgayBatDau <= currentDate && ctkm.MaCtkmNavigation.NgayKetThuc <= currentDate
                        select new SanphamKhuyemMai
                        {
                            mathang = mathang,
                            danhmuc = danhmuc,
                            ctkm = ctkm,
                            Phantramkhuyenmai = ctkm != null ? ctkm.Phantramkhuyenmai != null ? ctkm.Phantramkhuyenmai : 0 : 0,
                            Giakhuyemai = ctkm != null ? mathang.GiaBan - mathang.GiaBan * (ctkm.Phantramkhuyenmai / 100.0) : 0
                        };



            var listsothichTH = await _context.Thuonghieusothiches.Where(x => x.Makh.Equals(IDKH)).ToListAsync();
            var listsothichDM = await _context.Danhmucsothiches.Where(x => x.Makh.Equals(IDKH)).ToListAsync();
            foreach (var itemTH in listsothichTH)
            {
                foreach (var itemDM in listsothichDM)
                {
                    var listSanpham = await query.Where(x => x.mathang.Thuonghieu.Equals(itemTH.Thuonghieuyeuthich) && x.danhmuc.Ten.Equals(itemDM.Loaisanphamyeuthich)).ToListAsync();
                    ListSanPhamHot.AddRange(listSanpham);
                }
            }
            return ListSanPhamHot;
        }

        // GET: Customer
        public async Task<IActionResult> Index1(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false, string khuyenmai = "0")
        {
            var splist = new List<SanPhamDTO>();
            DateTime currentDate = DateTime.Now;
            var query = _context.Mhs.Include(m => m.Danhmucs).AsQueryable();
            var ctkmsp = await _context.CtKhuyenMaiSanPhams.ToListAsync();
            var dssp = await _context.Mathangs.Include(x => x.Danhmucs).ToListAsync();
            bool checkKM = false;
            if (getID() != null) ViewData["MaKH"] = getID().ID; else ViewData["MaKH"] = null;

            foreach (var mh in query)
            {
                var newdssp = dssp.Where(x => x.MaMhchinh.Equals(mh.Id)).ToList();
                var sanpham = new SanPhamDTO();
                sanpham.mh = mh;
                sanpham.GiaGiaoDong = tinhgiagiaodong(newdssp);
                sanpham.giacaonhat = tinhgiacaonhat(newdssp);
                foreach (var item in newdssp)
                {

                    if (await CheckKhuyenMai(item.MaMh))
                    {
                        checkKM = true;
                        sanpham.DanhGiaTrungBinh = 4;
                        sanpham.NoiBat = true;
                        break;
                    }
                    else
                    {
                        checkKM = false;
                        sanpham.DanhGiaTrungBinh = 4;
                        sanpham.NoiBat = true;
                    }
                }
                sanpham.KhuyenMai = checkKM;
                splist.Add(sanpham);
            }

            // Filter by category name if provided
            if (!string.IsNullOrEmpty(category) && category != "Tất cả")
            {
                splist = splist.Where(m => m.mh.Danhmucs.Ten.Equals(category)).ToList();
                ViewBag.category = category;
            }
            else
            {
                ViewBag.category = "Tất cả";
            }
            // Sort by price
            if (sort == "priceHighToLow")
            {
                splist = splist.OrderByDescending(m => m.giacaonhat).ToList();
                ViewBag.sort = sort;

            }
            else if (sort == "priceLowToHigh")
            {
                splist = splist.OrderBy(m => m.giacaonhat).ToList();
                ViewBag.sort = sort;

            }
            else
            {
                ViewBag.sort = "Không sắp xếp";
            }


            // Filter by category name if provided
            if (khuyenmai == "1")
            {
                splist = splist.Where(m => m.KhuyenMai == true).ToList();
                ViewBag.khuyenmai = "1";
            }
            else
            {
                ViewBag.khuyenmai = "0";
            }


            if (!string.IsNullOrEmpty(keyword))
            {
                splist = splist.Where(x => x.mh.Ten.IndexOf(keyword.Trim(), StringComparison.OrdinalIgnoreCase) != -1).ToList();
            }

            var items = new List<Mh>();
            // Apply pagination
            items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Tính toán các thông tin phân trang
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.keyword = keyword;
            ViewBag.Fill = Fill;
            ViewBag.ThuongHieu = await _context.Thuonghieus.ToListAsync();
            // Populate the dropdown with categories
            ViewBag.ListDanhMuc = await _context.Danhmucs.ToListAsync();

            var danhsachsanpham = new SanPham();
            danhsachsanpham.ListSanPhamDTOs = splist;
            danhsachsanpham.ListSanPhamHot = await DanhSachSanPhamHOT();
            if (getID() != null) danhsachsanpham.ListSanPhamGoiY = await DanhSachSanGoiYDuaVaoSoThich(getID().ID); else danhsachsanpham.ListSanPhamGoiY = null;
            ViewData["CheckSoThich"] = null;
            if (danhsachsanpham.ListSanPhamGoiY != null)
            {
                if (danhsachsanpham.ListSanPhamGoiY.Count > 0)
                    ViewData["CheckSoThich"] = "ok";
            }
            return View(danhsachsanpham);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mathangs == null)
            {
                return NotFound();
            }
            var sp = await _context.Mathangs.FirstOrDefaultAsync(x => x.MaMh.Equals(id));
            var newsp = new MathangKM();
            newsp.mathang = sp;
            if (await CheckKhuyenMai(sp.MaMh))
            {
                var ctkm = await CheckKhuyenMaiSanpham(sp.MaMh);
                newsp.Phantramkhuyenmai = ctkm != null ? ctkm.PhanTramGiamGia : 0;
                newsp.Giakhuyemai = ctkm != null ? sp.GiaBan - sp.GiaBan * (ctkm.PhanTramGiamGia / 100.0) : 0;
            }
            else
            {
                newsp.Phantramkhuyenmai = 0;
                newsp.Giakhuyemai = 0;
            }
            ViewBag.ListSanPhamGoiY = await DanhSachSanPhamTuongtu(sp.MaDm, sp.Thuonghieu, sp.Kichco, sp.Mausac);
            return View(newsp);
        }
        // View Giỏ hàng
        public async Task<IActionResult> Cart(bool isusenearest = false)
        {
            try
            {
                var cusstomerAddress = new CustomerAddress();
                var sesssionKh = getID();
                if (sesssionKh != null)
                {
                    cusstomerAddress.KhachhangLogin = sesssionKh;
                    var danhsachdiachi = await _context.Diachis.Where(x => x.Makh.Equals(sesssionKh.ID)).ToListAsync();
                    var diachiganday = await _context.Diachis.Where(x => x.Macdinh.Equals(1)).FirstOrDefaultAsync();
                    if (diachiganday != null)
                    {
                        cusstomerAddress.Diachiganday = diachiganday;
                    }
                    cusstomerAddress.Diachis = danhsachdiachi;
                    ViewBag.UseNearest = isusenearest;
                    return View(cusstomerAddress);
                }
                ViewBag.UseNearest = isusenearest;
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        // Xử lý profile
        public async Task<IActionResult> Profile1(string id = null)
        {
            if (id != null)
            {
                // Tạo URL mới với fragment identifier và chuyển hướng đến đó
                var urlWithFragment = Url.Action("Profile") + id;
                return Redirect(urlWithFragment);
            }
            return RedirectToAction(nameof(Profile));
        }
        // View thông tin
        public async Task<IActionResult> Profile(int page = 1, int pageSize = 6, string id = null)
        {

            ProfileCustomer profile = new ProfileCustomer();
            var sessionKH = getID();
            if (sessionKH != null)
            {
                profile.KhachHang = await _context.Khachhangs.FirstOrDefaultAsync(x => x.MaKh.Equals(sessionKH.ID));
                profile.WistList = GetWishlist();

                var totalCount = await _context.Hoadons
                    .Where(x => x.Makh.Equals(sessionKH.ID))
                    .CountAsync();

                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                // Ensure page is within valid range
                page = Math.Max(1, Math.Min(page, totalPages));

                var skipAmount = (page - 1) * pageSize;

                profile.HoadonList = await _context.Hoadons
                    .Include(m => m.Cthoadons)
                    .Where(x => x.Makh.Equals(sessionKH.ID))
                    .OrderByDescending(x => x.Ngay) // Add the property you want to order by
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToListAsync();

                profile.Diachi = await _context.Diachis
                    .Where(x => x.MakhNavigation.MaKh.Equals(sessionKH.ID))
                    .ToListAsync();

                ViewBag.PageNumber = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;
                var donhang = _context.Hoadons.AsQueryable();
                ViewData["soluongdonhang"] = await donhang.CountAsync();
                var wishlistCart = GetWishlist();
                if (wishlistCart != null)
                    ViewData["slSPYT"] = wishlistCart.Count;
                else
                    ViewData["slSPYT"] = 0;
                return View(profile);
            }

            return View("Login");
        }
        // Lấy ID khách hàng
        public KhachHangLogin getID()
        {
            var sessionKh = HttpContext.Session;
            string jsonKH = sessionKh.GetString("KH_SESSION");
            if (jsonKH != null)
            {
                return JsonConvert.DeserializeObject<KhachHangLogin>(jsonKH);
            }
            return null;
        }
        // Lấy địa chỉ lưu session
        public Diachi1 getDiachi()
        {
            var sessionKh = HttpContext.Session;
            string jsonKH = sessionKh.GetString("diachi");
            if (jsonKH != null)
            {
                return JsonConvert.DeserializeObject<Diachi1>(jsonKH);
            }
            return null;
        }
        // lấy hóa đơn lưu session
        public Hoadon GetHoaDon()
        {
            var jsonKH = HttpContext.Session.GetString("hoadon");
            return jsonKH != null ? JsonConvert.DeserializeObject<Hoadon>(jsonKH) : null;
        }
        // Lấy danh sách sản phẩm yêu thích
        public List<Cart> GetWishlist()
        {
            var jsonKH = HttpContext.Session.GetString("wishlist");
            return jsonKH != null ? JsonConvert.DeserializeObject<List<Cart>>(jsonKH) : null;
        }
        public List<Cart> GetCartList()
        {
            var jsonKH = HttpContext.Session.GetString("cart");
            return jsonKH != null ? JsonConvert.DeserializeObject<List<Cart>>(jsonKH) : null;
        }

        private async Task<Mathang> GetMathangById(int id)
        {
            return await _context.Mathangs.FirstOrDefaultAsync(x => x.MaMh == id);
        }
        //Lưu địa chỉ session
        private void SaveDiachiToSession(Diachi1 carts)
        {
            var sessionKh = HttpContext.Session;
            sessionKh.SetString("diachi", JsonConvert.SerializeObject(carts));
        }
        //Lưu SPYT session
        private void SaveWishlistToSession(List<Cart> carts)
        {
            var sessionKh = HttpContext.Session;
            sessionKh.SetString("wishlist", JsonConvert.SerializeObject(carts));
        }
        //Lưu giỏ hàng session
        private void SaveCarrtToSession(List<Cart> carts)
        {
            var sessionKh = HttpContext.Session;
            sessionKh.SetString("cart", JsonConvert.SerializeObject(carts));
        }
        //Lưu hóa đơn session
        private void SaveHoadonToSession(Hoadon hoadon)
        {
            var sessionKh = HttpContext.Session;
            sessionKh.SetString("hoadon", JsonConvert.SerializeObject(hoadon));
        }
        //Thêm sản phaame yeu thich
        public async Task<IActionResult> AddToWishlistAsync(int id, int giakhuyenmai)
        {
            var sesskh = getID();
            if (sesskh == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                var mathang = await GetMathangById(id);
                if (mathang == null)
                {
                    return RedirectToAction("Index", "Customer");
                }

                var carts = GetWishlist() ?? new List<Cart>();
                var cartItem = carts.Find(x => x.id.Equals(id));

                if (cartItem == null)
                {
                    var cartItemAdd = new Cart
                    {
                        id = id,
                        ten = mathang.Ten,
                        soluongmua = 1,
                        giaban = mathang.GiaBan,
                        hinhanh = mathang.HinhAnh,
                        giakhuyenmai = giakhuyenmai
                    };

                    carts.Add(cartItemAdd);
                    SaveWishlistToSession(carts);
                }
                // Tạo URL mới với fragment identifier và chuyển hướng đến đó
                var urlWithFragment = Url.Action("Profile") + "#sanphamyeuthich";
                return Redirect(urlWithFragment);
            }
        }
        // Xóa tất cả sản phẩm yêu thích
        public async Task<IActionResult> RemoveAllWishlist()
        {
            var sesskh = getID();
            if (sesskh == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                RemoveAll();

                var urlWithFragment = Url.Action("Profile") + "#sanphamyeuthich";
                return Redirect(urlWithFragment);
            }
        }
        // Xóa sản phẩm yêu thích
        public async Task<IActionResult> RemoveItemWish(int id)
        {
            var sesskh = getID();
            if (sesskh == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                RemoveWishItem(id);

                var urlWithFragment = Url.Action("Profile") + "#sanphamyeuthich";
                return Redirect(urlWithFragment);
            }
        }
        // Cập nhật mật khẩu
        public async Task<IActionResult> ChangePassWord(string matkhauthaydoi, string matkhauxacthuc)
        {
            var sesskh = getID();
            if (sesskh == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                var khachhang = await _context.Khachhangs.FirstOrDefaultAsync(x => x.MaKh.Equals(sesskh.ID));
                bool passwordMatch = HashPasword.HashPasword.VerifyPassword(matkhauxacthuc, khachhang.Matkhau, khachhang.Salt);
                if (passwordMatch)
                {
                    (string hashedPassword, byte[] salt) = HashPasword.HashPasword.HashPasword1(matkhauthaydoi);

                    khachhang.Matkhau = hashedPassword;
                    khachhang.Salt = salt;
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                    // Tạo URL mới với fragment identifier và chuyển hướng đến đó
                    TempData["Success"] = "Cập nhật mật khẩu thành công";
                    var urlWithFragment = Url.Action("Profile") + "#thongtintaikhoan";
                    return Redirect(urlWithFragment);
                }
                else
                {
                    TempData["Message"] = "Mật khẩu cũ không đúng";
                    // Tạo URL mới với fragment identifier và chuyển hướng đến đó
                    var urlWithFragment = Url.Action("Profile") + "#thongtintaikhoan";
                    return Redirect(urlWithFragment);
                }
            }
        }

        // thêm mới địa chỉ
        public async Task<IActionResult> CreateAddress(string tinh, string huyen, string xa, string diachi)
        {
            var sesskh = getID();
            if (sesskh == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                var checkKh = await _context.Khachhangs.FirstOrDefaultAsync(x => x.MaKh.Equals(sesskh.ID));
                if (checkKh == null)
                {
                    return RedirectToAction("Login", "Customer");
                }
                var diachiadd = new Diachi();
                diachiadd.Makh = checkKh.MaKh;
                diachiadd.Tinhthanh = tinh;
                diachiadd.Quanhuyen = huyen;
                diachiadd.Phuongxa = xa;
                diachiadd.Diachi1 = diachi;
                diachiadd.Macdinh = 0;
                _context.Diachis.Add(diachiadd);
                await _context.SaveChangesAsync();
                TempData["SuccessAddress"] = "Thêm địa chỉ thành công !";

                // Tạo URL mới với fragment identifier và chuyển hướng đến đó
                var urlWithFragment = Url.Action("Profile") + "#diachi";
                return Redirect(urlWithFragment);
            }
        }

        // Câp nhật ttcn
        public async Task<IActionResult> UpdateInfo(Khachhang kh, IFormFile fileanh)
        {
            var sesskh = getID();
            if (sesskh == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else
            {
                var checkKh = await _context.Khachhangs.FirstOrDefaultAsync(x => x.MaKh.Equals(kh.MaKh));
                if (checkKh == null)
                {
                    return RedirectToAction("Login", "Customer");
                }
                if (fileanh != null && fileanh.Length > 0)
                {
                    // Tạo đường dẫn cho thư mục lưu trữ hình ảnh trong wwwroot/images/products
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "account");

                    var tenfile = DateTime.Now.ToString("yyyyMMddHHmmss") + fileanh.FileName;

                    // Tạo đường dẫn tới file hình ảnh
                    var fileName = Path.Combine(imagePath, tenfile);

                    // Lưu file xuống đường dẫn tạm thời
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await fileanh.CopyToAsync(stream);
                    }

                    // Cập nhật tên file hình ảnh trong đối tượng mathang
                    checkKh.HinhAnh = tenfile;
                }
                checkKh.Ten = kh.Ten;
                checkKh.Email = kh.Email;
                checkKh.Dienthoai = kh.Dienthoai;
                _context.Khachhangs.Update(checkKh);
                await _context.SaveChangesAsync();
                TempData["SuccessUpdate"] = "Cập nhật tài khoản thành công!";

                // Tạo URL mới với fragment identifier và chuyển hướng đến đó
                var urlWithFragment = Url.Action("Profile") + "#thongtintaikhoan";
                return Redirect(urlWithFragment);
            }
        }
        // Kiểm tra số lượng khi mua hàng
        private async Task<int> checksoluongsanpham(int mamh, int soluongmua)
        {
            var mathang = await _context.Mathangs.FirstOrDefaultAsync(x => x.MaMh.Equals(mamh));
            if (mathang.SoLuong >= soluongmua)
                return 0;

            return mathang.SoLuong;
        }

        // Thanh toán khi nhận hàng
        [HttpPost]
        public async Task<IActionResult> Total([FromBody] TotalRequestModel totalRequestModel)
        {
            //Check các sản phẩm trong giỏ hàng có còn thời hạn khuyến mãi hay không 
            foreach (var itemsp in totalRequestModel.cartItems)
            {
                if (await CheckKhuyenMaiMuaHang(itemsp.id) == false)
                {
                    return Json(new
                    {
                        success = false,
                        status = 1,
                        message = "Sản phẩm " + itemsp.ten + " đã hết chương trình khuyến mãi!"
                    });
                }

            }
            //Check các sản phẩm có đủ bán không 
            foreach (var itemsp in totalRequestModel.cartItems)
            {
                var checksoluong = await checksoluongsanpham(itemsp.id, itemsp.soluongmua);
                if (checksoluong > 0)
                {
                    return Json(new
                    {
                        success = false,
                        status = 1,
                        message = $"Số lượng của sản phẩm  {itemsp.ten} chỉ còn {checksoluong} ! Vui lòng chọn lại!"
                    });
                }

            }

            KhachHangLogin idKH = new KhachHangLogin();
            var sessionKh = HttpContext.Session;
            string jsonKH = sessionKh.GetString("KH_SESSION");
            if (jsonKH != null)
            {
                idKH = JsonConvert.DeserializeObject<KhachHangLogin>(jsonKH);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Vui lòng đăng nhập"
                });
            }
            if (totalRequestModel != null && totalRequestModel.cartItems.Count > 0)
            {
                try
                {
                    // Xử lý dữ liệu ở đây
                    // Ví dụ: tính tổng giá trị đơn hàng từ danh sách sản phẩm trong giỏ hàng
                    int totalPrice = totalRequestModel.cartItems.Sum(item => item.giaban * item.soluongmua); // Giả sử có thuộc tính Price trong CartItem
                    var hoadon = new Hoadon();
                    // hoadon.Makh = totalRequestModel.ID;
                    hoadon.Makh = idKH.ID;
                    hoadon.Ngay = DateTime.Now;
                    hoadon.Tongtien = totalPrice;
                    hoadon.Tennguoinhan = totalRequestModel.info.hoten;
                    hoadon.Sodienthoai = totalRequestModel.info.sodienthoai;
                    hoadon.Diachi = totalRequestModel.diachi.Diachi;
                    hoadon.Xaphuong = totalRequestModel.diachi.phuongxa;
                    hoadon.Quanhuyen = totalRequestModel.diachi.quanhuyen;
                    hoadon.Tinh = totalRequestModel.diachi.tinhthanh;
                    hoadon.Trangthai = 0;
                    _context.Hoadons.Add(hoadon);
                    await _context.SaveChangesAsync();  // Save changes to get the generated MAHD
                    foreach (var item in totalRequestModel.cartItems)
                    {
                        var cthd = new Cthoadon();
                        cthd.Mahd = hoadon.Mahd;
                        cthd.Mamh = item.id;
                        cthd.Soluong = item.soluongmua;
                        cthd.Thanhtien = item.soluongmua * item.giaban;
                        var mathang = await _context.Mathangs.FirstOrDefaultAsync(x => x.MaMh.Equals(item.id));
                        mathang.LuotMua += item.soluongmua;
                        mathang.SoLuong -= 1;
                        cthd.Dongia = item.giaban;
                        _context.Cthoadons.Add(cthd);
                    }
                    await _context.SaveChangesAsync();

                    var listdiachikhachhang = await _context.Diachis.Where(d => d.Makh.Equals(idKH.ID)).ToListAsync();
                    if (listdiachikhachhang.Count == 0)
                    {
                        var diachikhachhang = new Diachi();
                        diachikhachhang.Makh = idKH.ID;
                        diachikhachhang.Diachi1 = totalRequestModel.diachi.Diachi;
                        diachikhachhang.Phuongxa = totalRequestModel.diachi.phuongxa;
                        diachikhachhang.Quanhuyen = totalRequestModel.diachi.quanhuyen;
                        diachikhachhang.Tinhthanh = totalRequestModel.diachi.tinhthanh;
                        diachikhachhang.Macdinh = totalRequestModel.diachi.macdinh;
                        _context.Diachis.Add(diachikhachhang);
                    }
                    else
                    {
                        // Lấy tất cả các bản ghi có Macdinh = 1
                        var currentDefaultAddresses = await _context.Diachis.Where(x => x.Macdinh == 1 && x.Makh == idKH.ID).ToListAsync();

                        // Cập nhật tất cả chúng để Macdinh = 0
                        foreach (var address in currentDefaultAddresses)
                        {
                            address.Macdinh = 0;
                        }

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await _context.SaveChangesAsync();
                        var item = listdiachikhachhang.Find(x => x.MaDc.Equals(totalRequestModel.diachi.id));
                        if (item != null)
                        {
                            item.Macdinh = 1;
                        }
                        else
                        {

                            var diachikhachhang = new Diachi();
                            diachikhachhang.Makh = idKH.ID;
                            diachikhachhang.Diachi1 = totalRequestModel.diachi.Diachi;
                            diachikhachhang.Phuongxa = totalRequestModel.diachi.phuongxa;
                            diachikhachhang.Quanhuyen = totalRequestModel.diachi.quanhuyen;
                            diachikhachhang.Tinhthanh = totalRequestModel.diachi.tinhthanh;
                            diachikhachhang.Macdinh = totalRequestModel.diachi.macdinh;
                            _context.Diachis.Add(diachikhachhang);
                        }
                    }
                    await _context.SaveChangesAsync();
                    var cartsession = HttpContext.Session;
                    cartsession.Remove("cart");

                    // Gởi email
                    try
                    {
                        MailInfo mailInfo = new MailInfo();
                        mailInfo.Subject = "Đặt hàng thành công tại ITShop.Com.Vn";
                        var datHangInfo = _context.Hoadons.Where(r => r.Mahd == hoadon.Mahd).Include(s => s.MakhNavigation).Include(s => s.Cthoadons).SingleOrDefault();
                        await _mailLogic.SendEmailDatHangThanhCong(datHangInfo, mailInfo);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                    // Trả về một phản hồi JSON với dữ liệu bạn muốn truyền về trang
                    return Json(new
                    {
                        success = true,
                        message = "Đặt hàng thành công!",
                        hoadon
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

            // Trả về một phản hồi nếu dữ liệu không hợp lệ hoặc không có
            return BadRequest();
        }
        // Đăng xuất
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            // Redirect to the specified returnUrl or a default page
            return RedirectToAction("Index", "Customer");
        }
        // Đăng ký
        public ActionResult Register(Khachhang loginModel)
        {

            if (loginModel.Ten == null)
            {
                return View(loginModel);
            }
            var returnUrlFromQuery = Request.Query["returnUrl"];
            if (!string.IsNullOrEmpty(returnUrlFromQuery))
            {
                HttpContext.Session.SetString("returnUrlsession", returnUrlFromQuery);
            }
            (string hashedPassword, byte[] salt) = HashPasword.HashPasword.HashPasword1(loginModel.Matkhau);

            loginModel.Matkhau = hashedPassword;
            loginModel.Salt = salt;
            _context.Add(loginModel);
            _context.SaveChanges();
            var session = new KhachHangLogin
            {
                ID = loginModel.MaKh,
                Name = loginModel.Ten,
                Email = loginModel.Email,
                Phone = loginModel.Dienthoai
            };

            // Store user information in session
            HttpContext.Session.SetString("KH_SESSION", JsonConvert.SerializeObject(session));

            // Retrieve the returnUrl from the session
            var returnUrlFromSession = HttpContext.Session.GetString("returnUrlsession");
            HttpContext.Session.Remove("returnUrlsession");
            // return View("Cart");
            // Redirect back to the original page after successful login
            return Redirect(!string.IsNullOrEmpty(returnUrlFromSession) ? returnUrlFromSession : "Index1");

        }
        // Đăng nhaapj
        public async Task<ActionResult> Login(Khachhang loginModel)
        {
            // Check if the user is already authenticated using session
            var khSession = HttpContext.Session.GetString("KH_SESSION");
            if (!string.IsNullOrEmpty(khSession))
            {
                // User is already authenticated, redirect to the Customer/Index page
                return RedirectToAction("Index1", "Customer");
            }

            // If there's a returnUrl in the query string, store it in the session
            var returnUrlFromQuery = Request.Query["returnUrl"];
            if (!string.IsNullOrEmpty(returnUrlFromQuery))
            {
                HttpContext.Session.SetString("returnUrlsession", returnUrlFromQuery);
            }

            if (!string.IsNullOrEmpty(loginModel.Email) && !string.IsNullOrEmpty(loginModel.Matkhau))
            {
                var user = await _context.Khachhangs.FirstOrDefaultAsync(x => x.Email == loginModel.Email);

                if (user == null)
                {
                    TempData["Message"] = "Email chưa đăng ký tài khoản!";

                }
                else
                {
                    string passwordToVerify = loginModel.Matkhau; // Replace with the password to verify
                    bool passwordMatch = HashPasword.HashPasword.VerifyPassword(passwordToVerify, user.Matkhau, user.Salt);

                    // Validate the entered password
                    if (passwordMatch)
                    {
                        var session = new KhachHangLogin
                        {
                            ID = user.MaKh,
                            Name = user.Ten,
                            Email = user.Email,
                            Phone = user.Dienthoai
                        };

                        // Store user information in session
                        HttpContext.Session.SetString("KH_SESSION", JsonConvert.SerializeObject(session));

                        // Retrieve the returnUrl from the session
                        var returnUrlFromSession = HttpContext.Session.GetString("returnUrlsession");
                        HttpContext.Session.Remove("returnUrlsession");
                        // return View("Cart");
                        // Redirect back to the original page after successful login
                        return Redirect(!string.IsNullOrEmpty(returnUrlFromSession) ? returnUrlFromSession : "Index1");
                    }
                    TempData["Message"] = "Mật khẩu không đúng. Vui lòng kiểm tra lại!";

                }
            }

            // Validate user credentials

            return View(loginModel);
        }
        //Kiểm tra khuyến mãi
        private async Task<bool> CheckKhuyenMai(int idsp)
        {
            var ctkmList = await _context.CtKhuyenMais.Include(x => x.CtKhuyenMaiSanPhams).ToListAsync();

            foreach (var ctkm in ctkmList)
            {
                var currentDateTime = DateTime.Now;

                foreach (var sp in ctkm.CtKhuyenMaiSanPhams)
                {
                    if (sp.Mamh.Equals(idsp) && ctkm.NgayBatDau <= currentDateTime && currentDateTime <= ctkm.NgayKetThuc)
                    {
                        return true; // Nếu tìm thấy khuyến mãi thỏa mãn, trả về true và kết thúc hàm
                    }
                }
            }

            return false; // Nếu không tìm thấy khuyến mãi thỏa mãn, trả về false
        }
        // Kiểm tra khuyên mãi mua hàng
        private async Task<bool> CheckKhuyenMaiMuaHang(int idsp)
        {
            var currentDateTime = DateTime.Now;

            var ctkmList = await _context.CtKhuyenMais
                .Include(x => x.CtKhuyenMaiSanPhams)
                .Where(ctkm => ctkm.NgayBatDau <= currentDateTime && currentDateTime <= ctkm.NgayKetThuc)
                .ToListAsync();

            foreach (var ctkm in ctkmList)
            {
                var isProductInPromotion = ctkm.CtKhuyenMaiSanPhams.Any(sp => sp.Mamh.Equals(idsp));

                if (isProductInPromotion)
                {
                    // If the product is in any active promotion, check if it has expired
                    if (currentDateTime <= ctkm.NgayKetThuc)
                    {
                        return true; // The product is in an active promotion
                    }
                    else
                    {
                        return false; // The product is in a promotion but has expired
                    }
                }
            }

            return true; // The product is not in any active promotion
        }
        // Kiểm tra khuyên mãi mua hàng
        private async Task<CtKhuyenMai> CheckKhuyenMaiSanpham(int idsp)
        {
            var ctkm = await _context.CtKhuyenMais.ToListAsync();
            if (ctkm.Count == 0)
            {
                return null;
            }
            // Assuming MaSP is the foreign key in CtKhuyenMais referencing CtKhuyenMaiSanPhams
            var hasKhuyenMai = await _context.CtKhuyenMais
                .Include(x => x.CtKhuyenMaiSanPhams)
                .AnyAsync(x => x.CtKhuyenMaiSanPhams.Any(sp => sp.Mamh == idsp) && x.NgayBatDau <= DateTime.Now && DateTime.Now <= x.NgayKetThuc || x.CtKhuyenMaiSanPhams.All(sp => sp.Mamh != idsp));
            if (hasKhuyenMai)
            {
                return await _context.CtKhuyenMais
                .Include(x => x.CtKhuyenMaiSanPhams)
                .FirstOrDefaultAsync(x => x.CtKhuyenMaiSanPhams.Any(sp => sp.Mamh == idsp) && x.NgayBatDau <= DateTime.Now && DateTime.Now <= x.NgayKetThuc || x.CtKhuyenMaiSanPhams.All(sp => sp.Mamh != idsp));
            }
            return null;
        }

        /// <summary>
        /// Thanh toán VNPay
        /// </summary>
        /// <param name="totalRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Payment([FromBody] TotalRequestModel totalRequestModel)
        {
            //Check các sản phẩm trong giỏ hàng có còn thời hạn khuyến mãi hay không 
            foreach (var itemsp in totalRequestModel.cartItems)
            {
                if (await CheckKhuyenMaiMuaHang(itemsp.id) == false)
                {
                    return Json(new
                    {
                        success = false,
                        status = 1,
                        message = "Sản phẩm " + itemsp.ten + " đã hết chương trình khuyến mãi!"
                    });
                }
            }
            //Check các sản phẩm có đủ bán không 
            foreach (var itemsp in totalRequestModel.cartItems)
            {
                var checksoluong = await checksoluongsanpham(itemsp.id, itemsp.soluongmua);
                if (checksoluong > 0)
                {
                    return Json(new
                    {
                        success = false,
                        status = 1,
                        message = $"Số lượng của sản phẩm  {itemsp.ten} chỉ còn {checksoluong} ! Vui lòng chọn lại!"
                    });
                }

            }
            double totalPrice = 0;
            var hoadon = new Hoadon();
            KhachHangLogin idKH = new KhachHangLogin();
            var sessionKh = HttpContext.Session;
            string jsonKH = sessionKh.GetString("KH_SESSION");
            if (jsonKH != null)
            {
                idKH = JsonConvert.DeserializeObject<KhachHangLogin>(jsonKH);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Vui lòng đăng nhập"
                });
            }
            if (totalRequestModel != null && totalRequestModel.cartItems.Count > 0)
            {
                try
                {
                    // Xử lý dữ liệu ở đây
                    // Ví dụ: tính tổng giá trị đơn hàng từ danh sách sản phẩm trong giỏ hàng
                    totalPrice = totalRequestModel.cartItems.Sum(item => item.giaban * item.soluongmua); // Giả sử có thuộc tính Price trong CartItem
                    // hoadon.Makh = totalRequestModel.ID;
                    hoadon.Makh = idKH.ID;
                    hoadon.Ngay = DateTime.Now;
                    hoadon.Tongtien = (int)totalPrice;
                    hoadon.Tennguoinhan = totalRequestModel.info.hoten;
                    hoadon.Sodienthoai = totalRequestModel.info.sodienthoai;
                    hoadon.Diachi = totalRequestModel.diachi.Diachi;
                    hoadon.Xaphuong = totalRequestModel.diachi.phuongxa;
                    hoadon.Quanhuyen = totalRequestModel.diachi.quanhuyen;
                    hoadon.Tinh = totalRequestModel.diachi.tinhthanh;
                    hoadon.Trangthai = 0;
                    SaveHoadonToSession(hoadon);
                    SaveCarrtToSession(totalRequestModel.cartItems);
                    SaveDiachiToSession(totalRequestModel.diachi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

            string url = _configuration["Vnpay:PaymentUrl"];
            string returnUrl = _configuration["Vnpay:ReturnUrl"];
            string tmnCode = _configuration["Vnpay:TmnCode"];
            string hashSecret = _configuration["Vnpay:HashSecret"];

            PayLib pay = new PayLib();
            var stringTotal = totalPrice * 100;
            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", stringTotal.ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress(HttpContextAccessor)); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang" + hoadon.Mahd); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Json(new
            {
                success = true,
                message = "Chuyển hướng thanh toán VNPay",
                url = paymentUrl
            });
            //  return Redirect(paymentUrl);
        }
        // Xác nhận thanh toán VNPay
        public async Task<IActionResult> PaymentConfirm()
        {
            if (HttpContext.Request.Query.Count > 0)
            {
                string hashSecret = _configuration["Vnpay:HashSecret"]; // Chuỗi bí mật
                var vnpayData = HttpContext.Request.Query;
                PayLib pay = new PayLib();

                // Lấy toàn bộ dữ liệu được trả về
                foreach (var (key, value) in vnpayData)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(key, value);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); // Mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); // Mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); // Response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = HttpContext.Request.Query["vnp_SecureHash"]; // Hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); // Check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")// Nếu response vnpcode la 00 thì thành công
                    {
                        // Thanh toán thành công
                        ViewData["success"] = "Thanh toán thành công !";
                        ViewBag.Message = $"Thanh toán VNPay thành công | Mã giao dịch: {vnpayTranId} vào lúc {DateTime.Now.ToString()}";
                        var cartlist = GetCartList();
                        var hoadon = GetHoaDon();
                        var diachi = getDiachi();
                        var kh = getID();
                        try
                        {

                            _context.Hoadons.Add(hoadon);
                            await _context.SaveChangesAsync();  // Save changes to get the generated MAHD
                            foreach (var item in cartlist)
                            {
                                var cthd = new Cthoadon();
                                cthd.Mahd = hoadon.Mahd;
                                cthd.Mamh = item.id;
                                cthd.Soluong = item.soluongmua;
                                cthd.Thanhtien = item.soluongmua * item.giaban;
                                var mathang = await _context.Mathangs.FirstOrDefaultAsync(x => x.MaMh.Equals(item.id));
                                mathang.LuotMua += item.soluongmua;
                                mathang.SoLuong -= 1;
                                cthd.Dongia = item.giaban;
                                _context.Cthoadons.Add(cthd);
                            }
                            var listdiachikhachhang = await _context.Diachis.Where(d => d.Makh.Equals(kh.ID)).ToListAsync();
                            if (listdiachikhachhang.Count == 0)
                            {
                                var diachikhachhang = new Diachi();
                                diachikhachhang.Makh = kh.ID;
                                diachikhachhang.Diachi1 = diachi.Diachi;
                                diachikhachhang.Phuongxa = diachi.phuongxa;
                                diachikhachhang.Quanhuyen = diachi.quanhuyen;
                                diachikhachhang.Tinhthanh = diachi.tinhthanh;
                                diachikhachhang.Macdinh = diachi.macdinh;
                                _context.Diachis.Add(diachikhachhang);
                            }
                            else
                            {
                                // Lấy tất cả các bản ghi có Macdinh = 1
                                var currentDefaultAddresses = await _context.Diachis.Where(x => x.Macdinh == 1 && x.Makh == kh.ID).ToListAsync();

                                // Cập nhật tất cả chúng để Macdinh = 0
                                foreach (var address in currentDefaultAddresses)
                                {
                                    address.Macdinh = 0;
                                }

                                // Lưu thay đổi vào cơ sở dữ liệu
                                await _context.SaveChangesAsync();
                                var item = listdiachikhachhang.Find(x => x.MaDc.Equals(diachi.id));
                                if (item != null)
                                {
                                    item.Macdinh = 1;
                                }
                                else
                                {
                                    var diachikhachhang = new Diachi();
                                    diachikhachhang.Makh = kh.ID;
                                    diachikhachhang.Diachi1 = diachi.Diachi;
                                    diachikhachhang.Phuongxa = diachi.phuongxa;
                                    diachikhachhang.Quanhuyen = diachi.quanhuyen;
                                    diachikhachhang.Tinhthanh = diachi.tinhthanh;
                                    diachikhachhang.Macdinh = diachi.macdinh;
                                    _context.Diachis.Add(diachikhachhang);
                                }
                            }
                            await _context.SaveChangesAsync();
                            var cartsession = HttpContext.Session;
                            cartsession.Remove("cart");
                            cartsession.Remove("diachi");
                            cartsession.Remove("hoadon");
                            var datHangInfo = _context.Hoadons.Where(r => r.Mahd == hoadon.Mahd).Include(s => s.MakhNavigation).Include(s => s.Cthoadons).ThenInclude(mh => mh.MamhNavigation).SingleOrDefault();

                            // Gởi email
                            try
                            {
                                MailInfo mailInfo = new MailInfo();
                                mailInfo.Subject = "Đặt hàng thành công tại AYTI SHOP - Cảm ơn quý khách đã mua sắm!";
                                await _mailLogic.SendEmailDatHangThanhCong(datHangInfo, mailInfo);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message.ToString());
                            }

                            return View(datHangInfo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    else
                    {
                        // Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        // Thanh toán thành công
                        ViewData["success"] = "Thanh toán thất bại !";
                        ViewBag.Message = $"Có lỗi xảy ra trong quá trình xử lý hóa đơn {orderId} | Mã giao dịch: {vnpayTranId} | Mã lỗi: {vnp_ResponseCode}";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return BadRequest();
        }
        // Câp nhật lượt xem
        [HttpPost]
        public async Task<IActionResult> UpdateLuotXem(int Id)
        {
            var mathang = await _context.Mhs.FirstOrDefaultAsync(x => x.Id.Equals(Id));
            if (mathang == null)
            {
                return Json(new
                {
                    success = true,
                    status = 1,
                    message = "Sản phẩm không tồn tại!"
                });
            }
            mathang.LuotXem += 1;
            await _context.SaveChangesAsync();
            return Json(new
            {
                success = false,
                status = 1,
                message = "Cập nhật không thành công !"
            });
        }
        // View xem chi tiết
        public async Task<IActionResult> Details2(int Id)
        {
            DateTime currentDate = DateTime.Now;
            var sanphamchitiet = new ChitietSanphamDTO();
            var mathang = await _context.Mhs.Include(m => m.Danhmucs).FirstOrDefaultAsync(x => x.Id.Equals(Id));
            ViewData["ID"] = mathang.Id;
            var danhsachsanpham = await _context.Mathangs.Where(x => x.MaMhchinh.Equals(Id)).ToListAsync();
            sanphamchitiet.GiaGiaoDong = tinhgiagiaodong(danhsachsanpham);
            var newdanhsachsanpham = new List<MathangKM>();
            foreach (var sp in danhsachsanpham)
            {
                var newsp = new MathangKM();
                newsp.mathang = sp;

                if (await CheckKhuyenMai(sp.MaMh))
                {
                    var ctkm = await CheckKhuyenMaiSanpham(sp.MaMh);
                    newsp.Phantramkhuyenmai = ctkm != null ? ctkm.PhanTramGiamGia : 0;
                    newsp.Giakhuyemai = ctkm != null ? sp.GiaBan - sp.GiaBan * (ctkm.PhanTramGiamGia / 100.0) : 0;
                }
                else
                {
                    newsp.Phantramkhuyenmai = 0;
                    newsp.Giakhuyemai = 0;
                }
                newdanhsachsanpham.Add(newsp);
            }
            sanphamchitiet.mathangchinh = mathang;

            sanphamchitiet.DanhsachMathang = newdanhsachsanpham.OrderBy(x => x.mathang.Kichco).ToList();
            sanphamchitiet.danhmuc = mathang.Danhmucs;
            return View(sanphamchitiet);
        }
        // Lưu sở thích khách hàng
        [HttpPost]
        public async Task<IActionResult> SaveFavorite(FavoriteViewModel model)
        {
            foreach (var item in model.SelectedDanhMuc)
            {
                var dmst = new Danhmucsothich();
                dmst.Makh = getID().ID;
                dmst.Loaisanphamyeuthich = item;
                _context.Danhmucsothiches.Add(dmst);
            }
            foreach (var item in model.SelectedThuongHieu)
            {
                var dmst = new Thuonghieusothich();
                dmst.Makh = getID().ID;
                dmst.Thuonghieuyeuthich = item;
                _context.Thuonghieusothiches.Add(dmst);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index1");
        }
        // Xóa địa chỉ
        [HttpPost]
        public async Task<IActionResult> XoaDiaChi(int id)
        {
            if (id == null)
            {
                return View("Profile");
            }

            var diachi = await _context.Diachis.FirstOrDefaultAsync(x => x.MaDc.Equals(id));

            if (diachi == null)
            {
                return NotFound();
            }

            _context.Diachis.Remove(diachi);
            await _context.SaveChangesAsync();

            // Tạo URL mới với fragment identifier và chuyển hướng đến đó
            var urlWithFragment = Url.Action("Profile") + "#diachi";
            return Redirect(urlWithFragment);
        }

        private void RemoveWishItem(int ID)
        {
            var list = GetWishlist();
            if (list != null)
            {
                var checkitem = list.FirstOrDefault(x => x.id.Equals(ID));
                list.Remove(checkitem);
                SaveWishlistToSession(list);
            }
        }
        private void RemoveAll()
        {
            var sessionKh = HttpContext.Session;
            sessionKh.SetString("wishlist", "");
        }

    }
}
