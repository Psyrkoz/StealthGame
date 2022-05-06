using System.Collections.Generic;
using System;
using UnityEngine;

namespace SimpleWaypoint
{
	public class SimpleWaypoint : MonoBehaviour
	{
        List<SimpleWaypoint> possibleWaypoints;

        public void Awake()
        {
            possibleWaypoints = new List<SimpleWaypoint>();
        }

        public void addPossibleWaypoints(SimpleWaypoint w)
        {
            if (w != null && !possibleWaypoints.Contains(w))
            {
                possibleWaypoints.Add(w);
            }
        }
        public bool removePossibleWaypoint(SimpleWaypoint w)
        {
            return possibleWaypoints.Remove(w);
        }

        public void debugLog()
        {
            string strDebug = gameObject.transform.name  + " --- ";
            foreach (SimpleWaypoint w in possibleWaypoints)
                strDebug += w.gameObject.name + ", ";

            Debug.Log(strDebug);
        }

        // Probably add some waypoit feature like:
        //		- min/max wait time
        //		- is possible starting point
        //		- is end point?
    }
}