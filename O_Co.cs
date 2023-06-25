using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_Demo_Game_Co_Caro
{
    class O_Co
    {
        public const int _Height = 25;
        public const int _Width = 25;
        private int _Own;
        private int _Column;
        private int _Row;
        private Point _Location;
        // Phương thức khởi tạo
        #region Phương thức khởi tạo
        public int Column
        {
            set
            {
                _Column = value;
            }
            get
            {
                return _Column;
            }
        }
        public int Row
        {
            set
            {
                _Row = value;
            }
            get
            {
                return _Row;
            }
        }
        public Point Location
        {
            set
            {
                _Location = value;
            }
            get
            {
                return _Location;
            }
        }
        public int Own
        {
            set
            {
                _Own = value;
            }
            get
            {
                return _Own;
            }
        }
        public O_Co()
        {

        }
        public O_Co(int Row, int Column, Point Location, int Own)
        {
            _Row = Row;
            _Column = Column;
            _Location = Location;
            _Own = Own;
        }
        #endregion
    }
}
