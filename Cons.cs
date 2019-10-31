using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight_Tour_Solution
{
    public class Cons
    {
        public static int BOARD_WIDTH = 441;
        public static int BOARD_SIZE = 7;
        public static int CELL_WIDTH = BOARD_WIDTH / BOARD_SIZE;
        public static int TOTAL_CELLS = BOARD_SIZE * BOARD_SIZE;
        //public static int CELL_SIZE = 55;
        public static float CELL_CENTER = CELL_WIDTH / 2;
        public static int KNIGHT_SPEED = 100;
        public static Image HORSE_SPRITE = Properties.Resources.horse_sprite;
        public static Image CHECKED_SPRITE = Properties.Resources._checked;
        public static Image CROSS_SPRITE = Properties.Resources.cross;
        public static String[] ROW_NAME = { "A", "B", "C", "D", "E", "F", "G" };
        public static String[] COL_NAME = { "7", "6", "5", "4", "3", "2", "1" };

        public static String GetCellName(int x, int y)
        {
            return ROW_NAME[x] + " - " + COL_NAME[y];
        }
    }
}
