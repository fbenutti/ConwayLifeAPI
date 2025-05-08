using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;
using System.Threading.Tasks;

namespace ConwayLifeAPI.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoardAsync(CreateBoardDto dto, CancellationToken cancellationToken);
        Task<bool[][]> GetBoardAsync(Guid boardId, CancellationToken cancellationToken);
        Task<bool[][]> GetNextStateAsync(Guid boardId, CancellationToken cancellationToken);
        Task<bool[][]> GetStateAheadAsync(Guid boardId, int steps, bool saveNewBoard, CancellationToken cancellationToken);
        Task<bool[][]> GetFinalStateAsync(Guid boardId, bool saveNewBoard, CancellationToken cancellationToken);
    }
}
