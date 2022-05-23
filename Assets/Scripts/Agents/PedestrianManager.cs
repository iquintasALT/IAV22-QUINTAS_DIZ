using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject pedestrianPrefab;

    public void SpawnPedestrians()
    {
        List<Point> houses = gridManager.grid.getAllHousePos();
        foreach (var house in houses)
        {
            Point endDest = houses[UnityEngine.Random.Range(0, houses.Count)];
            if (house.Equals(endDest)) continue;
            else SpawnPedestrian(house, endDest);
        }
    }

    private void SpawnPedestrian(Point startPoint, Point endPoint)
    {
        List<Vector3Int> startNeighbours = gridManager.getNeightbourPos(new Vector3Int(startPoint.X, 0, startPoint.Y));
        List<Vector3Int> endNeighbours = gridManager.getNeightbourPos(new Vector3Int(endPoint.X, 0, endPoint.Y));

        int startIndex = startNeighbours.FindIndex(x => gridManager.grid[x.x, x.z] == CellType.Road);
        int endIndex = endNeighbours.FindIndex(x => gridManager.grid[x.x, x.z] == CellType.Road);

        Vector3Int startPosition = startNeighbours[startIndex];
        Vector3Int endPosition = endNeighbours[endIndex];

        var agent = Instantiate(pedestrianPrefab, startPosition, Quaternion.identity);
        var path = gridManager.GetPathBetween(startPosition, endPosition, true);
        if (path.Count > 0)
        {
            path.Reverse();
            Pedestrian pedestrian = agent.GetComponent<Pedestrian>();
            pedestrian.Initialize(new List<Vector3>(path.Select(x => (Vector3)x).ToList()));
        }
    }
}
