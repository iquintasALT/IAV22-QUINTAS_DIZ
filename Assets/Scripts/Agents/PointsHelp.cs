using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHelp : MonoBehaviour
{
    [SerializeField]
    protected List<RoadPoint> pedestrianPoints;
    [SerializeField]
    protected bool isCorner;
    [SerializeField]
    protected bool hasCrosswalks;

    float threshold = 0.3f; //usado para comprobar distancias

    public virtual RoadPoint GetposPedestrianToSpwan(Vector3 housePos)
    {
        return GetClosestPointTo(housePos, pedestrianPoints);
    }

    private RoadPoint GetClosestPointTo(Vector3 pos, List<RoadPoint> pedestrianPoints)
    {
        if (isCorner)
        {
            //preguntamos directamnte por la distancia de cada punto
            foreach (var point in pedestrianPoints)
            {
                var direction = point.GetPos - pos;
                direction.Normalize();
                if (Mathf.Abs(direction.x) < threshold || Mathf.Abs(direction.z) < threshold)
                {
                    return point;
                }
            }
            return null;
        }
        else
        {
            RoadPoint closestPoint = null;
            float distance = float.MaxValue;
            foreach (var point in pedestrianPoints)
            {
                var pointDistance = Vector3.Distance(pos, point.GetPos);
                if (distance > pointDistance)
                {
                    distance = pointDistance;
                    closestPoint = point;
                }
            }
            return closestPoint;
        }
    }

    public Vector3 GetClosestPedestrainPosition(Vector3 currentPosition)
    {
        return GetClosestPointTo(currentPosition, pedestrianPoints).GetPos;
    }

    public List<RoadPoint> GetAllPedestrianRoadPoints()
    {
        return pedestrianPoints;
    }
}
