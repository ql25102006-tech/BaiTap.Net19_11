using System;
using System.Collections.Generic;
using System.Data;
using Bai6_5_KaraokeManagement.DAL;
using Bai6_5_KaraokeManagement.DTO;

namespace Bai6_5_KaraokeManagement.BUS
{
    public class KaraokeBUS
    {
        private readonly KaraokeDAL dal = new KaraokeDAL();

        public List<PhongHatDTO> GetAllPhong() => dal.GetAllPhong();
        public List<DichVuDTO> GetAllDichVu() => dal.GetAllDichVu();

        public bool InsertDichVu(DichVuDTO dv)
        {
            if (string.IsNullOrWhiteSpace(dv.TenDV)) throw new ArgumentException("Tên dịch vụ không được để trống!");
            if (dv.DonGia <= 0) throw new ArgumentException("Đơn giá phải lớn hơn 0!");
            if (dv.TonKho < 0) throw new ArgumentException("Tồn kho không được âm!");
            return dal.InsertDichVu(dv);
        }

        public bool UpdateDichVu(DichVuDTO dv)
        {
            if (string.IsNullOrWhiteSpace(dv.TenDV)) throw new ArgumentException("Tên dịch vụ không được để trống!");
            if (dv.DonGia <= 0) throw new ArgumentException("Đơn giá phải lớn hơn 0!");
            if (dv.TonKho < 0) throw new ArgumentException("Tồn kho không được âm!");
            return dal.UpdateDichVu(dv);
        }

        public bool DeleteDichVu(int maDV) => dal.DeleteDichVu(maDV);

        public int MoPhong(int maPhong) => dal.MoPhong(maPhong);
        public HoaDonDTO? GetActiveHoaDon(int maPhong) => dal.GetActiveHoaDon(maPhong);
        public DataTable GetChiTietDichVuTable(int maHD) => dal.GetChiTietDichVuTable(maHD);

        public void ThemDichVuVaoPhong(int maHD, int maDV, int soLuong, decimal donGia)
        {
            if (soLuong <= 0) throw new ArgumentException("Số lượng phải lớn hơn 0!");
            dal.ThemDichVuVaoPhong(maHD, maDV, soLuong, donGia);
        }

        public (decimal TienPhong, decimal TienDichVu, double SoGio) ThanhToanPhong(int maPhong)
            => dal.ThanhToanPhong(maPhong);

        public List<HoaDonDTO> GetLichSuHoaDon() => dal.GetLichSuHoaDon();
    }
}
