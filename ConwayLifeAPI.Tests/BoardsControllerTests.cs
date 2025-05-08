using Moq;
using Microsoft.AspNetCore.Mvc;
using ConwayLifeAPI.Controllers;
using ConwayLifeAPI.Services.Interfaces;
using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;
using Newtonsoft.Json;

namespace ConwayLifeAPI.Tests
{
    public class BoardsControllerTests
    {
        private readonly Mock<IBoardService> _mockService;
        private readonly BoardsController _controller;

        public BoardsControllerTests()
        {
            _mockService = new Mock<IBoardService>();
            _controller = new BoardsController(_mockService.Object);
        }

        [Fact]
        public async Task CreateBoard_WithValidInput_ReturnsOkWithId()
        {
            // Arrange
            var input = new CreateBoardDto
            {
                Cells = new[]
                {
                    new[] { false, true, false },
                    new[] { false, true, false },
                    new[] { false, true, false }
                }
            };

            var fakeBoard = new Board { Id = Guid.NewGuid() };
            _mockService.Setup(s => s.CreateBoardAsync(input, CancellationToken.None))
                        .ReturnsAsync(fakeBoard);

            // Act
            var result = await _controller.CreateBoard(input, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CreateBoardResponse>(okResult.Value);
            Assert.Equal(fakeBoard.Id, response.Id);
        }

        [Fact]
        public async Task GetNextState_ReturnsMatrix()
        {
            // Arrange
            var boardId = Guid.NewGuid();
            var expected = new[]
            {
                new[] { false, false, false },
                new[] { true,  true,  true  },
                new[] { false, false, false }
            };

            _mockService.Setup(s => s.GetNextStateAsync(boardId, CancellationToken.None))
                        .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetNextState(boardId, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<bool[][]>(okResult.Value);
            Assert.Equal(expected, data);
        }

        [Fact]
        public async Task GetFutureState_ReturnsMatrix()
        {
            // Arrange
            var boardId = Guid.NewGuid();
            var expected = new[]
            {
                new[] { true, false, true },
                new[] { false, true, false },
                new[] { true, false, true }
            };

            _mockService.Setup(s => s.GetStateAheadAsync(boardId, 5, false, CancellationToken.None))
                        .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetStateAhead(boardId, 5, false, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<bool[][]>(okResult.Value);
            Assert.Equal(expected, data);
        }

        [Fact]
        public async Task GetFinalState_WhenStable_ReturnsMatrix()
        {
            // Arrange
            var boardId = Guid.NewGuid();
            var expected = new[]
            {
                new[] { true, true },
                new[] { true, true }
            };

            _mockService.Setup(s => s.GetFinalStateAsync(boardId, false, CancellationToken.None))
                        .ReturnsAsync(expected);

            // Act
            var result = await _controller.GetFinalState(boardId, false, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<bool[][]>(okResult.Value);
            Assert.Equal(expected, data);
        }
    }
}
