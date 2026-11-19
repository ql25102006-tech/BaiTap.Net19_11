using System;
using System.Collections.Generic;
using Bai6_3_ProjectTaskManagement.DAL;
using Bai6_3_ProjectTaskManagement.DTO;

namespace Bai6_3_ProjectTaskManagement.BUS
{
    public class ProjectTaskBUS
    {
        private readonly DuAnDAL duAnDAL = new DuAnDAL();
        private readonly ThanhVienDAL tvDAL = new ThanhVienDAL();
        private readonly CongViecDAL cvDAL = new CongViecDAL();

        public List<DuAnDTO> GetAllDuAn() => duAnDAL.GetAll();
        public List<ThanhVienDTO> GetAllThanhVien() => tvDAL.GetAll();

        public bool InsertDuAn(DuAnDTO da)
        {
            if (string.IsNullOrWhiteSpace(da.TenDuAn))
                throw new ArgumentException("Tên dự án không được để trống!");
            if (da.NgayKetThuc < da.NgayBatDau)
                throw new ArgumentException("Ngày kết thúc không được trước ngày bắt đầu!");
            return duAnDAL.Insert(da);
        }

        public bool UpdateDuAn(DuAnDTO da)
        {
            if (string.IsNullOrWhiteSpace(da.TenDuAn))
                throw new ArgumentException("Tên dự án không được để trống!");
            if (da.NgayKetThuc < da.NgayBatDau)
                throw new ArgumentException("Ngày kết thúc không được trước ngày bắt đầu!");
            return duAnDAL.Update(da);
        }

        public bool DeleteDuAn(int maDuAn) => duAnDAL.Delete(maDuAn);

        public List<CongViecDTO> GetTasks(int? maDuAn, string? trangThai, string? keyword)
            => cvDAL.GetByFilter(maDuAn, trangThai, keyword);

        public int InsertTask(CongViecDTO task, List<int> memberIds)
        {
            if (task.MaDuAn <= 0)
                throw new ArgumentException("Vui lòng chọn dự án cho công việc!");
            if (string.IsNullOrWhiteSpace(task.TenTask))
                throw new ArgumentException("Tên công việc (Task) không được để trống!");
            return cvDAL.Insert(task, memberIds);
        }

        public bool UpdateTaskStatus(int maTask, string newStatus)
        {
            if (maTask <= 0) throw new ArgumentException("Mã công việc không hợp lệ!");
            return cvDAL.UpdateStatus(maTask, newStatus);
        }

        public bool DeleteTask(int maTask) => cvDAL.Delete(maTask);

        public List<CongViecDTO> GetOverdueTasks() => cvDAL.GetOverdueTasks();

        public List<ThongKeThanhVienDTO> GetThongKeThanhVien(int? maDuAn) => cvDAL.GetThongKeThanhVien(maDuAn);
    }
}
