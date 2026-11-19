using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Bai6_3_ProjectTaskManagement.BUS;
using Bai6_3_ProjectTaskManagement.DTO;

namespace Bai6_3_ProjectTaskManagement.GUI
{
    public partial class frmProjectTaskMain : Form
    {
        private readonly ProjectTaskBUS bus = new ProjectTaskBUS();
        private List<ThanhVienDTO> dsThanhVienCache = new List<ThanhVienDTO>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmProjectTaskMain()
        {
            InitializeComponent();
        }

        private void frmProjectTaskMain_Load(object? sender, EventArgs e)
        {
            SetupComboboxes();
            SetupGrids();
            UpdateClock();
            if (AutoLoadDatabase)
            {
                LoadAllData();
            }
        }

        private void SetupComboboxes()
        {
            cboDoUuTien.Items.AddRange(new object[] { "Cao", "Trung bình", "Thấp" });
            cboDoUuTien.SelectedIndex = 1;

            cboTrangThaiTask.Items.AddRange(new object[] { "To Do", "In Progress", "Review", "Done" });
            cboTrangThaiTask.SelectedIndex = 0;

            cboFilterTrangThai.Items.AddRange(new object[] { "Tất cả", "To Do", "In Progress", "Review", "Done" });
            cboFilterTrangThai.SelectedIndex = 0;

            cboTrangThaiDA.Items.AddRange(new object[] { "Đang chạy", "Hoàn thành", "Tạm dừng" });
            cboTrangThaiDA.SelectedIndex = 0;
        }

        private void SetupGrids()
        {
            // Tasks Grid
            dgvTasks.AutoGenerateColumns = false;
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaTask", HeaderText = "Mã", Width = 55 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDuAn", HeaderText = "Dự án", Width = 180 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenTask", HeaderText = "Tên công việc", Width = 220 });
            var colHan = new DataGridViewTextBoxColumn { DataPropertyName = "HanNop", HeaderText = "Hạn nộp", Width = 95 };
            colHan.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvTasks.Columns.Add(colHan);
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DoUuTien", HeaderText = "Ưu tiên", Width = 85 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", Width = 95 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NguoiPhuTrach", HeaderText = "Người phụ trách", Width = 170 });

            // DuAn Grid
            dgvDuAn.AutoGenerateColumns = false;
            dgvDuAn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaDuAn", HeaderText = "Mã DA", Width = 70 });
            dgvDuAn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDuAn", HeaderText = "Tên dự án", Width = 350 });
            var colBD = new DataGridViewTextBoxColumn { DataPropertyName = "NgayBatDau", HeaderText = "Ngày bắt đầu", Width = 120 };
            colBD.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDuAn.Columns.Add(colBD);
            var colKT = new DataGridViewTextBoxColumn { DataPropertyName = "NgayKetThuc", HeaderText = "Ngày kết thúc", Width = 120 };
            colKT.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDuAn.Columns.Add(colKT);
            dgvDuAn.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", Width = 120 });

            // Overdue Grid
            dgvOverdue.AutoGenerateColumns = false;
            dgvOverdue.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaTask", HeaderText = "Mã Task", Width = 75 });
            dgvOverdue.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDuAn", HeaderText = "Dự án", Width = 210 });
            dgvOverdue.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenTask", HeaderText = "Công việc trễ hạn", Width = 240 });
            var colHanOD = new DataGridViewTextBoxColumn { DataPropertyName = "HanNop", HeaderText = "Hạn nộp", Width = 100 };
            colHanOD.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvOverdue.Columns.Add(colHanOD);
            dgvOverdue.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", Width = 100 });
            dgvOverdue.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NguoiPhuTrach", HeaderText = "Phụ trách", Width = 180 });

            // ThongKe Grid
            dgvThongKe.AutoGenerateColumns = false;
            dgvThongKe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaThanhVien", HeaderText = "Mã TV", Width = 70 });
            dgvThongKe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Thành viên", Width = 200 });
            dgvThongKe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ChuyenMon", HeaderText = "Chuyên môn", Width = 220 });
            dgvThongKe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TongTaskDuocGiao", HeaderText = "Tổng Task", Width = 100 });
            dgvThongKe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoTaskHoanThanh", HeaderText = "Đã hoàn thành", Width = 120 });
            dgvThongKe.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TyLeHoanThanh", HeaderText = "Tỷ lệ hoàn thành (%)", Width = 140 });
        }

        private void LoadAllData()
        {
            try
            {
                LoadDuAn();
                LoadThanhVien();
                LoadTasks();
                LoadReports();
                sslStatus.Text = "Kết nối CSDL ProjectTaskDB thành công (Kiến trúc 3-Tier)";
            }
            catch (Exception ex)
            {
                sslStatus.Text = "Chưa kết nối được SQL Server (Kiểm tra App.config / ScriptDB.sql)";
                MessageBox.Show($"Không thể kết nối CSDL ProjectTaskDB:\n{ex.Message}", "Thông báo kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDuAn()
        {
            var list = bus.GetAllDuAn();
            dgvDuAn.DataSource = null;
            dgvDuAn.DataSource = list;

            cboTaskDuAn.DataSource = null;
            cboTaskDuAn.DataSource = new List<DuAnDTO>(list);
            cboTaskDuAn.DisplayMember = "TenDuAn";
            cboTaskDuAn.ValueMember = "MaDuAn";

            var filterList = new List<DuAnDTO> { new DuAnDTO { MaDuAn = 0, TenDuAn = "-- Tất cả dự án --" } };
            filterList.AddRange(list);
            cboFilterDuAn.DataSource = null;
            cboFilterDuAn.DataSource = filterList;
            cboFilterDuAn.DisplayMember = "TenDuAn";
            cboFilterDuAn.ValueMember = "MaDuAn";
        }

        private void LoadThanhVien()
        {
            dsThanhVienCache = bus.GetAllThanhVien();
            clbThanhVien.Items.Clear();
            foreach (var tv in dsThanhVienCache)
            {
                clbThanhVien.Items.Add($"{tv.HoTen} ({tv.ChuyenMon})");
            }
        }

        private void LoadTasks()
        {
            int? maDuAn = null;
            if (cboFilterDuAn.SelectedValue != null && int.TryParse(cboFilterDuAn.SelectedValue.ToString(), out int daId) && daId > 0)
                maDuAn = daId;

            string? tt = cboFilterTrangThai.SelectedItem?.ToString();
            if (tt == "Tất cả") tt = string.Empty;

            dgvTasks.DataSource = null;
            dgvTasks.DataSource = bus.GetTasks(maDuAn, tt, txtSearchTask.Text);
        }

        private void LoadReports()
        {
            dgvOverdue.DataSource = null;
            dgvOverdue.DataSource = bus.GetOverdueTasks();

            dgvThongKe.DataSource = null;
            dgvThongKe.DataSource = bus.GetThongKeThanhVien(null);
        }

        private void dgvTasks_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0 && dgvTasks.SelectedRows[0].DataBoundItem is CongViecDTO t)
            {
                cboTaskDuAn.SelectedValue = t.MaDuAn;
                txtTenTask.Text = t.TenTask;
                txtMoTa.Text = t.MoTa;
                dtpHanNop.Value = t.HanNop;
                cboDoUuTien.SelectedItem = t.DoUuTien;
                cboTrangThaiTask.SelectedItem = t.TrangThai;
            }
        }

        private void btnAddTask_Click(object? sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (string.IsNullOrWhiteSpace(txtTenTask.Text))
            {
                errorProvider1.SetError(txtTenTask, "Tên công việc không được để trống!");
                return;
            }
            try
            {
                var memberIds = new List<int>();
                for (int i = 0; i < clbThanhVien.Items.Count; i++)
                {
                    if (clbThanhVien.GetItemChecked(i) && i < dsThanhVienCache.Count)
                        memberIds.Add(dsThanhVienCache[i].MaThanhVien);
                }

                var task = new CongViecDTO
                {
                    MaDuAn = Convert.ToInt32(cboTaskDuAn.SelectedValue),
                    TenTask = txtTenTask.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    HanNop = dtpHanNop.Value,
                    DoUuTien = cboDoUuTien.SelectedItem?.ToString() ?? "Trung bình",
                    TrangThai = cboTrangThaiTask.SelectedItem?.ToString() ?? "To Do"
                };

                bus.InsertTask(task, memberIds);
                LoadTasks();
                LoadReports();
                MessageBox.Show("Thêm công việc và phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnUpdateStatus_Click(object? sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0 || dgvTasks.SelectedRows[0].DataBoundItem is not CongViecDTO t) return;
            string newStatus = cboTrangThaiTask.SelectedItem?.ToString() ?? "In Progress";
            try
            {
                if (bus.UpdateTaskStatus(t.MaTask, newStatus))
                {
                    LoadTasks();
                    LoadReports();
                    MessageBox.Show($"Đã chuyển trạng thái Task #{t.MaTask} sang [{newStatus}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi cập nhật trạng thái: {ex.Message}"); }
        }

        private void btnDeleteTask_Click(object? sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0 || dgvTasks.SelectedRows[0].DataBoundItem is not CongViecDTO t) return;
            if (MessageBox.Show($"Xóa công việc [{t.TenTask}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try { bus.DeleteTask(t.MaTask); LoadTasks(); LoadReports(); }
                catch (Exception ex) { MessageBox.Show($"Lỗi xóa Task: {ex.Message}"); }
            }
        }

        private void btnFilterTask_Click(object? sender, EventArgs e) { try { LoadTasks(); } catch (Exception ex) { MessageBox.Show(ex.Message); } }

        private void btnResetFilter_Click(object? sender, EventArgs e)
        {
            txtSearchTask.Clear();
            if (cboFilterDuAn.Items.Count > 0) cboFilterDuAn.SelectedIndex = 0;
            if (cboFilterTrangThai.Items.Count > 0) cboFilterTrangThai.SelectedIndex = 0;
            try { LoadTasks(); } catch { }
        }

        private void dgvDuAn_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvDuAn.SelectedRows.Count > 0 && dgvDuAn.SelectedRows[0].DataBoundItem is DuAnDTO da)
            {
                txtTenDuAn.Text = da.TenDuAn;
                dtpNgayBD.Value = da.NgayBatDau;
                dtpNgayKT.Value = da.NgayKetThuc;
                cboTrangThaiDA.SelectedItem = da.TrangThai;
            }
        }

        private void btnAddDA_Click(object? sender, EventArgs e)
        {
            try
            {
                var da = new DuAnDTO { TenDuAn = txtTenDuAn.Text.Trim(), NgayBatDau = dtpNgayBD.Value, NgayKetThuc = dtpNgayKT.Value, TrangThai = cboTrangThaiDA.Text };
                if (bus.InsertDuAn(da)) { LoadDuAn(); MessageBox.Show("Thêm dự án thành công!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        private void btnUpdateDA_Click(object? sender, EventArgs e)
        {
            if (dgvDuAn.SelectedRows.Count == 0 || dgvDuAn.SelectedRows[0].DataBoundItem is not DuAnDTO sel) return;
            try
            {
                sel.TenDuAn = txtTenDuAn.Text.Trim();
                sel.NgayBatDau = dtpNgayBD.Value;
                sel.NgayKetThuc = dtpNgayKT.Value;
                sel.TrangThai = cboTrangThaiDA.Text;
                if (bus.UpdateDuAn(sel)) { LoadDuAn(); MessageBox.Show("Cập nhật dự án thành công!"); }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        private void btnDeleteDA_Click(object? sender, EventArgs e)
        {
            if (dgvDuAn.SelectedRows.Count == 0 || dgvDuAn.SelectedRows[0].DataBoundItem is not DuAnDTO sel) return;
            if (MessageBox.Show($"Xóa dự án [{sel.TenDuAn}]?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { bus.DeleteDuAn(sel.MaDuAn); LoadDuAn(); }
                catch (Exception ex) { MessageBox.Show($"Lỗi xóa dự án: {ex.Message}"); }
            }
        }

        private void btnClearDA_Click(object? sender, EventArgs e) { txtTenDuAn.Clear(); dtpNgayBD.Value = DateTime.Today; dtpNgayKT.Value = DateTime.Today.AddMonths(1); }

        private void btnRefreshReport_Click(object? sender, EventArgs e) { try { LoadReports(); } catch (Exception ex) { MessageBox.Show(ex.Message); } }

        private void UpdateClock() => sslClock.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        private void timerClock_Tick(object? sender, EventArgs e) => UpdateClock();
    }
}
