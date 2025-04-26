namespace ConwayLifeAPI.DTOs
{
    public class CreateBoardDto
    {
        /// <summary>
        /// 2D Array: true = alive, false = dead;
        /// </summary>
        public bool[,] Cells { get; set; } = new bool[0, 0];
    }
}
