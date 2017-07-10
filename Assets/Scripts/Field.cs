using System;

[Serializable]
public class Field
{
    public int X;
    public int Y;
    public bool Alive;

    public Field(int x, int y, bool alive = false)
    {
        X = x;
        Y = y;
        Alive = alive;
    }
}