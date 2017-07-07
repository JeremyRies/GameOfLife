class SingleThreadSimulation : IGameSimulation
{
    public Board Simulate(Board board, Board helperBoard)
    {
        foreach (var boardField in board.Fields)
        {  
            boardField.Alive = helperBoard.ShouldLiveNextTurn(boardField);
        }

        return board;
    }
}