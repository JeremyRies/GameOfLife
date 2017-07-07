using System.Linq;
using System.Threading;

class MultiThreadSimulation : IGameSimulation
{

    public Board Simulate(Board board, Board helperBoard)
    {
        var computedRows = new bool[board.Height];

        for (int y = 0; y < board.Height; y++)
        {
            var copyY = y;
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                ComputeRow(board, helperBoard, copyY);
                computedRows[copyY] = true;
            });
        }

        while (computedRows.Any(item => item != true))
        {
            Thread.SpinWait(1000);
        }
        return board;
    }

    private void ComputeRow(Board board,Board helperBoard, int y)
    {
        for (int x = 0; x < board.Width; x++)
        {
            var field = helperBoard.Fields[x, y];
            board.Fields[x, y].Alive = helperBoard.ShouldLiveNextTurn(field);
        }
    }
}