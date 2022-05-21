using System;
using System.Collections.Generic;

/// <summary>
/// Clase para representar posiciones del Grid. Código base hecho con ayuda de esta documentación
/// https://docs.microsoft.com/es-es/dotnet/api/system.object.equals?view=net-6.0
/// </summary>

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is Point)
        {
            Point p = obj as Point;
            return this.X == p.X && this.Y == p.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 6949;
            hash = hash * 7907 + X.GetHashCode();
            hash = hash * 7907 + Y.GetHashCode();
            return hash;
        }
    }

    public override string ToString()
    {
        return "Point: (" + this.X + ", " + this.Y + ")";
    }
}
