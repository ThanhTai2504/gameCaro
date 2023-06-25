
namespace GameCaro
{
    partial class frmCoCaro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlChuChay = new System.Windows.Forms.Panel();
            this.lblChuoichu = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnPlayvsAI = new System.Windows.Forms.Button();
            this.btnPlayvsPlayer = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlBanco = new System.Windows.Forms.Panel();
            this.tmChuChay = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.chứcNăngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.pnlChuChay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lime;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pnlChuChay);
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.btnPlayvsAI);
            this.panel1.Controls.Add(this.btnPlayvsPlayer);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(850, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 700);
            this.panel1.TabIndex = 2;
            // 
            // pnlChuChay
            // 
            this.pnlChuChay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pnlChuChay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlChuChay.Controls.Add(this.lblChuoichu);
            this.pnlChuChay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlChuChay.Location = new System.Drawing.Point(0, 230);
            this.pnlChuChay.Name = "pnlChuChay";
            this.pnlChuChay.Size = new System.Drawing.Size(432, 224);
            this.pnlChuChay.TabIndex = 5;
            // 
            // lblChuoichu
            // 
            this.lblChuoichu.AutoSize = true;
            this.lblChuoichu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblChuoichu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuoichu.Location = new System.Drawing.Point(3, 163);
            this.lblChuoichu.Name = "lblChuoichu";
            this.lblChuoichu.Size = new System.Drawing.Size(78, 20);
            this.lblChuoichu.TabIndex = 0;
            this.lblChuoichu.Text = "Luat choi";
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(165, 630);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(123, 47);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnPlayvsAI
            // 
            this.btnPlayvsAI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPlayvsAI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayvsAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayvsAI.ForeColor = System.Drawing.Color.White;
            this.btnPlayvsAI.Location = new System.Drawing.Point(98, 555);
            this.btnPlayvsAI.Name = "btnPlayvsAI";
            this.btnPlayvsAI.Size = new System.Drawing.Size(260, 69);
            this.btnPlayvsAI.TabIndex = 5;
            this.btnPlayvsAI.Text = "Player vs AI";
            this.btnPlayvsAI.UseVisualStyleBackColor = false;
            // 
            // btnPlayvsPlayer
            // 
            this.btnPlayvsPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPlayvsPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayvsPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayvsPlayer.ForeColor = System.Drawing.Color.White;
            this.btnPlayvsPlayer.Location = new System.Drawing.Point(98, 480);
            this.btnPlayvsPlayer.Name = "btnPlayvsPlayer";
            this.btnPlayvsPlayer.Size = new System.Drawing.Size(260, 69);
            this.btnPlayvsPlayer.TabIndex = 4;
            this.btnPlayvsPlayer.Text = "Player vs Player";
            this.btnPlayvsPlayer.UseVisualStyleBackColor = false;
            this.btnPlayvsPlayer.Click += new System.EventHandler(this.btnPlayvsPlayer_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GameCaro.Properties.Resources.cao_thu_co_caro_640;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(434, 236);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pnlBanco
            // 
            this.pnlBanco.BackColor = System.Drawing.Color.Lime;
            this.pnlBanco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBanco.Location = new System.Drawing.Point(71, 27);
            this.pnlBanco.Name = "pnlBanco";
            this.pnlBanco.Size = new System.Drawing.Size(703, 703);
            this.pnlBanco.TabIndex = 4;
            this.pnlBanco.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBanco_Paint);
            this.pnlBanco.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlBanco_MouseClick);
            // 
            // tmChuChay
            // 
            this.tmChuChay.Interval = 500;
            this.tmChuChay.Tick += new System.EventHandler(this.tmChuChay_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chứcNăngToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1303, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // chứcNăngToolStripMenuItem
            // 
            this.chứcNăngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.chứcNăngToolStripMenuItem.Name = "chứcNăngToolStripMenuItem";
            this.chứcNăngToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.chứcNăngToolStripMenuItem.Text = "Menu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 26);
            this.toolStripMenuItem1.Text = "Player 1 vs Player 2";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.PvsP);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.toolStripMenuItem2.Size = new System.Drawing.Size(241, 26);
            this.toolStripMenuItem2.Text = "Player vs AI";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(128, 26);
            this.toolStripMenuItem3.Text = "Undo";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(128, 26);
            this.toolStripMenuItem4.Text = "Redo";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // frmCoCaro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1303, 789);
            this.Controls.Add(this.pnlBanco);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCoCaro";
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmCoCaro_Load);
            this.panel1.ResumeLayout(false);
            this.pnlChuChay.ResumeLayout(false);
            this.pnlChuChay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnPlayvsAI;
        private System.Windows.Forms.Button btnPlayvsPlayer;
        private System.Windows.Forms.Panel pnlBanco;
        private System.Windows.Forms.Label lblChuoichu;
        private System.Windows.Forms.Timer tmChuChay;
        private System.Windows.Forms.Panel pnlChuChay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chứcNăngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    }
}

