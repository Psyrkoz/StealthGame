using UnityEditor;
using UnityEngine;

namespace SimpleWaypoint
{
    [CustomEditor(typeof(SimpleWaypointManager))]
    public class SimpleWaypointManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SimpleWaypointManager manager = (target as SimpleWaypointManager);
            if (GUILayout.Button("Create route"))
            {
                GameObject go = new GameObject("Route");
                go.AddComponent<SimpleWaypointRoute>();
                go.transform.SetParent(manager.transform);
            }
        }
    }

}
