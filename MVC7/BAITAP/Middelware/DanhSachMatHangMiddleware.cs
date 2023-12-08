using BAITAP.Data;
using BAITAP.Models;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Middelware
{
    public class DanhSachMatHangMiddleware
    {
        private readonly RequestDelegate _next;

        public DanhSachMatHangMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            try
            {
                if (context == null)
                {
                    // Log hoặc xử lý lỗi ở đây
                    return;
                }
                // Danh sách các mặt hàng
                List<Danhmuc> danhSachMatHang = await dbContext.Danhmucs.ToListAsync();

                // Gán danh sách vào HttpContext.Items
                context.Items["DanhSachMatHang"] = danhSachMatHang;

                // Chuyển xử lý cho Middleware tiếp theo hoặc Controller
                await _next(context);
            }
           
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                // Ví dụ: Ghi log, trả về lỗi HTTP, hoặc thực hiện xử lý khác theo yêu cầu của bạn
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }

    }
    public static class DanhSachMatHangMiddlewareExtensions
    {
        public static IApplicationBuilder UseDanhSachMatHangMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DanhSachMatHangMiddleware>();
        }
    }
}
