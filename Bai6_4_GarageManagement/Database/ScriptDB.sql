-- =============================================
-- Script khởi tạo CSDL: GarageDB
-- Bài 6.4: Hệ thống Quản lý Garage & Bảo dưỡng Xe Ô tô
-- =============================================

IF DB_ID('GarageDB') IS NULL
BEGIN
    CREATE DATABASE GarageDB;
END
GO

USE GarageDB;
GO

IF OBJECT_ID('ChiTietSuaChua', 'U') IS NOT NULL DROP TABLE ChiTietSuaChua;
IF OBJECT_ID('PhieuSuaChua', 'U') IS NOT NULL DROP TABLE PhieuSuaChua;
IF OBJECT_ID('PhuTung', 'U') IS NOT NULL DROP TABLE PhuTung;
IF OBJECT_ID('Xe', 'U') IS NOT NULL DROP TABLE Xe;
IF OBJECT_ID('KhachHang', 'U') IS NOT NULL DROP TABLE KhachHang;
GO

-- 1. Bảng KhachHang
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) UNIQUE,
    DiaChi NVARCHAR(200)
);
GO

-- 2. Bảng Xe
CREATE TABLE Xe (
    BienSo VARCHAR(20) PRIMARY KEY,
    MaKH INT NOT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    HieuXe NVARCHAR(50),
    NamSanXuat INT
);
GO

-- 3. Bảng PhuTung
CREATE TABLE PhuTung (
    MaPhuTung INT IDENTITY(1,1) PRIMARY KEY,
    TenPhuTung NVARCHAR(150) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia > 0),
    SoLuongTon INT NOT NULL CHECK (SoLuongTon >= 0)
);
GO

-- 4. Bảng PhieuSuaChua
CREATE TABLE PhieuSuaChua (
    MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
    BienSo VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Xe(BienSo),
    NgayNhan DATETIME DEFAULT GETDATE(),
    NoiDungSua NVARCHAR(MAX),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) DEFAULT N'Đang sửa'
);
GO

-- 5. Bảng ChiTietSuaChua
CREATE TABLE ChiTietSuaChua (
    MaPhieu INT NOT NULL FOREIGN KEY REFERENCES PhieuSuaChua(MaPhieu),
    MaPhuTung INT NOT NULL FOREIGN KEY REFERENCES PhuTung(MaPhuTung),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    TienCong DECIMAL(18,2) NOT NULL DEFAULT 0,
    PRIMARY KEY (MaPhieu, MaPhuTung)
);
GO

-- =============================================
-- DỮ LIỆU MẪU (SAMPLE DATA)
-- =============================================

INSERT INTO KhachHang (TenKH, DienThoai, DiaChi) VALUES
(N'Lê Ngọc Quyền', '0901234561', N'235 Hoàng Quốc Việt, Cầu Giấy, Hà Nội'),
(N'Nguyễn Hoàng Nam', '0912345662', N'18 Phạm Hùng, Nam Từ Liêm, Hà Nội'),
(N'Trần Thị Lan Anh', '0987654363', N'55 Giải Phóng, Hai Bà Trưng, Hà Nội');
GO

INSERT INTO Xe (BienSo, MaKH, HieuXe, NamSanXuat) VALUES
('30H-123.45', 1, N'Toyota', 2022),
('30K-678.90', 2, N'VinFast', 2024),
('29A-555.88', 3, N'Honda', 2021);
GO

INSERT INTO PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES
(N'Dầu nhớt động cơ Castrol 4L', 650000, 40),
(N'Lọc dầu động cơ chính hãng', 220000, 35),
(N'Má phanh trước (Bộ)', 1250000, 15),
(N'Bugi Iridium (Chiếc)', 320000, 50),
(N'Lọc gió điều hòa', 280000, 25),
(N'Ắc quy GS 12V 60Ah', 1850000, 10);
GO

INSERT INTO PhieuSuaChua (BienSo, NgayNhan, NoiDungSua, TongTien, TrangThai) VALUES
('30H-123.45', DATEADD(DAY, -1, GETDATE()), N'Bảo dưỡng định kỳ 20.000km, thay dầu và lọc dầu', 1170000, N'Đã hoàn thành'),
('30K-678.90', DATEADD(DAY, -3, GETDATE()), N'Thay má phanh trước và vệ sinh hệ thống phanh', 1650000, N'Đã thanh toán');
GO

INSERT INTO ChiTietSuaChua (MaPhieu, MaPhuTung, SoLuong, TienCong) VALUES
(1, 1, 1, 150000),
(1, 2, 1, 150000),
(2, 3, 1, 400000);
GO
