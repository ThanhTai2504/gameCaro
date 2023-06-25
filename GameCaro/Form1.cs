using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class frmCoCaro : Form
    {
        private CaroChess caroChess;
        private Graphics grs;
        public frmCoCaro()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            btnPlayvsPlayer.Click += new EventHandler(PvsP);
            toolStripMenuItem2.Click += new EventHandler(PvsC_Click);
            btnPlayvsAI.Click += new EventHandler(PvsC_Click);
            caroChess.KhoiTaoMangOco();
            grs = pnlBanco.CreateGraphics();
        }

        private void frmCoCaro_Load(object sender, EventArgs e)
        {
            lblChuoichu.Text = "                     Chào các bạn!\n" +
                "************************************************\n " +
                "Có lẽ bạn đã quen thuộc với game Caro rồi.\n " +
                "Nhưng bạn nên chú ý một số vấn đề sau:\n" +
                "************************************************\n" +
                " + Bất kì người chơi nào được 5 quân liên tiếp là thắng.\n " +
                "+ Nếu bàn cờ đầy thì hòa cờ \n" +
                "+ Nếu chuỗi 5 quân bị chặn 2 đầu trò chơi vẫn tiếp tục\n" +
                "*************Chúc các bạn vui vẻ*************\n";
            tmChuChay.Enabled = true;

        }

        private void tmChuChay_Tick(object sender, EventArgs e)
        {
            lblChuoichu.Location = new Point(lblChuoichu.Location.X, lblChuoichu.Location.Y - 8);
            if (lblChuoichu.Location.Y + lblChuoichu.Height < 0)
            {
                lblChuoichu.Location = new Point(lblChuoichu.Location.X, pnlChuChay.Height);
            }
        }

        private void pnlBanco_Paint(object sender, PaintEventArgs e)
        {
            caroChess.VeBanCo(grs);
            caroChess.VeLaiQuanCo(grs);
        }

        // Kiểm tra chiến thắng 
        private void pnlBanco_MouseClick(object sender, MouseEventArgs e)
        {
            if (!caroChess.SanSang)
                return;

            caroChess.DanhCo(e.X, e.Y, grs);  // e là mouse người dùng click vào
                                              // X,Y là cột , dòng
            if (caroChess.KiemTraChienThang())
                caroChess.KetThucTroChoi();
            else
            {

                if (caroChess.CheDoChoi == 2)
                {
                    caroChess.KhoiDongComputer(grs);
                    if (caroChess.KiemTraChienThang())
                        caroChess.KetThucTroChoi();
                }
            }
        }

        private void PvsP(object sender, EventArgs e) // menu 1
        {
            MessageBox.Show("Bắt đầu chơi với player", "Thông báo");
            grs.Clear(pnlBanco.BackColor);
            caroChess.StarPlayervsPlayer(grs);
        }



        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            caroChess.Undo(grs);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            caroChess.Redo(grs);
        }

        private void PvsC_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bắt đầu chơi với máy", "Thông báo");
         
            grs.Clear(pnlBanco.BackColor);
            caroChess.StarPlayervsCom(grs);
        }

        private void btnPlayvsPlayer_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có chắc muốn thoát không?",
                "Error", MessageBoxButtons.YesNoCancel);
            Application.Exit();
        }
    }
}
