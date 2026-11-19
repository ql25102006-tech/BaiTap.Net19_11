using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Bai6_4_GarageManagement.DTO;

namespace Bai6_4_GarageManagement.DAL
{
    public class GarageDAL
    {
        public List<XeDTO> GetAllXe(string? bienSoFilter = null)
        {
            var list = new List<XeDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT x.BienSo, x.MaKH, kh.TenKH, kh.DienThoai, x.HieuXe, x.NamSanXuat
                    FROM Xe x
                    INNER JOIN KhachHang kh ON x.MaKH = kh.MaKH
                    WHERE (@BienSo = '' OR x.BienSo LIKE @Pattern OR kh.TenKH LIKE @Pattern)
                    ORDER BY x.BienSo";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string kw = bienSoFilter?.Trim() ?? string.Empty;
                    cmd.Parameters.AddWithValue("@BienSo", kw);
                    cmd.Parameters.AddWithValue("@Pattern", "%" + kw + "%");
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new XeDTO
                            {
                                BienSo = r.GetString(0),
                                MaKH = r.GetInt32(1),
                                TenKH = r.GetString(2),
                                DienThoai = r.IsDBNull(3) ? "" : r.GetString(3),
                                HieuXe = r.IsDBNull(4) ? "" : r.GetString(4),
                                NamSanXuat = r.IsDBNull(5) ? 2020 : r.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool ThemXeVaChuXe(KhachHangDTO kh, XeDTO xe)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int maKH;
                        string sqlKH = "INSERT INTO KhachHang (TenKH, DienThoai, DiaChi) OUTPUT INSERTED.MaKH VALUES (@TenKH, @DienThoai, @DiaChi)";
                        using (SqlCommand cmdKH = new SqlCommand(sqlKH, conn, trans))
                        {
                            cmdKH.Parameters.AddWithValue("@TenKH", kh.TenKH);
                            cmdKH.Parameters.AddWithValue("@DienThoai", kh.DienThoai);
                            cmdKH.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                            maKH = (int)cmdKH.ExecuteScalar();
                        }

                        string sqlXe = "INSERT INTO Xe (BienSo, MaKH, HieuXe, NamSanXuat) VALUES (@BienSo, @MaKH, @HieuXe, @NamSX)";
                        using (SqlCommand cmdXe = new SqlCommand(sqlXe, conn, trans))
                        {
                            cmdXe.Parameters.AddWithValue("@BienSo", xe.BienSo);
                            cmdXe.Parameters.AddWithValue("@MaKH", maKH);
                            cmdXe.Parameters.AddWithValue("@HieuXe", xe.HieuXe);
                            cmdXe.Parameters.AddWithValue("@NamSX", xe.NamSanXuat);
                            cmdXe.ExecuteNonQuery();
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

        public List<PhuTungDTO> GetAllPhuTung()
        {
            var list = new List<PhuTungDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaPhuTung, TenPhuTung, DonGia, SoLuongTon FROM PhuTung ORDER BY MaPhuTung";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new PhuTungDTO
                            {
                                MaPhuTung = r.GetInt32(0),
                                TenPhuTung = r.GetString(1),
                                DonGia = r.GetDecimal(2),
                                SoLuongTon = r.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool InsertPhuTung(PhuTungDTO pt)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "INSERT INTO PhuTung (TenPhuTung, DonGia, SoLuongTon) VALUES (@Ten, @Gia, @Ton)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", pt.TenPhuTung);
                    cmd.Parameters.AddWithValue("@Gia", pt.DonGia);
                    cmd.Parameters.AddWithValue("@Ton", pt.SoLuongTon);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdatePhuTung(PhuTungDTO pt)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "UPDATE PhuTung SET TenPhuTung = @Ten, DonGia = @Gia, SoLuongTon = @Ton WHERE MaPhuTung = @Ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", pt.MaPhuTung);
                    cmd.Parameters.AddWithValue("@Ten", pt.TenPhuTung);
                    cmd.Parameters.AddWithValue("@Gia", pt.DonGia);
                    cmd.Parameters.AddWithValue("@Ton", pt.SoLuongTon);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeletePhuTung(int maPT)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "DELETE FROM PhuTung WHERE MaPhuTung = @Ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", maPT);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Lập Phiếu sửa chữa + Chi tiết + Trừ tồn kho Phụ tùng trong cùng SqlTransaction
        /// </summary>
        public int LapPhieuSuaChua(PhieuSuaChuaDTO phieu)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlPhieu = @"
                            INSERT INTO PhieuSuaChua (BienSo, NgayNhan, NoiDungSua, TongTien, TrangThai)
                            OUTPUT INSERTED.MaPhieu
                            VALUES (@BienSo, GETDATE(), @NoiDung, @TongTien, N'Đã hoàn thành')";
                        int maPhieu;
                        using (SqlCommand cmd = new SqlCommand(sqlPhieu, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@BienSo", phieu.BienSo);
                            cmd.Parameters.AddWithValue("@NoiDung", phieu.NoiDungSua);
                            cmd.Parameters.AddWithValue("@TongTien", phieu.TongTien);
                            maPhieu = (int)cmd.ExecuteScalar();
                        }

                        foreach (var ct in phieu.ChiTiet)
                        {
                            string sqlCheck = "SELECT SoLuongTon FROM PhuTung WHERE MaPhuTung = @MaPT";
                            int tonKho = 0;
                            using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaPT", ct.MaPhuTung);
                                object? res = cmdCheck.ExecuteScalar();
                                if (res == null || res == DBNull.Value)
                                    throw new InvalidOperationException($"Phụ tùng #{ct.MaPhuTung} không tồn tại!");
                                tonKho = Convert.ToInt32(res);
                            }

                            if (tonKho < ct.SoLuong)
                            {
                                throw new InvalidOperationException(
                                    $"Phụ tùng [{ct.TenPhuTung}] không đủ tồn kho! Hiện còn: {tonKho}, yêu cầu: {ct.SoLuong}. Giao dịch đã Rollback.");
                            }

                            string sqlCT = "INSERT INTO ChiTietSuaChua (MaPhieu, MaPhuTung, SoLuong, TienCong) VALUES (@MaPhieu, @MaPT, @SL, @TienCong)";
                            using (SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans))
                            {
                                cmdCT.Parameters.AddWithValue("@MaPhieu", maPhieu);
                                cmdCT.Parameters.AddWithValue("@MaPT", ct.MaPhuTung);
                                cmdCT.Parameters.AddWithValue("@SL", ct.SoLuong);
                                cmdCT.Parameters.AddWithValue("@TienCong", ct.TienCong);
                                cmdCT.ExecuteNonQuery();
                            }

                            string sqlTru = "UPDATE PhuTung SET SoLuongTon = SoLuongTon - @SL WHERE MaPhuTung = @MaPT";
                            using (SqlCommand cmdTru = new SqlCommand(sqlTru, conn, trans))
                            {
                                cmdTru.Parameters.AddWithValue("@SL", ct.SoLuong);
                                cmdTru.Parameters.AddWithValue("@MaPT", ct.MaPhuTung);
                                cmdTru.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return maPhieu;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<PhieuSuaChuaDTO> GetAllPhieuSuaChua()
        {
            var list = new List<PhieuSuaChuaDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT p.MaPhieu, p.BienSo, kh.TenKH, p.NgayNhan, p.NoiDungSua, p.TongTien, p.TrangThai
                    FROM PhieuSuaChua p
                    INNER JOIN Xe x ON p.BienSo = x.BienSo
                    INNER JOIN KhachHang kh ON x.MaKH = kh.MaKH
                    ORDER BY p.NgayNhan DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new PhieuSuaChuaDTO
                            {
                                MaPhieu = r.GetInt32(0),
                                BienSo = r.GetString(1),
                                TenKH = r.GetString(2),
                                NgayNhan = r.GetDateTime(3),
                                NoiDungSua = r.IsDBNull(4) ? "" : r.GetString(4),
                                TongTien = r.GetDecimal(5),
                                TrangThai = r.GetString(6)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool ThanhToanPhieu(int maPhieu)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "UPDATE PhieuSuaChua SET TrangThai = N'Đã thanh toán' WHERE MaPhieu = @MaPhieu";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
