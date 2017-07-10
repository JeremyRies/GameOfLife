using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class IntVector2
{
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((IntVector2)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (X * 397) ^ Y;
        }
    }

    public static explicit operator Vector2(IntVector2 v)
    {
        return new Vector2(v.X, v.Y);
    }

    public static explicit operator IntVector2(Vector2 v)
    {
        return new IntVector2((int)v.x,(int)v.y);
    }

    public static readonly IntVector2 Up = new IntVector2(0, 1);
    public static readonly IntVector2 Down = new IntVector2(0, -1);
    public static readonly IntVector2 Left = new IntVector2(-1, 0);
    public static readonly IntVector2 Right = new IntVector2(1, 0);

    [SerializeField]
    public readonly int X;
    [SerializeField]
    public readonly int Y;

    public IntVector2(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    private float magnitudeF
    {
        get { return (float)Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2)); }
    }
    private float magnitudeI
    {
        get { return (int)Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2)); }
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X + b.X, a.Y + b.Y);
    }
    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        return new IntVector2(a.X - b.X, a.Y - b.Y);
    }
    public static IntVector2 operator *(int a, IntVector2 b)
    {
        return new IntVector2(a * b.X, a * b.Y);
    }
    public static IntVector2 operator /(int a, IntVector2 b)
    {
        return new IntVector2(b.X / a, b.Y / a);
    }

    public static IntVector2 Max(IntVector2 a, IntVector2 b)
    {
        return a.magnitudeF > b.magnitudeF ? a : b;
    }
    public static IntVector2 Min(IntVector2 a, IntVector2 b)
    {
        return a.magnitudeF < b.magnitudeF ? a : b;
    }

    public IntVector2 TurnLeft()
    {
        return new IntVector2(-this.Y, this.X);
    }

    public IntVector2 TurnRight()
    {
        return new IntVector2(this.Y, -this.X);
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", this.X, this.Y);
    }

    public bool Equals(IntVector2 other)
    {
        if ((object)other == null)
        {
            return false;
        }

        return (X == other.X) && (Y == other.Y);
    }


    public static bool operator ==(IntVector2 a, IntVector2 b)
    {
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        return a.X == b.X && a.Y == b.Y;

    }

    public static bool operator !=(IntVector2 a, IntVector2 b)
    {
        return !(a == b);
    }
}