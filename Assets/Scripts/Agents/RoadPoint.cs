using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadPoint : MonoBehaviour
{
    public Vector3 GetPos { get => transform.position; }

    public List<RoadPoint> nextRoadPoints;

    [SerializeField]
    private bool open;

    public bool OpenForconnections
    {
        get { return open; }
    }

    public List<Vector3> GetAdjacentPositions()
    {
        return new List<Vector3>(nextRoadPoints.Select(x => x.GetPos).ToList());
    }
}
