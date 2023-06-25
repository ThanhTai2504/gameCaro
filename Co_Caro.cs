using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_Demo_Game_Co_Caro
{
    class Co_Caro
    {
        // Thuộc tính
        #region Thuộc tính
        public static Pen pen;
        public static Pen penO;
        public static Pen penX;
        private O_Co[,] _Mang_O_Co;
        private Ban_Co _Ban_Co;
        private int _Luot_Di;
        private int _Che_Do_Choi;
        private Stack<O_Co> Stack_Cac_Nuoc_Da_Di;
        private Stack<O_Co> Stack_Cac_Nuoc_Undo;
        public Label lbl = new Label();
        private bool _San_Sang;
        private int _Ket_Thuc = -1;
        public int O;
        public int X;
        public int P1;
        public int P2;
        public int C;
        public int P;
        public int m, n;
        #endregion
        // Phương thức khởi tạo Co_Caro, San_Sang, Che_Do_Choi, Luot_Di, KTM_O_Co
        #region Phương thức khởi tạo Co_Caro, San_Sang, Che_Do_Choi, Luot_Di, KTM_O_Co
        public Co_Caro()
        {
            pen = new Pen(Color.Green);
            penO = new Pen(Color.Red, 2f);
            penX = new Pen(Color.Blue, 2f);
            _Ban_Co = new Ban_Co(20, 20);
            _Luot_Di = 1;
            _Mang_O_Co = new O_Co[_Ban_Co.NRow,_Ban_Co.NCoLumn];
            Stack_Cac_Nuoc_Da_Di = new Stack<O_Co>();
            Stack_Cac_Nuoc_Undo = new Stack<O_Co>();
            lbl.Text = "O đi trước";
            O = 0;
            X = 0;
            P1 = 0;
            P2 = 0;
            C = 0;
            P = 0;
            m = n = 0;
        }
        public bool San_Sang
        {
            get
            {
                return _San_Sang;
            }
        }
        public int Che_Do_Choi
        {
            get
            {
                return _Che_Do_Choi;
            }
        }
        public int Luot_Di
        {
            get
            {
                return _Luot_Di;
            }
        }
        public void KTM_O_Co()
        {
            for (int i = 0; i < _Ban_Co.NRow; i++)
            {
                for (int j = 0; j < _Ban_Co.NCoLumn; j++)
                {
                    _Mang_O_Co[i, j] = new O_Co(i, j, new Point(j * O_Co._Width, i * O_Co._Height), 0);
                }
            }
        }
        #endregion
        // Phương thức Vẽ Bàn Cờ, Đánh cờ, Vẽ Lại Quân Cờ
        #region Phương thức Vẽ Bàn Cờ, Đánh cờ, Vẽ Lại Quân Cờ
        public void Ve_Ban_Co(Graphics g)
        {
            _Ban_Co.Ve_Ban_Co(g);
        }
        public bool Danh_Co(int MouseX, int MouseY, Graphics g)
        {
            if (MouseX % O_Co._Width == 0 || MouseY % O_Co._Height == 0)
                return false;
            int Column = MouseX / O_Co._Width;
            int Row = MouseY / O_Co._Height;
            if (_Mang_O_Co[Row, Column].Own != 0)
                return false;
            m = Row;
            n = Column;
            switch(_Luot_Di)
            {
                case 1:
                    _Mang_O_Co[Row, Column].Own = 1;
                    _Ban_Co.VeO(g,_Mang_O_Co[Row, Column].Location);
                    lbl.Text = "Đến lượt X đi.";
                    _Luot_Di = 2;
                    O++;
                    break;
                case 2:
                    _Mang_O_Co[Row, Column].Own = 2;
                    _Ban_Co.VeX(g,_Mang_O_Co[Row, Column].Location);
                    lbl.Text = "Đến lượt O đi.";
                    _Luot_Di = 1;
                    X++;
                    break;
                default:
                    MessageBox.Show("Có lỗi xảy ra.", "Thông báo");
                    break;
            }
            O_Co _oco = new O_Co(_Mang_O_Co[Row, Column].Row,_Mang_O_Co[Row, Column].Column,
                _Mang_O_Co[Row, Column].Location,_Mang_O_Co[Row, Column].Own);
            Stack_Cac_Nuoc_Undo = new Stack<O_Co>();
            Stack_Cac_Nuoc_Da_Di.Push(_oco);
            return true;
        }
        public void Ve_Lai_Quan_Co(Graphics g)
        {
            foreach(O_Co o_co in Stack_Cac_Nuoc_Da_Di)
            {
                if (o_co.Own == 1)
                    _Ban_Co.VeO(g, o_co.Location);
                else if (o_co.Own == 2)
                    _Ban_Co.VeX(g, o_co.Location);
            }
        }
        #endregion
        // Start PvP, PvC
        #region Start PvP, PvC
        public void StartPVP(Graphics g)
        {
            _San_Sang = true; 
            Stack_Cac_Nuoc_Da_Di = new Stack<O_Co>();
            Stack_Cac_Nuoc_Undo = new Stack<O_Co>();
            _Luot_Di = 1;
            _Che_Do_Choi = 1; // P v P
            O = X = P = C = 0;
            KTM_O_Co();
            Ve_Ban_Co(g);
        }
        public void StartPVC(Graphics g)
        {
            _San_Sang = true;
            Stack_Cac_Nuoc_Da_Di = new Stack<O_Co>();
            Stack_Cac_Nuoc_Undo = new Stack<O_Co>();
            _Luot_Di = 1;
            _Che_Do_Choi = 2; // P v C
            O = X = P1 = P2 = 0;
            KTM_O_Co();
            Ve_Ban_Co(g);
            StartCom(g);
        }
        #endregion
        // Undo, Redo
        #region Undo, Redo
        public void Undo(Graphics g, Color clr)
        {
            if (Stack_Cac_Nuoc_Da_Di.Count != 0 && _San_Sang == true)
            {
                O_Co _o_co = Stack_Cac_Nuoc_Da_Di.Pop();
                Stack_Cac_Nuoc_Undo.Push(new O_Co(_o_co.Column,_o_co.Row,_o_co.Location,_o_co.Own));
                if (_o_co.Own == 1)
                {
                    _Ban_Co.Xoa_Quan_Co_O(g, _o_co.Location, clr);
                    _Luot_Di = 1;
                    lbl.Text = "Đến lượt O đi.";
                    _Mang_O_Co[_o_co.Row, _o_co.Column].Own = 0;
                }
                else if (_o_co.Own == 2)
                {
                    _Ban_Co.Xoa_Quan_Co_X(g, _o_co.Location, clr);
                    _Luot_Di = 2;
                    lbl.Text = "Đến lượt X đi.";
                    _Mang_O_Co[_o_co.Row, _o_co.Column].Own = 0;
                }
            }
        }
        public void Redo(Graphics g)
        {
            if (Stack_Cac_Nuoc_Undo.Count !=0)
            {
                O_Co _o_co = Stack_Cac_Nuoc_Undo.Pop();
                Stack_Cac_Nuoc_Da_Di.Push(new O_Co(_o_co.Column, _o_co.Row, _o_co.Location, _o_co.Own));
                if (_o_co.Own == 1)
                {
                    _Ban_Co.VeO(g, _o_co.Location);
                    _Luot_Di = 2;
                    lbl.Text = "Đến lượt X đi.";
                    _Mang_O_Co[_o_co.Row, _o_co.Column].Own = 1;
                }
                else if (_o_co.Own == 2)
                {
                    _Ban_Co.VeX(g, _o_co.Location);
                    _Luot_Di = 1;
                    lbl.Text = "Đến lượt O đi.";
                    _Mang_O_Co[_o_co.Row, _o_co.Column].Own = 2;
                }
            }
        }
        #endregion
        // Duyệt Game Win
        #region DuyetGame
        public void Game_Over()
        {
            switch(_Ket_Thuc)
            {
                case 0: // hòa cờ
                    MessageBox.Show("Hòa cờ!");
                    break;
                case 1: // người 1 thắng
                    MessageBox.Show("Người chơi 1 thắng!");
                    P1++;
                    break;
                case 2: // người 2 thắng
                    MessageBox.Show("Người chơi 2 thắng!");
                    P2++;
                    break;
                case 3: // máy thắng
                    MessageBox.Show("Máy thắng!");
                    C++;
                    break;
                case 4: // bạn thắng
                    MessageBox.Show("Bạn thắng!");
                    P++;
                    break;
            }
            _San_Sang = false;
        }
        public bool Check_Game()
        {
            if (Stack_Cac_Nuoc_Da_Di.Count == _Ban_Co.NRow * _Ban_Co.NCoLumn)
            {
                _Ket_Thuc = 0; //hòa cờ
                return true;
            }
            foreach(O_Co _O_co in Stack_Cac_Nuoc_Da_Di)
            {

                if (Duyet_Doc(_O_co.Row, _O_co.Column, _O_co.Own) == true || Duyet_Ngang(_O_co.Row, _O_co.Column, _O_co.Own) == true || 
                    Duyet_Cheo_Nguoc(_O_co.Row, _O_co.Column, _O_co.Own) == true || Duyet_Cheo_Xuoi(_O_co.Row, _O_co.Column, _O_co.Own) == true)
                {
                    if (_Che_Do_Choi == 1)
                    {
                        _Ket_Thuc = _O_co.Own == 1 ? 1 : 2;
                        return true;
                    }
                    else if(_Che_Do_Choi == 2)
                    {
                        _Ket_Thuc = _O_co.Own == 1 ? 3 : 4;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Duyet_Doc(int crRow, int crColumn, int crOwn)
        {
            if (crRow > _Ban_Co.NRow - 5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_Mang_O_Co[crRow + Dem, crColumn].Own != crOwn)
                    return false;
            }
            return true;
        }
        public bool Duyet_Ngang(int crRow, int crColumn, int crOwn)
        {
            if (crColumn > _Ban_Co.NCoLumn - 5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_Mang_O_Co[crRow, crColumn + Dem].Own != crOwn)
                    return false;
            }
            return true;
        }
        public bool Duyet_Cheo_Nguoc(int crRow, int crColumn, int crOwn)
        {
            if (crRow < 4 || crColumn > _Ban_Co.NCoLumn - 5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_Mang_O_Co[crRow - Dem, crColumn + Dem].Own != crOwn)
                    return false;
            }
            return true;
        }
        public bool Duyet_Cheo_Xuoi(int crRow, int crColumn, int crOwn)
        {
            if (crRow > _Ban_Co.NRow - 5 || crColumn > _Ban_Co.NCoLumn - 5)
                return false;
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_Mang_O_Co[crRow + Dem, crColumn + Dem].Own != crOwn)
                    return false;
            }
            return true;
        }
        #endregion
        //TTBT - AI(Artificial Intelligence)
        #region TTNT-AI(Artificial Intelligence)
        private long[] _MD_TC = new long[6] {0,64,4096,262144,16777216,1073741824}; //Điểm tấn công
        private long[] _MD_PT = new long[6] {0,8,512,32768,2097152,134217728}; // Điểm phòng thủ
        public void StartCom(Graphics g)
        {
            if(Stack_Cac_Nuoc_Da_Di.Count == 0)
            {
                Danh_Co(_Ban_Co.NRow / 2 * O_Co._Height + 1, _Ban_Co.NCoLumn / 2 * O_Co._Width + 1, g);
            }
            else
            {
                O_Co _o_co = Tim_Kiem_Nuoc_Di();
                Danh_Co(_o_co.Location.X + 1, _o_co.Location.Y + 1, g);
            }

        }
        private O_Co Tim_Kiem_Nuoc_Di()
        {
            O_Co _ocoResult = new O_Co();
            long _Diem_Max = 0;
            for (int i = 0; i < _Ban_Co.NRow; i++ )
            {
                for (int j = 0; j < _Ban_Co.NCoLumn; j++)
                {
                    if (_Mang_O_Co[i,j].Own == 0)
                    {
                        long _Diem_TC = DTC_DuyetDoc(i, j) + DTC_DuyetNgang(i, j) + DTC_DuyetCheoXuoi(i, j) + DTC_DuyetCheoNguoc(i, j);
                        long _Diem_PT = DPT_DuyetNgang(i, j) + DPT_DuyetDoc(i, j) + DPT_DuyetCheoXuoi(i, j) + DPT_DuyetCheoNguoc(i, j);
                        long _Diem_Tam_Thoi = _Diem_TC > _Diem_PT? _Diem_TC:_Diem_PT;
                        long _Diem_Tong = (_Diem_PT + _Diem_TC) > _Diem_Tam_Thoi ? (_Diem_PT + _Diem_TC) : _Diem_Tam_Thoi;
                        if (_Diem_Max < _Diem_Tong)
                        {
                            _Diem_Max = _Diem_Tong;
                            _ocoResult = new O_Co(_Mang_O_Co[i,j].Row,_Mang_O_Co[i,j].Column,_Mang_O_Co[i,j].Location,_Mang_O_Co[i,j].Own);
                        }
                    }
                }
            }
            return _ocoResult;
        }
        // Duyệt điểm tấn công
        #region DuyetDiemTanCong
        private long DTC_DuyetDoc(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            if (crRow + 1 < _Ban_Co.NRow && _Mang_O_Co[crRow + 1, crColumn].Own == 0)
            {
                
            }
            if (crRow > 0 && _Mang_O_Co[crRow - 1, crColumn].Own == 0)
            {
                
            }
            //
            for (int dem = 1; dem < 5 && crRow + dem < _Ban_Co.NRow; dem++)
            {
                if (_Mang_O_Co[crRow + dem, crColumn].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow + dem, crColumn].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crRow + dem2 < _Ban_Co.NRow; dem2++)
                        if (_Mang_O_Co[crRow + dem2, crColumn].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow + dem2, crColumn].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crRow - dem >= 0; dem++)
            {
                if (_Mang_O_Co[crRow - dem, crColumn].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow - dem, crColumn].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crRow - dem2 >= 0; dem2++)
                        if (_Mang_O_Co[crRow - dem2, crColumn].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow - dem2, crColumn].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (_SoQD == 2)
                return 0;
            if (_SoQD == 0)
                _Diem_Tong += _MD_TC[_SoQT] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT];
            if (_SoQD2 == 0)
                _Diem_Tong += _MD_TC[_SoQT2] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT2];
            if (_SoQT >= _SoQT2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQT == 4)
                _Diem_Tong *= 2;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            return _Diem_Tong;
        }
        private long DTC_DuyetNgang(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            if (crColumn + 1 < _Ban_Co.NCoLumn && _Mang_O_Co[crRow, crColumn + 1].Own == 0)
            {
                
            }
            if (crColumn > 0 && _Mang_O_Co[crRow, crColumn - 1].Own == 0)
            {
                
            }
            //
            for (int dem = 1; dem < 5 && crColumn + dem < _Ban_Co.NCoLumn; dem++)
            {
                if (_Mang_O_Co[crRow, crColumn + dem].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow, crColumn + dem].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crColumn + dem2 < _Ban_Co.NCoLumn; dem2++)
                        if (_Mang_O_Co[crRow, crColumn + dem2].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow, crColumn + dem2].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crColumn - dem >= 0; dem++)
            {
                if (_Mang_O_Co[crRow, crColumn - dem].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow, crColumn - dem].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crColumn - dem2 >= 0; dem2++)
                        if (_Mang_O_Co[crRow, crColumn - dem2].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow, crColumn - dem2].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (_SoQD == 2)
                return 0;
            if (_SoQD == 0)
                _Diem_Tong += _MD_TC[_SoQT] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT];
            if (_SoQD2 == 0)
                _Diem_Tong += _MD_TC[_SoQT2] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT2];
            if (_SoQT >= _SoQT2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQT == 4)
                _Diem_Tong *= 2;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            return _Diem_Tong;
        }
        private long DTC_DuyetCheoXuoi(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            if (crRow + 1 < _Ban_Co.NRow && crColumn + 1 <_Ban_Co.NCoLumn && _Mang_O_Co[crRow + 1, crColumn + 1].Own == 0)
            {
                
            }
            if (crRow > 0 && crColumn > 0 && _Mang_O_Co[crRow - 1, crColumn - 1].Own == 0)
            {
                
            }
            //
            for (int dem = 1; dem < 5 && crColumn + dem < _Ban_Co.NCoLumn && crRow + dem < _Ban_Co.NRow; dem++)
            {
                if (_Mang_O_Co[crRow + dem, crColumn + dem].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow + dem, crColumn + dem].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crColumn + dem2 < _Ban_Co.NCoLumn && crRow + dem2 < _Ban_Co.NRow; dem2++)
                        if (_Mang_O_Co[crRow + dem2, crColumn + dem2].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow + dem2, crColumn + dem2].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crColumn - dem >= 0 && crRow - dem >= 0; dem++)
            {
                if (_Mang_O_Co[crRow - dem, crColumn - dem].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow - dem, crColumn - dem].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crColumn - dem2 >= 0 && crRow - dem2 >= 0; dem2++)
                        if (_Mang_O_Co[crRow - dem2, crColumn - dem2].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow - dem2, crColumn - dem2].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (_SoQD == 2)
                return 0;
            if (_SoQD == 0)
                _Diem_Tong += _MD_TC[_SoQT] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT];
            if (_SoQD2 == 0)
                _Diem_Tong += _MD_TC[_SoQT2] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT2];
            if (_SoQT >= _SoQT2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;

            if (_SoQT == 4)
                _Diem_Tong *= 2;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            return _Diem_Tong;
        }
        private long DTC_DuyetCheoNguoc(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            if (crRow > 0 && crColumn + 1 < _Ban_Co.NCoLumn && _Mang_O_Co[crRow - 1, crColumn + 1].Own == 0)
            {
                
            }
            if (crRow + 1 < _Ban_Co.NRow && crColumn > 0 && _Mang_O_Co[crRow + 1, crColumn - 1].Own == 0)
            {
                
            }
            //
            for (int dem = 1; dem < 5 && crColumn + dem < _Ban_Co.NCoLumn && crRow - dem > 0; dem++)
            {
                if (_Mang_O_Co[crRow - dem, crColumn + dem].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow - dem, crColumn + dem].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crColumn + dem2 < _Ban_Co.NCoLumn && crRow - dem2 > 0; dem2++)
                        if (_Mang_O_Co[crRow - dem2, crColumn + dem2].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow - dem2, crColumn + dem2].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crColumn - dem >= 0 && crRow + dem < _Ban_Co.NRow; dem++)
            {
                if (_Mang_O_Co[crRow + dem, crColumn - dem].Own == 1)
                {
                    _SoQT++;
                }
                else if (_Mang_O_Co[crRow + dem, crColumn - dem].Own == 2)
                {
                    _SoQD++;
                    break;
                }
                else // Own = 0
                {
                    for (int dem2 = 1; dem2 < 6 && crColumn - dem2 >= 0 && crRow + dem2 < _Ban_Co.NRow; dem2++)
                        if (_Mang_O_Co[crRow + dem2, crColumn - dem2].Own == 1)
                        {
                            _SoQT2++;
                        }
                        else if (_Mang_O_Co[crRow + dem2, crColumn - dem2].Own == 2)
                        {
                            _SoQD2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (_SoQD == 2)
                return 0;
            if (_SoQD == 0)
                _Diem_Tong += _MD_TC[_SoQT] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT];
            if (_SoQD2 == 0)
                _Diem_Tong += _MD_TC[_SoQT2] * 2;
            else
                _Diem_Tong += _MD_TC[_SoQT2];
            if (_SoQT >= _SoQT2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQT == 4)
                _Diem_Tong *= 2;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            return _Diem_Tong;
        }
        #endregion
        // Duyệt điểm phòng thủ
        #region DuyetDiemPhongThu
        private long DPT_DuyetDoc(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            for (int dem = 1; dem < 5 && crRow + dem < _Ban_Co.NRow; dem++)
            {
                if (_Mang_O_Co[crRow + dem, crColumn].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow + dem, crColumn].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crRow + dem2 < _Ban_Co.NRow; dem2++ )
                        if (_Mang_O_Co[crRow + dem, crColumn].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow + dem, crColumn].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crRow - dem >= 0; dem++)
            {
                if (_Mang_O_Co[crRow - dem, crColumn].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow - dem, crColumn].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for(int dem2 = 2; dem2 < 6 && crRow - dem2 >= 0; dem2++)
                        if (_Mang_O_Co[crRow - dem2, crColumn].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow - dem2, crColumn].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else 
                            break;
                    break;
                }
            }
            if (_SoQT == 2)
                return 0;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            /* 
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            */
            if (_SoQD >= _SoQD2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQD == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        private long DPT_DuyetNgang(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            for (int dem = 1; dem < 5 && crColumn + dem < _Ban_Co.NCoLumn; dem++)
            {
                if (_Mang_O_Co[crRow, crColumn + dem].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow, crColumn + dem].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for(int dem2 = 2; dem2 < 6 && crColumn + dem2 < _Ban_Co.NCoLumn; dem2++)
                        if (_Mang_O_Co[crRow, crColumn + dem2].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow, crColumn + dem2].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crColumn - dem >= 0; dem++)
            {
                if (_Mang_O_Co[crRow, crColumn - dem].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow, crColumn - dem].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for(int dem2 = 2; dem2 < 6 && crColumn - dem2 >= 0; dem2++)
                        if (_Mang_O_Co[crRow, crColumn - dem2].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow, crColumn - dem2].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else break;
                    break;
                }
            }
            if (_SoQT == 2)
                return 0;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            /* 
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            */
            if (_SoQD >= _SoQD2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQD == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        private long DPT_DuyetCheoXuoi(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            for (int dem = 1; dem < 5 && crColumn + dem < _Ban_Co.NCoLumn && crRow + dem < _Ban_Co.NRow; dem++)
            {
                if (_Mang_O_Co[crRow + dem, crColumn + dem].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow + dem, crColumn + dem].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for(int dem2 = 2; dem2 < 6 && crRow + dem2 < _Ban_Co.NRow && crColumn + dem2 < _Ban_Co.NCoLumn; dem2++)
                        if (_Mang_O_Co[crRow + dem2, crColumn + dem2].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow + dem2, crColumn + dem2].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crColumn - dem >= 0 && crRow - dem >= 0; dem++)
            {
                if (_Mang_O_Co[crRow - dem, crColumn - dem].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow - dem, crColumn - dem].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crColumn - dem2 >= 0 && crRow - dem2 >= 0; dem2++ )
                        if (_Mang_O_Co[crRow - dem2, crColumn - dem2].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow - dem2, crColumn - dem2].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else
                            break;
                    break;
                }
            }
            if (_SoQT == 2)
                return 0;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            /* 
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            */
            if (_SoQD >= _SoQD2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQD == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        private long DPT_DuyetCheoNguoc(int crRow, int crColumn)
        {
            long _Diem_Tong = 0;
            int _SoQT = 0;
            int _SoQD = 0;
            int _SoQT2 = 0;
            int _SoQD2 = 0;
            for (int dem = 1; dem < 5 && crColumn + dem < _Ban_Co.NCoLumn && crRow - dem > 0; dem++)
            {
                if (_Mang_O_Co[crRow - dem, crColumn + dem].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow - dem, crColumn + dem].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for(int dem2 = 2; dem2 < 6 && crRow - dem2 >=0 && crColumn + dem2 < _Ban_Co.NCoLumn; dem2++)
                        if (_Mang_O_Co[crRow - dem2, crColumn + dem2].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow - dem2, crColumn + dem2].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && crColumn - dem >= 0 && crRow + dem < _Ban_Co.NRow; dem++)
            {
                if (_Mang_O_Co[crRow + dem, crColumn - dem].Own == 1)
                {
                    _SoQT++;
                    break;
                }
                else if (_Mang_O_Co[crRow + dem, crColumn - dem].Own == 2)
                {
                    _SoQD++;
                }
                else // Own = 0
                {
                    for (int dem2 = 2; dem2 < 6 && crRow + dem2 < _Ban_Co.NRow && crColumn - dem2 >= 0; dem2++ )
                        if (_Mang_O_Co[crRow + dem2, crColumn - dem2].Own == 1)
                        {
                            _SoQT2++;
                            break;
                        }
                        else if (_Mang_O_Co[crRow + dem2, crColumn - dem2].Own == 2)
                        {
                            _SoQD2++;
                        }
                        else
                            break;
                    break;
                }
            }
            if (_SoQT == 2)
                return 0;
            if (_SoQT == 0)
                _Diem_Tong += _MD_PT[_SoQD] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD];
            /* 
            if (_SoQT2 == 0)
                _Diem_Tong += _MD_PT[_SoQD2] * 2;
            else
                _Diem_Tong += _MD_PT[_SoQD2];
            */
            if (_SoQD >= _SoQD2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (_SoQD == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        #endregion
        #endregion
    }
}
