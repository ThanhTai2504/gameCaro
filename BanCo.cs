using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    class BanCo
    {
        private int _SoDong;
        private int _SoCot;
        // dong goi
        public int SoDong
        {
            get { return _SoDong; }
        }
        public int SoCot
        {
            get { return _SoCot; }
        }


        // phương thức khởi tạo cho bàn cờ
        public BanCo()
        {
            _SoDong = 0;
            _SoCot = 0;
        }
        // phương thức khởi tạo contructor
        public BanCo(int soDong, int soCot)
        {
            _SoDong = soDong;
            _SoCot = soCot;
        }
        // phương thức vẽ bàn cờ
        public void VeBanCo(Graphics g)
        {
            for (int i = 0; i <= _SoCot; i++)
            {
                g.DrawLine(CaroChess.pen, i * OCo._ChieuRong, 0, i * OCo._ChieuRong, _SoDong * OCo._ChieuCao);
            }
            for (int j = 0; j <= _SoDong; j++)
            {
                g.DrawLine(CaroChess.pen, 0, j * OCo._ChieuCao, _SoCot * OCo._ChieuRong, j * OCo._ChieuCao);
            }
        }
        // Khai báo vẽ quân cờ
        public void VeQuanCo(Graphics g, Point point, SolidBrush sb)
        {
            g.FillEllipse(sb, point.X + 2, point.Y + 2, OCo._ChieuRong - 4, OCo._ChieuCao - 4);
        }
        public void XoaQuanCo(Graphics g, Point point, SolidBrush sb)
        {
            g.FillRectangle(sb, point.X +1, point.Y +1, OCo._ChieuRong - 2, OCo._ChieuCao - 2);

        }
    }
}
