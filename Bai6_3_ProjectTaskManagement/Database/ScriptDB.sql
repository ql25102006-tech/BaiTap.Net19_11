-- =============================================
-- Script khởi tạo CSDL: ProjectTaskDB
-- Bài 6.3: Hệ thống Quản lý Công việc & Tiến độ Dự án
-- =============================================

IF DB_ID('ProjectTaskDB') IS NULL
BEGIN
    CREATE DATABASE ProjectTaskDB;
END
GO

USE ProjectTaskDB;
GO

IF OBJECT_ID('PhanCong', 'U') IS NOT NULL DROP TABLE PhanCong;
IF OBJECT_ID('CongViec', 'U') IS NOT NULL DROP TABLE CongViec;
IF OBJECT_ID('ThanhVien', 'U') IS NOT NULL DROP TABLE ThanhVien;
IF OBJECT_ID('DuAn', 'U') IS NOT NULL DROP TABLE DuAn;
GO

-- 1. Bảng DuAn
CREATE TABLE DuAn (
    MaDuAn INT IDENTITY(1,1) PRIMARY KEY,
    TenDuAn NVARCHAR(150) NOT NULL,
    NgayBatDau DATETIME,
    NgayKetThuc DATETIME,
    TrangThai NVARCHAR(50) DEFAULT N'Đang chạy'
);
GO

-- 2. Bảng ThanhVien
CREATE TABLE ThanhVien (
    MaThanhVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE,
    ChuyenMon NVARCHAR(100)
);
GO

-- 3. Bảng CongViec
CREATE TABLE CongViec (
    MaTask INT IDENTITY(1,1) PRIMARY KEY,
    MaDuAn INT NOT NULL FOREIGN KEY REFERENCES DuAn(MaDuAn),
    TenTask NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX),
    HanNop DATETIME,
    DoUuTien NVARCHAR(20) DEFAULT N'Trung bình',
    TrangThai NVARCHAR(50) DEFAULT N'To Do'
);
GO

-- 4. Bảng PhanCong
CREATE TABLE PhanCong (
    MaTask INT NOT NULL FOREIGN KEY REFERENCES CongViec(MaTask),
    MaThanhVien INT NOT NULL FOREIGN KEY REFERENCES ThanhVien(MaThanhVien),
    NgayGiao DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (MaTask, MaThanhVien)
);
GO

-- =============================================
-- DỮ LIỆU MẪU (SAMPLE DATA)
-- =============================================

INSERT INTO DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) VALUES
(N'Hệ thống Quản lý Bán hàng E-Commerce', '2026-10-01', '2026-12-31', N'Đang chạy'),
(N'Ứng dụng Mobile Đặt lịch Khám bệnh', '2026-09-15', '2026-11-30', N'Đang chạy'),
(N'Cổng thông tin Sinh viên EPU', '2026-06-01', '2026-10-15', N'Hoàn thành');
GO

INSERT INTO ThanhVien (HoTen, Email, ChuyenMon) VALUES
(N'Lê Ngọc Quyền', 'quyen.le@company.vn', N'Backend .NET / SQL Server'),
(N'Nguyễn Văn An', 'an.nguyen@company.vn', N'Frontend / UI-UX'),
(N'Trần Thị Bích', 'bich.tran@company.vn', N'QA / Tester'),
(N'Phạm Minh Đức', 'duc.pham@company.vn', N'Mobile Flutter');
GO

INSERT INTO CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai) VALUES
(1, N'Thiết kế CSDL SQL Server 3-Tier', N'Tạo bảng, khóa ngoại và dữ liệu mẫu', DATEADD(DAY, -5, GETDATE()), N'Cao', N'Done'),
(1, N'Xây dựng module Giỏ hàng & Thanh toán', N'Áp dụng SqlTransaction trừ tồn kho', DATEADD(DAY, -2, GETDATE()), N'Cao', N'In Progress'),
(1, N'Viết báo cáo thống kê Doanh thu', N'Dùng ExecuteScalar và SqlDataReader', DATEADD(DAY, 5, GETDATE()), N'Trung bình', N'To Do'),
(2, N'Thiết kế màn hình Đăng nhập & OTP', N'Xác thực số điện thoại bệnh nhân', DATEADD(DAY, -3, GETDATE()), N'Cao', N'Review'),
(2, N'Kiểm thử tích hợp API đặt lịch', N'Chạy test case đồng thời', DATEADD(DAY, 4, GETDATE()), N'Trung bình', N'To Do'),
(3, N'Triển khai hệ thống lên Server', N'Cấu hình IIS và backup định kỳ', DATEADD(DAY, -15, GETDATE()), N'Cao', N'Done');
GO

INSERT INTO PhanCong (MaTask, MaThanhVien) VALUES
(1, 1),
(2, 1),
(2, 2),
(3, 1),
(4, 2),
(4, 4),
(5, 3),
(6, 1),
(6, 3);
GO
