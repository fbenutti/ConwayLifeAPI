using ConwayLifeAPI.Models;

namespace ConwayLifeAPI.Data.Repository
{
    public interface IBoardRepository
    {

        public Task<bool[][]> GetBoardByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Board> AddBoardAsync(Board board, CancellationToken cancellationToken);
        public Task<bool[][]> GetNextStateAsync(Guid boardId, CancellationToken cancellationToken);
        public Task<bool[][]> GetStateAheadAsync(Guid boardId, int steps, bool persistNewBoard, CancellationToken cancellationToken);
        public Task UpdateBoardAsync(Board board, CancellationToken cancellation);
        public Task<bool[][]> GetFinalStateAsync(Guid boardId, bool persistNewBoard, CancellationToken cancellationToken);

    }
}
