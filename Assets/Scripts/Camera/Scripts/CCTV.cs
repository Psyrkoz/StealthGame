using UnityEngine;

/***
 *  Class used to move a CCTV (Camera) between a min & max rotation+
 *  Handle:
 *      - time to travel between min & max angle
 *      - min & max angle
 *      - Pause at min & max angle
 *      - Invert rotation
 * */

namespace CCTV
{
    public class CCTV : MonoBehaviour
    {
        [SerializeField]
        GameObject swivel;

        [SerializeField]
        private float minAngle = -90;
        [SerializeField]
        private float maxAngle = 90;
        [SerializeField]
        private float timeToFullAngle = 10.0f;
        [SerializeField]
        private float pauseAtMinMaxAngle = 1.0f;
        [SerializeField]
        private bool invertDirection = false;

        private float timeElapsed = 0.0f;

        private void Update()
        {
            timeElapsed += Time.deltaTime;

            bool waiting = timeElapsed > timeToFullAngle;
            if (waiting)
            {
                waiting = (timeElapsed < (timeToFullAngle + pauseAtMinMaxAngle));
                if (!waiting)
                {
                    timeElapsed = 0.0f;
                    invertDirection = !invertDirection;
                }
            }
            else
            {
                float newRotation = Mathf.Lerp(minAngle, maxAngle, timeElapsed / timeToFullAngle) * (invertDirection ? 1 : -1);
                swivel.transform.rotation = Quaternion.Euler(swivel.transform.eulerAngles.x,
                                                                        newRotation,
                                                                        swivel.transform.eulerAngles.z);
            }
        }
    }

}
