using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    [SerializeField]
    private int width = 15, height = 15;
    [SerializeField]
    GameObject deadEnd, road, curve, way3, way4;

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

    /// <summary>
    /// factoria de objetos que van a ocupar las celdas
    /// </summary>
    private void placeOnCell(Vector3Int pos, GameObject prefab, CellType type)
    {
        grid[pos.x, pos.z] = type;
        Vector3 centered = new Vector3(0.5f, 0.01f, 0.5f);


        GameObject structure = Instantiate(prefab, transform);
        structure.transform.SetParent(transform);
        structure.transform.localPosition = pos + centered;
        //TODO rotacion segun vecinos
    }

    public void checkNeighboursAndPlace(Vector3Int pos)
    {
        if (!isValidPos(pos) || !isFreePos(pos)) return;

        //obtenemos vecinos
        List<CellType> adyacents = getNeightbours(pos);
        //contamos cuantas carreteras hay alrededor de una posicion
        int count = adyacents.Where(x => x == CellType.Road).Count();

        if (count == 0 || count == 1) placeOnCell(pos, deadEnd, CellType.Road);
        else if (count == 2)
        {
            if (adyacents[0] == CellType.Road && adyacents[2] == CellType.Road)
                placeOnCell(pos, road, CellType.Road);
            else placeOnCell(pos, curve, CellType.Road);
        }
        else if(count == 3) placeOnCell(pos, way3, CellType.Road);
        else placeOnCell(pos, way4, CellType.Road);

        //placeOnCell(pos, road, CellType.Road);
        //GameObject placingRoad = Instantiate(road, pos + centered, Quaternion.identity);
    }

    /// <summary>
    /// Casillas vecinas y validas a una posicion dada
    /// </summary>
    private List<CellType> getNeightbours(Vector3Int pos)
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
    
}
