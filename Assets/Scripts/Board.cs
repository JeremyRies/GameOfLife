using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

public class Board
{
    public int Width
    {
        get { return _width; }
    }

    public int Height
    {
        get { return _height; }
    }

    public readonly Field[,] Fields;
    private readonly int _width;
    private readonly int _height;

    public Board(int width, int height)
    {
        _width = width;
        _height = height;
        Fields = new Field[Width,Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var intPos = new IntVector2(x,y);
                
                Fields[x, y] = new Field(intPos.X, intPos.Y, false);
            }
        }
    }

    public bool ShouldLiveNextTurn(Field field)
    {
        var aliveFields = GetSurroundingAliveFieldsCount(field);
        if (aliveFields > 3)
        {
            return false;
        }
        if (aliveFields < 2 && field.Alive)
        {
            return false;
        }
        if(aliveFields < 4 && field.Alive)
        {
            return true;
        }
        if(aliveFields == 3)
        {
            return true;
        }

        return false;
    }

    public int GetSurroundingAliveFieldsCount(Field field)
    {
        var sum = 0;
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                var posX = field.Position.X + x;
                var posY = field.Position.Y + y;

                if(!IsInBoard(posX, posY))
                    continue;

                if (Fields[posX, posY].Alive)
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    
    public bool IsInBoard(int x, int y)
    {
        if (x < 0 || y < 0) return false;
        if (x >= _width || y >= _height) return false;
        return true;
    }

    //private List<Field> GetSurroundingFields(Field field)
    //{
    //    var surroundingFields = new List<Field>();
    //    for (int x = -1; x <= 1; x++)
    //    {
    //        for (int y = -1; y <= 1; y++)
    //        {
    //            if (x ==0 && y ==0)continue;

    //            var posX = field.Position.X + x;
    //            var posY = field.Position.Y + y;
    //            if (IsInBoard(new IntVector2(posX,posY)))
    //            {
    //                surroundingFields.Add( Fields[posX, posY]);
    //            }
    //        }
    //    }
    //    return surroundingFields;
    //}

}