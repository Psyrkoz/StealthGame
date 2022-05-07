using UnityEditor;
using UnityEngine;

namespace SimpleWaypoint
{
	public class SimpleWaypointEditor : Editor
	{
		[MenuItem("Tools/Simple Tools/Create Waypoint Manager")]
		public static void createWaypointManager()
		{
			SetupWaypointManager();
		}

		public static void SetupWaypointManager()
		{
			// Prevent multiple Manager in one scene
			if (!FindObjectOfType<SimpleWaypointManager>())
			{
				GameObject go = new GameObject("AwesomeWaypointManager");
				go.AddComponent<SimpleWaypointManager>();
				go.transform.position = Vector3.zero;
			}
		}

		[MenuItem("Tools/Simple Tools/Create AI Manager")]
		public static void createAIManager()
		{
			SetupAIManager();
		}

		public static void SetupAIManager()
		{
			// Prevent multiple Manager in one scene
			if (!FindObjectOfType<SimpleAIManager>())
			{
				GameObject go = new GameObject("Simple AI Manager");
				go.AddComponent<SimpleAIManager>();
				go.transform.position = Vector3.zero;
			}
		}
	}
}

