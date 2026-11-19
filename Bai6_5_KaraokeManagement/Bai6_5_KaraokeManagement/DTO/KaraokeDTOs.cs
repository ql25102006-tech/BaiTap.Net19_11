using System;

namespace Bai6_5_KaraokeManagement.DTO
{
    public class PhongHatDTO
    {
        public int MaPhong { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public string LoaiPhong { get; set; } = "Thường";
        public decimal GiaTheoGio { get; set; }
        public string TrangThai { get; set; } = "Trống";
    }

    public class DichVuDTO
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public int TonKho { get; set; }
    }

    public class HoaDonDTO
    {
        public int MaHD { get; set; }
        public int MaPhong { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public DateTime GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public decimal TongTienPhong { get; set; }
        public decimal TongTienDichVu { get; set; }
        public decimal TongCong => TongTienPhong + TongTienDichVu;
        public bool TrangThai { get; set; }
    }
}
