using UnityEditor;
using UnityEngine;

namespace SimpleWaypoint
{
    [CustomEditor(typeof(SimpleWaypoint))]
    [CanEditMultipleObjects]
    public class SimpleWaypointInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Ground waypoint"))
            {
                foreach(Object target in targets)
                {
                    SimpleWaypoint waypoint = target as SimpleWaypoint;
                    
                    RaycastHit groundHit;
                    Ray ray = new Ray(waypoint.transform.position, Vector3.down);
                    if (Physics.Raycast(ray, out groundHit))
                    {
                        waypoint.transform.position = groundHit.point;
                    }
                }
            }

            // Only showing on single selection
            if(targets.Length == 1)
            {
                if (GUILayout.Button("Create a route"))
                {
                    SimpleWaypoint waypoint = targets[0] as SimpleWaypoint;
                    GameObject gameObject = new GameObject();

                    gameObject.transform.SetParent(waypoint.transform);
                    gameObject.name = "Route";
                    gameObject.tag = "EditorOnly";
                    gameObject.transform.localPosition = Vector3.zero;

                    SimpleWaypointRoute route = gameObject.AddComponent<SimpleWaypointRoute>();
                    route._color = waypoint.GetComponent<Renderer>().sharedMaterial.color;
                }
            }
        }
    }

}
