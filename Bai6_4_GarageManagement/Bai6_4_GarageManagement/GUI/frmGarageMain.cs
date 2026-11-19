using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bai6_4_GarageManagement.BUS;
using Bai6_4_GarageManagement.DTO;

namespace Bai6_4_GarageManagement.GUI
{
    public partial class frmGarageMain : Form
    {
        private readonly GarageBUS bus = new GarageBUS();
        private BindingList<ChiTietSuaChuaDTO> dsChiTiet = new BindingList<ChiTietSuaChuaDTO>();
        private List<PhuTungDTO> dsPhuTungCache = new List<PhuTungDTO>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmGarageMain()
        {
            InitializeComponent();
        }

        private void frmGarageMain_Load(object? sender, EventArgs e)
        {
            cboHieuXe.Items.AddRange(new object[] { "Toyota", "Honda", "VinFast", "Hyundai", "Mazda", "Ford", "Kia", "Mercedes-Benz" });
            cboHieuXe.SelectedIndex = 0;

            SetupGrids();
            UpdateClock();
            if (AutoLoadDatabase) LoadAllData();
        }

        private void SetupGrids()
        {
            // Chi tiết sửa chữa
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaPhuTung", HeaderText = "Mã PT", Width = 70 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPhuTung", HeaderText = "Tên phụ tùng", Width = 260 });
            var cGia = new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá", Width = 120 };
            cGia.DefaultCellStyle.Format = "N0"; cGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTiet.Columns.Add(cGia);
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "SL", Width = 60 });
            var cCong = new DataGridViewTextBoxColumn { DataPropertyName = "TienCong", HeaderText = "Tiền công", Width = 120 };
            cCong.DefaultCellStyle.Format = "N0"; cCong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTiet.Columns.Add(cCong);
            var cTT = new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành tiền (VNĐ)", Width = 150 };
            cTT.DefaultCellStyle.Format = "N0"; cTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTiet.Columns.Add(cTT);
            dgvChiTiet.DataSource = dsChiTiet;

            // Xe
            dgvXe.AutoGenerateColumns = false;
            dgvXe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BienSo", HeaderText = "Biển số xe", Width = 120 });
            dgvXe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenKH", HeaderText = "Chủ xe", Width = 200 });
            dgvXe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DienThoai", HeaderText = "Điện thoại", Width = 130 });
            dgvXe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HieuXe", HeaderText = "Hiệu xe", Width = 130 });
            dgvXe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NamSanXuat", HeaderText = "Năm SX", Width = 90 });

            // Phụ tùng
            dgvPhuTung.AutoGenerateColumns = false;
            dgvPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaPhuTung", HeaderText = "Mã PT", Width = 80 });
            dgvPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPhuTung", HeaderText = "Tên phụ tùng", Width = 340 });
            var cGiaPT = new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá (VNĐ)", Width = 150 };
            cGiaPT.DefaultCellStyle.Format = "N0"; cGiaPT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPhuTung.Columns.Add(cGiaPT);
            dgvPhuTung.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongTon", HeaderText = "Tồn kho", Width = 100 });

            // Phiếu sửa chữa
            dgvPhieu.AutoGenerateColumns = false;
            dgvPhieu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaPhieu", HeaderText = "Mã phiếu", Width = 80 });
            dgvPhieu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BienSo", HeaderText = "Biển số", Width = 110 });
            dgvPhieu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenKH", HeaderText = "Chủ xe", Width = 160 });
            var cNgay = new DataGridViewTextBoxColumn { DataPropertyName = "NgayNhan", HeaderText = "Ngày nhận", Width = 115 };
            cNgay.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvPhieu.Columns.Add(cNgay);
            dgvPhieu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NoiDungSua", HeaderText = "Nội dung sửa chữa", Width = 240 });
            var cTong = new DataGridViewTextBoxColumn { DataPropertyName = "TongTien", HeaderText = "Tổng tiền (VNĐ)", Width = 130 };
            cTong.DefaultCellStyle.Format = "N0"; cTong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPhieu.Columns.Add(cTong);
            dgvPhieu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", Width = 120 });
        }

        private void LoadAllData()
        {
            try
            {
                LoadXe();
                LoadPhuTung();
                LoadPhieu();
                sslStatus.Text = "Kết nối CSDL GarageDB thành công (Kiến trúc 3-Tier & SqlTransaction)";
            }
            catch (Exception ex)
            {
                sslStatus.Text = "Chưa kết nối được SQL Server (Kiểm tra App.config / ScriptDB.sql)";
                MessageBox.Show($"Không thể kết nối CSDL GarageDB:\n{ex.Message}", "Thông báo kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadXe(string? filter = null)
        {
            var list = bus.GetAllXe(filter);
            dgvXe.DataSource = null;
            dgvXe.DataSource = list;

            if (string.IsNullOrEmpty(filter))
            {
                cboBienSoXe.DataSource = null;
                cboBienSoXe.DataSource = list.Select(x => new { x.BienSo, Display = $"{x.BienSo} - {x.TenKH} ({x.HieuXe})" }).ToList();
                cboBienSoXe.DisplayMember = "Display";
                cboBienSoXe.ValueMember = "BienSo";
            }
        }

        private void LoadPhuTung()
        {
            dsPhuTungCache = bus.GetAllPhuTung();
            dgvPhuTung.DataSource = null;
            dgvPhuTung.DataSource = dsPhuTungCache;

            cboPhuTung.DataSource = null;
            cboPhuTung.DataSource = dsPhuTungCache.Select(p => new { p.MaPhuTung, Display = $"{p.TenPhuTung} ({p.DonGia:N0} đ | Tồn: {p.SoLuongTon})" }).ToList();
            cboPhuTung.DisplayMember = "Display";
            cboPhuTung.ValueMember = "MaPhuTung";
        }

        private void LoadPhieu()
        {
            var list = bus.GetAllPhieuSuaChua();
            dgvPhieu.DataSource = null;
            dgvPhieu.DataSource = list;
            decimal dt = list.Where(p => p.TrangThai == "Đã thanh toán").Sum(p => p.TongTien);
            lblTongDoanhThu.Text = $"Tổng doanh thu đã thanh toán: {dt:N0} VNĐ";
        }

        private void btnThemCT_Click(object? sender, EventArgs e)
        {
            if (cboPhuTung.SelectedValue == null) return;
            int maPT = Convert.ToInt32(cboPhuTung.SelectedValue);
            var pt = dsPhuTungCache.FirstOrDefault(x => x.MaPhuTung == maPT);
            if (pt == null) return;

            if (!decimal.TryParse(txtTienCong.Text.Trim().Replace(",", ""), out decimal tienCong) || tienCong < 0)
            {
                errorProvider1.SetError(txtTienCong, "Tiền công phải là số không âm!");
                return;
            }
            errorProvider1.Clear();

            var existing = dsChiTiet.FirstOrDefault(c => c.MaPhuTung == maPT);
            if (existing != null)
            {
                existing.SoLuong += (int)numSLPT.Value;
                existing.TienCong += tienCong;
                dsChiTiet.ResetBindings();
            }
            else
            {
                dsChiTiet.Add(new ChiTietSuaChuaDTO
                {
                    MaPhuTung = pt.MaPhuTung,
                    TenPhuTung = pt.TenPhuTung,
                    DonGia = pt.DonGia,
                    SoLuong = (int)numSLPT.Value,
                    TienCong = tienCong
                });
            }
            CapNhatTongTien();
        }

        private void btnXoaCT_Click(object? sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0 && dgvChiTiet.SelectedRows[0].DataBoundItem is ChiTietSuaChuaDTO ct)
            {
                dsChiTiet.Remove(ct);
                CapNhatTongTien();
            }
        }

        private void CapNhatTongTien()
        {
            decimal tong = dsChiTiet.Sum(c => c.ThanhTien);
            txtTongTienSC.Text = $"{tong:N0} VNĐ";
        }

        private void btnLuuPhieu_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cboBienSoXe.SelectedValue == null) { MessageBox.Show("Vui lòng chọn xe!"); return; }
                var phieu = new PhieuSuaChuaDTO
                {
                    BienSo = cboBienSoXe.SelectedValue.ToString()!,
                    NoiDungSua = txtNoiDungSua.Text.Trim(),
                    TongTien = dsChiTiet.Sum(c => c.ThanhTien),
                    ChiTiet = dsChiTiet.ToList()
                };

                int maPhieu = bus.LapPhieuSuaChua(phieu);
                MessageBox.Show($"Lập phiếu sửa chữa #{maPhieu} thành công!\nĐã trừ tồn kho phụ tùng an toàn (SqlTransaction Commit).",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dsChiTiet.Clear();
                txtNoiDungSua.Clear();
                CapNhatTongTien();
                LoadAllData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giao dịch thất bại (Đã Rollback):\n{ex.Message}", "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemXe_Click(object? sender, EventArgs e)
        {
            try
            {
                var kh = new KhachHangDTO { TenKH = txtTenKH.Text.Trim(), DienThoai = txtDTKH.Text.Trim(), DiaChi = txtDiaChiKH.Text.Trim() };
                var xe = new XeDTO { BienSo = txtBienSoMoi.Text.Trim().ToUpper(), HieuXe = cboHieuXe.Text, NamSanXuat = (int)numNamSX.Value };
                if (bus.ThemXeVaChuXe(kh, xe))
                {
                    LoadXe();
                    MessageBox.Show("Tiếp nhận xe mới và chủ xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi tiếp nhận xe: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnTimXe_Click(object? sender, EventArgs e) { try { LoadXe(txtTimBienSo.Text); } catch (Exception ex) { MessageBox.Show(ex.Message); } }
        private void btnReloadXe_Click(object? sender, EventArgs e) { txtTimBienSo.Clear(); try { LoadXe(); } catch { } }

        private void dgvPhuTung_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvPhuTung.SelectedRows.Count > 0 && dgvPhuTung.SelectedRows[0].DataBoundItem is PhuTungDTO pt)
            {
                txtTenPT.Text = pt.TenPhuTung;
                txtGiaPT.Text = pt.DonGia.ToString("G0");
                numTonPT.Value = pt.SoLuongTon;
            }
        }

        private void btnAddPT_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtGiaPT.Text.Trim().Replace(",", ""), out decimal gia)) { errorProvider1.SetError(txtGiaPT, "Đơn giá không hợp lệ!"); return; }
                errorProvider1.Clear();
                if (bus.InsertPhuTung(new PhuTungDTO { TenPhuTung = txtTenPT.Text.Trim(), DonGia = gia, SoLuongTon = (int)numTonPT.Value }))
                { LoadPhuTung(); MessageBox.Show("Thêm phụ tùng thành công!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        private void btnUpdatePT_Click(object? sender, EventArgs e)
        {
            if (dgvPhuTung.SelectedRows.Count == 0 || dgvPhuTung.SelectedRows[0].DataBoundItem is not PhuTungDTO sel) return;
            try
            {
                if (!decimal.TryParse(txtGiaPT.Text.Trim().Replace(",", ""), out decimal gia)) { errorProvider1.SetError(txtGiaPT, "Đơn giá không hợp lệ!"); return; }
                errorProvider1.Clear();
                sel.TenPhuTung = txtTenPT.Text.Trim(); sel.DonGia = gia; sel.SoLuongTon = (int)numTonPT.Value;
                if (bus.UpdatePhuTung(sel)) { LoadPhuTung(); MessageBox.Show("Cập nhật phụ tùng thành công!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        private void btnDeletePT_Click(object? sender, EventArgs e)
        {
            if (dgvPhuTung.SelectedRows.Count == 0 || dgvPhuTung.SelectedRows[0].DataBoundItem is not PhuTungDTO sel) return;
            if (MessageBox.Show($"Xóa phụ tùng [{sel.TenPhuTung}]?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { bus.DeletePhuTung(sel.MaPhuTung); LoadPhuTung(); }
                catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
            }
        }

        private void btnThanhToanPhieu_Click(object? sender, EventArgs e)
        {
            if (dgvPhieu.SelectedRows.Count == 0 || dgvPhieu.SelectedRows[0].DataBoundItem is not PhieuSuaChuaDTO sel) return;
            if (sel.TrangThai == "Đã thanh toán") { MessageBox.Show("Phiếu này đã được thanh toán!"); return; }
            try
            {
                if (bus.ThanhToanPhieu(sel.MaPhieu))
                {
                    LoadPhieu();
                    MessageBox.Show($"Đã thanh toán hóa đơn sửa chữa #{sel.MaPhieu} ({sel.TongTien:N0} VNĐ)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi thanh toán: {ex.Message}"); }
        }

        private void btnReloadPhieu_Click(object? sender, EventArgs e) { try { LoadPhieu(); } catch { } }
        private void UpdateClock() => sslClock.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        private void timerClock_Tick(object? sender, EventArgs e) => UpdateClock();
    }
}
