using System.Collections.Generic;
using System;
using UnityEngine;

namespace SimpleWaypoint
{
    public class SimpleWaypoint : MonoBehaviour
    {
        private List<SimpleWaypoint> linkedWaypoints;

        // TODO: Add a "routeBeginning" boolean value so we can set up a proper spawn point for AI
        //       and not random. Probably also add a "use random spawn point in manager editor" so its still possible
        public bool startWaypoint = false;
        public bool accessible = true;

        [SerializeField]
        private bool taken = false;
        public int minWaitTime = 1;
        public int maxWaitTime = 5;

        public void Awake()
        {
            linkedWaypoints = new List<SimpleWaypoint>();
        }

        public List<SimpleWaypoint> getPossibleNextWaypoints()
        {
            List<SimpleWaypoint> possibleWaypoints = new List<SimpleWaypoint>();
            foreach(SimpleWaypoint waypoint in linkedWaypoints)
            {
                if (!waypoint.isTaken() && waypoint.isAccessible())
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

        public void setTaken(bool newTaken)
        {
            taken = newTaken;
        }
        public bool isTaken()
        {
            return taken;
        }
        public bool isAccessible()
        {
            return accessible;
        }
        public bool isStartWaypoint()
        {
            return startWaypoint;
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