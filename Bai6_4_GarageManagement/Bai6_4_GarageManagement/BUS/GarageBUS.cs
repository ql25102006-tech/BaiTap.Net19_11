using System;
using System.Collections.Generic;
using Bai6_4_GarageManagement.DAL;
using Bai6_4_GarageManagement.DTO;

namespace Bai6_4_GarageManagement.BUS
{
    public class GarageBUS
    {
        private readonly GarageDAL dal = new GarageDAL();

        public List<XeDTO> GetAllXe(string? filter = null) => dal.GetAllXe(filter);

        public bool ThemXeVaChuXe(KhachHangDTO kh, XeDTO xe)
        {
            if (string.IsNullOrWhiteSpace(kh.TenKH)) throw new ArgumentException("Tên chủ xe không được để trống!");
            if (string.IsNullOrWhiteSpace(xe.BienSo)) throw new ArgumentException("Biển số xe không được để trống!");
            return dal.ThemXeVaChuXe(kh, xe);
        }

        public List<PhuTungDTO> GetAllPhuTung() => dal.GetAllPhuTung();

        public bool InsertPhuTung(PhuTungDTO pt)
        {
            if (string.IsNullOrWhiteSpace(pt.TenPhuTung)) throw new ArgumentException("Tên phụ tùng không được để trống!");
            if (pt.DonGia <= 0) throw new ArgumentException("Đơn giá phụ tùng phải lớn hơn 0!");
            if (pt.SoLuongTon < 0) throw new ArgumentException("Số lượng tồn không được âm!");
            return dal.InsertPhuTung(pt);
        }

        public bool UpdatePhuTung(PhuTungDTO pt)
        {
            if (string.IsNullOrWhiteSpace(pt.TenPhuTung)) throw new ArgumentException("Tên phụ tùng không được để trống!");
            if (pt.DonGia <= 0) throw new ArgumentException("Đơn giá phải lớn hơn 0!");
            if (pt.SoLuongTon < 0) throw new ArgumentException("Số lượng tồn không được âm!");
            return dal.UpdatePhuTung(pt);
        }

        public bool DeletePhuTung(int maPT) => dal.DeletePhuTung(maPT);

        public int LapPhieuSuaChua(PhieuSuaChuaDTO phieu)
        {
            if (string.IsNullOrWhiteSpace(phieu.BienSo)) throw new ArgumentException("Vui lòng chọn biển số xe!");
            if (phieu.ChiTiet == null || phieu.ChiTiet.Count == 0) throw new ArgumentException("Vui lòng thêm ít nhất 1 phụ tùng/dịch vụ sửa chữa!");
            return dal.LapPhieuSuaChua(phieu);
        }

        public List<PhieuSuaChuaDTO> GetAllPhieuSuaChua() => dal.GetAllPhieuSuaChua();

        public bool ThanhToanPhieu(int maPhieu) => dal.ThanhToanPhieu(maPhieu);
    }
}
