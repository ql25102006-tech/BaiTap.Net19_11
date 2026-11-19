using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Bai6_3_ProjectTaskManagement.DTO;

namespace Bai6_3_ProjectTaskManagement.DAL
{
    public class DuAnDAL
    {
        public List<DuAnDTO> GetAll()
        {
            var list = new List<DuAnDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, TrangThai FROM DuAn ORDER BY MaDuAn";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new DuAnDTO
                            {
                                MaDuAn = r.GetInt32(0),
                                TenDuAn = r.GetString(1),
                                NgayBatDau = r.IsDBNull(2) ? DateTime.Today : r.GetDateTime(2),
                                NgayKetThuc = r.IsDBNull(3) ? DateTime.Today : r.GetDateTime(3),
                                TrangThai = r.IsDBNull(4) ? "Đang chạy" : r.GetString(4)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool Insert(DuAnDTO da)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "INSERT INTO DuAn (TenDuAn, NgayBatDau, NgayKetThuc, TrangThai) VALUES (@TenDuAn, @NgayBatDau, @NgayKetThuc, @TrangThai)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDuAn", da.TenDuAn);
                    cmd.Parameters.AddWithValue("@NgayBatDau", da.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", da.NgayKetThuc);
                    cmd.Parameters.AddWithValue("@TrangThai", da.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(DuAnDTO da)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "UPDATE DuAn SET TenDuAn = @TenDuAn, NgayBatDau = @NgayBatDau, NgayKetThuc = @NgayKetThuc, TrangThai = @TrangThai WHERE MaDuAn = @MaDuAn";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDuAn", da.MaDuAn);
                    cmd.Parameters.AddWithValue("@TenDuAn", da.TenDuAn);
                    cmd.Parameters.AddWithValue("@NgayBatDau", da.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", da.NgayKetThuc);
                    cmd.Parameters.AddWithValue("@TrangThai", da.TrangThai);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int maDuAn)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "DELETE FROM DuAn WHERE MaDuAn = @MaDuAn";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDuAn", maDuAn);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }

    public class ThanhVienDAL
    {
        public List<ThanhVienDTO> GetAll()
        {
            var list = new List<ThanhVienDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaThanhVien, HoTen, Email, ChuyenMon FROM ThanhVien ORDER BY MaThanhVien";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new ThanhVienDTO
                            {
                                MaThanhVien = r.GetInt32(0),
                                HoTen = r.GetString(1),
                                Email = r.IsDBNull(2) ? string.Empty : r.GetString(2),
                                ChuyenMon = r.IsDBNull(3) ? string.Empty : r.GetString(3)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
