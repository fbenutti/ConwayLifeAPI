using ConwayLifeAPI.Domain;

namespace ConwayLifeAPI.Tests
{
    public class GameOfLifeTests
    {
        [Fact]
        public void Blinker_ShouldOscillate()
        {
            //Arrange
            bool[][] input = new[]
            {
                new[] { false, true,  false },
                new[] { false, true,  false },
                new[] { false, true,  false }
            };

            bool[][] expected = new[]
            {
                new[] { false, false, false },
                new[] { true,  true,  true  },
                new[] { false, false, false }
            };

            //Act
            GameOfLife game = new GameOfLife();

            var result = game.CalculateNextState(input);

            //Assert
            Assert.Equal(expected.Length, result.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }

        [Fact]
        public void Block_ShouldRemainStable()
        {
            //Arrange
            bool[][] input = new[]
            {
                new[] { true,  true },
                new[] { true,  true }
            };

            //Act
            GameOfLife game = new GameOfLife();

            var result = game.CalculateNextState(input);

            //Assert
            Assert.Equal(input.Length, result.Length);
            for (int i = 0; i < input.Length; i++)
            {
                Assert.Equal(input[i], result[i]);
            }
        }

        [Fact]
        public void EmptyBoard_ShouldStayEmpty()
        {
            //Arrange
            bool[][] input = new[]
            {
                new[] { false, false },
                new[] { false, false }
            };

            //Act
            GameOfLife game = new GameOfLife();

            var result = game.CalculateNextState(input);

            //Assert
            for (int i = 0; i < input.Length; i++)
            {
                Assert.Equal(input[i], result[i]);
            }
        }

        [Fact]
        public void OverpopulatedCell_ShouldDie()
        {
            //Arrange
            bool[][] input = new[]
            {
                new[] { true, true, true },
                new[] { true, true, false },
                new[] { false, false, false }
            };

            //Act
            GameOfLife game = new GameOfLife();

            var result = game.CalculateNextState(input);

            //Assert
            // Center cell [1,1] should die
            Assert.False(result[1][1]);
        }

        [Fact]
        public void DeadCellWithThreeNeighbors_ShouldBecomeAlive()
        {
            //Arrange
            bool[][] input = new[]
            {
                new[] { true,  false, false },
                new[] { true,  false, false },
                new[] { true,  false, false }
            };

            //Act
            GameOfLife game = new GameOfLife();

            var result = game.CalculateNextState(input);

            //Assert
            // Cell [1,1] should come to life
            Assert.True(result[1][1]);
        }

        [Fact]
        public void NoOutOfBounds_OnBorderCells()
        {
            //Arrange
            bool[][] input = new[]
            {
                new[] { false, false, false },
                new[] { false, true,  false },
                new[] { false, false, false }
            };

            //Act
            GameOfLife game = new GameOfLife();

            var result = game.CalculateNextState(input);

            //Assert
            Assert.NotNull(result);
        }

    }
}