using ConwayLifeAPI.Data;
using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;
using ConwayLifeAPI.Services.Interfaces;
using Newtonsoft.Json;

namespace ConwayLifeAPI.Services
{
    public class BoardService : IBoardService
    {
        private readonly AppDbContext _context;

        public BoardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Board> CreateBoardAsync(CreateBoardDto dto, CancellationToken cancellationToken)
        {
            var board = new Board 
            { 
                StateJson = JsonConvert.SerializeObject(dto.Cells),
                Generation = 0,
                CreatedAt = DateTime.UtcNow
            };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync(cancellationToken);

            return board;
        }

        public async Task<bool[][]> GetNextStateAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync( boardId, cancellationToken);

            if (board == null)
            {
                throw new Exception("Board not found");
            }

            var currentState = JsonConvert.DeserializeObject<bool[][]>(board.StateJson);
            var nextState = CalculateNextState(currentState);

            return nextState;
        }

        private bool[][] CalculateNextState(bool[][]? currentState)
        {
            int rows = currentState!.Length;
            int cols = currentState[0].Length;

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
                    int liveNeighbors = CountLiveNeighbors(currentState, row, col);
                    // Apply the rules of Conway's Game of Life
                    if (currentState[row][col])
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

        private int CountLiveNeighbors(bool[][] currentState, int row, int col)
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
    }
}
