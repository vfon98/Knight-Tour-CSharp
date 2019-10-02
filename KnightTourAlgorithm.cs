using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight_Tour_Solution
{
    class KnightTourAlgorithm
    {
        private int[,] chessBoard;
        private int[,] solutionArray;
        private readonly int[] dX;
        private readonly int[] dY;
        private int stepNum;
        private int beginX;
        private int beginY;
        private Stack<int[]> solution = new Stack<int[]>();

        public KnightTourAlgorithm(int beginX, int beginY)
        {
            // ALL CHESSBOARD CELLS ASSIGNED AS 0
            this.chessBoard = new int[Cons.BOARD_SIZE, Cons.BOARD_SIZE];
            this.solutionArray = new int[Cons.TOTAL_CELLS, 2];
            this.chessBoard[beginX, beginY] = 1;
            // POSIBLE STEP OF HORSE KNIGHT
            this.dX = new int[] { -2, -2, -1, +1, +2, +2, +1, -1 };
            this.dY = new int[] { +1, -1, -2, -2, -1, +1, +2, +2 };
            // INITIAL STEP
            this.stepNum = 2;
            this.beginX = beginX;
            this.beginY = beginY;
        }

        private bool IsValidMove(int nextX, int nextY)
        {
            return nextX >= 0 && nextX < Cons.BOARD_SIZE && nextY >= 0 && nextY < Cons.BOARD_SIZE
                && chessBoard[nextX, nextY] == 0;
        }

        private bool TryMovingTo(int x, int y)
        {
            //stepNum++;
            //chessBoard[x, y] = stepNum;
            //for (int i = 0; i < 8; i++)
            //{
            //    if (stepNum == Cons.TOTAL_CELLS && !found)
            //    {
            //        found = true;
            //        return;
            //    }
            //    else if (!found)
            //    {
            //        int nextX = x + dX[i];
            //        int nextY = y + dY[i];

            //        if (IsValidMove(nextX, nextY))
            //        {
            //            TryMovingTo(nextX, nextY);
            //        }
            //    }
            //}
            //// BACKTRACKING
            //stepNum--;
            //chessBoard[x, y] = 0;

            if (stepNum == Cons.TOTAL_CELLS + 1)
                return true;
            for (int i = 0; i < 8; i++)
            {
                int nextX = x + dX[i];
                int nextY = y + dY[i];
                if (IsValidMove(nextX, nextY))
                {
                    chessBoard[nextX, nextY] = stepNum;
                    solutionArray[stepNum - 1, 0] = nextX;
                    solutionArray[stepNum - 1, 1] = nextY;
                    stepNum++;
                  
                    if (TryMovingTo(nextX, nextY))
                        return true;
                    else
                    {
                        // BACKTRACKING HERE
                        chessBoard[nextX, nextY] = 0;
                        stepNum--;
                    }
                }
            }
            return false;
        }

        public int[,] FindPath()
        {
            if (TryMovingTo(beginX, beginY))
            {
                return this.solutionArray;
            }
            else
            {
                return null;
            }
        }
    }
}
