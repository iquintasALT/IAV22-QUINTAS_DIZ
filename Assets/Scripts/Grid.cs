using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que representa el tablero.
/// Documentación para definir operador [] https://docs.microsoft.com/es-es/dotnet/csharp/programming-guide/indexers/using-indexers
/// </summary>
public enum CellType
{
    Empty, // aun sin establecer. Al ser 0 es el valor de inicialización por defecto.
    Road,
    House,
    None  // para usar en comparaciones
}

public class Grid
{
    //Atributos
    private CellType[,] grid;
    private int width;
    private int height;
    public int Width { get { return width; } }
    public int Height { get { return height; } }

    //izquierda, arriba, derecha, abajo
    public static Vector3Int[] neightbours = { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1), new Vector3Int(1, 0, 0), new Vector3Int(0, 0, -1) };

    //Metodos
    private List<Point> roadList = new List<Point>();
    private List<Point> houseList = new List<Point>();
    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new CellType[width, height];
    }

    /// <summary>
    /// Getter y setter definidos en el mismo método. Con esto podemos actualizar información interna del tablero (listas) a la vez que lo inicializamos.
    /// </summary>
    public CellType this[int i, int j]
    {
        get
        {
            return grid[i, j];
        }
        set
        {
            if (value == CellType.Road)
            {
                roadList.Add(new Point(i, j));
            }
            else
            {
                roadList.Remove(new Point(i, j));
            }
            if (value == CellType.House)
            {
                houseList.Add(new Point(i, j));
            }
            else
            {
                houseList.Remove(new Point(i, j));
            }
            grid[i, j] = value;
        }
    }

    internal float GetCostOfEnteringCell(Point pos)
    {
        return 1;
    }

    public List<Point> getAllRoadPos() { return roadList; }
    public List<Point> getAllHousePos() { return houseList; }
}
