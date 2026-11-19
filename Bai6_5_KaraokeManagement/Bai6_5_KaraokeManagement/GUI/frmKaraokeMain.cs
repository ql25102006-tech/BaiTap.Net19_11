using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bai6_5_KaraokeManagement.BUS;
using Bai6_5_KaraokeManagement.DTO;

namespace Bai6_5_KaraokeManagement.GUI
{
    public partial class frmKaraokeMain : Form
    {
        private readonly KaraokeBUS bus = new KaraokeBUS();
        private List<PhongHatDTO> dsPhongCache = new List<PhongHatDTO>();
        private List<DichVuDTO> dsDichVuCache = new List<DichVuDTO>();
        private PhongHatDTO? selectedPhong;
        private HoaDonDTO? activeHoaDon;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmKaraokeMain()
        {
            InitializeComponent();
        }

        private void frmKaraokeMain_Load(object? sender, EventArgs e)
        {
            SetupGrids();
            UpdateClock();
            if (AutoLoadDatabase) LoadAllData();
        }

        private void SetupGrids()
        {
            // Dịch vụ CRUD
            dgvDichVu.AutoGenerateColumns = false;
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaDV", HeaderText = "Mã DV", Width = 80 });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDV", HeaderText = "Tên dịch vụ / đồ uống", Width = 340 });
            var cGia = new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá (VNĐ)", Width = 150 };
            cGia.DefaultCellStyle.Format = "N0"; cGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDichVu.Columns.Add(cGia);
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TonKho", HeaderText = "Tồn kho", Width = 100 });

            // Lịch sử Hóa đơn
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaHD", HeaderText = "Mã HD", Width = 70 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPhong", HeaderText = "Phòng hát", Width = 140 });
            var cVao = new DataGridViewTextBoxColumn { DataPropertyName = "GioVao", HeaderText = "Giờ vào", Width = 130 };
            cVao.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvHoaDon.Columns.Add(cVao);
            var cRa = new DataGridViewTextBoxColumn { DataPropertyName = "GioRa", HeaderText = "Giờ ra", Width = 130 };
            cRa.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvHoaDon.Columns.Add(cRa);
            var cTP = new DataGridViewTextBoxColumn { DataPropertyName = "TongTienPhong", HeaderText = "Tiền phòng", Width = 120 };
            cTP.DefaultCellStyle.Format = "N0"; cTP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoaDon.Columns.Add(cTP);
            var cTDV = new DataGridViewTextBoxColumn { DataPropertyName = "TongTienDichVu", HeaderText = "Tiền dịch vụ", Width = 120 };
            cTDV.DefaultCellStyle.Format = "N0"; cTDV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoaDon.Columns.Add(cTDV);
            var cTC = new DataGridViewTextBoxColumn { DataPropertyName = "TongCong", HeaderText = "Tổng thanh toán (VNĐ)", Width = 150 };
            cTC.DefaultCellStyle.Format = "N0"; cTC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHoaDon.Columns.Add(cTC);
        }

        private void LoadAllData()
        {
            try
            {
                LoadRooms();
                LoadDichVu();
                LoadHoaDonReport();
                sslStatus.Text = "Kết nối CSDL KaraokeDB thành công (Visual Grid, Disconnected DataTable & SqlTransaction)";
            }
            catch (Exception ex)
            {
                sslStatus.Text = "Chưa kết nối được SQL Server (Kiểm tra App.config / ScriptDB.sql)";
                MessageBox.Show($"Không thể kết nối CSDL KaraokeDB:\n{ex.Message}", "Thông báo kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RenderRoomButtons(List<PhongHatDTO> rooms)
        {
            dsPhongCache = rooms;
            flpRooms.Controls.Clear();
            foreach (var p in rooms)
            {
                Button btn = new Button
                {
                    Width = 175,
                    Height = 95,
                    Margin = new Padding(8),
                    Text = $"{p.TenPhong}\n({p.LoaiPhong} - {p.GiaTheoGio:N0}đ/h)\n[{p.TrangThai}]",
                    Tag = p,
                    BackColor = p.TrangThai == "Đang hát" ? Color.LightCoral : Color.LightGreen
                };
                btn.Click += RoomButton_Click;
                flpRooms.Controls.Add(btn);
            }
        }

        private void LoadRooms()
        {
            var rooms = bus.GetAllPhong();
            RenderRoomButtons(rooms);
        }

        private void RoomButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is PhongHatDTO p)
            {
                SelectRoom(p);
            }
        }

        private void SelectRoom(PhongHatDTO p)
        {
            selectedPhong = p;
            lblRoomSelected.Text = $"Phòng đang chọn: {p.TenPhong} ({p.LoaiPhong} - {p.GiaTheoGio:N0} VNĐ/giờ)";

            if (p.TrangThai == "Đang hát")
            {
                btnMoPhong.Enabled = false;
                btnThemDV.Enabled = true;
                btnThanhToan.Enabled = true;

                try
                {
                    activeHoaDon = bus.GetActiveHoaDon(p.MaPhong);
                    if (activeHoaDon != null)
                    {
                        lblGioVao.Text = $"Giờ vào: {activeHoaDon.GioVao:HH:mm dd/MM/yyyy} | Hóa đơn #{activeHoaDon.MaHD} | Trạng thái: Đang hát";
                        LoadDichVuCuaPhong(activeHoaDon.MaHD);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi tải phòng: {ex.Message}"); }
            }
            else
            {
                activeHoaDon = null;
                btnMoPhong.Enabled = true;
                btnThemDV.Enabled = false;
                btnThanhToan.Enabled = false;
                lblGioVao.Text = "Giờ vào: --:-- | Trạng thái: Trống (Bấm 'Mở phòng hát' để bắt đầu)";
                dgvDichVuPhong.DataSource = null;
                txtTienDV.Text = "0 VNĐ";
            }
        }

        private void LoadDichVuCuaPhong(int maHD)
        {
            DataTable dt = bus.GetChiTietDichVuTable(maHD);
            dgvDichVuPhong.DataSource = dt;

            decimal tongDV = 0m;
            foreach (DataRow row in dt.Rows)
            {
                tongDV += Convert.ToDecimal(row["Thành tiền"]);
            }
            txtTienDV.Text = $"{tongDV:N0} VNĐ";
        }

        private void LoadDichVu()
        {
            dsDichVuCache = bus.GetAllDichVu();
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = dsDichVuCache;

            cboDichVu.DataSource = null;
            cboDichVu.DataSource = dsDichVuCache.Select(d => new { d.MaDV, Display = $"{d.TenDV} ({d.DonGia:N0}đ | Tồn: {d.TonKho})" }).ToList();
            cboDichVu.DisplayMember = "Display";
            cboDichVu.ValueMember = "MaDV";
        }

        private void LoadHoaDonReport()
        {
            var list = bus.GetLichSuHoaDon();
            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = list;
            lblTongDoanhThu.Text = $"Tổng doanh thu Karaoke: {list.Sum(x => x.TongCong):N0} VNĐ";
        }

        private void btnMoPhong_Click(object? sender, EventArgs e)
        {
            if (selectedPhong == null) { MessageBox.Show("Vui lòng chọn 1 phòng trống!"); return; }
            try
            {
                bus.MoPhong(selectedPhong.MaPhong);
                LoadRooms();
                var updated = dsPhongCache.FirstOrDefault(x => x.MaPhong == selectedPhong.MaPhong);
                if (updated != null) SelectRoom(updated);
                MessageBox.Show($"Đã mở [{selectedPhong.TenPhong}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi mở phòng: {ex.Message}"); }
        }

        private void btnThemDV_Click(object? sender, EventArgs e)
        {
            if (activeHoaDon == null || cboDichVu.SelectedValue == null) return;
            int maDV = Convert.ToInt32(cboDichVu.SelectedValue);
            var dv = dsDichVuCache.FirstOrDefault(x => x.MaDV == maDV);
            if (dv == null) return;

            try
            {
                bus.ThemDichVuVaoPhong(activeHoaDon.MaHD, dv.MaDV, (int)numSLDV.Value, dv.DonGia);
                LoadDichVuCuaPhong(activeHoaDon.MaHD);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi gọi dịch vụ: {ex.Message}"); }
        }

        private void btnThanhToan_Click(object? sender, EventArgs e)
        {
            if (selectedPhong == null || activeHoaDon == null) return;
            if (MessageBox.Show($"Xác nhận thanh toán và trả [{selectedPhong.TenPhong}]?", "Thanh toán (SqlTransaction)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var res = bus.ThanhToanPhong(selectedPhong.MaPhong);
                    decimal tongCong = res.TienPhong + res.TienDichVu;
                    MessageBox.Show($"Thanh toán thành công [{selectedPhong.TenPhong}]!\n" +
                                    $"- Thời gian hát: {res.SoGio} giờ\n" +
                                    $"- Tiền phòng: {res.TienPhong:N0} VNĐ\n" +
                                    $"- Tiền dịch vụ: {res.TienDichVu:N0} VNĐ\n" +
                                    $"=> TỔNG CỘNG: {tongCong:N0} VNĐ\n(Đã trừ tồn kho và trả phòng về Trống)",
                        "Thanh toán thành công (Commit)", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAllData();
                    var updated = dsPhongCache.FirstOrDefault(x => x.MaPhong == selectedPhong.MaPhong);
                    if (updated != null) SelectRoom(updated);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Thanh toán thất bại (Đã Rollback):\n{ex.Message}", "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDichVu_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count > 0 && dgvDichVu.SelectedRows[0].DataBoundItem is DichVuDTO dv)
            {
                txtTenDV.Text = dv.TenDV;
                txtGiaDV.Text = dv.DonGia.ToString("G0");
                numTonDV.Value = dv.TonKho;
            }
        }

        private void btnAddDV_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtGiaDV.Text.Trim().Replace(",", ""), out decimal gia)) { errorProvider1.SetError(txtGiaDV, "Đơn giá không hợp lệ!"); return; }
                errorProvider1.Clear();
                if (bus.InsertDichVu(new DichVuDTO { TenDV = txtTenDV.Text.Trim(), DonGia = gia, TonKho = (int)numTonDV.Value }))
                { LoadDichVu(); MessageBox.Show("Thêm dịch vụ thành công!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        private void btnUpdateDV_Click(object? sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count == 0 || dgvDichVu.SelectedRows[0].DataBoundItem is not DichVuDTO sel) return;
            try
            {
                if (!decimal.TryParse(txtGiaDV.Text.Trim().Replace(",", ""), out decimal gia)) { errorProvider1.SetError(txtGiaDV, "Đơn giá không hợp lệ!"); return; }
                errorProvider1.Clear();
                sel.TenDV = txtTenDV.Text.Trim(); sel.DonGia = gia; sel.TonKho = (int)numTonDV.Value;
                if (bus.UpdateDichVu(sel)) { LoadDichVu(); MessageBox.Show("Cập nhật dịch vụ thành công!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        private void btnDeleteDV_Click(object? sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count == 0 || dgvDichVu.SelectedRows[0].DataBoundItem is not DichVuDTO sel) return;
            if (MessageBox.Show($"Xóa dịch vụ [{sel.TenDV}]?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { bus.DeleteDichVu(sel.MaDV); LoadDichVu(); }
                catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
            }
        }

        private void btnReloadBC_Click(object? sender, EventArgs e) { try { LoadHoaDonReport(); } catch { } }
        private void UpdateClock() => sslClock.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        private void timerClock_Tick(object? sender, EventArgs e) => UpdateClock();
    }
}
