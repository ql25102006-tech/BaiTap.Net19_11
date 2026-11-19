using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Bai6_5_KaraokeManagement.DTO;

namespace Bai6_5_KaraokeManagement.DAL
{
    public class KaraokeDAL
    {
        public List<PhongHatDTO> GetAllPhong()
        {
            var list = new List<PhongHatDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaPhong, TenPhong, LoaiPhong, GiaTheoGio, TrangThai FROM PhongHat ORDER BY MaPhong";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new PhongHatDTO
                            {
                                MaPhong = r.GetInt32(0),
                                TenPhong = r.GetString(1),
                                LoaiPhong = r.IsDBNull(2) ? "Thường" : r.GetString(2),
                                GiaTheoGio = r.GetDecimal(3),
                                TrangThai = r.IsDBNull(4) ? "Trống" : r.GetString(4)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public List<DichVuDTO> GetAllDichVu()
        {
            var list = new List<DichVuDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaDV, TenDV, DonGia, TonKho FROM DichVu ORDER BY MaDV";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new DichVuDTO
                            {
                                MaDV = r.GetInt32(0),
                                TenDV = r.GetString(1),
                                DonGia = r.GetDecimal(2),
                                TonKho = r.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool InsertDichVu(DichVuDTO dv)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "INSERT INTO DichVu (TenDV, DonGia, TonKho) VALUES (@Ten, @Gia, @Ton)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", dv.TenDV);
                    cmd.Parameters.AddWithValue("@Gia", dv.DonGia);
                    cmd.Parameters.AddWithValue("@Ton", dv.TonKho);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateDichVu(DichVuDTO dv)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "UPDATE DichVu SET TenDV = @Ten, DonGia = @Gia, TonKho = @Ton WHERE MaDV = @Ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", dv.MaDV);
                    cmd.Parameters.AddWithValue("@Ten", dv.TenDV);
                    cmd.Parameters.AddWithValue("@Gia", dv.DonGia);
                    cmd.Parameters.AddWithValue("@Ton", dv.TonKho);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteDichVu(int maDV)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "DELETE FROM DichVu WHERE MaDV = @Ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", maDV);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public int MoPhong(int maPhong)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlHD = "INSERT INTO HoaDon (MaPhong, GioVao, TongTienPhong, TongTienDichVu, TrangThai) OUTPUT INSERTED.MaHD VALUES (@MaPhong, GETDATE(), 0, 0, 0)";
                        int maHD;
                        using (SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans))
                        {
                            cmdHD.Parameters.AddWithValue("@MaPhong", maPhong);
                            maHD = (int)cmdHD.ExecuteScalar();
                        }

                        string sqlPhong = "UPDATE PhongHat SET TrangThai = N'Đang hát' WHERE MaPhong = @MaPhong";
                        using (SqlCommand cmdP = new SqlCommand(sqlPhong, conn, trans))
                        {
                            cmdP.Parameters.AddWithValue("@MaPhong", maPhong);
                            cmdP.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return maHD;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public HoaDonDTO? GetActiveHoaDon(int maPhong)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT TOP 1 hd.MaHD, hd.MaPhong, p.TenPhong, hd.GioVao, hd.TongTienPhong, hd.TongTienDichVu
                    FROM HoaDon hd
                    INNER JOIN PhongHat p ON hd.MaPhong = p.MaPhong
                    WHERE hd.MaPhong = @MaPhong AND hd.TrangThai = 0
                    ORDER BY hd.MaHD DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new HoaDonDTO
                            {
                                MaHD = r.GetInt32(0),
                                MaPhong = r.GetInt32(1),
                                TenPhong = r.GetString(2),
                                GioVao = r.GetDateTime(3),
                                TongTienPhong = r.GetDecimal(4),
                                TongTienDichVu = r.GetDecimal(5),
                                TrangThai = false
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Nạp danh sách dịch vụ đã đặt bằng SqlDataAdapter + DataTable (Disconnected Model)
        /// </summary>
        public DataTable GetChiTietDichVuTable(int maHD)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT ct.MaDV AS [Mã DV], dv.TenDV AS [Tên dịch vụ], ct.SoLuong AS [Số lượng],
                           ct.DonGia AS [Đơn giá], (ct.SoLuong * ct.DonGia) AS [Thành tiền]
                    FROM ChiTietDichVu ct
                    INNER JOIN DichVu dv ON ct.MaDV = dv.MaDV
                    WHERE ct.MaHD = @MaHD";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@MaHD", maHD);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public void ThemDichVuVaoPhong(int maHD, int maDV, int soLuong, decimal donGia)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    IF EXISTS (SELECT 1 FROM ChiTietDichVu WHERE MaHD = @MaHD AND MaDV = @MaDV)
                        UPDATE ChiTietDichVu SET SoLuong = SoLuong + @SoLuong WHERE MaHD = @MaHD AND MaDV = @MaDV
                    ELSE
                        INSERT INTO ChiTietDichVu (MaHD, MaDV, SoLuong, DonGia) VALUES (@MaHD, @MaDV, @SoLuong, @DonGia)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Thanh toán phòng Karaoke bằng SqlTransaction: Tính tiền giờ + Trừ tồn kho DichVu + Đóng HoaDon + Trả PhongHat về Trống
        /// </summary>
        public (decimal TienPhong, decimal TienDichVu, double SoGio) ThanhToanPhong(int maPhong)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Lấy hóa đơn đang mở & giá phòng
                        string sqlInfo = @"
                            SELECT TOP 1 hd.MaHD, hd.GioVao, p.GiaTheoGio
                            FROM HoaDon hd
                            INNER JOIN PhongHat p ON hd.MaPhong = p.MaPhong
                            WHERE hd.MaPhong = @MaPhong AND hd.TrangThai = 0
                            ORDER BY hd.MaHD DESC";
                        int maHD = 0;
                        DateTime gioVao = DateTime.Now;
                        decimal giaGio = 0;

                        using (SqlCommand cmd = new SqlCommand(sqlInfo, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                if (!r.Read())
                                    throw new InvalidOperationException("Không tìm thấy hóa đơn đang hoạt động cho phòng này!");
                                maHD = r.GetInt32(0);
                                gioVao = r.GetDateTime(1);
                                giaGio = r.GetDecimal(2);
                            }
                        }

                        // Tính thời gian hát & tiền phòng (tối thiểu 0.5 giờ)
                        DateTime gioRa = DateTime.Now;
                        double soGio = Math.Round(Math.Max(0.5, (gioRa - gioVao).TotalHours), 2);
                        decimal tienPhong = Math.Round((decimal)soGio * giaGio, 0);

                        // 2. Lấy chi tiết dịch vụ & kiểm tra + trừ tồn kho DichVu
                        var dsDV = new List<(int MaDV, string TenDV, int SoLuong, decimal DonGia, int TonKho)>();
                        string sqlCT = @"
                            SELECT ct.MaDV, dv.TenDV, ct.SoLuong, ct.DonGia, dv.TonKho
                            FROM ChiTietDichVu ct
                            INNER JOIN DichVu dv ON ct.MaDV = dv.MaDV
                            WHERE ct.MaHD = @MaHD";
                        using (SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans))
                        {
                            cmdCT.Parameters.AddWithValue("@MaHD", maHD);
                            using (SqlDataReader r = cmdCT.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    dsDV.Add((r.GetInt32(0), r.GetString(1), r.GetInt32(2), r.GetDecimal(3), r.GetInt32(4)));
                                }
                            }
                        }

                        decimal tienDV = 0m;
                        foreach (var item in dsDV)
                        {
                            if (item.TonKho < item.SoLuong)
                            {
                                throw new InvalidOperationException(
                                    $"Dịch vụ [{item.TenDV}] không đủ tồn kho! Còn: {item.TonKho}, đã gọi: {item.SoLuong}. Giao dịch đã Rollback.");
                            }

                            tienDV += item.SoLuong * item.DonGia;
                            using (SqlCommand cmdTru = new SqlCommand("UPDATE DichVu SET TonKho = TonKho - @SL WHERE MaDV = @MaDV", conn, trans))
                            {
                                cmdTru.Parameters.AddWithValue("@SL", item.SoLuong);
                                cmdTru.Parameters.AddWithValue("@MaDV", item.MaDV);
                                cmdTru.ExecuteNonQuery();
                            }
                        }

                        // 3. Cập nhật HoaDon (GioRa, TongTienPhong, TongTienDichVu, TrangThai = 1)
                        string sqlUpHD = @"
                            UPDATE HoaDon
                            SET GioRa = @GioRa, TongTienPhong = @TP, TongTienDichVu = @TDV, TrangThai = 1
                            WHERE MaHD = @MaHD";
                        using (SqlCommand cmdUpHD = new SqlCommand(sqlUpHD, conn, trans))
                        {
                            cmdUpHD.Parameters.AddWithValue("@GioRa", gioRa);
                            cmdUpHD.Parameters.AddWithValue("@TP", tienPhong);
                            cmdUpHD.Parameters.AddWithValue("@TDV", tienDV);
                            cmdUpHD.Parameters.AddWithValue("@MaHD", maHD);
                            cmdUpHD.ExecuteNonQuery();
                        }

                        // 4. Trả trạng thái PhongHat về Trống
                        using (SqlCommand cmdP = new SqlCommand("UPDATE PhongHat SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong", conn, trans))
                        {
                            cmdP.Parameters.AddWithValue("@MaPhong", maPhong);
                            cmdP.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return (tienPhong, tienDV, soGio);
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<HoaDonDTO> GetLichSuHoaDon()
        {
            var list = new List<HoaDonDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT hd.MaHD, hd.MaPhong, p.TenPhong, hd.GioVao, hd.GioRa, hd.TongTienPhong, hd.TongTienDichVu, hd.TrangThai
                    FROM HoaDon hd
                    INNER JOIN PhongHat p ON hd.MaPhong = p.MaPhong
                    WHERE hd.TrangThai = 1
                    ORDER BY hd.GioRa DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new HoaDonDTO
                            {
                                MaHD = r.GetInt32(0),
                                MaPhong = r.GetInt32(1),
                                TenPhong = r.GetString(2),
                                GioVao = r.GetDateTime(3),
                                GioRa = r.IsDBNull(4) ? null : r.GetDateTime(4),
                                TongTienPhong = r.GetDecimal(5),
                                TongTienDichVu = r.GetDecimal(6),
                                TrangThai = r.GetBoolean(7)
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
