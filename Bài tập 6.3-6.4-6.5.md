BÀI 6.3: HỆ THỐNG QUẢN LÝ CÔNG VIỆC & TIẾN ĐỘ DỰ ÁN  (PROJECT & TASK MANAGEMENT) 

Mục tiêu  

 Xây dựng ứng dụng quản lý task, phân công công việc, giao diện  Kanban/List view.  

 Áp dụng kiến trúc 3-Tier phân lớp chuẩn (DTO, DAL, BUS, GUI) và truy  vấn dữ liệu theo trạng thái công việc.  

1\. Hướng dẫn chi tiết xây dựng CSDL (ProjectTaskDB)  

Tạo CSDL SQL Server bao gồm 4 bảng:  

1\. DuAn (Dự án) 

o MaDuAn (int, Primary Key, Identity)  

o TenDuAn (nvarchar(150), NOT NULL)  

o NgayBatDau (datetime)  

o NgayKetThuc (datetime)  

o TrangThai (nvarchar(50)) — Ví dụ: "Đang chạy", "Hoàn thành",  "Tạm dừng" 

2\. ThanhVien (Thành viên nhóm) 

o MaThanhVien (int, Primary Key, Identity)  

o HoTen (nvarchar(100), NOT NULL)  

o Email (varchar(100), Unique)  

o ChuyenMon (nvarchar(100))  

3\. CongViec (Công việc / Task) 

o MaTask (int, Primary Key, Identity)  

o MaDuAn (int, Foreign Key DuAn)  

o TenTask (nvarchar(200), NOT NULL)  

o MoTa (nvarchar(max))  

o HanNop (datetime)   
o DoUuTien (nvarchar(20)) — Ví dụ: "Cao", "Trung bình", "Thấp" 

o TrangThai (nvarchar(50)) — Ví dụ: "To Do", "In Progress",  "Review", "Done" 

4\. PhanCong (Phân công công việc) 

o MaTask (int, Foreign Key CongViec)  

o MaThanhVien (int, Foreign Key ThanhVien)  

o NgayGiao (datetime, Default GetDate())  

o Primary Key: (MaTask, MaThanhVien)  

2\. Yêu cầu chức năng hệ thống  

1\. Quản lý Dự án & Task (3-Tier Layered): 

o Thêm, sửa, xóa dự án và tạo công việc con thuộc dự án.  

o Giao việc (Phân công) cho một hoặc nhiều thành viên tham gia giải  quyết Task qua DTO/BUS/DAL.  

2\. Cập nhật Tiến độ & Chuyển Trạng thái Task: 

o Lọc danh sách Task theo từng Dự án và theo Trạng thái ("To Do",  "In Progress", "Done").  

o Chuyển trạng thái công việc (Ví dụ: Chuyển từ "In Progress" sang  "Done") → Cập nhật cơ sở dữ liệu qua lệnh Parameterized SQL  Update.  

3\. Cảnh báo Trễ hạn (Overdue) & Báo cáo: 

o Dùng SqlDataReader hiển thị các công việc đã quá HanNop mà  TrangThai \!= 'Done'.  

o Thống kê tỷ lệ % hoàn thành công việc của từng thành viên trong  dự án bằng ExecuteScalar hoặc các câu lệnh GROUP BY.  

BÀI 6.4: HỆ THỐNG QUẢN LÝ GARAGE VÀ BẢO BƯỠNG XE Ô TÔ  (GARAGE MANAGEMENT SYSTEM)  

Mục tiêu  

 Quản lý tiếp nhận xe, lập phiếu sửa chữa, thay thế phụ tùng và xuất hóa  đơn dịch vụ.   
 Đảm bảo tính nhất quán dữ liệu bằng SqlTransaction khi lập phiếu tiếp  nhận và trừ phụ tùng trong kho.  

1\. Hướng dẫn chi tiết xây dựng CSDL (GarageDB)  

Tạo CSDL SQL Server bao gồm 5 bảng:  

1\. KhachHang (Chủ xe) 

o MaKH (int, Primary Key, Identity)  

o TenKH (nvarchar(100), NOT NULL)  

o DienThoai (varchar(15), Unique)  

o DiaChi (nvarchar(200))  

2\. Xe (Thông tin ô tô) 

o BienSo (varchar(20), Primary Key)  

o MaKH (int, Foreign Key KhachHang)  

o HieuXe (nvarchar(50)) — Ví dụ: "Toyota", "Honda", "VinFast" o NamSanXuat (int)  

3\. PhuTung (Phụ tùng / Vật tư) 

o MaPhuTung (int, Primary Key, Identity)  

o TenPhuTung (nvarchar(150), NOT NULL)  

o DonGia (decimal(18,2))  

o SoLuongTon (int, Check \>= 0\)  

4\. PhieuSuaChua (Phiếu sửa chữa) 

o MaPhieu (int, Primary Key, Identity)  

o BienSo (varchar(20), Foreign Key Xe)  

o NgayNhan (datetime, Default GetDate())  

o NoiDungSua (nvarchar(max))  

o TongTien (decimal(18,2))  

o TrangThai (nvarchar(30)) — Ví dụ: "Đang sửa", "Đã hoàn thành",  "Đã thanh toán"  
5\. ChiTietSuaChua (Chi tiết phụ tùng & tiền công) 

o MaPhieu (int, Foreign Key PhieuSuaChua)  

o MaPhuTung (int, Foreign Key PhuTung)  

o SoLuong (int, Check \> 0\)  

o TienCong (decimal(18,2))  

o Primary Key: (MaPhieu, MaPhuTung)  

2\. Yêu cầu chức năng hệ thống  

1\. Tiếp nhận Xe & Quản lý Chủ xe (CRUD): 

o Tra cứu biển số xe bằng SqlParameter. Nếu xe mới → Nhập thông  tin Chủ xe và Biển số xe.  

o Quản lý danh mục Phụ tùng và cập nhật đơn giá, số lượng tồn kho.  2\. Lập Phiếu Sửa chữa & Thay thế Phụ tùng (SqlTransaction):  

o Lập phiếu sửa chữa cho xe → Thêm các dịch vụ/phụ tùng cần thay  thế kèm số lượng.  

o Khi xác nhận hoàn tất phiếu sửa chữa, chạy một SqlTransaction:  1\. Tạo bản ghi PhieuSuaChua.  

2\. Tạo các bản ghi ChiTietSuaChua.  

3\. Trừ số lượng phụ tùng tương ứng trong bảng PhuTung  

(SoLuongTon \-= SoLuong).  

4\. Nếu phụ tùng không đủ số lượng trong kho → 

trans.Rollback() và xuất thông báo.  

3\. Thanh toán & Xuất Hóa đơn: 

o Tính tổng chi phí \= Tổng (Số lượng phụ tùng × Đơn giá) \+ Tổng  tiền công.  

o Chuyển trạng thái phiếu sửa chữa sang "Đã thanh toán".  

BÀI 6.5: HỆ THỐNG QUẢN LÝ ĐẶT PHÒNG & DỊCH VỤ KARAOKE  (KARAOKE MANAGEMENT SYSTEM)  

Mục tiêu   
 Xây dựng sơ đồ phòng hát trực quan (Visual Room Grid) bằng  WinForms.  

 Quản lý tính giờ tự động, gọi dịch vụ ngắt kết nối (DataTable) và thanh  toán hóa đơn bằng SqlTransaction.  

1\. Hướng dẫn chi tiết xây dựng CSDL (KaraokeDB)  

Tạo CSDL SQL Server bao gồm 4 bảng:  

1\. PhongHat (Danh sách phòng hát) 

o MaPhong (int, Primary Key, Identity)  

o TenPhong (nvarchar(50), NOT NULL) — Ví dụ: "Phòng VIP 01",  "Phòng Thường 02" 

o LoaiPhong (nvarchar(30)) — Ví dụ: "VIP", "Thường" 

o GiaTheoGio (decimal(18,2))  

o TrangThai (nvarchar(20)) — Mặc định: "Trống", "Đang hát" 2\. DichVu (Mặt hàng dịch vụ / Đồ uống) 

o MaDV (int, Primary Key, Identity)  

o TenDV (nvarchar(100), NOT NULL) — Ví dụ: "Bia Heineken",  "Trái cây dĩa" 

o DonGia (decimal(18,2))  

o TonKho (int)  

3\. HoaDon (Hóa đơn sử dụng) 

o MaHD (int, Primary Key, Identity)  

o MaPhong (int, Foreign Key PhongHat)  

o GioVao (datetime, NOT NULL)  

o GioRa (datetime, Nullable)  

o TongTienPhong (decimal(18,2))  

o TongTienDichVu (decimal(18,2))  

o TrangThai (bit) — 0: Chưa thanh toán, 1: Đã thanh toán  
4\. ChiTietDichVu (Chi tiết dịch vụ gọi) 

o MaHD (int, Foreign Key HoaDon)  

o MaDV (int, Foreign Key DichVu)  

o SoLuong (int, Check \> 0\)  

o DonGia (decimal(18,2))  

o Primary Key: (MaHD, MaDV)  

2\. Yêu cầu chức năng hệ thống  

1\. Sơ đồ Phòng hát & Mở phòng (Visual Grid Layout): 

o Hiển thị danh sách các phòng hát dạng danh sách nút (Button  động). Phòng "Trống" màu xanh, phòng "Đang hát" màu đỏ.  

o Khi chọn phòng "Trống" → Bấm "Mở phòng" → Tạo HoaDon mới  ghi nhận GioVao \= GetDate(), đổi TrangThai phòng sang "Đang  hát".  

2\. Gọi Dịch vụ / Đồ uống (Disconnected Model):  

o Chọn phòng đang hát → Hiển thị danh sách dịch vụ đã đặt bằng  SqlDataAdapter \+ DataTable trên DataGridView.  

o Thêm đồ uống/dịch vụ vào phòng.  

3\. Tính Tiền & Thanh Toán (SqlTransaction):  

o Khi bấm "Thanh toán":  

 Hệ thống tự động tính thời gian hát \= GioRa \- GioVao.  

 TongTienPhong \= (Số giờ) \* GiaTheoGio.  

 Mở một SqlTransaction thực hiện:  

1\. Cập nhật GioRa, TongTienPhong, TongTienDichVu,  

TrangThai \= 1 cho HoaDon.  

2\. Trừ TonKho tương ứng trong bảng DichVu.  

3\. Cập nhật trạng thái PhongHat về "Trống".  

4\. Gọi trans.Commit().   
YÊU CẦU NỘP BÀI TRÊN GITHUB & CẤU TRÚC README.MD  

Sinh viên khởi tạo Repository công khai (Public) trên GitHub và tổ chức mã  nguồn theo quy chuẩn sau:  

1\. Cấu trúc thư mục Solution C\# (Chuẩn 3-Tier Architecture)  ├── Database/  

│ └── ScriptDB.sql \# Script khởi tạo CSDL SQL Server và chèn dữ  liệu mẫu  

├── ProjectName/  

│ ├── DTO/ \# Chứa các lớp Data Transfer Object  

│ ├── DAL/ \# Chứa các lớp Data Access Layer (Truy vấn  ADO.NET)  

│ ├── BUS/ \# Chứa các lớp Business Logic Layer (Xử lý nghiệp  vụ)  

│ ├── GUI/ \# Chứa Form giao diện WinForms (Presentation  Layer)  

│ └── App.config / Program.cs  

├── README.md \# Báo cáo chi tiết bài tập  

└── ProjectName.sln  

2\. Yêu cầu nội dung File README.md  

File README.md đặt ở thư mục gốc của repository phải có đầy đủ các nội  dung sau:  

MỤC 1: THÔNG TIN SINH VIÊN  

 Họ và tên sinh viên: \[Điền họ tên\]  

 Mã sinh viên: \[Điền MSV\]  

 Lớp / Khóa: \[Điền tên lớp\]   
MỤC 2: HÌNH ẢNH KẾT QUẢ CÁC MÀN HÌNH CHỨC NĂNG  

(Sinh viên chụp ảnh màn hình ứng dụng đang chạy thực tế và nhúng vào file  Markdown theo cú pháp \!\[Tên ảnh\](đường-dẫn-ảnh)) 

1\. Màn hình Chính / Tổng quan hệ thống:\!\[Màn hình  

chính\](images/01\_main\_screen.png)  

2\. Màn hình Quản lý Danh mục (CRUD \- Thêm/Sửa/Xóa):\!\[Màn hình  CRUD\](images/02\_crud\_screen.png)  

3\. Màn hình Thực thi Nghiệp vụ chính (Lập Hóa đơn / Bán hàng /  Chấm công / Đặt phòng):\!\[Màn hình Nghiệp  

vụ\](images/03\_transaction\_screen.png)  

4\. Màn hình Báo cáo \- Thống kê:\!\[Màn hình Thống  

kê\](images/04\_report\_screen.png)  

5\. Minh họa An toàn Dữ liệu (Phòng chống SQL Injection / Báo lỗi  Transaction Rollback):\!\[Minh họa Xử lý  

lỗi\](images/05\_error\_handling.png) 