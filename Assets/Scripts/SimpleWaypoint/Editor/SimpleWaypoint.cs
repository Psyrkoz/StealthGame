using UnityEditor;
using UnityEngine;

namespace SimpleWaypoint
{
	public class SimpleWaypointEditor : Editor
	{
		[MenuItem("Tools/Simple Waypoint/Create Manager")]
		public static void createManager()
		{
			SetupManager();
		}

		public static void SetupManager()
		{
			// Prevent multiple Manager in one scene
			if (!FindObjectOfType<SimpleWaypointManager>())
			{
				GameObject go = new GameObject("AwesomeWaypointManager");
				go.AddComponent<SimpleWaypointManager>();
				go.transform.position = Vector3.zero;
			}
		}
	}
}

