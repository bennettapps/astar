using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphwayTest : MonoBehaviour
{
    public float speed = 5;
    public Vector2 target;

    private GwWaypoint[] waypoints;

    public void MoveAgent()
    {
        // TODO your code to move will go here...move to waypoints[0].position using your system
        transform.Translate((waypoints[0].position - transform.position).normalized * Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, waypoints[0].position) <= 3f)
        {
            NextWaypoint();
        }
    }

    public void SetAgentTarget(Vector2 point)
    {
        target = point;
        Graphway.FindPath(transform.position, target, FindPathCallback, true, false);
    }

    void Update()
    {

        if (waypoints != null && waypoints.Length > 0)
        {
            MoveAgent();
        }
        else
        {
            // TODO this is the method you call to tell the agent to move to a point
            SetAgentTarget(target);
        }
    }

    private void NextWaypoint()
    {
        if (waypoints.Length > 1)
        {
            GwWaypoint[] newWaypoints = new GwWaypoint[waypoints.Length - 1];

            for (int i = 1; i < waypoints.Length; i++)
            {
                newWaypoints[i - 1] = waypoints[i];
            }

            waypoints = newWaypoints;
        }
        else
        {
             waypoints = null;
        }
    }

    private void FindPathCallback(GwWaypoint[] path)
    {
        // Graphway returns 'null' if no path found
        // OR GwWaypoint array of waypoints to destination

        if (path == null)
        {
            Debug.Log("Path to target position not found!");
        }
        else
        {
            waypoints = path;
        }
    }
}
