using UnityEditor;
using UnityEngine;

namespace CCTV
{
    [CustomEditor(typeof(CCTV))]
    [CanEditMultipleObjects]
    public class CCTVInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Snap to wall"))
            {
                CCTV cam = target as CCTV;
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity))
                {
                    // TODO: Fix the CCTV's rotation so it really snap to wall!
                    cam.transform.position = hit.point;
                }
            }
        }
    }

}
