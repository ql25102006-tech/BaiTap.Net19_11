namespace Bai6_4_GarageManagement.GUI
{
    partial class frmGarageMain
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
            tabSuaChua = new TabPage();
            dgvChiTiet = new DataGridView();
            pnlSCBottom = new Panel();
            btnLuuPhieu = new Button();
            btnXoaCT = new Button();
            txtTongTienSC = new TextBox();
            lblTongSC = new Label();
            grpSCInput = new GroupBox();
            btnThemCT = new Button();
            txtTienCong = new TextBox();
            lblTienCong = new Label();
            numSLPT = new NumericUpDown();
            lblSLPT = new Label();
            cboPhuTung = new ComboBox();
            lblChonPT = new Label();
            txtNoiDungSua = new TextBox();
            lblNoiDung = new Label();
            cboBienSoXe = new ComboBox();
            lblBienSoSC = new Label();
            tabTiepNhan = new TabPage();
            dgvXe = new DataGridView();
            grpTimXe = new GroupBox();
            btnReloadXe = new Button();
            btnTimXe = new Button();
            txtTimBienSo = new TextBox();
            lblTimBS = new Label();
            grpXeInput = new GroupBox();
            btnThemXe = new Button();
            numNamSX = new NumericUpDown();
            lblNamSX = new Label();
            cboHieuXe = new ComboBox();
            lblHieuXe = new Label();
            txtBienSoMoi = new TextBox();
            lblBSMoi = new Label();
            txtDiaChiKH = new TextBox();
            lblDiaChiKH = new Label();
            txtDTKH = new TextBox();
            lblDTKH = new Label();
            txtTenKH = new TextBox();
            lblTenKH = new Label();
            tabPhuTung = new TabPage();
            dgvPhuTung = new DataGridView();
            grpPTInput = new GroupBox();
            btnDeletePT = new Button();
            btnUpdatePT = new Button();
            btnAddPT = new Button();
            numTonPT = new NumericUpDown();
            lblTonPT = new Label();
            txtGiaPT = new TextBox();
            lblGiaPT = new Label();
            txtTenPT = new TextBox();
            lblTenPT = new Label();
            tabHoaDon = new TabPage();
            dgvPhieu = new DataGridView();
            pnlHDTop = new Panel();
            lblTongDoanhThu = new Label();
            btnReloadPhieu = new Button();
            btnThanhToanPhieu = new Button();
            statusStrip1 = new StatusStrip();
            sslStatus = new ToolStripStatusLabel();
            sslClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            errorProvider1 = new ErrorProvider(components);
            tabMain.SuspendLayout(); tabSuaChua.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            pnlSCBottom.SuspendLayout(); grpSCInput.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)numSLPT).BeginInit();
            tabTiepNhan.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvXe).BeginInit(); grpTimXe.SuspendLayout();
            grpXeInput.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)numNamSX).BeginInit();
            tabPhuTung.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvPhuTung).BeginInit();
            grpPTInput.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)numTonPT).BeginInit();
            tabHoaDon.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvPhieu).BeginInit(); pnlHDTop.SuspendLayout();
            statusStrip1.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();

            // tabMain
            tabMain.Controls.AddRange(new Control[] { tabSuaChua, tabTiepNhan, tabPhuTung, tabHoaDon });
            tabMain.Dock = DockStyle.Fill; tabMain.Size = new Size(1060, 624);

            // Tab 1: Lập Phiếu Sửa chữa
            tabSuaChua.Controls.Add(dgvChiTiet); tabSuaChua.Controls.Add(pnlSCBottom); tabSuaChua.Controls.Add(grpSCInput);
            tabSuaChua.Padding = new Padding(8); tabSuaChua.Text = "Lập Phiếu Sửa chữa (SqlTransaction)"; tabSuaChua.UseVisualStyleBackColor = true;
            grpSCInput.Controls.AddRange(new Control[] { lblBienSoSC, cboBienSoXe, lblNoiDung, txtNoiDungSua, lblChonPT, cboPhuTung, lblSLPT, numSLPT, lblTienCong, txtTienCong, btnThemCT });
            grpSCInput.Dock = DockStyle.Top; grpSCInput.Size = new Size(1036, 125); grpSCInput.Text = "Thông tin Phiếu Sửa chữa & Thay thế Phụ tùng";
            lblBienSoSC.AutoSize = true; lblBienSoSC.Location = new Point(15, 32); lblBienSoSC.Text = "Chọn xe (Biển số):";
            cboBienSoXe.DropDownStyle = ComboBoxStyle.DropDownList; cboBienSoXe.Location = new Point(145, 29); cboBienSoXe.Size = new Size(280, 28);
            lblNoiDung.AutoSize = true; lblNoiDung.Location = new Point(445, 32); lblNoiDung.Text = "Nội dung sửa:";
            txtNoiDungSua.Location = new Point(550, 29); txtNoiDungSua.Size = new Size(465, 27);
            lblChonPT.AutoSize = true; lblChonPT.Location = new Point(15, 76); lblChonPT.Text = "Phụ tùng thay thế:";
            cboPhuTung.DropDownStyle = ComboBoxStyle.DropDownList; cboPhuTung.Location = new Point(145, 73); cboPhuTung.Size = new Size(340, 28);
            lblSLPT.AutoSize = true; lblSLPT.Location = new Point(500, 76); lblSLPT.Text = "SL:";
            numSLPT.Location = new Point(530, 73); numSLPT.Minimum = 1; numSLPT.Maximum = 1000; numSLPT.Value = 1; numSLPT.Size = new Size(65, 27);
            lblTienCong.AutoSize = true; lblTienCong.Location = new Point(610, 76); lblTienCong.Text = "Tiền công:";
            txtTienCong.Location = new Point(690, 73); txtTienCong.Size = new Size(125, 27); txtTienCong.Text = "150000";
            btnThemCT.Location = new Point(830, 70); btnThemCT.Size = new Size(185, 32); btnThemCT.Text = "Thêm phụ tùng vào phiếu"; btnThemCT.UseVisualStyleBackColor = true; btnThemCT.Click += btnThemCT_Click;
            pnlSCBottom.Controls.AddRange(new Control[] { lblTongSC, txtTongTienSC, btnXoaCT, btnLuuPhieu });
            pnlSCBottom.Dock = DockStyle.Bottom; pnlSCBottom.Size = new Size(1036, 70);
            lblTongSC.AutoSize = true; lblTongSC.Font = new Font("Segoe UI", 10F, FontStyle.Bold); lblTongSC.Location = new Point(15, 22); lblTongSC.Text = "Tổng chi phí sửa chữa:";
            txtTongTienSC.Font = new Font("Segoe UI", 10F, FontStyle.Bold); txtTongTienSC.Location = new Point(195, 19); txtTongTienSC.ReadOnly = true; txtTongTienSC.Size = new Size(210, 30); txtTongTienSC.Text = "0 VNĐ"; txtTongTienSC.TextAlign = HorizontalAlignment.Right;
            btnXoaCT.Location = new Point(425, 16); btnXoaCT.Size = new Size(160, 36); btnXoaCT.Text = "Xóa dòng chi tiết"; btnXoaCT.UseVisualStyleBackColor = true; btnXoaCT.Click += btnXoaCT_Click;
            btnLuuPhieu.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnLuuPhieu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); btnLuuPhieu.Location = new Point(725, 14); btnLuuPhieu.Size = new Size(295, 40); btnLuuPhieu.Text = "Hoàn tất & Lưu Phiếu (SqlTransaction)"; btnLuuPhieu.UseVisualStyleBackColor = true; btnLuuPhieu.Click += btnLuuPhieu_Click;
            dgvChiTiet.AllowUserToAddRows = false; dgvChiTiet.AllowUserToDeleteRows = false; dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvChiTiet.Dock = DockStyle.Fill; dgvChiTiet.ReadOnly = true; dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tab 2: Tiếp nhận Xe & Chủ xe
            tabTiepNhan.Controls.Add(dgvXe); tabTiepNhan.Controls.Add(grpTimXe); tabTiepNhan.Controls.Add(grpXeInput);
            tabTiepNhan.Padding = new Padding(8); tabTiepNhan.Text = "Tiếp nhận Xe & Chủ xe"; tabTiepNhan.UseVisualStyleBackColor = true;
            grpXeInput.Controls.AddRange(new Control[] { lblTenKH, txtTenKH, lblDTKH, txtDTKH, lblDiaChiKH, txtDiaChiKH, lblBSMoi, txtBienSoMoi, lblHieuXe, cboHieuXe, lblNamSX, numNamSX, btnThemXe });
            grpXeInput.Dock = DockStyle.Top; grpXeInput.Size = new Size(1036, 125); grpXeInput.Text = "Tiếp nhận Xe mới & Thông tin Chủ xe";
            lblTenKH.AutoSize = true; lblTenKH.Location = new Point(15, 32); lblTenKH.Text = "Tên chủ xe:";
            txtTenKH.Location = new Point(105, 29); txtTenKH.Size = new Size(200, 27);
            lblDTKH.AutoSize = true; lblDTKH.Location = new Point(320, 32); lblDTKH.Text = "Điện thoại:";
            txtDTKH.Location = new Point(405, 29); txtDTKH.Size = new Size(160, 27);
            lblDiaChiKH.AutoSize = true; lblDiaChiKH.Location = new Point(580, 32); lblDiaChiKH.Text = "Địa chỉ:";
            txtDiaChiKH.Location = new Point(645, 29); txtDiaChiKH.Size = new Size(370, 27);
            lblBSMoi.AutoSize = true; lblBSMoi.Location = new Point(15, 76); lblBSMoi.Text = "Biển số xe:";
            txtBienSoMoi.Location = new Point(105, 73); txtBienSoMoi.Size = new Size(200, 27);
            lblHieuXe.AutoSize = true; lblHieuXe.Location = new Point(320, 76); lblHieuXe.Text = "Hiệu xe:";
            cboHieuXe.DropDownStyle = ComboBoxStyle.DropDownList; cboHieuXe.Location = new Point(405, 73); cboHieuXe.Size = new Size(160, 28);
            lblNamSX.AutoSize = true; lblNamSX.Location = new Point(580, 76); lblNamSX.Text = "Năm SX:";
            numNamSX.Location = new Point(645, 73); numNamSX.Minimum = 1990; numNamSX.Maximum = 2030; numNamSX.Value = 2024; numNamSX.Size = new Size(90, 27);
            btnThemXe.Location = new Point(765, 70); btnThemXe.Size = new Size(250, 34); btnThemXe.Text = "Lưu Xe mới & Chủ xe"; btnThemXe.UseVisualStyleBackColor = true; btnThemXe.Click += btnThemXe_Click;
            grpTimXe.Controls.AddRange(new Control[] { lblTimBS, txtTimBienSo, btnTimXe, btnReloadXe });
            grpTimXe.Dock = DockStyle.Top; grpTimXe.Size = new Size(1036, 65); grpTimXe.Text = "Tra cứu Biển số xe (SqlParameter)";
            lblTimBS.AutoSize = true; lblTimBS.Location = new Point(15, 28); lblTimBS.Text = "Nhập biển số / Tên chủ xe:";
            txtTimBienSo.Location = new Point(200, 25); txtTimBienSo.Size = new Size(340, 27);
            btnTimXe.Location = new Point(555, 23); btnTimXe.Size = new Size(135, 30); btnTimXe.Text = "Tra cứu xe"; btnTimXe.UseVisualStyleBackColor = true; btnTimXe.Click += btnTimXe_Click;
            btnReloadXe.Location = new Point(705, 23); btnReloadXe.Size = new Size(125, 30); btnReloadXe.Text = "Tải lại"; btnReloadXe.UseVisualStyleBackColor = true; btnReloadXe.Click += btnReloadXe_Click;
            dgvXe.AllowUserToAddRows = false; dgvXe.AllowUserToDeleteRows = false; dgvXe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvXe.Dock = DockStyle.Fill; dgvXe.ReadOnly = true; dgvXe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tab 3: Kho Phụ tùng
            tabPhuTung.Controls.Add(dgvPhuTung); tabPhuTung.Controls.Add(grpPTInput);
            tabPhuTung.Padding = new Padding(8); tabPhuTung.Text = "Quản lý Kho Phụ tùng (CRUD)"; tabPhuTung.UseVisualStyleBackColor = true;
            grpPTInput.Controls.AddRange(new Control[] { lblTenPT, txtTenPT, lblGiaPT, txtGiaPT, lblTonPT, numTonPT, btnAddPT, btnUpdatePT, btnDeletePT });
            grpPTInput.Dock = DockStyle.Top; grpPTInput.Size = new Size(1036, 115); grpPTInput.Text = "Thông tin Phụ tùng / Vật tư";
            lblTenPT.AutoSize = true; lblTenPT.Location = new Point(15, 32); lblTenPT.Text = "Tên phụ tùng:";
            txtTenPT.Location = new Point(120, 29); txtTenPT.Size = new Size(320, 27);
            lblGiaPT.AutoSize = true; lblGiaPT.Location = new Point(460, 32); lblGiaPT.Text = "Đơn giá:";
            txtGiaPT.Location = new Point(530, 29); txtGiaPT.Size = new Size(160, 27);
            lblTonPT.AutoSize = true; lblTonPT.Location = new Point(710, 32); lblTonPT.Text = "Tồn kho:";
            numTonPT.Location = new Point(780, 29); numTonPT.Maximum = 100000; numTonPT.Size = new Size(100, 27);
            btnAddPT.Location = new Point(120, 68); btnAddPT.Size = new Size(150, 34); btnAddPT.Text = "Thêm phụ tùng"; btnAddPT.UseVisualStyleBackColor = true; btnAddPT.Click += btnAddPT_Click;
            btnUpdatePT.Location = new Point(285, 68); btnUpdatePT.Size = new Size(150, 34); btnUpdatePT.Text = "Cập nhật giá/tồn"; btnUpdatePT.UseVisualStyleBackColor = true; btnUpdatePT.Click += btnUpdatePT_Click;
            btnDeletePT.Location = new Point(450, 68); btnDeletePT.Size = new Size(150, 34); btnDeletePT.Text = "Xóa phụ tùng"; btnDeletePT.UseVisualStyleBackColor = true; btnDeletePT.Click += btnDeletePT_Click;
            dgvPhuTung.AllowUserToAddRows = false; dgvPhuTung.AllowUserToDeleteRows = false; dgvPhuTung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvPhuTung.Dock = DockStyle.Fill; dgvPhuTung.ReadOnly = true; dgvPhuTung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhuTung.SelectionChanged += dgvPhuTung_SelectionChanged;

            // Tab 4: Hóa đơn & Thanh toán
            tabHoaDon.Controls.Add(dgvPhieu); tabHoaDon.Controls.Add(pnlHDTop);
            tabHoaDon.Padding = new Padding(8); tabHoaDon.Text = "Danh sách Phiếu & Thanh toán"; tabHoaDon.UseVisualStyleBackColor = true;
            pnlHDTop.Controls.AddRange(new Control[] { btnThanhToanPhieu, btnReloadPhieu, lblTongDoanhThu });
            pnlHDTop.Dock = DockStyle.Top; pnlHDTop.Size = new Size(1036, 55);
            btnThanhToanPhieu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); btnThanhToanPhieu.Location = new Point(10, 10); btnThanhToanPhieu.Size = new Size(260, 36); btnThanhToanPhieu.Text = "Xác nhận Thanh toán Phiếu đã chọn"; btnThanhToanPhieu.UseVisualStyleBackColor = true; btnThanhToanPhieu.Click += btnThanhToanPhieu_Click;
            btnReloadPhieu.Location = new Point(285, 10); btnReloadPhieu.Size = new Size(140, 36); btnReloadPhieu.Text = "Tải lại danh sách"; btnReloadPhieu.UseVisualStyleBackColor = true; btnReloadPhieu.Click += btnReloadPhieu_Click;
            lblTongDoanhThu.Anchor = AnchorStyles.Top | AnchorStyles.Right; lblTongDoanhThu.AutoSize = true; lblTongDoanhThu.Font = new Font("Segoe UI", 10F, FontStyle.Bold); lblTongDoanhThu.Location = new Point(680, 16); lblTongDoanhThu.Text = "Tổng doanh thu đã thanh toán: 0 VNĐ";
            dgvPhieu.AllowUserToAddRows = false; dgvPhieu.AllowUserToDeleteRows = false; dgvPhieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvPhieu.Dock = DockStyle.Fill; dgvPhieu.ReadOnly = true; dgvPhieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // statusStrip1
            statusStrip1.Items.AddRange(new ToolStripItem[] { sslStatus, sslClock });
            sslStatus.Spring = true; sslStatus.Text = "Sẵn sàng - Kiến trúc 3-Tier (GarageDB)"; sslStatus.TextAlign = ContentAlignment.MiddleLeft;
            sslClock.Text = "Thời gian: 00:00:00";
            timerClock.Enabled = true; timerClock.Interval = 1000; timerClock.Tick += timerClock_Tick;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink; errorProvider1.ContainerControl = this;

            AutoScaleDimensions = new SizeF(8F, 20F); AutoScaleMode = AutoScaleMode.Font; ClientSize = new Size(1060, 650);
            Controls.Add(tabMain); Controls.Add(statusStrip1); MinimumSize = new Size(960, 580); StartPosition = FormStartPosition.CenterScreen;
            Text = "Bài 6.4: Hệ thống Quản lý Garage & Bảo dưỡng Xe Ô tô (Garage Management System)";
            Load += frmGarageMain_Load;
            tabMain.ResumeLayout(false); tabSuaChua.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            pnlSCBottom.ResumeLayout(false); pnlSCBottom.PerformLayout(); grpSCInput.ResumeLayout(false); grpSCInput.PerformLayout(); ((System.ComponentModel.ISupportInitialize)numSLPT).EndInit();
            tabTiepNhan.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvXe).EndInit(); grpTimXe.ResumeLayout(false); grpTimXe.PerformLayout();
            grpXeInput.ResumeLayout(false); grpXeInput.PerformLayout(); ((System.ComponentModel.ISupportInitialize)numNamSX).EndInit();
            tabPhuTung.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvPhuTung).EndInit();
            grpPTInput.ResumeLayout(false); grpPTInput.PerformLayout(); ((System.ComponentModel.ISupportInitialize)numTonPT).EndInit();
            tabHoaDon.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvPhieu).EndInit(); pnlHDTop.ResumeLayout(false); pnlHDTop.PerformLayout();
            statusStrip1.ResumeLayout(false); statusStrip1.PerformLayout(); ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false); PerformLayout();
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabSuaChua, tabTiepNhan, tabPhuTung, tabHoaDon;
        private GroupBox grpSCInput, grpXeInput, grpTimXe, grpPTInput;
        private Panel pnlSCBottom, pnlHDTop;
        private Label lblBienSoSC, lblNoiDung, lblChonPT, lblSLPT, lblTienCong, lblTongSC;
        private Label lblTenKH, lblDTKH, lblDiaChiKH, lblBSMoi, lblHieuXe, lblNamSX, lblTimBS;
        private Label lblTenPT, lblGiaPT, lblTonPT, lblTongDoanhThu;
        private ComboBox cboBienSoXe, cboPhuTung, cboHieuXe;
        private TextBox txtNoiDungSua, txtTienCong, txtTongTienSC, txtTenKH, txtDTKH, txtDiaChiKH, txtBienSoMoi, txtTimBienSo, txtTenPT, txtGiaPT;
        private NumericUpDown numSLPT, numNamSX, numTonPT;
        private Button btnThemCT, btnXoaCT, btnLuuPhieu, btnThemXe, btnTimXe, btnReloadXe, btnAddPT, btnUpdatePT, btnDeletePT, btnThanhToanPhieu, btnReloadPhieu;
        private DataGridView dgvChiTiet, dgvXe, dgvPhuTung, dgvPhieu;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel sslStatus, sslClock;
        private System.Windows.Forms.Timer timerClock;
        private ErrorProvider errorProvider1;
    }
}
