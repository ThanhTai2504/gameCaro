using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    class OCo
    {
        public const int _ChieuRong = 35;
        public const int _ChieuCao = 35;
        private int _Dong;
        private int _Cot;
        // khai bao phuong thuc khoi tao
        public OCo(int dong, int cot, Point vitri, int sohuu)
        {
            _Dong = dong;
            _Cot = cot;
            _Vitri = vitri;
            _SoHuu = sohuu;
        }
        public OCo()
        {

        }
        // khai báo điểm
        private Point _Vitri;
        // khai báo sở hữu
        private int _SoHuu;
        public int Dong { get => _Dong; set => _Dong = value; }
        public int Cot { get => _Cot; set => _Cot = value; }
        public Point Vitri { get => _Vitri; set => _Vitri = value; }
        public int SoHuu { get => _SoHuu; set => _SoHuu = value; }
    }
}
