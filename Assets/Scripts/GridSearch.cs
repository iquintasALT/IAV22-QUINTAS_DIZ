using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Algoritmo de A* modificado para adecuarse a la practica
/// </summary>
public class GridSearch {

    public struct SearchResult
    {
        public List<Point> Path { get; set; }
    }

    public static List<Point> AStarSearch(GridManager gridManager, Point startPosition, Point endPosition)
    {
        List<Point> path = new List<Point>();

        List<Point> positionsTocheck = new List<Point>();
        Dictionary<Point, float> costDictionary = new Dictionary<Point, float>();
        Dictionary<Point, float> priorityDictionary = new Dictionary<Point, float>();
        Dictionary<Point, Point> parentsDictionary = new Dictionary<Point, Point>();

        positionsTocheck.Add(startPosition);
        priorityDictionary.Add(startPosition, 0);
        costDictionary.Add(startPosition, 0);
        parentsDictionary.Add(startPosition, null);

        while (positionsTocheck.Count > 0)
        {
            if(gridManager.grid[positionsTocheck[0].X, positionsTocheck[0].Y] != CellType.Road)
            {
                positionsTocheck.RemoveAt(0);
                continue;
            }
            Point current = GetClosestVertex(positionsTocheck, priorityDictionary);
            positionsTocheck.Remove(current);
            if (current.Equals(endPosition))
            {
                path = GeneratePath(parentsDictionary, current);
                return path;
            }

            foreach (Vector3Int neighbourVec in gridManager.getNeightbourPos(new Vector3Int(current.X, 0, current.Y)))
            {
                Point neighbour = new Point(neighbourVec.x, neighbourVec.z);

                if (gridManager.grid[neighbour.X, neighbour.Y] != CellType.Road)
                {
                    continue;
                }

                float newCost = costDictionary[current] + gridManager.grid.GetCostOfEnteringCell(neighbour);
                if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                {
                    costDictionary[neighbour] = newCost;

                    float priority = newCost + ManhattanDiscance(endPosition, neighbour);
                    positionsTocheck.Add(neighbour);
                    priorityDictionary[neighbour] = priority;

                    parentsDictionary[neighbour] = current;
                }
            }
        }
        return path;
    }

    private static Point GetClosestVertex(List<Point> list, Dictionary<Point, float> distanceMap)
    {
        Point candidate = list[0];
        foreach (Point vertex in list)
        {
            if (distanceMap[vertex] < distanceMap[candidate])
            {
                candidate = vertex;
            }
        }
        return candidate;
    }

    private static float ManhattanDiscance(Point endPos, Point point)
    {
        return Math.Abs(endPos.X - point.X) + Math.Abs(endPos.Y - point.Y);
    }

    public static List<Point> GeneratePath(Dictionary<Point, Point> parentMap, Point endState)
    {
        List<Point> path = new List<Point>();
        Point parent = endState;
        while (parent != null && parentMap.ContainsKey(parent))
        {
            path.Add(parent);
            parent = parentMap[parent];
        }
        return path;
    }
}
