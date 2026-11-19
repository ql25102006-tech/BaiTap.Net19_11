# Báo Cáo Thực Hành Bài Tập Ngày 19/11 (Bài 6.3 - 6.4 - 6.5)

## MỤC 1: THÔNG TIN SINH VIÊN
- **Họ và tên sinh viên:** Lê Ngọc Quyền
- **Mã sinh viên:** 24810310259
- **Lớp / Khóa:** D19CNPM3
- **Môn học:** Lập trình C# / Windows Forms (ADO.NET, 3-Tier Architecture, SqlTransaction & Disconnected Model)

---

## DANH SÁCH BÀI TẬP ĐÃ HOÀN THÀNH

1. **[Bài 6.3: Hệ thống Quản lý Công việc & Tiến độ Dự án (Project & Task Management)](./Bai6_3_ProjectTaskManagement)**
   - Chuẩn 3-Tier (`DTO`, `DAL`, `BUS`, `GUI`), `Database/ScriptDB.sql` (`ProjectTaskDB`), `App.config` (`localhost\QUYEN`), 5 ảnh minh họa trong `images/`.
   - Quản lý dự án & công việc, phân công nhiều thành viên, lọc/chuyển trạng thái bằng `SqlParameter`, cảnh báo quá hạn (`SqlDataReader`) và thống kê `%` hoàn thành (`GROUP BY`).

2. **[Bài 6.4: Hệ thống Quản lý Garage & Bảo dưỡng Xe Ô tô (Garage Management System)](./Bai6_4_GarageManagement)**
   - Chuẩn 3-Tier (`DTO`, `DAL`, `BUS`, `GUI`), `Database/ScriptDB.sql` (`GarageDB`), `App.config` (`localhost\QUYEN`), 5 ảnh minh họa trong `images/`.
   - Tra cứu biển số xe, quản lý kho phụ tùng, lập phiếu sửa chữa & trừ tồn kho bằng `SqlTransaction` (Rollback nếu thiếu phụ tùng), tính tổng tiền & thanh toán.

3. **[Bài 6.5: Hệ thống Quản lý Đặt phòng & Dịch vụ Karaoke (Karaoke Management System)](./Bai6_5_KaraokeManagement)**
   - Chuẩn 3-Tier (`DTO`, `DAL`, `BUS`, `GUI`), `Database/ScriptDB.sql` (`KaraokeDB`), `App.config` (`localhost\QUYEN`), 5 ảnh minh họa trong `images/`.
   - Sơ đồ phòng hát trực quan (Trống: xanh / Đang hát: đỏ), gọi dịch vụ bằng `SqlDataAdapter` + `DataTable`, tính giờ tự động và thanh toán bằng `SqlTransaction`.
