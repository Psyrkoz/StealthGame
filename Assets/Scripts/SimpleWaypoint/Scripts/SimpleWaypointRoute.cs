using UnityEngine;
using System.Collections.Generic;

namespace SimpleWaypoint
{
	// AI will use this class to travel trough the level
	public class SimpleWaypointRoute : MonoBehaviour
	{
		#region VARIABLE DECLARATION

		public Color color;

		[SerializeField]
		private bool isCircular;
		private List<SimpleWaypoint> waypoints = new List<SimpleWaypoint>();

		#endregion

		public void Awake()
		{
			waypoints.Clear();
			loadRoute(waypoints);

			/*
			Debug.Log("Loading route " + name);
			foreach (SimpleWaypoint waypoint in waypoints)
				waypoint.debugLog();
			*/
		}

		private void loadRoute(List<SimpleWaypoint> waypoints, SimpleWaypoint parent = null)
        {
			if(parent == null)
            {
				parent = GetComponentInParent<SimpleWaypoint>();
			}

			for (int i = 0; i < transform.childCount; i++)
			{
				SimpleWaypoint waypoint = transform.GetChild(i).GetComponent<SimpleWaypoint>();

				if(parent != null && i == 0)
                {
					waypoint.addPossibleWaypoints(parent);
					parent.addPossibleWaypoints(waypoint);

					if(!waypoints.Contains(parent))
						waypoints.Add(parent);
                }

				// Add previous and following child to path
				if(i - 1 >= 0)
                {
					SimpleWaypoint possibility = transform.GetChild(i - 1).GetComponent<SimpleWaypoint>();
					waypoint.addPossibleWaypoints(possibility);
				}
				if(i + 1 < transform.childCount)
                {
					SimpleWaypoint possibility = transform.GetChild(i + 1).GetComponent<SimpleWaypoint>();
					waypoint.addPossibleWaypoints(possibility);
				}

				// For each sub route, add first element
				for(int j = 0; j < waypoint.transform.childCount; j++)
                {
					SimpleWaypointRoute route = waypoint.transform.GetChild(j).GetComponent<SimpleWaypointRoute>();
					route.loadRoute(waypoints, waypoint);
                }

				if(!waypoints.Contains(waypoint))
					waypoints.Add(waypoint);
			}

			if(isCircular)
            {
				if (parent == null)
				{
					parent = transform.GetChild(0).GetComponent<SimpleWaypoint>();
				}

				SimpleWaypoint end = transform.GetChild(transform.childCount - 1).GetComponent<SimpleWaypoint>();
				parent.addPossibleWaypoints(end);
				end.addPossibleWaypoints(parent);
            }
		}

		public SimpleWaypoint getRandomUntakenWaypoint()
        {
			List<SimpleWaypoint> untakenWaypoint = new List<SimpleWaypoint>();
			foreach (SimpleWaypoint waypoint in waypoints)
			{
				if (!waypoint.taken)
				{
					untakenWaypoint.Add(waypoint);
				}
			}

			if(untakenWaypoint.Count > 0)
				return untakenWaypoint[Random.Range(0, untakenWaypoint.Count)];

			return null;
        }
		// Used for editor only
		void OnDrawGizmos ()
		{
			// For some reasons, have to reverse the loop so in a non circular route, 
			// the last waypoint is the farthest from beginning and not the closest
			List<SimpleWaypoint> waypoints = new List<SimpleWaypoint>();
			for(int i = transform.childCount - 1; i >= 0 ; i--)
			{
				waypoints.Add(transform.GetChild(i).GetComponent<SimpleWaypoint>());
            }

			// If the route parent is a SimpleWaypoint, add it to the route
			if(transform.parent.GetComponent<SimpleWaypoint>() != null)
				waypoints.Add(transform.parent.GetComponent<SimpleWaypoint>());


			SimpleWaypointManager manager = FindObjectOfType<SimpleWaypointManager>();
			Gizmos.color = new Color(color.r, color.g, color.b); // Mandatory to use new Color(r, g, b) since a is at 0
			if (manager != null && manager.drawGizmos && waypoints.Count > 1)
            {
				for (int i = 0; i < waypoints.Count - 1; i++)
                {
					Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
				}

				// Closing the circuit
				if (isCircular)
				{
					Gizmos.DrawLine(waypoints[waypoints.Count - 1].transform.position, waypoints[0].transform.position);
				}
            }
		}
	}
}