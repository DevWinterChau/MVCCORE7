using BAITAP.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
namespace BAITAP.MailService
{
    public class MailLogic : IMailLogic
    {
        private readonly MailSettings _mailSettings;

        public MailLogic(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmail(MailInfo mailInfo)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Address));
            email.To.Add(new MailboxAddress(null, mailInfo.ToEmail));
            email.Subject = mailInfo.Subject;
            var builder = new BodyBuilder();
            if (mailInfo.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailInfo.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailInfo.Body;
            email.Body = builder.ToMessageBody();

            var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Address, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailDatHangThanhCong(Hoadon datHang, MailInfo mailInfo)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Emails\\DatHangThanhCong.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            string chiTiet = "";
            int stt = 1;
            decimal tongTien = 0;
            foreach (var item in datHang.Cthoadons)
        {
                chiTiet += "<tr>" +
                "<td>" + stt + "</td>" +
                "<td>" + item.MamhNavigation.Ten + "</td>" +
                "<td style='text-align:center'>" + item.Soluong + "</p></td>" +
                "<td style='text-align:center'>" + string.Format("{0:N0}", item.Dongia) + "</td>" +
                "<td style='text-align:center'>" + string.Format("{0:N0}", (item.Soluong * item.Dongia)) + "<sup>đ</sup></td>" +
                "</tr>";
                tongTien += item.Soluong * item.Dongia;
                stt++;
            }

            MailText = MailText.Replace("[HoVaTen]", datHang.MakhNavigation.Ten)
            .Replace("[DienThoaiGiaoHang]", datHang.Sodienthoai)
            .Replace("[DiaChiGiaoHang]", datHang.Diachi+", "+ datHang.Xaphuong+", "+ datHang.Quanhuyen+", "+ datHang.Tinh)
            .Replace("[DatHang_ChiTiet]", chiTiet)
            .Replace("[TongTienSanPham]", string.Format("{0:N0}", tongTien));

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Address));
            email.To.Add(new MailboxAddress(datHang.MakhNavigation.Ten, datHang.MakhNavigation.Email));
            email.Subject = mailInfo.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Address, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
