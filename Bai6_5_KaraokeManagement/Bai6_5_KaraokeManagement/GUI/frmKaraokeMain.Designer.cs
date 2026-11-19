namespace Bai6_5_KaraokeManagement.GUI
{
    partial class frmKaraokeMain
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
            tabSoDoPhong = new TabPage();
            splitRoom = new SplitContainer();
            grpRooms = new GroupBox();
            flpRooms = new FlowLayoutPanel();
            grpRoomDetail = new GroupBox();
            dgvDichVuPhong = new DataGridView();
            pnlRoomBottom = new Panel();
            btnThanhToan = new Button();
            txtTienDV = new TextBox();
            lblTienDV = new Label();
            pnlRoomTop = new Panel();
            btnThemDV = new Button();
            numSLDV = new NumericUpDown();
            lblSLDV = new Label();
            cboDichVu = new ComboBox();
            lblChonDV = new Label();
            btnMoPhong = new Button();
            lblGioVao = new Label();
            lblRoomSelected = new Label();
            tabDichVu = new TabPage();
            dgvDichVu = new DataGridView();
            grpDVInput = new GroupBox();
            btnDeleteDV = new Button();
            btnUpdateDV = new Button();
            btnAddDV = new Button();
            numTonDV = new NumericUpDown();
            lblTonDV = new Label();
            txtGiaDV = new TextBox();
            lblGiaDV = new Label();
            txtTenDV = new TextBox();
            lblTenDV = new Label();
            tabBaoCao = new TabPage();
            dgvHoaDon = new DataGridView();
            pnlBCTop = new Panel();
            lblTongDoanhThu = new Label();
            btnReloadBC = new Button();
            statusStrip1 = new StatusStrip();
            sslStatus = new ToolStripStatusLabel();
            sslClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            errorProvider1 = new ErrorProvider(components);
            tabMain.SuspendLayout(); tabSoDoPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitRoom).BeginInit();
            splitRoom.Panel1.SuspendLayout(); splitRoom.Panel2.SuspendLayout(); splitRoom.SuspendLayout();
            grpRooms.SuspendLayout(); grpRoomDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDichVuPhong).BeginInit();
            pnlRoomBottom.SuspendLayout(); pnlRoomTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSLDV).BeginInit();
            tabDichVu.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            grpDVInput.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)numTonDV).BeginInit();
            tabBaoCao.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit(); pnlBCTop.SuspendLayout();
            statusStrip1.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();

            // tabMain
            tabMain.Controls.AddRange(new Control[] { tabSoDoPhong, tabDichVu, tabBaoCao });
            tabMain.Dock = DockStyle.Fill; tabMain.Size = new Size(1060, 624);

            // Tab 1: Sơ đồ phòng hát
            tabSoDoPhong.Controls.Add(splitRoom); tabSoDoPhong.Padding = new Padding(8);
            tabSoDoPhong.Text = "Sơ đồ Phòng hát & Gọi dịch vụ / Thanh toán"; tabSoDoPhong.UseVisualStyleBackColor = true;
            splitRoom.Dock = DockStyle.Fill; splitRoom.SplitterDistance = 420;
            splitRoom.Panel1.Controls.Add(grpRooms);
            splitRoom.Panel2.Controls.Add(grpRoomDetail);

            grpRooms.Controls.Add(flpRooms); grpRooms.Dock = DockStyle.Fill; grpRooms.Padding = new Padding(8);
            grpRooms.Text = "Sơ đồ Phòng hát (Xanh: Trống | Đỏ: Đang hát)";
            flpRooms.AutoScroll = true; flpRooms.Dock = DockStyle.Fill;

            grpRoomDetail.Controls.Add(dgvDichVuPhong);
            grpRoomDetail.Controls.Add(pnlRoomBottom);
            grpRoomDetail.Controls.Add(pnlRoomTop);
            grpRoomDetail.Dock = DockStyle.Fill; grpRoomDetail.Padding = new Padding(8);
            grpRoomDetail.Text = "Chi tiết Phòng hát & Gọi dịch vụ (Disconnected DataTable)";

            pnlRoomTop.Controls.AddRange(new Control[] { lblRoomSelected, lblGioVao, btnMoPhong, lblChonDV, cboDichVu, lblSLDV, numSLDV, btnThemDV });
            pnlRoomTop.Dock = DockStyle.Top; pnlRoomTop.Size = new Size(600, 120);
            lblRoomSelected.AutoSize = true; lblRoomSelected.Font = new Font("Segoe UI", 10F, FontStyle.Bold); lblRoomSelected.Location = new Point(10, 10); lblRoomSelected.Text = "Phòng đang chọn: [Chưa chọn]";
            lblGioVao.AutoSize = true; lblGioVao.Location = new Point(10, 38); lblGioVao.Text = "Giờ vào: --:-- | Trạng thái: --";
            btnMoPhong.Location = new Point(420, 12); btnMoPhong.Size = new Size(165, 36); btnMoPhong.Text = "Mở phòng hát"; btnMoPhong.UseVisualStyleBackColor = true; btnMoPhong.Click += btnMoPhong_Click;
            lblChonDV.AutoSize = true; lblChonDV.Location = new Point(10, 78); lblChonDV.Text = "Dịch vụ:";
            cboDichVu.DropDownStyle = ComboBoxStyle.DropDownList; cboDichVu.Location = new Point(75, 75); cboDichVu.Size = new Size(235, 28);
            lblSLDV.AutoSize = true; lblSLDV.Location = new Point(320, 78); lblSLDV.Text = "SL:";
            numSLDV.Location = new Point(350, 75); numSLDV.Minimum = 1; numSLDV.Maximum = 100; numSLDV.Value = 1; numSLDV.Size = new Size(60, 27);
            btnThemDV.Location = new Point(420, 72); btnThemDV.Size = new Size(165, 32); btnThemDV.Text = "Thêm dịch vụ"; btnThemDV.UseVisualStyleBackColor = true; btnThemDV.Click += btnThemDV_Click;

            pnlRoomBottom.Controls.AddRange(new Control[] { lblTienDV, txtTienDV, btnThanhToan });
            pnlRoomBottom.Dock = DockStyle.Bottom; pnlRoomBottom.Size = new Size(600, 65);
            lblTienDV.AutoSize = true; lblTienDV.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); lblTienDV.Location = new Point(10, 22); lblTienDV.Text = "Tiền dịch vụ:";
            txtTienDV.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); txtTienDV.Location = new Point(120, 18); txtTienDV.ReadOnly = true; txtTienDV.Size = new Size(175, 29); txtTienDV.Text = "0 VNĐ"; txtTienDV.TextAlign = HorizontalAlignment.Right;
            btnThanhToan.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnThanhToan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); btnThanhToan.Location = new Point(325, 12); btnThanhToan.Size = new Size(260, 40); btnThanhToan.Text = "Thanh toán & Trả phòng (Transaction)"; btnThanhToan.UseVisualStyleBackColor = true; btnThanhToan.Click += btnThanhToan_Click;

            dgvDichVuPhong.AllowUserToAddRows = false; dgvDichVuPhong.AllowUserToDeleteRows = false; dgvDichVuPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvDichVuPhong.Dock = DockStyle.Fill; dgvDichVuPhong.ReadOnly = true; dgvDichVuPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tab 2: Danh mục Dịch vụ
            tabDichVu.Controls.Add(dgvDichVu); tabDichVu.Controls.Add(grpDVInput); tabDichVu.Padding = new Padding(8);
            tabDichVu.Text = "Quản lý Dịch vụ / Đồ uống (CRUD)"; tabDichVu.UseVisualStyleBackColor = true;
            grpDVInput.Controls.AddRange(new Control[] { lblTenDV, txtTenDV, lblGiaDV, txtGiaDV, lblTonDV, numTonDV, btnAddDV, btnUpdateDV, btnDeleteDV });
            grpDVInput.Dock = DockStyle.Top; grpDVInput.Size = new Size(1036, 115); grpDVInput.Text = "Thông tin Mặt hàng Dịch vụ / Đồ uống";
            lblTenDV.AutoSize = true; lblTenDV.Location = new Point(15, 32); lblTenDV.Text = "Tên dịch vụ:";
            txtTenDV.Location = new Point(110, 29); txtTenDV.Size = new Size(310, 27);
            lblGiaDV.AutoSize = true; lblGiaDV.Location = new Point(445, 32); lblGiaDV.Text = "Đơn giá:";
            txtGiaDV.Location = new Point(515, 29); txtGiaDV.Size = new Size(160, 27);
            lblTonDV.AutoSize = true; lblTonDV.Location = new Point(700, 32); lblTonDV.Text = "Tồn kho:";
            numTonDV.Location = new Point(770, 29); numTonDV.Maximum = 100000; numTonDV.Size = new Size(100, 27);
            btnAddDV.Location = new Point(110, 68); btnAddDV.Size = new Size(140, 34); btnAddDV.Text = "Thêm dịch vụ"; btnAddDV.UseVisualStyleBackColor = true; btnAddDV.Click += btnAddDV_Click;
            btnUpdateDV.Location = new Point(265, 68); btnUpdateDV.Size = new Size(140, 34); btnUpdateDV.Text = "Cập nhật"; btnUpdateDV.UseVisualStyleBackColor = true; btnUpdateDV.Click += btnUpdateDV_Click;
            btnDeleteDV.Location = new Point(420, 68); btnDeleteDV.Size = new Size(140, 34); btnDeleteDV.Text = "Xóa dịch vụ"; btnDeleteDV.UseVisualStyleBackColor = true; btnDeleteDV.Click += btnDeleteDV_Click;
            dgvDichVu.AllowUserToAddRows = false; dgvDichVu.AllowUserToDeleteRows = false; dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvDichVu.Dock = DockStyle.Fill; dgvDichVu.ReadOnly = true; dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.SelectionChanged += dgvDichVu_SelectionChanged;

            // Tab 3: Báo cáo Doanh thu
            tabBaoCao.Controls.Add(dgvHoaDon); tabBaoCao.Controls.Add(pnlBCTop); tabBaoCao.Padding = new Padding(8);
            tabBaoCao.Text = "Báo cáo Doanh thu & Lịch sử Hóa đơn"; tabBaoCao.UseVisualStyleBackColor = true;
            pnlBCTop.Controls.AddRange(new Control[] { btnReloadBC, lblTongDoanhThu }); pnlBCTop.Dock = DockStyle.Top; pnlBCTop.Size = new Size(1036, 52);
            btnReloadBC.Location = new Point(10, 10); btnReloadBC.Size = new Size(175, 34); btnReloadBC.Text = "Cập nhật báo cáo"; btnReloadBC.UseVisualStyleBackColor = true; btnReloadBC.Click += btnReloadBC_Click;
            lblTongDoanhThu.Anchor = AnchorStyles.Top | AnchorStyles.Right; lblTongDoanhThu.AutoSize = true; lblTongDoanhThu.Font = new Font("Segoe UI", 10F, FontStyle.Bold); lblTongDoanhThu.Location = new Point(660, 16); lblTongDoanhThu.Text = "Tổng doanh thu Karaoke: 0 VNĐ";
            dgvHoaDon.AllowUserToAddRows = false; dgvHoaDon.AllowUserToDeleteRows = false; dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvHoaDon.Dock = DockStyle.Fill; dgvHoaDon.ReadOnly = true; dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // statusStrip1
            statusStrip1.Items.AddRange(new ToolStripItem[] { sslStatus, sslClock });
            sslStatus.Spring = true; sslStatus.Text = "Sẵn sàng - Kiến trúc 3-Tier (KaraokeDB)"; sslStatus.TextAlign = ContentAlignment.MiddleLeft;
            sslClock.Text = "Thời gian: 00:00:00";
            timerClock.Enabled = true; timerClock.Interval = 1000; timerClock.Tick += timerClock_Tick;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink; errorProvider1.ContainerControl = this;

            AutoScaleDimensions = new SizeF(8F, 20F); AutoScaleMode = AutoScaleMode.Font; ClientSize = new Size(1060, 650);
            Controls.Add(tabMain); Controls.Add(statusStrip1); MinimumSize = new Size(960, 580); StartPosition = FormStartPosition.CenterScreen;
            Text = "Bài 6.5: Hệ thống Quản lý Đặt phòng & Dịch vụ Karaoke (Karaoke Management System)";
            Load += frmKaraokeMain_Load;
            tabMain.ResumeLayout(false); tabSoDoPhong.ResumeLayout(false);
            splitRoom.Panel1.ResumeLayout(false); splitRoom.Panel2.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)splitRoom).EndInit(); splitRoom.ResumeLayout(false);
            grpRooms.ResumeLayout(false); grpRoomDetail.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvDichVuPhong).EndInit();
            pnlRoomBottom.ResumeLayout(false); pnlRoomBottom.PerformLayout(); pnlRoomTop.ResumeLayout(false); pnlRoomTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSLDV).EndInit(); tabDichVu.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvDichVu).EndInit();
            grpDVInput.ResumeLayout(false); grpDVInput.PerformLayout(); ((System.ComponentModel.ISupportInitialize)numTonDV).EndInit();
            tabBaoCao.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit(); pnlBCTop.ResumeLayout(false); pnlBCTop.PerformLayout();
            statusStrip1.ResumeLayout(false); statusStrip1.PerformLayout(); ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false); PerformLayout();
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabSoDoPhong, tabDichVu, tabBaoCao;
        private SplitContainer splitRoom;
        private GroupBox grpRooms, grpRoomDetail, grpDVInput;
        private FlowLayoutPanel flpRooms;
        private Panel pnlRoomTop, pnlRoomBottom, pnlBCTop;
        private Label lblRoomSelected, lblGioVao, lblChonDV, lblSLDV, lblTienDV, lblTenDV, lblGiaDV, lblTonDV, lblTongDoanhThu;
        private ComboBox cboDichVu;
        private NumericUpDown numSLDV, numTonDV;
        private TextBox txtTienDV, txtTenDV, txtGiaDV;
        private Button btnMoPhong, btnThemDV, btnThanhToan, btnAddDV, btnUpdateDV, btnDeleteDV, btnReloadBC;
        private DataGridView dgvDichVuPhong, dgvDichVu, dgvHoaDon;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel sslStatus, sslClock;
        private System.Windows.Forms.Timer timerClock;
        private ErrorProvider errorProvider1;
    }
}
