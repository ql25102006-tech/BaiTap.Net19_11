namespace Bai6_3_ProjectTaskManagement.GUI
{
    partial class frmProjectTaskMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabMain = new TabControl();
            tabTasks = new TabPage();
            dgvTasks = new DataGridView();
            grpFilterTask = new GroupBox();
            btnResetFilter = new Button();
            btnFilterTask = new Button();
            txtSearchTask = new TextBox();
            lblSearchTask = new Label();
            cboFilterTrangThai = new ComboBox();
            lblFilterTT = new Label();
            cboFilterDuAn = new ComboBox();
            lblFilterDA = new Label();
            grpTaskInput = new GroupBox();
            btnDeleteTask = new Button();
            btnUpdateStatus = new Button();
            btnAddTask = new Button();
            clbThanhVien = new CheckedListBox();
            lblPhanCong = new Label();
            cboTrangThaiTask = new ComboBox();
            lblTTTask = new Label();
            cboDoUuTien = new ComboBox();
            lblUuTien = new Label();
            dtpHanNop = new DateTimePicker();
            lblHanNop = new Label();
            txtMoTa = new TextBox();
            lblMoTa = new Label();
            txtTenTask = new TextBox();
            lblTenTask = new Label();
            cboTaskDuAn = new ComboBox();
            lblTaskDA = new Label();
            tabDuAn = new TabPage();
            dgvDuAn = new DataGridView();
            grpDAInput = new GroupBox();
            btnClearDA = new Button();
            btnDeleteDA = new Button();
            btnUpdateDA = new Button();
            btnAddDA = new Button();
            cboTrangThaiDA = new ComboBox();
            lblTTDA = new Label();
            dtpNgayKT = new DateTimePicker();
            lblNgayKT = new Label();
            dtpNgayBD = new DateTimePicker();
            lblNgayBD = new Label();
            txtTenDuAn = new TextBox();
            lblTenDA = new Label();
            tabBaoCao = new TabPage();
            grpThongKe = new GroupBox();
            dgvThongKe = new DataGridView();
            grpOverdue = new GroupBox();
            dgvOverdue = new DataGridView();
            pnlBaoCaoTop = new Panel();
            btnRefreshReport = new Button();
            lblReportTitle = new Label();
            statusStrip1 = new StatusStrip();
            sslStatus = new ToolStripStatusLabel();
            sslClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            errorProvider1 = new ErrorProvider(components);
            tabMain.SuspendLayout();
            tabTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            grpFilterTask.SuspendLayout();
            grpTaskInput.SuspendLayout();
            tabDuAn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDuAn).BeginInit();
            grpDAInput.SuspendLayout();
            tabBaoCao.SuspendLayout();
            grpThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThongKe).BeginInit();
            grpOverdue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOverdue).BeginInit();
            pnlBaoCaoTop.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // tabMain
            tabMain.Controls.Add(tabTasks);
            tabMain.Controls.Add(tabDuAn);
            tabMain.Controls.Add(tabBaoCao);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.Size = new Size(1060, 624);
            tabMain.TabIndex = 0;
            // tabTasks
            tabTasks.Controls.Add(dgvTasks);
            tabTasks.Controls.Add(grpFilterTask);
            tabTasks.Controls.Add(grpTaskInput);
            tabTasks.Padding = new Padding(8);
            tabTasks.Text = "Quản lý Công việc & Phân công (Task)";
            tabTasks.UseVisualStyleBackColor = true;
            // grpTaskInput
            grpTaskInput.Controls.AddRange(new Control[] { lblTaskDA, cboTaskDuAn, lblTenTask, txtTenTask, lblMoTa, txtMoTa, lblHanNop, dtpHanNop, lblUuTien, cboDoUuTien, lblTTTask, cboTrangThaiTask, lblPhanCong, clbThanhVien, btnAddTask, btnUpdateStatus, btnDeleteTask });
            grpTaskInput.Dock = DockStyle.Top;
            grpTaskInput.Size = new Size(1036, 175);
            grpTaskInput.Text = "Thông tin Công việc & Giao việc (Phân công nhiều thành viên)";
            lblTaskDA.AutoSize = true; lblTaskDA.Location = new Point(15, 30); lblTaskDA.Text = "Dự án:";
            cboTaskDuAn.DropDownStyle = ComboBoxStyle.DropDownList; cboTaskDuAn.Location = new Point(95, 27); cboTaskDuAn.Size = new Size(250, 28);
            lblTenTask.AutoSize = true; lblTenTask.Location = new Point(365, 30); lblTenTask.Text = "Tên Task:";
            txtTenTask.Location = new Point(440, 27); txtTenTask.Size = new Size(260, 27);
            lblMoTa.AutoSize = true; lblMoTa.Location = new Point(15, 68); lblMoTa.Text = "Mô tả:";
            txtMoTa.Location = new Point(95, 65); txtMoTa.Size = new Size(605, 27);
            lblHanNop.AutoSize = true; lblHanNop.Location = new Point(15, 106); lblHanNop.Text = "Hạn nộp:";
            dtpHanNop.Format = DateTimePickerFormat.Custom; dtpHanNop.CustomFormat = "dd/MM/yyyy"; dtpHanNop.Location = new Point(95, 103); dtpHanNop.Size = new Size(135, 27);
            lblUuTien.AutoSize = true; lblUuTien.Location = new Point(245, 106); lblUuTien.Text = "Ưu tiên:";
            cboDoUuTien.DropDownStyle = ComboBoxStyle.DropDownList; cboDoUuTien.Location = new Point(310, 103); cboDoUuTien.Size = new Size(125, 28);
            lblTTTask.AutoSize = true; lblTTTask.Location = new Point(450, 106); lblTTTask.Text = "Trạng thái:";
            cboTrangThaiTask.DropDownStyle = ComboBoxStyle.DropDownList; cboTrangThaiTask.Location = new Point(530, 103); cboTrangThaiTask.Size = new Size(170, 28);
            lblPhanCong.AutoSize = true; lblPhanCong.Location = new Point(720, 25); lblPhanCong.Text = "Phân công thành viên:";
            clbThanhVien.CheckOnClick = true; clbThanhVien.Location = new Point(720, 48); clbThanhVien.Size = new Size(295, 112);
            btnAddTask.Location = new Point(95, 138); btnAddTask.Size = new Size(185, 30); btnAddTask.Text = "Thêm Task & Phân công"; btnAddTask.UseVisualStyleBackColor = true; btnAddTask.Click += btnAddTask_Click;
            btnUpdateStatus.Location = new Point(295, 138); btnUpdateStatus.Size = new Size(210, 30); btnUpdateStatus.Text = "Chuyển trạng thái (SQL Update)"; btnUpdateStatus.UseVisualStyleBackColor = true; btnUpdateStatus.Click += btnUpdateStatus_Click;
            btnDeleteTask.Location = new Point(520, 138); btnDeleteTask.Size = new Size(115, 30); btnDeleteTask.Text = "Xóa Task"; btnDeleteTask.UseVisualStyleBackColor = true; btnDeleteTask.Click += btnDeleteTask_Click;
            // grpFilterTask
            grpFilterTask.Controls.AddRange(new Control[] { lblFilterDA, cboFilterDuAn, lblFilterTT, cboFilterTrangThai, lblSearchTask, txtSearchTask, btnFilterTask, btnResetFilter });
            grpFilterTask.Dock = DockStyle.Top;
            grpFilterTask.Size = new Size(1036, 65);
            grpFilterTask.Text = "Lọc & Tìm kiếm Công việc";
            lblFilterDA.AutoSize = true; lblFilterDA.Location = new Point(15, 28); lblFilterDA.Text = "Dự án:";
            cboFilterDuAn.DropDownStyle = ComboBoxStyle.DropDownList; cboFilterDuAn.Location = new Point(70, 25); cboFilterDuAn.Size = new Size(200, 28);
            lblFilterTT.AutoSize = true; lblFilterTT.Location = new Point(285, 28); lblFilterTT.Text = "Trạng thái:";
            cboFilterTrangThai.DropDownStyle = ComboBoxStyle.DropDownList; cboFilterTrangThai.Location = new Point(365, 25); cboFilterTrangThai.Size = new Size(135, 28);
            lblSearchTask.AutoSize = true; lblSearchTask.Location = new Point(515, 28); lblSearchTask.Text = "Từ khóa:";
            txtSearchTask.Location = new Point(580, 25); txtSearchTask.Size = new Size(200, 27);
            btnFilterTask.Location = new Point(795, 23); btnFilterTask.Size = new Size(105, 30); btnFilterTask.Text = "Lọc Task"; btnFilterTask.UseVisualStyleBackColor = true; btnFilterTask.Click += btnFilterTask_Click;
            btnResetFilter.Location = new Point(910, 23); btnResetFilter.Size = new Size(105, 30); btnResetFilter.Text = "Tải lại"; btnResetFilter.UseVisualStyleBackColor = true; btnResetFilter.Click += btnResetFilter_Click;
            // dgvTasks
            dgvTasks.AllowUserToAddRows = false; dgvTasks.AllowUserToDeleteRows = false; dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.Dock = DockStyle.Fill; dgvTasks.MultiSelect = false; dgvTasks.ReadOnly = true; dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            // tabDuAn
            tabDuAn.Controls.Add(dgvDuAn);
            tabDuAn.Controls.Add(grpDAInput);
            tabDuAn.Padding = new Padding(8);
            tabDuAn.Text = "Quản lý Dự án (CRUD)";
            tabDuAn.UseVisualStyleBackColor = true;
            // grpDAInput
            grpDAInput.Controls.AddRange(new Control[] { lblTenDA, txtTenDuAn, lblNgayBD, dtpNgayBD, lblNgayKT, dtpNgayKT, lblTTDA, cboTrangThaiDA, btnAddDA, btnUpdateDA, btnDeleteDA, btnClearDA });
            grpDAInput.Dock = DockStyle.Top;
            grpDAInput.Size = new Size(1036, 125);
            grpDAInput.Text = "Thông tin Dự án";
            lblTenDA.AutoSize = true; lblTenDA.Location = new Point(15, 32); lblTenDA.Text = "Tên dự án:";
            txtTenDuAn.Location = new Point(105, 29); txtTenDuAn.Size = new Size(340, 27);
            lblTTDA.AutoSize = true; lblTTDA.Location = new Point(470, 32); lblTTDA.Text = "Trạng thái:";
            cboTrangThaiDA.DropDownStyle = ComboBoxStyle.DropDownList; cboTrangThaiDA.Location = new Point(555, 29); cboTrangThaiDA.Size = new Size(170, 28);
            lblNgayBD.AutoSize = true; lblNgayBD.Location = new Point(15, 74); lblNgayBD.Text = "Ngày bắt đầu:";
            dtpNgayBD.Format = DateTimePickerFormat.Custom; dtpNgayBD.CustomFormat = "dd/MM/yyyy"; dtpNgayBD.Location = new Point(120, 71); dtpNgayBD.Size = new Size(140, 27);
            lblNgayKT.AutoSize = true; lblNgayKT.Location = new Point(280, 74); lblNgayKT.Text = "Ngày kết thúc:";
            dtpNgayKT.Format = DateTimePickerFormat.Custom; dtpNgayKT.CustomFormat = "dd/MM/yyyy"; dtpNgayKT.Location = new Point(390, 71); dtpNgayKT.Size = new Size(140, 27);
            btnAddDA.Location = new Point(750, 26); btnAddDA.Size = new Size(125, 34); btnAddDA.Text = "Thêm dự án"; btnAddDA.UseVisualStyleBackColor = true; btnAddDA.Click += btnAddDA_Click;
            btnUpdateDA.Location = new Point(885, 26); btnUpdateDA.Size = new Size(125, 34); btnUpdateDA.Text = "Cập nhật"; btnUpdateDA.UseVisualStyleBackColor = true; btnUpdateDA.Click += btnUpdateDA_Click;
            btnDeleteDA.Location = new Point(750, 68); btnDeleteDA.Size = new Size(125, 34); btnDeleteDA.Text = "Xóa dự án"; btnDeleteDA.UseVisualStyleBackColor = true; btnDeleteDA.Click += btnDeleteDA_Click;
            btnClearDA.Location = new Point(885, 68); btnClearDA.Size = new Size(125, 34); btnClearDA.Text = "Làm mới"; btnClearDA.UseVisualStyleBackColor = true; btnClearDA.Click += btnClearDA_Click;
            // dgvDuAn
            dgvDuAn.AllowUserToAddRows = false; dgvDuAn.AllowUserToDeleteRows = false; dgvDuAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDuAn.Dock = DockStyle.Fill; dgvDuAn.MultiSelect = false; dgvDuAn.ReadOnly = true; dgvDuAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDuAn.SelectionChanged += dgvDuAn_SelectionChanged;
            // tabBaoCao
            tabBaoCao.Controls.Add(grpThongKe);
            tabBaoCao.Controls.Add(grpOverdue);
            tabBaoCao.Controls.Add(pnlBaoCaoTop);
            tabBaoCao.Padding = new Padding(8);
            tabBaoCao.Text = "Cảnh báo Trễ hạn & Báo cáo Tiến độ";
            tabBaoCao.UseVisualStyleBackColor = true;
            pnlBaoCaoTop.Controls.AddRange(new Control[] { lblReportTitle, btnRefreshReport });
            pnlBaoCaoTop.Dock = DockStyle.Top; pnlBaoCaoTop.Size = new Size(1036, 46);
            lblReportTitle.AutoSize = true; lblReportTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); lblReportTitle.Location = new Point(5, 12); lblReportTitle.Text = "Báo cáo Công việc Trễ hạn (SqlDataReader) & Tỷ lệ % Hoàn thành theo Thành viên (GROUP BY)";
            btnRefreshReport.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnRefreshReport.Location = new Point(850, 6); btnRefreshReport.Size = new Size(175, 32); btnRefreshReport.Text = "Cập nhật báo cáo"; btnRefreshReport.UseVisualStyleBackColor = true; btnRefreshReport.Click += btnRefreshReport_Click;
            grpOverdue.Controls.Add(dgvOverdue); grpOverdue.Dock = DockStyle.Top; grpOverdue.Padding = new Padding(6); grpOverdue.Size = new Size(1036, 250); grpOverdue.Text = "Cảnh báo Công việc Quá hạn (HanNop < Hiện tại & Trạng thái != Done)";
            dgvOverdue.AllowUserToAddRows = false; dgvOverdue.AllowUserToDeleteRows = false; dgvOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvOverdue.Dock = DockStyle.Fill; dgvOverdue.ReadOnly = true; dgvOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grpThongKe.Controls.Add(dgvThongKe); grpThongKe.Dock = DockStyle.Fill; grpThongKe.Padding = new Padding(6); grpThongKe.Text = "Thống kê Tỷ lệ % Hoàn thành Công việc của từng Thành viên";
            dgvThongKe.AllowUserToAddRows = false; dgvThongKe.AllowUserToDeleteRows = false; dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvThongKe.Dock = DockStyle.Fill; dgvThongKe.ReadOnly = true; dgvThongKe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // statusStrip1
            statusStrip1.Items.AddRange(new ToolStripItem[] { sslStatus, sslClock });
            sslStatus.Spring = true; sslStatus.Text = "Sẵn sàng - Kiến trúc 3-Tier (ProjectTaskDB)"; sslStatus.TextAlign = ContentAlignment.MiddleLeft;
            sslClock.Text = "Thời gian: 00:00:00";
            timerClock.Enabled = true; timerClock.Interval = 1000; timerClock.Tick += timerClock_Tick;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink; errorProvider1.ContainerControl = this;
            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 650);
            Controls.Add(tabMain);
            Controls.Add(statusStrip1);
            MinimumSize = new Size(960, 580);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bài 6.3: Hệ thống Quản lý Công việc & Tiến độ Dự án (Project & Task Management)";
            Load += frmProjectTaskMain_Load;
            tabMain.ResumeLayout(false); tabTasks.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            grpFilterTask.ResumeLayout(false); grpFilterTask.PerformLayout(); grpTaskInput.ResumeLayout(false); grpTaskInput.PerformLayout();
            tabDuAn.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvDuAn).EndInit(); grpDAInput.ResumeLayout(false); grpDAInput.PerformLayout();
            tabBaoCao.ResumeLayout(false); grpThongKe.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvThongKe).EndInit();
            grpOverdue.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvOverdue).EndInit(); pnlBaoCaoTop.ResumeLayout(false); pnlBaoCaoTop.PerformLayout();
            statusStrip1.ResumeLayout(false); statusStrip1.PerformLayout(); ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false); PerformLayout();
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabTasks, tabDuAn, tabBaoCao;
        private GroupBox grpTaskInput, grpFilterTask, grpDAInput, grpOverdue, grpThongKe;
        private Panel pnlBaoCaoTop;
        private Label lblTaskDA, lblTenTask, lblMoTa, lblHanNop, lblUuTien, lblTTTask, lblPhanCong;
        private Label lblFilterDA, lblFilterTT, lblSearchTask, lblTenDA, lblNgayBD, lblNgayKT, lblTTDA, lblReportTitle;
        private ComboBox cboTaskDuAn, cboDoUuTien, cboTrangThaiTask, cboFilterDuAn, cboFilterTrangThai, cboTrangThaiDA;
        private TextBox txtTenTask, txtMoTa, txtSearchTask, txtTenDuAn;
        private DateTimePicker dtpHanNop, dtpNgayBD, dtpNgayKT;
        private CheckedListBox clbThanhVien;
        private Button btnAddTask, btnUpdateStatus, btnDeleteTask, btnFilterTask, btnResetFilter;
        private Button btnAddDA, btnUpdateDA, btnDeleteDA, btnClearDA, btnRefreshReport;
        private DataGridView dgvTasks, dgvDuAn, dgvOverdue, dgvThongKe;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel sslStatus, sslClock;
        private System.Windows.Forms.Timer timerClock;
        private ErrorProvider errorProvider1;
    }
}
