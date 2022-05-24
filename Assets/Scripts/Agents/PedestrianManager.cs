using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject pedestrianPrefab;

    AdjacencyGraph graph = new AdjacencyGraph();
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


        PointsHelp startPointsHelp = gridManager.GetStructureAt(startPosition).GetComponent<PointsHelp>();
        var startMarkerPosition = 
            startPointsHelp.GetClosestPedestrainPosition(new Vector3(startPoint.X, 0, startPoint.Y));
        var endMarkerPosition =
            gridManager.GetStructureAt(endPosition).GetComponent<PointsHelp>().GetClosestPedestrainPosition(new Vector3(endPoint.X, 0, endPoint.Y));

        //gridManager.gameObjectGrid[new Vector2Int(startPosition.x, startPosition.z)].GetComponent<PointsHelp>().GetposPedestrianToSpwan(startPosition);

        var agent = Instantiate(pedestrianPrefab, startPosition, Quaternion.identity);
        var path = gridManager.GetPathBetween(startPosition, endPosition, true);
        if (path.Count > 0)
        {
            path.Reverse();
            List<Vector3> agentPath = GetPedestrianPath(path, startMarkerPosition, endMarkerPosition);
            Pedestrian pedestrian = agent.GetComponent<Pedestrian>();
            pedestrian.Initialize(new List<Vector3>(path.Select(x => (Vector3)x).ToList()));
        }
    }

    private List<Vector3> GetPedestrianPath(List<Vector3Int> path, Vector3 startPosition, Vector3 endPosition)
    {
        graph.ClearGraph();
        CreatAGraph(path);
        Debug.Log(graph);
        return AdjacencyGraph.AStarSearch(graph, startPosition, endPosition);
    }

    /// <summary>
    /// Crea un grafo a partir de una camino ya calculado
    /// </summary>
    /// <param name="path"></param>
    private void CreatAGraph(List<Vector3Int> path)
    {
        //para los cruces. Luego se ordenaran
        Dictionary<RoadPoint, Vector3> tempDictionary = new Dictionary<RoadPoint, Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            var currentPosition = path[i];
            var roadGO = gridManager.GetStructureAt(currentPosition);
            if(roadGO == null)
            {
                Debug.Log("AA");
            }
            var RoadPointsList = roadGO.GetComponent<PointsHelp>().GetAllPedestrianRoadPoints();
            bool limitDistance = RoadPointsList.Count == 4;
            tempDictionary.Clear();
            foreach (var RoadPoint in RoadPointsList)
            {
                graph.AddVertex(RoadPoint.GetPos);
                foreach (var RoadPointNeighbourPosition in RoadPoint.GetAdjacentPositions())
                {
                    graph.AddEdge(RoadPoint.GetPos, RoadPointNeighbourPosition);
                }

                if (RoadPoint.OpenForconnections && i + 1 < path.Count)
                {
                    var nextRoadStructure = gridManager.gameObjectGrid[new Vector2Int(path[i + 1].x, path[i + 1].z)];
                    if (limitDistance)
                    {
                        tempDictionary.Add(RoadPoint, nextRoadStructure.GetComponent<PointsHelp>().GetClosestPedestrainPosition(RoadPoint.GetPos));
                    }
                    else
                    {
                        graph.AddEdge(RoadPoint.GetPos, nextRoadStructure.GetComponent<PointsHelp>().GetClosestPedestrainPosition(RoadPoint.GetPos));
                    }
                }
            }
            //para los cruces, solo obtnenemos los 2 mas cercanos
            if (limitDistance && tempDictionary.Count == 4)
            {
                var distanceSortedRoadPoints = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.GetPos, x.Value)).ToList();
                for (int j = 0; j < 2; j++)
                {
                    graph.AddEdge(distanceSortedRoadPoints[j].Key.GetPos, distanceSortedRoadPoints[j].Value);
                }
            }
        }
    }
    private void Update()
    {
        foreach (var vertex in graph.GetVertices())
        {
            foreach (var vertexNeighbour in graph.GetConnectedVerticesTo(vertex))
            {
                Debug.DrawLine(vertex.Position + Vector3.up, vertexNeighbour.Position + Vector3.up, Color.red);
            }
        }
    }
}
