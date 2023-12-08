USE [master]
GO
/****** Object:  Database [ITShop]    Script Date: 11/17/2023 8:21:19 AM ******/
CREATE DATABASE [ITShop] ON  PRIMARY 
( NAME = N'ITShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DONGCHAU\MSSQL\DATA\ITShop.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ITShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DONGCHAU\MSSQL\DATA\ITShop_log.LDF' , SIZE = 768KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ITShop] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ITShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ITShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ITShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ITShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ITShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ITShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [ITShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ITShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ITShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ITShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ITShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ITShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ITShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ITShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ITShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ITShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ITShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ITShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ITShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ITShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ITShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ITShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ITShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ITShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ITShop] SET  MULTI_USER 
GO
ALTER DATABASE [ITShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ITShop] SET DB_CHAINING OFF 
GO
USE [ITShop]
GO
/****** Object:  Table [dbo].[CHUCVU]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHUCVU](
	[MACV] [int] IDENTITY(1,1) NOT NULL,
	[TEN] [nvarchar](100) NOT NULL,
	[HESO] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MACV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTHOADON]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTHOADON](
	[MACTHD] [int] IDENTITY(1,1) NOT NULL,
	[MAHD] [int] NOT NULL,
	[MAMH] [int] NOT NULL,
	[DONGIA] [int] NULL,
	[SOLUONG] [smallint] NULL,
	[THANHTIEN] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MACTHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DANHMUC]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DANHMUC](
	[MaDM] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DIACHI]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DIACHI](
	[MaDC] [int] IDENTITY(1,1) NOT NULL,
	[MAKH] [int] NOT NULL,
	[DIACHI] [nvarchar](100) NOT NULL,
	[PHUONGXA] [nvarchar](20) NOT NULL,
	[QUANHUYEN] [nvarchar](50) NOT NULL,
	[TINHTHANH] [nvarchar](50) NOT NULL,
	[MACDINH] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MAHD] [int] IDENTITY(1,1) NOT NULL,
	[MAKH] [int] NOT NULL,
	[NGAY] [datetime] NULL,
	[TONGTIEN] [int] NOT NULL,
	[TRANGTHAI] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MAHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NULL,
	[DIENTHOAI] [varchar](20) NOT NULL,
	[EMAIL] [varchar](50) NOT NULL,
	[MATKHAU] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MATHANG]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATHANG](
	[MaMH] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](100) NOT NULL,
	[GiaGoc] [int] NULL,
	[GiaBan] [int] NULL,
	[SoLuong] [smallint] NULL,
	[MoTa] [nvarchar](1000) NULL,
	[HinhAnh] [varchar](255) NULL,
	[MaDM] [int] NOT NULL,
	[LuotXem] [int] NULL,
	[LuotMua] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 11/17/2023 8:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MANV] [int] IDENTITY(1,1) NOT NULL,
	[TEN] [nvarchar](100) NOT NULL,
	[MACV] [int] NOT NULL,
	[DIENTHOAI] [varchar](20) NULL,
	[EMAIL] [varchar](50) NULL,
	[MATKHAU] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MANV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DANHMUC] ON 

INSERT [dbo].[DANHMUC] ([MaDM], [Ten]) VALUES (1, N'Sản phẩm điện tử')
INSERT [dbo].[DANHMUC] ([MaDM], [Ten]) VALUES (2, N'Dụng cụ văn phòng')
INSERT [dbo].[DANHMUC] ([MaDM], [Ten]) VALUES (3, N'Dụng cụ học sinh')
INSERT [dbo].[DANHMUC] ([MaDM], [Ten]) VALUES (4, N'Sản phẩm giấy')
SET IDENTITY_INSERT [dbo].[DANHMUC] OFF
GO
SET IDENTITY_INSERT [dbo].[MATHANG] ON 

INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (1, N'Máy Tính Văn Phòng Casio SX 100 - W-DP ', 200000, 200000, 10, N'Kích thước (Dài × Rộng × Dày) : 110,5 × 91 × 9,4 mm.  Màn hình lớn dễ dàng đọc dữ liệu.  Có 2 nguồn năng lượng: mặt trời & pin.', N'm1.jpg', 1, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (2, N'Máy Tính Casio FX580VN X-PK (Màu Hồng) ', 757000, 757000, 20, N'Máy Tính Casio FX580VN X-PK (Màu Hồng) thuộc dòng máy tính khoa học ClassWiz, được hãng máy tính Casio Nhật Bản sản xuất dành riêng cho nền giáo dục Việt. Sản phẩm tích hợp tới 521 tính năng, trong đó có rất nhiều tính năng mà các dòng máy tính khoa học trên thị trường hiện nay không có được. Casio fx-580VN X được phép đưa vào phòng thi.  Tốc độ xử lý nhanh gấp 4 lần, giảm thời gian tính toán xuống mức tối thiểu 521 tính năng, nhiều tính năng mà các máy tính khác không có.  Mua một lần, dùng nhiều cấp học.  Hỗ trợ đắc lực giải toán cao cấp ở đại học.  Dung lượng bộ nhớ lớn gấp 2 lần.  Độ phân giải gấp 4 lần, hiển thị đầy đủ phép tính.  Có ngôn ngữ tiếng Việt vô cùng tiện dụng. ', N'm2.jpg', 1, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (5, N'Máy Tính CASIO FX880BTG - Màu Xanh Biển ', 820000, 820000, 20, N'Máy tính Casio fx-880BTG thuộc dòng máy tính khoa học ClassWiz của hãng máy tính CASIO. Máy tính Casio fx-880BTG đã ra đời với nhiều cải tiến về: thiết kế - giao diện, tính năng nổi trội và độ chính xác cao… để đáp ứng thực tiễn dạy và học tại Việt Nam, đồng thời thay đổi tư duy học tập lâu nay của học sinh. Tính năng nổi bật: - QR Code hỗ trợ dạy và học - Bảng tính spreadsheet - Hộp toán học Math Box - Bảng tuần hoàn - Kiểm chứng - Gian diện mới với thao tác đơn giản hơn - Kết quả tính toán chính xác lên đến 23 chữ số ', N'm3.jpg', 1, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (6, N'Máy Tính Khoa Học Thiên Long Flexio Fx680VN Plus - Màu Trắng ', 635000, 635000, 30, N'Máy Tính Khoa Học Thiên Long Flexio Fx680VN Plus - Màu Trắng:  512 + 12 Tính Năng. Mày tính đạt chuẩn mang vào phòng thi: Không truyền tín hiệu, Không có chức năng soạn thảo văn bản và thẻ nhớ.  Máy tính có thiết kế hiện đại, cá tính.  Máy mỏng nhẹ với chất liệu bền bỉ, phím bấm có độ chống mòn và mờ cao.  Dàn phím được bố trí khoa học và thuận tiện cho các thao tác trên máy tính.  Độ bền phím bấm lên tới 500.000 lần.  Có tính năng thông báo pin yếu. ', N'm4.jpg', 1, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (7, N'Kệ Nhựa 3 Tầng - King Star - Màu Xanh Dương ', 178000, 178000, 25, N'Màu sắc trang nhã.  Khay thiết kế 3 tầng tiện dụng.  Thao tác lắp đặt, tháo rời đơn giản, dễ dàng.  Bề mặt có các khe hở, tránh ẩm mốc.  Khay đựng tài liệu 3 tầng thường được dùng trong các văn phòng công sở, trường học để cất giữ, bảo quản, phân loại các giấy tờ, tài liệu quan trọng.', N'v1.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (8, N'Bìa Trình Ký Đôi Toppoint A4 TOP-134A - Xanh Lá', 52000, 52000, 50, N'Sản phẩm được làm từ chất liệu nhựa cứng PP cao cấp. Thanh kẹp cũng được thiết kế bằng chất liệu có độ bền cao. Sản phẩm có kích thước phù hợp với khổ giấy A4. Giấy được gọng sắt giữ lại, không lo giấy bị bay hoặc thất lạc khi chờ ký. Gọng sắt bền, không bị gãy khi sử dụng quá nhiều. ', N'v2.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (9, N'Khay Cắm Bút Flexoffice FO-PS01 ', 60000, 60000, 30, N'Sản phẩm làm bằng nhựa cao cấp, thân thiện với môi trường, an toàn khi sử dụng. Có nhiều ngăn để đựng bút viết. Thiết kế trong suốt hiện đại, đơn giản nhưng tinh tế với kiểu dáng nhỏ gọn tiện dụng. ', N'v3.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (10, N'Cắm Bút Moshi 016', 60000, 60000, 20, N'Sản phẩm làm bằng nhựa cao cấp, màu sắc đẹp và bắt mắt. Sản phẩm được thiết kế với kiểu dáng đơn giản, kích thước nhỏ gọn không chiếm nhiều diện tích. Sản phẩm giúp cho việc sắp xếp các đồ dùng nơi bàn làm việc trở nên gọn gàng, ngăn nắp.', N'v4.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (11, N'Bìa Còng 5P F4 Kokuyo 285B - Màu Xanh ', 85000, 85000, 10, N'Có thiết kế khổ F4 đáp ứng tốt việc lưu trữ số lượng lớn tài liệu và giấy tờ quan trọng. Có thể lưu trữ tài liệu thuộc nhiều kích cỡ, phù hợp với hầu hết các loại giấy tờ Áp dụng công nghệ tiên tiến của Nhật Bản trong sản xuất đảm bảo chất lượng sản phẩm. Chất liệu bìa bằng simili cao cấp, phủ màng OPP với ưu điểm bền chắc, không bám bụi và tránh được nhiều trường hợp cong vênh trong quá trình sử dụng.', N'v5.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (12, N'Máy Bấm Giá Hand MX5500 - Màu Xanh ', 205000, 205000, 20, N'Sản phẩm là dụng cụ tiện lợi dùng để bấm giá đồng loạt các sản phẩm một cách nhanh chóng. Cấu tạo máy bấm đơn giản, giúp bạn dễ dàng bấm in giá và điều chỉnh giá theo ý muốn. Sản phẩm được làm bằng chất liệu nhựa chắc chắn, kháng vỡ, cho lực bấm nhẹ, thao tác nhanh chóng và dễ dàng hơn. Thiết kế sản phẩm nhỏ gọn, tiện lợi', N'v7.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (13, N'Hộp 24 Kẹp Bướm Màu 41mm - Hồng Hà 6642 ', 83000, 83000, 20, N'- Sản xuất từ chất liệu kim loại cao cấp, được phủ Niken chống gỉ giúp kẹp bướm luôn bền đẹp theo thời gian - Phần lò xo của kẹp linh hoạt, nhẹ, dễ dùng, không bị lỏng hay bung rời sau nhiều lần sử dụng . - Tay cầm chắc chắn, vừa vặn tạo cảm giác thoải mái khi sử dụng - Đa dạng đủ mọi kích cỡ từ 15 - 51 mm phù hợp với mọi nhu cầu - Có 4 màu tươi sáng, hợp xu hướng giúp bàn làm việc nhiều màu sắc, khơi gợi cảm hứng và sáng tạo', N'v6.jpg', 2, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (14, N'Bộ Compa 8 Món - Bút Chì Kim - Yalong 19020 ', 53000, 53000, 20, N'Compass được làm từ kim loại cứng cáp, không gỉ sét, độ bền cao. Sản phẩm có thiết kế chắc chắn, dễ sử dụng, giúp bạn vẽ hình đẹp và chuẩn xác. Đầu nhọn của một bên chân compass có lực cố định vừa phải, giúp compass đứng vững trên giấy khi quay. Bộ sản phẩm được đựng trong hộp thiếc chắc chắn, chất liệu cứng cáp, dễ dàng cất giữ và bảo quản. Bộ sản phẩm gồm: 2 compass, 1 chì gỗ, 1 gọt bút chì, 1 gôm, 1 thước kẻ 15 cm, 1 thước đo độ và 2 thước eke.  ', N'h1.jpg', 3, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (15, N'Bóp Viết Vải Polyester Stacom 2 Ngăn Hình Hoa Cúc PB-2011C - Màu Xanh Mint ', 70000, 70000, 20, N'Sản phẩm được làm bằng chất liệu vải Polyester chống thấm nước và dễ dàng tẩy rửa khi bị bẩn, bền đẹp dùng để sắp xếp các vật dụng, thuận tiện mang đi mọi lúc mọi nơi. Kích thước nhỏ gọn với hình dáng chữ nhật cùng họa tiết dễ thương. Thiết kế dây khóa kéo chắc chắn, trơn tru, đóng mở dễ dàng, thuận tiện khi sử dụng, độ bền cao. Sản phẩm có 2 ngăn, đủ để chứa tất cả những đồ dùng học tập cần thiết. ', N'h2.jpg', 3, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (17, N'Bộ 2 Hộp Thực Hành Toán Và Tiếng Việt Lớp 1 ', 240000, 240000, 20, N'Chất liệu: Nhựa Số Lượng/Bộ: 2 Trọng lượng (gr): 1100 Kích Thước Bao Bì: 25.5 x 18.5 x 8.5 cm ', N'h3.jpg', 3, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (20, N'Bảng Bộ 2 Mặt A4 - Queen BS-02 - Viền Cam\', 54000, 54000, 20, N'Sản phẩm bao gồm 1 bảng 2 mặt (1 mặt viết phấn, 1 mặt viết lông bảng) và 01 bút lông bảng có sẵn đồ bôi chuyên dành cho học sinh. Mặt viết phấn mịn và bám phấn giúp viết rõ nét, chữ viết đẹp. Mặt viết lông bảng màu trắng, mặt trơn dễ viết, dễ xóa. Bảng có kích thước A4 nhỏ gọn, tiện lợi. ', N'h4.jpg', 3, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (21, N'Bộ Lắp Ghép Mô Hình Kỹ Thuật (Lớp 4, Lớp 5)', 92000, 92000, 20, N'Bộ Lắp Ghép Mô Hình Kỹ Thuật là sản phẩm vô cùng tiện lợi, giúp các bé phát triển tư duy một cách tự nhiên nhất theo phương pháp vừa học vừa chơi. Bộ dụng cụ có màu sắc đa dạng, tạo sự hứng cho người dùng. Ngoài ra còn được làm bằng chất liệu an toàn, thiết kế dễ dàng tháo ráp và sử dụng ', N'h5.jpg', 3, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (22, N'Thước Bộ Eke - Keyroad KR971430 ', 19000, 19000, 20, N'Thước bộ eke là dụng cụ học tập phổ biến, được sử dụng ngay từ cấp tiểu học. Bộ thước gồm: 1 thước thẳng 15cm, 1 thước đo độ, 2 thước eke. Sản phẩm được làm bằng chất liệu nhựa cứng siêu bền, thân trong, in dãy vạch số chính xác và sắc nét. Túi đựng bộ thước tiện dụng xinh xắn, gọn nhẹ, dễ dàng cất vào cặp. Chất liệu nhựa dẻo chất lượng tốt, trong suốt, thẳng, độ bền cao, không tróc chữ số và đặc biệt có thể bẻ cong thoải mái mà không gãy.', N'h6.jpg', 3, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (23, N'Giấy Photo A4 70gsm - IK Plus (500 Tờ) ', 84000, 84000, 20, N'- Giấy in A4 của thương hiệu IK Plus được sản xuất từ những nguyên liệu sợi cây cao cấp với quy trình sản xuất theo công nghệ hiện đại và tiên tiến, không chứa chất gây độc hại và mùi khó chịu. - Được nhập khẩu từ Indonesia đạt tiêu chuẩn ISO 9001 và ISO 14001, nên độ ổn định của giấy luôn đảm bảo chất lượng và uy tín cho người tiêu dùng. Giấy không bị bụi, giúp bảo vệ sức khỏe người sử dụng,thân thiện với môi trường.', N'g1.jpg', 4, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (26, N'Giấy photo Double A A4/80 gsm ', 108000, 108000, 20, N'Giấy photo Double A A4/80 gsm với kích thước A4, thân thiện với môi trường và thích hợp với hầu hết các loại máy in phun, máy in laser, máy fax laser, máy photocopy. Sản phẩm thiết kế khổ giấy A4, thích hợp sử dụng làm giấy in, photo trong các văn phòng hoặc trong gia đình.  Giấy có bề dày tốt, bề mặt láng mịn, độ cản quang của giấy cao hơn do đó giảm hiện tượng nhìn thấu trang và cho phép sử dụng cả hai mặt giấy một cách toàn diện nhất. Chất liệu giấy an toàn, không chứa chất gây độc hại và mùi khó chịu, thân thiện với môi trường  ', N'g2.jpg', 4, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (27, N'Tập Doraemon Fly - A5 5 Ô Ly 96 Trang ĐL 120g/m2 - Campus NBADFL96 (Màu Ngẫu Nhiên) ', 27000, 27000, 20, N'- Chất liệu: + Giấy ngoại nhập chất lượng cao, bề mặt giấy trơn láng, viết đẹp, mượt mà. + Định lượng: 120g/m2. + Đặc biệt, tập 5 ô ly Doraemon Fly của Campus đã được cải tiến hoàn toàn mới với giấy tập đảm bảo 100% không lem cùng bìa tập trắng sáng hơn, dày hơn. - Thiết kế: + Bìa vở trẻ trung, bắt mắt và là thiết kế độc quyền của Campus. + Hệ thống đánh dấu bằng số thông minh cùng dòng kẻ (5 ô ly 2 x 2mm) in chính xác, rõ nét. ', N'g3.jpg', 4, 0, 0)
INSERT [dbo].[MATHANG] ([MaMH], [Ten], [GiaGoc], [GiaBan], [SoLuong], [MoTa], [HinhAnh], [MaDM], [LuotXem], [LuotMua]) VALUES (28, N'Sổ Diary Icon The Sun', 39000, 39000, 20, N'Sản phẩm sử dụng loại giấy láng, mịn, không lóa mắt, phù hợp để viết và vẽ rất dễ dàng. Sổ được sử dụng lò xo chắc chắn còn giúp bạn dễ dàng lật mở, gập sổ tiết kiệm không gian dễ dàng trong ghi chú, tốc ký. Giấy sổ là loại giấy chất lượng cao, độ bền cao, bề mặt giấy mềm mượt, viết êm, không thấm mực và không dễ quăn hay rách, nhàu. Cuốn sổ thích hợp dùng cho học sinh, sinh viên, nhân viên văn phòng  ', N'g4.jpg', 4, 0, 0)
SET IDENTITY_INSERT [dbo].[MATHANG] OFF
GO
ALTER TABLE [dbo].[DIACHI] ADD  DEFAULT ((0)) FOR [MACDINH]
GO
ALTER TABLE [dbo].[HOADON] ADD  DEFAULT (getdate()) FOR [NGAY]
GO
ALTER TABLE [dbo].[HOADON] ADD  DEFAULT ((0)) FOR [TRANGTHAI]
GO
ALTER TABLE [dbo].[MATHANG] ADD  DEFAULT ((0)) FOR [GiaGoc]
GO
ALTER TABLE [dbo].[MATHANG] ADD  DEFAULT ((0)) FOR [GiaBan]
GO
ALTER TABLE [dbo].[MATHANG] ADD  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[MATHANG] ADD  DEFAULT ((0)) FOR [LuotXem]
GO
ALTER TABLE [dbo].[MATHANG] ADD  DEFAULT ((0)) FOR [LuotMua]
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD FOREIGN KEY([MAHD])
REFERENCES [dbo].[HOADON] ([MAHD])
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD FOREIGN KEY([MAMH])
REFERENCES [dbo].[MATHANG] ([MaMH])
GO
ALTER TABLE [dbo].[DIACHI]  WITH CHECK ADD FOREIGN KEY([MAKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD FOREIGN KEY([MAKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[MATHANG]  WITH CHECK ADD FOREIGN KEY([MaDM])
REFERENCES [dbo].[DANHMUC] ([MaDM])
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD FOREIGN KEY([MACV])
REFERENCES [dbo].[CHUCVU] ([MACV])
GO
USE [master]
GO
ALTER DATABASE [ITShop] SET  READ_WRITE 
GO
