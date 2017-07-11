using UnityEngine;

public class RandomStartPositionProvider : AbstractStartPositionProvider
{
    public override void SetStartFields(Board board)
    {
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                board.Fields[x, y].Alive = Random.Range(0, 2) == 0;
            }
        }
    }
}