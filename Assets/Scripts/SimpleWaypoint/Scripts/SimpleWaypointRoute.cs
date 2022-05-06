using UnityEngine;
using System.Collections.Generic;

namespace SimpleWaypoint
{
	// AI will use this class to travel trough the level
	public class SimpleWaypointRoute : MonoBehaviour
	{
		#region VARIABLE DECLARATION

		public Color color;
		private LinkedList<GameObject> waypoints = new LinkedList<GameObject>();
        
		#endregion

        public void Start()
		{
			loadWaypoints();
		}

		private void loadWaypoints()
        {
			waypoints.Clear();

			SimpleWaypoint[] waypointArray = GetComponentsInChildren<SimpleWaypoint>();
			if(waypointArray.Length > 0)
            {
				waypoints.AddFirst(waypointArray[0].gameObject);
				for(int i = 1; i < waypointArray.Length - 1; i++)
                {
					waypoints.AddAfter(waypoints.Last, waypointArray[i].gameObject);
                }
			}
        }

		// Used for editor only
		void OnDrawGizmos ()
		{
			SimpleWaypoint[] waypoints = GetComponentsInChildren<SimpleWaypoint>();
			SimpleWaypointManager manager = transform.parent.GetComponent<SimpleWaypointManager>();

			// Mandatory to use new Color(r, g, b) since a is at 0
			Gizmos.color = new Color(color.r, color.g, color.b);
			if (manager != null && manager.drawGizmos && waypoints.Length > 1)
            {
				for (int i = 0; i < waypoints.Length - 1; i++)
                {
					Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
				}
				Gizmos.DrawLine(waypoints[waypoints.Length - 1].transform.position, waypoints[0].transform.position);
            }
		}
	}
}