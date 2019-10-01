using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knight_Tour_Solution
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();

            new KnightTourAlgorithm(0, 0);
            // AVOID PANEL FLICKERING
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, pnlChessBoard, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
             BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
             null, pnlBanner, new object[] { true });
        }

        private void pnlChessBoard_Paint(object sender, PaintEventArgs e)
        {
            // DRAW CHESSBOARD
            Graphics g = e.Graphics;
            Image bgBlack = Properties.Resources.bg_black;
            Image bgWhite = Properties.Resources.bg_white;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        g.DrawImage(bgWhite, i * 55, j * 55, 55, 55);
                    }
                    else if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                    {
                        g.DrawImage(bgBlack, i * 55, j * 55, 55, 55);
                    }
                }
            }
        }

        private void pnlChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            // GET CELL NAME
            int rowIndex = e.X / Cons.CELL_SIZE;
            int colIndex = e.Y / Cons.CELL_SIZE;
            lblBeginCellName.Text = Cons.GetCellName(rowIndex, colIndex);
            // DRAW KNIGHT PIECE
            this.Refresh();
            Graphics g = pnlChessBoard.CreateGraphics();
            drawImageCentered(g, Cons.HORSE_SPRITE, rowIndex * 55 + 55 / 2, colIndex * 55 + 55 / 2);
        }

        private void drawImageCentered(Graphics g, Image img, int x, int y)
        {
            g.DrawImage(img, x - img.Width / 2, y - img.Height / 2);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = false;
            StartFindPathAsync();
        }


        private void StartFindPathAsync()
        {
            KnightTourAlgorithm algo = new KnightTourAlgorithm(0, 0);
            int[,] resultChessboard = algo.FindPath();
            if (resultChessboard != null)
            {
                string result = "";
                btnBegin.Enabled = true;
                for (int i = 0; i < Cons.BOARD_SIZE; i++)
                {
                    for (int j = 0; j < Cons.BOARD_SIZE; j++)
                    {
                        result += resultChessboard[i, j] + " ";
                    }
                    result += "\n";
                }
                MessageBox.Show("Tim thay duong di\n" + result);
            }
            else
            {
                MessageBox.Show("Khong tim thay");
            }
        }

        private void timerFindPath_Tick(object sender, EventArgs e)
        {

            KnightTourAlgorithm algo = new KnightTourAlgorithm(0, 0);
            int[,] resultChessboard = algo.FindPath();
            if (resultChessboard != null)
            {
                btnBegin.Enabled = true;
                MessageBox.Show("Tim thay duong di");
            }
            else
            {
                MessageBox.Show("Khong tim thay");
            }
        }
    }
}
