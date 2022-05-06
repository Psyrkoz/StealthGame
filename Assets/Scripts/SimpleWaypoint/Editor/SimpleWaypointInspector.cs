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
        }
    }

}
