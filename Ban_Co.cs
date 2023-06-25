using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_Demo_Game_Co_Caro
{
    class Ban_Co
    {
        private int _NColumn;
        private int _NRow;
        public static Pen penXO;
        public static Pen penXX;
        // Phương thức khởi tạo
        #region Phương thức khởi tạo
        public Ban_Co()
        {
            _NColumn = 0;
            _NRow = 0;
        }
        public Ban_Co(int NColumn,int NRow)
        {
            _NRow = NRow;
            _NColumn = NColumn;
        }
        public int NCoLumn
        {
            get { return _NColumn; }
        }
        public int NRow
        {
            get { return _NColumn; }
        }
        public void Ve_Ban_Co(Graphics g)
        {
            for (int i = 0; i <= _NColumn; i++)
            {
                g.DrawLine(Co_Caro.pen, i * O_Co._Width, 0, i * O_Co._Width, _NRow * O_Co._Height);
            }
            for (int i = 0; i <= _NRow; i++)
            {
                g.DrawLine(Co_Caro.pen, 0, i * O_Co._Height, _NColumn * O_Co._Width, i * O_Co._Height);
            }
        }
        public void VeO(Graphics g, Point point)
        {
   //         penXO = new Pen(clr, 2f);
            g.DrawEllipse(Co_Caro.penO, point.X + 5, point.Y + 5, O_Co._Width - 10, O_Co._Height - 10);
        }
        public void VeX(Graphics g, Point point)
        {
   //         penXX = new Pen(clr, 2f);
            g.DrawLine(Co_Caro.penX, point.X + 5, point.Y + 5, point.X + 20, point.Y + 20);
            g.DrawLine(Co_Caro.penX, point.X + 20, point.Y + 5, point.X + 5, point.Y + 20);
        }
        public void Xoa_Quan_Co_O(Graphics g, Point point, Color clr)
        {
            penXO = new Pen(clr, 2f);
            g.DrawEllipse(penXO, point.X + 5, point.Y + 5, O_Co._Width - 10, O_Co._Height - 10);
        }
        public void Xoa_Quan_Co_X(Graphics g, Point point, Color clr)
        {
            penXX = new Pen(clr, 2f);
            g.DrawLine(penXX, point.X + 5, point.Y + 5, point.X + 20, point.Y + 20);
            g.DrawLine(penXX, point.X + 20, point.Y + 5, point.X + 5, point.Y + 20);
        }
        #endregion

    }
}
