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
        private int step = 0;
        private int[,] solutionArray;
        public formMain()
        {
            InitializeComponent();

            timerShowSolution.Interval = trackbarSpeed.Value * Cons.KNIGHT_SPEED;

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
                        g.DrawImage(bgWhite, i * Cons.CELL_WIDTH, j * Cons.CELL_WIDTH, Cons.CELL_WIDTH, Cons.CELL_WIDTH);
                    }
                    else if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                    {
                        g.DrawImage(bgBlack, i * Cons.CELL_WIDTH, j * Cons.CELL_WIDTH, Cons.CELL_WIDTH, Cons.CELL_WIDTH);
                    }
                }
            }
        }

        private void pnlChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            btnBegin.Enabled = true;
            // GET CELL NAME
            curColIndex = e.X / Cons.CELL_WIDTH;
            curRowIndex = e.Y / Cons.CELL_WIDTH;
            lblCurrentCellName.Text = Cons.GetCellName(curRowIndex, curColIndex);
            // DRAW KNIGHT PIECE
            pnlChessBoard.Refresh();
            Graphics g = pnlChessBoard.CreateGraphics();
            drawImageAtCell(g, Cons.HORSE_SPRITE, curColIndex, curRowIndex);
        }

        private void drawImageAtCell(Graphics g, Image img, int col, int row)
        {
            float posX = (col * Cons.CELL_WIDTH + Cons.CELL_CENTER) - img.Height / 2;
            float posY = (row * Cons.CELL_WIDTH + Cons.CELL_CENTER) - img.Width / 2;
            g.DrawImage(img, posX, posY);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = false;
            //StartFindPath(curRowIndex, curColIndex);
            Graphics g = pnlChessBoard.CreateGraphics();
            this.Refresh();
            drawImageAtCell(g, Cons.HORSE_SPRITE, 0, 0);
            StartFindPath(0, 0);
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
                    result +="Buoc " + (i+1) + " : " + solutionArray[i, 0] + ", " + solutionArray[i, 1];
                    result += "\n";
                }
                MessageBox.Show(this, "Tìm thấy đường đi cho quân Mã !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(this, result, "Ket qua");
                pnlChessBoard.Enabled = false;
                timerShowSolution.Start();
            }
            else
            {
                MessageBox.Show(this, "Không tìm thấy đường đi !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerShowSolution_Tick(object sender, EventArgs e)
        {
            if (step == Cons.TOTAL_CELLS)
            {
                timerShowSolution.Stop();
                MessageBox.Show(this, "Hoàn thành !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resetBoard();
                return;
            }
            Graphics g = pnlChessBoard.CreateGraphics();
            drawImageAtCell(g, Cons.HORSE_SPRITE, solutionArray[step, 0], solutionArray[step, 1]);
            // Draw a tick on the knight excel first cell
            if (step > 1)
            {
                int nextCol = solutionArray[step - 1, 0];
                int nextRow = solutionArray[step - 1, 1];
                drawImageAtCell(g, Cons.CHECKED_SPRITE, nextCol, nextRow);
            }
            Debug.WriteLine(step++);
            lblCurrentCellName.Text = Cons.GetCellName(solutionArray[step - 1, 0], solutionArray[step - 1, 1]);
            lblStep.Text = step.ToString();
        }

        private void trackbarSpeed_Scroll(object sender, EventArgs e)
        {
            int delayTime = trackbarSpeed.Value * Cons.KNIGHT_SPEED;
            Debug.WriteLine(delayTime);
            timerShowSolution.Interval = delayTime;
        }

        private void resetBoard()
        {
            btnBegin.Enabled = false;
            pnlChessBoard.Enabled = true;
            step = 0;
            lblStep.Text = "0";
        }
    }
}
