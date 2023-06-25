using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_Demo_Game_Co_Caro
{
    public partial class Form1 : Form
    {
        private Co_Caro _Co_Caro;
        private Graphics grp;
        private int OF;
        public Form1()
        {
            InitializeComponent();
            _Co_Caro = new Co_Caro();
            _Co_Caro.KTM_O_Co();
            grp = pnlBanCo.CreateGraphics();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblLuotDi.Text = _Co_Caro.lbl.Text;
            label3.Text = "Số nước đã đi";
            label4.Text = "O : 0";
            label5.Text = "X : 0";
            label9.Text = " A  B  C  D  E   F  G   H   I   J  K   L  M  N  O   P  Q  R  S  T";
            label10.Text = "1" + Environment.NewLine + "2" + Environment.NewLine + "3" + Environment.NewLine +
                "4" + Environment.NewLine + "5" + Environment.NewLine + "6" + Environment.NewLine +
                "7" + Environment.NewLine + "8" + Environment.NewLine + "9" + Environment.NewLine +
                "10" + Environment.NewLine + "11" + Environment.NewLine + "12" + Environment.NewLine +
                "13" + Environment.NewLine + "14" + Environment.NewLine + "15" + Environment.NewLine +
                "16" + Environment.NewLine + "17" + Environment.NewLine + "18" + Environment.NewLine +
                "19" + Environment.NewLine + "20" + Environment.NewLine;
            label1.Text = "-O đi trước, X đi sau";
            label2.Text = "-Bên nào có 5 quân cùng" + Environment.NewLine + "màu xếp thành hàng ngang," + Environment.NewLine
                + "hàng dọc, đường chéo" + Environment.NewLine + "trước thì thắng.";
            OF = 0;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
        }
        // Bàn cờ
        #region Bàn cờ
        private void pnlBanCo_Paint(object sender, PaintEventArgs e)
        {
            _Co_Caro.Ve_Ban_Co(grp);
            _Co_Caro.Ve_Lai_Quan_Co(grp);
        }
        private void pnlBanCo_MouseClick(object sender, MouseEventArgs e)
        {
            String []stg = new String[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
                "O", "P", "Q", "R", "S", "T"};
            if (!_Co_Caro.San_Sang)
                return;
            if (_Co_Caro.Danh_Co(e.X, e.Y, grp))
            {
                lblLuotDi.Text = _Co_Caro.lbl.Text;
                label5.Text = "X : " + _Co_Caro.X;
                if (_Co_Caro.Check_Game())
                    _Co_Caro.Game_Over();
                else
                {
                    if (_Co_Caro.Che_Do_Choi == 2)
                    {
                        lblLuotDi.Text = "Đến lượt bạn đi (X)";
                        _Co_Caro.StartCom(grp);
                        label4.Text = "O : " + _Co_Caro.O;
                        if (_Co_Caro.Check_Game())
                            _Co_Caro.Game_Over();
                    }
                }
                if (_Co_Caro.Che_Do_Choi == 2)
                {
                    label8.Text = "O (" + (_Co_Caro.m + 1).ToString() + ", " + stg[_Co_Caro.n] + ")";
                    label8.ForeColor = Color.Red;
                }
                else
                {
                    if (_Co_Caro.Luot_Di == 2)
                    {
                        label8.Text = "O (" + (_Co_Caro.m + 1).ToString() + ", " + stg[_Co_Caro.n] + ")";
                        label8.ForeColor = Color.Red;
                    }
                    else
                    {
                        label8.Text = "X (" + (_Co_Caro.m + 1).ToString() + ", " + stg[_Co_Caro.n] + ")";
                        label8.ForeColor = Color.Blue;
                    }
                }
                label4.Text = "O : " + _Co_Caro.O;
                label5.Text = "X : " + _Co_Caro.X;
                if (_Co_Caro.Che_Do_Choi == 1)
                {
                    label6.Text = "Player 1 : " + _Co_Caro.P1;
                    label7.Text = "Player 2 : " + _Co_Caro.P2;
                }
                else
                {
                    label6.Text = "Com : " + _Co_Caro.C;
                    label7.Text = "Player : " + _Co_Caro.P;
                }
            }
        }
        #endregion
        // P v P
        #region PvP
        private void pVsPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PvP();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PvP();
        }
        private void PvP()
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn chơi mới không?", "Cờ Caro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                grp.Clear(pnlBanCo.BackColor);
                _Co_Caro.StartPVP(grp);
                lblLuotDi.Text = "O đi trước";
                label4.Text = "O : 0";
                label5.Text = "X : 0";
                label6.Text = "Player 1 : " + _Co_Caro.P1;
                label7.Text = "Player 2 : " + _Co_Caro.P2;
                label8.Text = "";
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                OF = 2;
            }
        }
        #endregion
        // P v C
        #region P v C
        private void button2_Click(object sender, EventArgs e)
        {
            PvC();
        }
        private void pVsCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PvC();
        }
        private void PvC()
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn chơi mới không?", "Cờ Caro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                grp.Clear(pnlBanCo.BackColor);
                _Co_Caro.StartPVC(grp);
                lblLuotDi.Text = "Đến lượt bạn đi (X)";
                label4.Text = "O : 1";
                label5.Text = "X : 0";
                label8.Text = "O (11, K)";
                label8.ForeColor = Color.Red;
                label6.Text = "Com : " + _Co_Caro.C;
                label7.Text = "Player : " + _Co_Caro.P;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                OF = 2;
            }
        }
        #endregion
        // ON _ OFF
        #region ON - OFF
        private void onOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (OF)
            {
                case 1:
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    OF = 2;
                    break;
                case 2:
                    label8.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    OF = 1;
                    break;
                default:
                    return;
            }
        }
        #endregion
        // Undo, Redo
        #region Undo, Redo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Co_Caro.Che_Do_Choi == 1)
            {
                _Co_Caro.Undo(grp, pnlBanCo.BackColor);
                lblLuotDi.Text = _Co_Caro.lbl.Text;
                if (_Co_Caro.Luot_Di == 1)
                    _Co_Caro.O--;
                else
                    _Co_Caro.X--;
                label4.Text = "O : " + _Co_Caro.O;
                label5.Text = "X : " + _Co_Caro.X;
            }
            else
            {
                _Co_Caro.Undo(grp, pnlBanCo.BackColor);
                lblLuotDi.Text = _Co_Caro.lbl.Text;
                _Co_Caro.Undo(grp, pnlBanCo.BackColor);
                lblLuotDi.Text = _Co_Caro.lbl.Text;
                _Co_Caro.O--;
                _Co_Caro.X--;
                label4.Text = "O : " + _Co_Caro.O;
                label5.Text = "X : " + _Co_Caro.X;
            }
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Co_Caro.Che_Do_Choi == 1)
            {
                _Co_Caro.Redo(grp);
                lblLuotDi.Text = _Co_Caro.lbl.Text;
            }
            else
            {
                _Co_Caro.Redo(grp);
                lblLuotDi.Text = _Co_Caro.lbl.Text;
                _Co_Caro.Redo(grp);
                lblLuotDi.Text = _Co_Caro.lbl.Text;
            }
        }
        #endregion
        // Color
        #region Color
        private void màuQuânCờOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.FullOpen = true;
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                Co_Caro.penO = new Pen(dlgColor.Color, 2f);
                _Co_Caro.Ve_Ban_Co(grp);
                _Co_Caro.Ve_Lai_Quan_Co(grp);
            }
        }
        private void màuQuânXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.FullOpen = true;
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                Co_Caro.penX = new Pen(dlgColor.Color, 2f);
                _Co_Caro.Ve_Ban_Co(grp);
                _Co_Caro.Ve_Lai_Quan_Co(grp);
            }
        }
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.FullOpen = true;
            if (dlgColor.ShowDialog() == DialogResult.OK)
                this.BackColor = dlgColor.Color;
        }
        private void colorBànCờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.FullOpen = true;
            if (dlgColor.ShowDialog() == DialogResult.OK)
                pnlBanCo.BackColor = dlgColor.Color;
        }
        private void màuĐườngKẻToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.FullOpen = true;
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                Co_Caro.pen = new Pen(dlgColor.Color);
                _Co_Caro.Ve_Ban_Co(grp);
            }
        }
        #endregion
        // Exit game
        #region Exit game
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có muốn thoát không?", "Cờ caro", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
                this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có muốn thoát không?", "Cờ caro", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
                this.Close();
        }
        #endregion
        // About game
        #region About game
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String Msg = "Game Cờ Caro trí tuệ nhân tạo" + Environment.NewLine + "Design by BC"
                + Environment.NewLine + "Ver 1.3";
            MessageBox.Show(Msg, "Thông tin game Cờ Caro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion   
    }
}
