using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Bai6_3_ProjectTaskManagement.DTO;

namespace Bai6_3_ProjectTaskManagement.DAL
{
    public class CongViecDAL
    {
        public List<CongViecDTO> GetByFilter(int? maDuAn, string? trangThai, string? keyword)
        {
            var list = new List<CongViecDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT cv.MaTask, cv.MaDuAn, da.TenDuAn, cv.TenTask, cv.MoTa, cv.HanNop, cv.DoUuTien, cv.TrangThai,
                           ISNULL((SELECT STRING_AGG(tv.HoTen, ', ')
                                   FROM PhanCong pc
                                   INNER JOIN ThanhVien tv ON pc.MaThanhVien = tv.MaThanhVien
                                   WHERE pc.MaTask = cv.MaTask), N'Chưa giao') AS NguoiPhuTrach
                    FROM CongViec cv
                    INNER JOIN DuAn da ON cv.MaDuAn = da.MaDuAn
                    WHERE (@MaDuAn IS NULL OR cv.MaDuAn = @MaDuAn)
                      AND (@TrangThai = '' OR cv.TrangThai = @TrangThai)
                      AND (@Keyword = '' OR cv.TenTask LIKE @KeywordPattern OR cv.MoTa LIKE @KeywordPattern)
                    ORDER BY cv.HanNop ASC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string tt = trangThai?.Trim() ?? string.Empty;
                    string kw = keyword?.Trim() ?? string.Empty;
                    cmd.Parameters.AddWithValue("@MaDuAn", (object?)maDuAn ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", tt);
                    cmd.Parameters.AddWithValue("@Keyword", kw);
                    cmd.Parameters.AddWithValue("@KeywordPattern", "%" + kw + "%");

                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new CongViecDTO
                            {
                                MaTask = r.GetInt32(0),
                                MaDuAn = r.GetInt32(1),
                                TenDuAn = r.GetString(2),
                                TenTask = r.GetString(3),
                                MoTa = r.IsDBNull(4) ? string.Empty : r.GetString(4),
                                HanNop = r.IsDBNull(5) ? DateTime.Today : r.GetDateTime(5),
                                DoUuTien = r.IsDBNull(6) ? "Trung bình" : r.GetString(6),
                                TrangThai = r.IsDBNull(7) ? "To Do" : r.GetString(7),
                                NguoiPhuTrach = r.IsDBNull(8) ? string.Empty : r.GetString(8)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public int Insert(CongViecDTO task, List<int> memberIds)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                            INSERT INTO CongViec (MaDuAn, TenTask, MoTa, HanNop, DoUuTien, TrangThai)
                            OUTPUT INSERTED.MaTask
                            VALUES (@MaDuAn, @TenTask, @MoTa, @HanNop, @DoUuTien, @TrangThai)";
                        int newTaskId;
                        using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaDuAn", task.MaDuAn);
                            cmd.Parameters.AddWithValue("@TenTask", task.TenTask);
                            cmd.Parameters.AddWithValue("@MoTa", task.MoTa);
                            cmd.Parameters.AddWithValue("@HanNop", task.HanNop);
                            cmd.Parameters.AddWithValue("@DoUuTien", task.DoUuTien);
                            cmd.Parameters.AddWithValue("@TrangThai", task.TrangThai);
                            newTaskId = (int)cmd.ExecuteScalar();
                        }

                        if (memberIds != null)
                        {
                            string sqlPC = "INSERT INTO PhanCong (MaTask, MaThanhVien, NgayGiao) VALUES (@MaTask, @MaTV, GETDATE())";
                            foreach (int maTV in memberIds)
                            {
                                using (SqlCommand cmdPC = new SqlCommand(sqlPC, conn, trans))
                                {
                                    cmdPC.Parameters.AddWithValue("@MaTask", newTaskId);
                                    cmdPC.Parameters.AddWithValue("@MaTV", maTV);
                                    cmdPC.ExecuteNonQuery();
                                }
                            }
                        }

                        trans.Commit();
                        return newTaskId;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool UpdateStatus(int maTask, string newStatus)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "UPDATE CongViec SET TrangThai = @TrangThai WHERE MaTask = @MaTask";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TrangThai", newStatus);
                    cmd.Parameters.AddWithValue("@MaTask", maTask);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int maTask)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdPC = new SqlCommand("DELETE FROM PhanCong WHERE MaTask = @MaTask", conn, trans))
                        {
                            cmdPC.Parameters.AddWithValue("@MaTask", maTask);
                            cmdPC.ExecuteNonQuery();
                        }
                        using (SqlCommand cmdCV = new SqlCommand("DELETE FROM CongViec WHERE MaTask = @MaTask", conn, trans))
                        {
                            cmdCV.Parameters.AddWithValue("@MaTask", maTask);
                            cmdCV.ExecuteNonQuery();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Dùng SqlDataReader lấy các công việc quá HanNop mà TrangThai != 'Done'
        /// </summary>
        public List<CongViecDTO> GetOverdueTasks()
        {
            var list = new List<CongViecDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT cv.MaTask, cv.MaDuAn, da.TenDuAn, cv.TenTask, cv.HanNop, cv.DoUuTien, cv.TrangThai,
                           ISNULL((SELECT STRING_AGG(tv.HoTen, ', ')
                                   FROM PhanCong pc
                                   INNER JOIN ThanhVien tv ON pc.MaThanhVien = tv.MaThanhVien
                                   WHERE pc.MaTask = cv.MaTask), N'Chưa giao') AS NguoiPhuTrach
                    FROM CongViec cv
                    INNER JOIN DuAn da ON cv.MaDuAn = da.MaDuAn
                    WHERE cv.HanNop < GETDATE() AND cv.TrangThai <> N'Done'
                    ORDER BY cv.HanNop ASC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new CongViecDTO
                            {
                                MaTask = r.GetInt32(0),
                                MaDuAn = r.GetInt32(1),
                                TenDuAn = r.GetString(2),
                                TenTask = r.GetString(3),
                                HanNop = r.GetDateTime(4),
                                DoUuTien = r.GetString(5),
                                TrangThai = r.GetString(6),
                                NguoiPhuTrach = r.GetString(7)
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Thống kê tỷ lệ % hoàn thành công việc của từng thành viên bằng GROUP BY
        /// </summary>
        public List<ThongKeThanhVienDTO> GetThongKeThanhVien(int? maDuAn)
        {
            var list = new List<ThongKeThanhVienDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT tv.MaThanhVien, tv.HoTen, tv.ChuyenMon,
                           COUNT(cv.MaTask) AS TongTask,
                           SUM(CASE WHEN cv.TrangThai = N'Done' THEN 1 ELSE 0 END) AS SoTaskDone
                    FROM ThanhVien tv
                    LEFT JOIN PhanCong pc ON tv.MaThanhVien = pc.MaThanhVien
                    LEFT JOIN CongViec cv ON pc.MaTask = cv.MaTask AND (@MaDuAn IS NULL OR cv.MaDuAn = @MaDuAn)
                    GROUP BY tv.MaThanhVien, tv.HoTen, tv.ChuyenMon
                    ORDER BY tv.MaThanhVien";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDuAn", (object?)maDuAn ?? DBNull.Value);
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new ThongKeThanhVienDTO
                            {
                                MaThanhVien = r.GetInt32(0),
                                HoTen = r.GetString(1),
                                ChuyenMon = r.IsDBNull(2) ? string.Empty : r.GetString(2),
                                TongTaskDuocGiao = r.GetInt32(3),
                                SoTaskHoanThanh = r.IsDBNull(4) ? 0 : r.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
