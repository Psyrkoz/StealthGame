using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SimpleWaypoint
{
    [CustomEditor(typeof(SimpleAIManager))]
    public class SimpleAIManagerInspector : Editor
    {
        private int selected;
        private SimpleWaypointRoute route;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            SimpleWaypointRoute[] routes= FindObjectsOfType<SimpleWaypointRoute>();
            string[] routesName = new string[routes.Length];
            for (int i = 0; i < routes.Length; i++)
                routesName[i] = routes[i].name;

            selected = EditorGUILayout.Popup("Route to add AI", selected, routesName);
            route = routes[selected];

            if (GUILayout.Button("Add AI"))
            {
                SimpleAIManager manager = target as SimpleAIManager;
                SimpleAI prefab = manager.getPrefab();
                if(prefab != null)
                {
                    SimpleAI instance = Instantiate(prefab, manager.transform);
                    instance.setRoute(route);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
