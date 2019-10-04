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
        private int curRowIndex = 0;
        private int curColIndex = 0;
        private int i = 0;
        private int[,] solutionArray;
        public formMain()
        {
            InitializeComponent();

            // AVOID PANEL FLICKERING
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, pnlChessBoard, new object[] { true });

            //typeof(Panel).InvokeMember("DoubleBuffered",
            // BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            // null, pnlBanner, new object[] { true });
        }

        private void pnlChessBoard_Paint(object sender, PaintEventArgs e)
        {
            // DRAW CHESSBOARD
            Graphics g = e.Graphics;
            Image bgBlack = Properties.Resources.bg_black;
            Image bgWhite = Properties.Resources.bg_white;
            for (int i = 0; i < Cons.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Cons.BOARD_SIZE; j++)
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
            btnBegin.Enabled = true;
            // GET CELL NAME
            curColIndex = e.X / Cons.CELL_SIZE;
            curRowIndex = e.Y / Cons.CELL_SIZE;
            lblBeginCellName.Text = Cons.GetCellName(curRowIndex, curColIndex);
            // DRAW KNIGHT PIECE
            pnlChessBoard.Refresh();
            Graphics g = pnlChessBoard.CreateGraphics();
            drawImageAtCell(g, Cons.HORSE_SPRITE, curColIndex, curRowIndex);
        }

        private void drawImageAtCell(Graphics g, Image img, int col, int row)
        {
            float posX = (col * Cons.CELL_SIZE + Cons.CELL_CENTER) - img.Height / 2;
            float posY = (row * Cons.CELL_SIZE + Cons.CELL_CENTER) - img.Width / 2;
            g.DrawImage(img, posX, posY);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = false;
            StartFindPath(curRowIndex, curColIndex);
            
        }


        private void StartFindPath(int beginX, int beginY)
        {
            KnightTourAlgorithm algo = new KnightTourAlgorithm(beginX, beginY);
            solutionArray = algo.FindPath();
            if (solutionArray != null)
            {
                string result = "";
                //btnBegin.Enabled = true;
                for (int i = 0; i < Cons.TOTAL_CELLS; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        result += solutionArray[i, j] + " ";
                    }
                    result += "\n";
                }
                MessageBox.Show("Tim thay duong di\n" + result);
                pnlChessBoard.Enabled = false;
                timerShowSolution.Start();
            }
            else
            {
                MessageBox.Show("Khong tim thay");
            }
        }

        private void timerShowSolution_Tick(object sender, EventArgs e)
        {
            if (i == Cons.TOTAL_CELLS)
            {
                timerShowSolution.Stop();
                MessageBox.Show("Hoàn thành !");
                btnBegin.Enabled = false;
                pnlChessBoard.Enabled = true;
                return;
            }
            Graphics g = pnlChessBoard.CreateGraphics();
            drawImageAtCell(g, Cons.HORSE_SPRITE, solutionArray[i, 0], solutionArray[i, 1]);
            if (i != 0)
            {
                drawImageAtCell(g, Cons.CHECKED_SPRITE, solutionArray[i - 1, 0], solutionArray[i - 1, 1]);
            }
            Debug.WriteLine(i++);
        }

        private void trackbarSpeed_Scroll(object sender, EventArgs e)
        {
            int delayTime = trackbarSpeed.Value * 200;
            Debug.WriteLine(delayTime);
            timerShowSolution.Interval = delayTime;
        }
    }
}
