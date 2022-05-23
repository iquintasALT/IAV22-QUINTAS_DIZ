using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    Dictionary<Vector2Int, GameObject> gameObjectGrid;
    [SerializeField]
    private int width = 15, height = 15;
    [SerializeField]
    GameObject deadEnd, road, curve, way3, way4, house;

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
        gameObjectGrid = new Dictionary<Vector2Int, GameObject>();
    }

    internal void correctNeightbours(Vector3Int pos)
    {
        List<Vector3Int> adyacents = getNeightbourPos(pos);
        foreach (Vector3Int p in adyacents)
        {
            if (grid[p.x, p.z] != CellType.Road) continue; //si no hay una carretera en la casilla, no tenemos nada que "arreglar"
            grid[p.x, p.z] = CellType.Empty;
            checkNeighboursAndPlaceRoad(p);
        }
    }

    /// <summary>
    /// factoria de objetos que van a ocupar las celdas
    /// </summary>
    private void placeOnCell(Vector3Int pos, GameObject prefab, CellType type, Quaternion rotation)
    {
        Vector2Int cellPos = new Vector2Int(pos.x, pos.z);
        //si ya habia algo en esta celda, lo borro
        if (gameObjectGrid.ContainsKey(cellPos))
        {
            Destroy(gameObjectGrid[cellPos]);
            gameObjectGrid.Remove(cellPos);
        }

        grid[pos.x, pos.z] = type;
        Vector3 centered = new Vector3(0.5f, 0.01f, 0.5f);


        GameObject structure = Instantiate(prefab, transform);
        structure.transform.SetParent(transform);
        structure.transform.localPosition = pos + centered;
        structure.transform.localRotation = rotation;
        gameObjectGrid.Add(cellPos, structure);
    }

    public void placeHouse(Vector3Int pos)
    {
        if (!isValidPos(pos) || !isFreePos(pos)) return;

        int count = getNeightbourTypes(pos).Where(x => x == CellType.Road).Count();
        if(count <= 0)
        {
            Debug.Log("No hay una carretera cerca");
            return;
        }

        placeOnCell(pos, house, CellType.House, Quaternion.Euler(0, 180, 0));
    }

    public void checkNeighboursAndPlaceRoad(Vector3Int pos)
    {
        if (!isValidPos(pos) || !isFreePos(pos)) return;

        //obtenemos vecinos
        List<CellType> adyacents = getNeightbourTypes(pos);
        //contamos cuantas carreteras hay alrededor de una posicion
        int count = adyacents.Where(x => x == CellType.Road).Count();

        if (count == 0)
        {
            placeOnCell(pos, deadEnd, CellType.Road, Quaternion.Euler(-90, 0, 0));
        }
        else if (count == 1)
        {
            int roadIndex = adyacents.FindIndex(x => x == CellType.Road);
            int[] myArray = { 2, 3, 0, 1 };
            placeOnCell(pos, deadEnd, CellType.Road, Quaternion.Euler(-90, myArray[roadIndex]*90, 0));
        }
        else if (count == 2)
        {
            if ((adyacents[0] == CellType.Road && adyacents[2] == CellType.Road))
                placeOnCell(pos, road, CellType.Road, Quaternion.Euler(-90, 0, 0));
            else if (adyacents[1] == CellType.Road && adyacents[3] == CellType.Road)
                placeOnCell(pos, road, CellType.Road, Quaternion.Euler(-90, 90, 0));
            else
            {
                int rotation = 0;
                if (adyacents[0] == CellType.Road)
                    if (adyacents[1] == CellType.Road) rotation = 0;
                    else rotation = 270;
                else
                {
                    if (adyacents[1] == CellType.Road) rotation = 90;
                    else rotation = 180;
                }

                placeOnCell(pos, curve, CellType.Road, Quaternion.Euler(-90, rotation, 0));
            }
        }
        else if (count == 3)
        {
            int noRoadIndex = adyacents.FindIndex(x => x != CellType.Road);
            placeOnCell(pos, way3, CellType.Road, Quaternion.Euler(-90, noRoadIndex * 90, 0));
        }
        else placeOnCell(pos, way4, CellType.Road, Quaternion.Euler(-90, 0, 0));

        //placeOnCell(pos, road, CellType.Road);
        //GameObject placingRoad = Instantiate(road, pos + centered, Quaternion.identity);
    }

    /// <summary>
    /// Casillas vecinas y validas a una posicion dada
    /// </summary>
    public List<CellType> getNeightbourTypes(Vector3Int pos)
    {
        List<CellType> adyacents = new List<CellType>();
        foreach (Vector3Int p in Grid.neightbours)
        {
            Vector3Int newP = p + pos;
            if (isValidPos(newP)) adyacents.Add(grid[newP.x, newP.z]);

            else adyacents.Add(CellType.None); //siempre se devuelven 4 posiciones en adyacentes, None indica que estan fuera de los limites del Grid
        }

        return adyacents;
    }

    /// <summary>
    /// Posiciones de casillas vecinas y validas a una posicion dada
    /// </summary>
    public List<Vector3Int> getNeightbourPos(Vector3Int pos)
    {
        List<Vector3Int> adyacents = new List<Vector3Int>();
        foreach (Vector3Int p in Grid.neightbours)
        {
            Vector3Int newP = p + pos;
            if (isValidPos(newP)) adyacents.Add(new Vector3Int(newP.x,0, newP.z));
        }

        return adyacents;
    }

}
