using UnityEngine;
using UnityEditor;

namespace SimpleWaypoint
{
	[DisallowMultipleComponent]
	[CustomEditor (typeof(SimpleWaypointRoute))]
	public class SimpleWaypointRouteInspector : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();

			SimpleWaypointRoute route = target as SimpleWaypointRoute;
			SimpleWaypoint[] waypoints = (target as SimpleWaypointRoute).GetComponentsInChildren<SimpleWaypoint>();

			if (GUILayout.Button("Set new color"))
            {
				Shader shader = Shader.Find("Universal Render Pipeline/Simple Lit");
				Material mat = new Material(shader);
				mat.color = route.color;

				foreach (SimpleWaypoint waypoint in waypoints)
				{
					waypoint.GetComponent<Renderer>().material = mat;
				}
            }

			// Generate a new waypoint on the route
			if (GUILayout.Button("Add New Waypoint")) 
			{
				Shader shader = Shader.Find("Universal Render Pipeline/Simple Lit");
				Material mat = new Material(shader);
				mat.color = route.color;

				GameObject waypoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				waypoint.transform.SetParent(route.transform);
				waypoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				waypoint.transform.localPosition = new Vector3(Random.Range(-10f, 10f), route.transform.localScale.y, Random.Range(-10f, 10f));
				waypoint.name = "Waypoint";
				waypoint.tag = "EditorOnly";

				waypoint.GetComponent<Renderer>().material = mat;
				waypoint.GetComponent<Collider>().isTrigger = true;
				waypoint.AddComponent<SimpleWaypoint>();
			}

			// Ground all waypoints from route object
			if(GUILayout.Button("Ground all waypoints"))
            {
				foreach(SimpleWaypoint waypoint in waypoints)
                {
					RaycastHit groundHit;
					Ray ray = new Ray(waypoint.transform.position, Vector3.down);
					if (Physics.Raycast(ray, out groundHit))
					{
						waypoint.transform.position = groundHit.point;
					}
				}
            }
		}
	}
}