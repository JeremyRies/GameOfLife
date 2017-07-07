using System.Security.Cryptography.X509Certificates;

public class Field
{
    public readonly IntVector2 Position;
    public bool Alive;

    public Field(int x, int y, bool alive = false)
    {
        Position = new IntVector2(x,y);
        Alive = alive;
    }
}