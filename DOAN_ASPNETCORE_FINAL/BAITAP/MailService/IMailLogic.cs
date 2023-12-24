using BAITAP.Models;

namespace BAITAP.MailService
{
    public interface IMailLogic
    {
        Task SendEmail(MailInfo mailInfo);
        Task SendEmailDatHangThanhCong(Hoadon datHang, MailInfo mailInfo);
    }
}
