﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight_Tour_Solution
{
    public class Cons
    {
        public static int BOARD_WIDTH = 440;
        public static int CELL_SIZE = 55;
        public static float CELL_CENTER = 55 / 2;
        public static Image HORSE_SPRITE = Properties.Resources.horse_sprite;
        public static String[] ROW_NAME = { "A", "B", "C", "D", "E", "F", "G", "H" };
        public static String[] COL_NAME = { "8", "7", "6", "5", "4", "3", "2", "1" };

        public static String GetCellName(int x, int y)
        {
            return ROW_NAME[x] + " - " + COL_NAME[y];
        }
    }
}