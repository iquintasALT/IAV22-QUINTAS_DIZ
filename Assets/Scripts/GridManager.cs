using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField]
    private int width = 15, height = 15;
    [SerializeField]
    GameObject road;

    public bool isValidPos(Vector3Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.z >= 0 && pos.z < height;
    }

    public bool isFreePos(Vector3Int pos)
    {
        return grid[pos.x, pos.z] == CellType.Empty;
    }

    private void Start()
    {
        grid = new Grid(width, height);
    }

    public void placeRoad(Vector3Int pos)
    {
        if (!isValidPos(pos) || !isFreePos(pos)) return;
        grid[pos.x, pos.z] = CellType.Road;
        Vector3 centered = new Vector3(0.5f, 0.01f, 0.5f);
        GameObject placingRoad = Instantiate(road, pos + centered, Quaternion.identity);
    }

    
}
