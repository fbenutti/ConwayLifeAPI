namespace ConwayLifeAPI.Domain
{
    public class GameOfLife
    {
        public const int MAX_STEPS = 100;

        /// <summary>
        /// Analyses if two boards are identical
        /// </summary>
        /// <param name="board1">First board to compare</param>
        /// <param name="board2">Second board to compare</param>
        /// <returns>boolean state whether the boards are equal</returns>
        public bool AreBoardsEqual(bool[][] board1, bool[][] board2)
        {
            if (board1.Length != board2.Length || board1[0].Length != board2[0].Length)
                return false;

            for (int r = 0; r < board1.Length; r++)
            {
                for (int c = 0; c < board1[r].Length; c++)
                {
                    if (board1[r][c] != board2[r][c])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculates the next state of the board based on the current state.
        /// </summary>
        /// <param name="currentStateBoard">Any board of any generation</param>
        /// <returns>The board representating the next generation</returns>
        public bool[][] CalculateNextState(bool[][]? currentStateBoard)
        {
            int rows = currentStateBoard!.Length;
            int cols = currentStateBoard[0].Length;

            // Create a new board to store the next state
            var nextStateBoard = new bool[rows][];
            for (int i = 0; i < rows; i++)
            {
                nextStateBoard[i] = new bool[cols];
            }

            // Iterate through each cell in the current state
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Count the number of live neighbors
                    int liveNeighbors = CountLiveNeighbors(currentStateBoard, row, col);
                    // Apply the rules of Conway's Game of Life
                    if (currentStateBoard[row][col])
                    {
                        // Cell is currently alive
                        nextStateBoard[row][col] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        // Cell is currently dead
                        nextStateBoard[row][col] = liveNeighbors == 3;
                    }
                }
            }

            return nextStateBoard;
        }

        /// <summary>
        /// Counts the number of live neighbors for a given cell in the board.
        /// </summary>
        /// <param name="currentState">the current board that's been analysed</param>
        /// <param name="row">current cell row's number</param>
        /// <param name="col">current cell column's number</param>
        /// <returns>The count of neighbors cells that are alive</returns>
        public int CountLiveNeighbors(bool[][] currentState, int row, int col)
        {
            int liveNeighbors = 0;
            int rows = currentState.Length;
            int cols = currentState[0].Length;
            // Check all 8 possible neighbors
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue; // Skip the cell itself
                    int newRow = row + i;
                    int newCol = col + j;
                    // Check if the neighbor is within bounds
                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        if (currentState[newRow][newCol])
                        {
                            liveNeighbors++;
                        }
                    }
                }
            }
            return liveNeighbors;
        }

        public bool checkSteps(int steps)
        {
            if (steps < 1)
                throw new ArgumentException("Steps must be greater than 0");

            if (steps > GameOfLife.MAX_STEPS)
                throw new ArgumentException("Steps must be less than 100");

            return true;
        }
    }
}
