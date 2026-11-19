-- =============================================
-- Script khởi tạo CSDL: KaraokeDB
-- Bài 6.5: Hệ thống Quản lý Đặt phòng & Dịch vụ Karaoke
-- =============================================

IF DB_ID('KaraokeDB') IS NULL
BEGIN
    CREATE DATABASE KaraokeDB;
END
GO

USE KaraokeDB;
GO

IF OBJECT_ID('ChiTietDichVu', 'U') IS NOT NULL DROP TABLE ChiTietDichVu;
IF OBJECT_ID('HoaDon', 'U') IS NOT NULL DROP TABLE HoaDon;
IF OBJECT_ID('DichVu', 'U') IS NOT NULL DROP TABLE DichVu;
IF OBJECT_ID('PhongHat', 'U') IS NOT NULL DROP TABLE PhongHat;
GO

-- 1. Bảng PhongHat
CREATE TABLE PhongHat (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(50) NOT NULL,
    LoaiPhong NVARCHAR(30) DEFAULT N'Thường',
    GiaTheoGio DECIMAL(18,2) NOT NULL CHECK (GiaTheoGio > 0),
    TrangThai NVARCHAR(20) DEFAULT N'Trống'
);
GO

-- 2. Bảng DichVu
CREATE TABLE DichVu (
    MaDV INT IDENTITY(1,1) PRIMARY KEY,
    TenDV NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia > 0),
    TonKho INT NOT NULL CHECK (TonKho >= 0)
);
GO

-- 3. Bảng HoaDon
CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong INT NOT NULL FOREIGN KEY REFERENCES PhongHat(MaPhong),
    GioVao DATETIME NOT NULL DEFAULT GETDATE(),
    GioRa DATETIME NULL,
    TongTienPhong DECIMAL(18,2) DEFAULT 0,
    TongTienDichVu DECIMAL(18,2) DEFAULT 0,
    TrangThai BIT DEFAULT 0 -- 0: Chưa thanh toán, 1: Đã thanh toán
);
GO

-- 4. Bảng ChiTietDichVu
CREATE TABLE ChiTietDichVu (
    MaHD INT NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD),
    MaDV INT NOT NULL FOREIGN KEY REFERENCES DichVu(MaDV),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaHD, MaDV)
);
GO

-- =============================================
-- DỮ LIỆU MẪU (SAMPLE DATA)
-- =============================================

INSERT INTO PhongHat (TenPhong, LoaiPhong, GiaTheoGio, TrangThai) VALUES
(N'Phòng VIP 01', N'VIP', 350000, N'Đang hát'),
(N'Phòng VIP 02', N'VIP', 350000, N'Trống'),
(N'Phòng VIP 03', N'VIP', 400000, N'Trống'),
(N'Phòng Thường 01', N'Thường', 180000, N'Đang hát'),
(N'Phòng Thường 02', N'Thường', 180000, N'Trống'),
(N'Phòng Thường 03', N'Thường', 200000, N'Trống');
GO

INSERT INTO DichVu (TenDV, DonGia, TonKho) VALUES
(N'Bia Heineken (Lon)', 35000, 200),
(N'Bia Tiger Bạc (Lon)', 28000, 180),
(N'Nước suối Aquafina', 15000, 150),
(N'Trái cây dĩa lớn', 180000, 30),
(N'Khô bò xé cay', 95000, 40),
(N'Khăn lạnh', 5000, 500);
GO

-- Hóa đơn đang mở cho Phòng VIP 01 (MaPhong=1) và Phòng Thường 01 (MaPhong=4)
INSERT INTO HoaDon (MaPhong, GioVao, GioRa, TongTienPhong, TongTienDichVu, TrangThai) VALUES
(1, DATEADD(MINUTE, -90, GETDATE()), NULL, 0, 530000, 0),
(4, DATEADD(MINUTE, -60, GETDATE()), NULL, 0, 175000, 0),
(2, DATEADD(HOUR, -5, GETDATE()), DATEADD(HOUR, -3, GETDATE()), 700000, 390000, 1);
GO

INSERT INTO ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia) VALUES
(1, 1, 10, 35000),
(1, 4, 1, 180000),
(2, 2, 5, 28000),
(2, 3, 2, 15000),
(2, 6, 1, 5000),
(3, 1, 6, 35000),
(3, 4, 1, 180000);
GO
