using System.Collections.Generic;
using System;
using UnityEngine;

namespace SimpleWaypoint
{
    public class SimpleWaypoint : MonoBehaviour
    {
        private List<SimpleWaypoint> linkedWaypoints;

        public bool taken = false;
        public float waitTime = 1.0f;

        public void Awake()
        {
            linkedWaypoints = new List<SimpleWaypoint>();
        }

        public List<SimpleWaypoint> getPossibleNextWaypoints()
        {
            List<SimpleWaypoint> possibleWaypoints = new List<SimpleWaypoint>();
            foreach(SimpleWaypoint waypoint in linkedWaypoints)
            {
                if (!waypoint.taken)
                {
                    possibleWaypoints.Add(waypoint);
                }
            }

            return possibleWaypoints;
        }

        public void addPossibleWaypoints(SimpleWaypoint w)
        {
            if (w != null && !linkedWaypoints.Contains(w))
            {
                linkedWaypoints.Add(w);
            }
        }
        public bool removePossibleWaypoint(SimpleWaypoint w)
        {
            return linkedWaypoints.Remove(w);
        }

        public void debugLog()
        {
            string strDebug = gameObject.transform.name  + " --- ";
            foreach (SimpleWaypoint w in linkedWaypoints)
                strDebug += w.gameObject.name + ", ";

            Debug.Log(strDebug);
        }

        // Probably add some waypoit feature like:
        //		- min/max wait time
        //		- is possible starting point
        //		- is end point?
    }
}