using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameCaro
{
    public enum KETTHUC
    {
        HoaCo,
        Player1,
        Player2,
        COM,
    }
    class CaroChess
    {
        public static Pen pen;
        public static SolidBrush sbWhite;
        public static SolidBrush sbBlack;
        public static SolidBrush sbGreen;
        private OCo[,] _MangOco;       // Khai bao mang hai chieu, mang o co moi 
        private BanCo _BanCo;
        private int _LuotDi;
        private bool _SanSang;
        private KETTHUC _ketThuc;
        private int _CheDoChoi;
        // tạo vùng nhớ cho các nước đã đánh 
        private Stack<OCo> stkCacNuocDaDi;
        private Stack<OCo> stkCacNuocUndo;

        public bool SanSang
        {
            get { return _SanSang; }
        }

        public int CheDoChoi
        {
            get { return _CheDoChoi; }
        }
        public CaroChess()
        {
            sbWhite = new SolidBrush(Color.White);
            sbBlack = new SolidBrush(Color.Black);
            sbGreen = new SolidBrush(Color.Lime);
            pen = new Pen(Color.Black);
            _BanCo = new BanCo(20, 20);
            _MangOco = new OCo[_BanCo.SoDong, _BanCo.SoCot];
            stkCacNuocDaDi = new Stack<OCo>();
            stkCacNuocUndo = new Stack<OCo>();
            _LuotDi = 1; //nguoi choi 1
        }

        // Vẽ bàn cờ
        #region Vẽ bàn cờ
        public void VeBanCo(Graphics g)
        {
            _BanCo.VeBanCo(g);          // gọi lại phương thức vẽ bàn cờ của bàn cờ
        }
        #endregion 


        public void KhoiTaoMangOco()
        {
            // tao vong lap tao moi cac doi tuong
            for (int i = 0; i < _BanCo.SoDong; i++)     // dong
            {
                for (int j = 0; j < _BanCo.SoCot; j++)   // cot
                {
                    _MangOco[i, j] = new OCo(i, j, new Point(j * OCo._ChieuRong, i * OCo._ChieuCao), 0);      // Tao moi vung nho

                }
            }
        }

        // Đánh cờ
        #region Đánh cờ

        // truyen toa do tham so x,y cua mouse so voi panel
        public bool DanhCo(int MouseX, int MouseY, Graphics g)
        {
            if (MouseX % OCo._ChieuRong == 0 || MouseY % OCo._ChieuCao == 0)
                return false;
            // xac dinh toa do x,y thuoc dong hay cot nao
            // 2 phep toan nay giup ta biet duoc dòng và cột tương ứng người chơi nhập vào
            int Cot = MouseX / OCo._ChieuRong;
            int Dong = MouseY / OCo._ChieuCao;
            if (_MangOco[Dong, Cot].SoHuu != 0) // ko cho đánh lại vào ô đã đánh rồi
                return false;
            switch (_LuotDi)
            {
                case 1:
                    _MangOco[Dong, Cot].SoHuu = 1; // nguoi choi 1
                                                   // phương thức vẽ quân cờ  
                    _BanCo.VeQuanCo(g, _MangOco[Dong, Cot].Vitri, sbBlack);
                    _LuotDi = 2;
                    break;
                case 2:
                    _MangOco[Dong, Cot].SoHuu = 2; // nguoi choi 2
                    _BanCo.VeQuanCo(g, _MangOco[Dong, Cot].Vitri, sbWhite);
                    _LuotDi = 1;
                    break;             
                default:
                    MessageBox.Show("Có lỗi");
                    break;
            }
            stkCacNuocUndo = new Stack<OCo>();
            OCo oco = new OCo(_MangOco[Dong, Cot].Dong, _MangOco[Dong, Cot].Cot, _MangOco[Dong, Cot].Vitri, _MangOco[Dong, Cot].SoHuu);
            // lưu các nước đi mỗi lần đánh xong
            stkCacNuocDaDi.Push(oco);  // Push là đưa vô, Pop là lấy ra
            return true;
        }
        #endregion

        // vẽ quân cờ
        #region Vẽ quân cờ
        public void VeLaiQuanCo(Graphics g)
        {
            foreach (OCo oco in stkCacNuocDaDi) // Cho mỗi đối tượng ô cờ duyệt trong danh sách các nước đã đi
            {
                // nếu player 1 thì vẽ màu đen và ngược lại
                if (oco.SoHuu == 1)
                    _BanCo.VeQuanCo(g, oco.Vitri, sbBlack);
                else
                    if (oco.SoHuu == 2)
                    _BanCo.VeQuanCo(g, oco.Vitri, sbWhite);
            }
        }
        #endregion

        public void StarPlayervsPlayer(Graphics g)
        {
            _SanSang = true;
            stkCacNuocDaDi = new Stack<OCo>();
            stkCacNuocUndo = new Stack<OCo>();
            _LuotDi = 1;
            _CheDoChoi = 1;
            KhoiTaoMangOco();
            VeBanCo(g);
        }
        public void StarPlayervsCom(Graphics g)
        {
            _SanSang = true;
            stkCacNuocDaDi = new Stack<OCo>();
            stkCacNuocUndo = new Stack<OCo>();
            _LuotDi = 1;
            _CheDoChoi = 2;
            KhoiTaoMangOco();
            VeBanCo(g);
            KhoiDongComputer(g);
        }

        // Undo
        #region Undo
        public void Undo(Graphics g)
        {
            if (stkCacNuocDaDi.Count != 0)
            {
                OCo oco = stkCacNuocDaDi.Pop(); // bỏ nước vừa mới đánh

                stkCacNuocUndo.Push(new OCo(oco.Dong, oco.Cot, oco.Vitri, oco.SoHuu));
                _MangOco[oco.Dong, oco.Cot].SoHuu = 0;
                _BanCo.XoaQuanCo(g, oco.Vitri, sbGreen);
                if (_LuotDi == 1)
                    _LuotDi = 2;
                else
                    _LuotDi = 1;
            }
        }
        #endregion
        // Redo
        #region Redo
        public void Redo(Graphics g)
        {
            if (stkCacNuocUndo.Count != 0)
            {
                OCo oco = stkCacNuocUndo.Pop(); // bỏ nước vừa mới đánh
                stkCacNuocDaDi.Push(new OCo(oco.Dong, oco.Cot, oco.Vitri, oco.SoHuu));
                _MangOco[oco.Dong, oco.Cot].SoHuu = oco.SoHuu;
                _BanCo.VeQuanCo(g, oco.Vitri, oco.SoHuu == 1 ? sbBlack : sbWhite);
                if (_LuotDi == 1)
                    _LuotDi = 2;
                else
                    _LuotDi = 1;
            }
        }
        #endregion
        // Duyệt chiến thắng
        #region Duyệt chiến thắng
        public void KetThucTroChoi()
        {
            switch (_ketThuc)
            {
                case KETTHUC.HoaCo:
                    MessageBox.Show("Hòa cờ!");
                    break;
                case KETTHUC.Player1:
                    MessageBox.Show("Player 1 chiến thắng!", "Thông báo");
                    break;
                case KETTHUC.Player2:
                    MessageBox.Show("Player 2 chiến thắng!", "Thông báo");
                    break;
                case KETTHUC.COM:
                    MessageBox.Show("Máy chiến thắng!", "Thông báo");
                    break;
            }
        }
        // kiểm tra chiến thắng
        #region Kiểm tra chiến thắng
        public bool KiemTraChienThang()
        {
            if (stkCacNuocDaDi.Count == _BanCo.SoCot * _BanCo.SoDong)
            {
                _ketThuc = KETTHUC.HoaCo;

                return true;
            }
            // xét toàn bộ những quân cờ đã đánh
            foreach (OCo oco in stkCacNuocDaDi)
            {
                if (DuyetDoc(oco.Dong, oco.Cot, oco.SoHuu) || DuyetNgang(oco.Dong, oco.Cot, oco.SoHuu) || DuyetCheoXuoi(oco.Dong, oco.Cot, oco.SoHuu) || DuyetCheoNguoc(oco.Dong, oco.Cot, oco.SoHuu))
                {
                    _ketThuc = oco.SoHuu == 1 ? KETTHUC.Player1 : KETTHUC.Player2;
                    return true;
                }
            }
            return false;
        }
        #endregion



        // kiểm tra duyệt dọc
        #region Duyệt dọc
        private bool DuyetDoc(int currDong, int currCot, int currSoHuu)
        {
            // xét trường hợp sai
            if (currDong > _BanCo.SoDong - 5)
                return false;

            // Xét trường hợp đúng
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong + Dem, currCot].SoHuu != currSoHuu)
                    return false;
            }
            // trường hợp giáp biên trên, biên dưới
            if (currDong == 0 || currDong + Dem == _BanCo.SoDong)
                return true;

            // trường hợp nằm giữa, đầu trên, đầu dưới có bị chăn hạy không?
            if (_MangOco[currDong - 1, currCot].SoHuu == 0 || _MangOco[currDong + Dem, currCot].SoHuu == 0)
                return true;
            return false;
        }
        #endregion



        // kiểm tra duyệt ngang
        #region Duyêt ngang
        private bool DuyetNgang(int currDong, int currCot, int currSoHuu)
        {
            // xét trường hợp sai
            if (currCot > _BanCo.SoCot - 5)
                return false;

            // Xét trường hợp đúng
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong, currCot + Dem].SoHuu != currSoHuu)
                    return false;
            }
            // trường hợp giáp biên trên, biên dưới
            if (currCot == 0 || currCot + Dem == _BanCo.SoCot)
                return true;

            // trường hợp nằm giữa, đầu trên, đầu dưới có bị chăn hạy không?
            if (_MangOco[currDong, currCot - 1].SoHuu == 0 || _MangOco[currDong, currCot + Dem].SoHuu == 0)
                return true;
            return false;
        }
        #endregion



        // kiểm tra duyệt chéo xuôi
        #region Duyệt chéo xuôi
        private bool DuyetCheoXuoi(int currDong, int currCot, int currSoHuu)
        {
            // xét trường hợp sai
            if (currDong > _BanCo.SoDong - 5 || currCot > _BanCo.SoCot - 5)
                return false;

            // Xét trường hợp đúng
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong + Dem, currCot + Dem].SoHuu != currSoHuu)
                    return false;
            }
            // trường hợp giáp biên trên, biên dưới
            if (currDong == 0 || currDong + Dem == _BanCo.SoDong || currCot == 0 || currCot + Dem == _BanCo.SoCot)
                return true;

            // trường hợp nằm giữa, đầu trên, đầu dưới có bị chăn hạy không?
            if (_MangOco[currDong - 1, currCot - 1].SoHuu == 0 || _MangOco[currDong + Dem, currCot + Dem].SoHuu == 0)
                return true;
            return false;
        }
        #endregion



        // kiểm tra duyệt chéo ngược
        #region Duyệt chéo ngược
        private bool DuyetCheoNguoc(int currDong, int currCot, int currSoHuu)
        {
            // xét trường hợp sai
            if (currDong < 4 || currCot > _BanCo.SoCot - 5)
                return false;

            // Xét trường hợp đúng
            int Dem;
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong - Dem, currCot + Dem].SoHuu != currSoHuu)
                    return false;
            }
            // trường hợp giáp biên trên, biên dưới
            if (currDong == 4 || currDong == _BanCo.SoDong - 1 || currCot == 0 || currCot + Dem == _BanCo.SoCot)
                return true;

            // trường hợp nằm giữa, đầu trên, đầu dưới có bị chăn hạy không?
            if (_MangOco[currDong + 1, currCot - 1].SoHuu == 0 || _MangOco[currDong - Dem, currCot + Dem].SoHuu == 0)
                return true;
            return false;
        }
        #endregion
        #endregion


        // Người chơi vs Máy
        #region AI
        // Khai báo mảng điểm
        private long[] MangDiemTanCong = new long[7] { 0, 3, 24, 192, 1536, 12288, 98304 };
        private long[] MangDiemPhongNgu = new long[7] { 0, 1, 9, 81, 729, 6561, 59049 };


        public void KhoiDongComputer(Graphics g)
        {
            if (stkCacNuocDaDi.Count == 0)
            {
                // Mặc định cho máy đánh vào giữa bàn cờ
                DanhCo(_BanCo.SoCot / 2 * OCo._ChieuRong + 1, _BanCo.SoDong / 2 * OCo._ChieuCao + 1, g);
            }
            else
            {
                OCo oco = TimKiemNuocDi();
                DanhCo(oco.Vitri.X + 1, oco.Vitri.Y + 1, g);
            }
        }
        private OCo TimKiemNuocDi()
        {
            OCo oCoResult = new OCo();
            // TẠo giá trị sẽ lưu lại điểm(Điểm MAX)
            long DiemMax = 0;
            // Thuật toán vét cạn, duyet ban co
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    if (_MangOco[i, j].SoHuu == 0) // nếu dòng i, cột j thuộc sở hữu = o
                    {
                        long DiemTanCong = DiemTanCong_DuyetDoc(i, j) + DiemTanCong_DuyetNgang(i, j) + DiemTanCong_DuyetCheoNguoc(i, j) + DiemTanCong_DuyetCheoXuoi(i, j);
                        long DiemPhongNgu = DiemPhongNgu_DuyetDoc(i, j) + DiemPhongNgu_DuyetNgang(i, j) + DiemPhongNgu_DuyetCheoNguoc(i, j) + DiemPhongNgu_DuyetCheoXuoi(i, j);
                        // Xét điểm tấn công và phòng ngự
                        long DiemTam = DiemTanCong > DiemPhongNgu ? DiemTanCong : DiemPhongNgu;
                        if (DiemMax < DiemTam)
                        {
                            DiemMax = DiemTam;
                            oCoResult = new OCo(_MangOco[i, j].Dong, _MangOco[i, j].Cot, _MangOco[i, j].Vitri, _MangOco[i, j].SoHuu);

                        }

                    }
                }
            }

            return oCoResult;
        }
        // Duyệt tấn công
        #region Duyệt tấn công
        private long DiemTanCong_DuyetDoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                // trường hợp giáp biên từ trên đầu xuống
                if (_MangOco[currDong + Dem, currCot].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong + Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương dọc bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currDong - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ dưới lên đầu
                if (_MangOco[currDong - Dem, currCot].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong - Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương dọc bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1];

            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }

        private long DiemTanCong_DuyetNgang(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currCot + Dem < _BanCo.SoCot; Dem++)
            {
                // trường hợp giáp biên từ trái sang phải
                if (_MangOco[currDong, currCot + Dem].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương ngang bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currCot - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ phải sang trái
                if (_MangOco[currDong, currCot - Dem].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương ngang bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1] ;
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }

        private long DiemTanCong_DuyetCheoNguoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ tren xuong duoi
                if (_MangOco[currDong - Dem, currCot + Dem].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong - Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương ngang bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currCot - Dem >= 0 && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                // trường hợp giáp biên từ duoi len tren
                if (_MangOco[currDong + Dem, currCot - Dem].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong + Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương ngang bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1];
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }

        private long DiemTanCong_DuyetCheoXuoi(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                // trường hợp giáp biên từ trái trên xuong phải duoi
                if (_MangOco[currDong + Dem, currCot + Dem].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong + Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương ngang bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currCot - Dem >= 0 && currDong - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ góc phải duoi len góc trái tren
                if (_MangOco[currDong - Dem, currCot - Dem].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (_MangOco[currDong - Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                    //  Tìm kiếm nước đi theo phương ngang bị chặn đầu thì thoát vòng lặp
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= MangDiemPhongNgu[SoQuanDich + 1];
            DiemTong += MangDiemTanCong[SoQuanTa];
            return DiemTong;
        }
        #endregion


        // Duyệt phòng ngự
        #region Duyệt phòng ngự
        private long DiemPhongNgu_DuyetDoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                // trường hợp giáp biên từ trên đầu xuống
                if (_MangOco[currDong + Dem, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong + Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;


                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currDong - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ dưới lên đầu
                if (_MangOco[currDong - Dem, currCot].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }

                else
                    if (_MangOco[currDong - Dem, currCot].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;


            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }

        private long DiemPhongNgu_DuyetNgang(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currCot + Dem < _BanCo.SoCot; Dem++)
            {
                // trường hợp giáp biên từ trái sang phải
                if (_MangOco[currDong, currCot + Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currCot - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ phải sang trái
                if (_MangOco[currDong, currCot - Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;

            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }

        private long DiemPhongNgu_DuyetCheoNguoc(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ tren xuong duoi
                if (_MangOco[currDong - Dem, currCot + Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong - Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currCot - Dem >= 0 && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                // trường hợp giáp biên từ duoi len tren
                if (_MangOco[currDong + Dem, currCot - Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong + Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;

            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }

        private long DiemPhongNgu_DuyetCheoXuoi(int currDong, int currCot)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int Dem = 1; Dem < 6 && currCot + Dem < _BanCo.SoCot && currDong + Dem < _BanCo.SoDong; Dem++)
            {
                // trường hợp giáp biên từ trái trên xuong phải duoi
                if (_MangOco[currDong + Dem, currCot + Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong + Dem, currCot + Dem].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            for (int Dem = 1; Dem < 6 && currCot - Dem >= 0 && currDong - Dem >= 0; Dem++)
            {
                // trường hợp giáp biên từ góc phải duoi len góc trái tren
                if (_MangOco[currDong - Dem, currCot - Dem].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else
                    if (_MangOco[currDong - Dem, currCot - Dem].SoHuu == 2)
                {
                    SoQuanDich++;

                }
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;

            DiemTong += MangDiemPhongNgu[SoQuanDich];
            return DiemTong;
        }
        #endregion
        #endregion


    }
}
