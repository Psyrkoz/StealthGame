using System;
using UnityEngine;

/***
 *  Class used to move a CCTV (Camera) between a min & max rotation
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
        GameObject _swivel;

        [SerializeField]
        private float _minAngle = -90;
        [SerializeField]
        private float _maxAngle = 90;
        [SerializeField]
        private float _timeToFullAngle = 10.0f;
        [SerializeField]
        private float _pauseAtMinMaxAngle = 1.0f;
        [SerializeField]
        private bool _invertDirection = false;

        private float _timeElapsed = 0.0f;

        private void Update()
        {
            _timeElapsed += Time.deltaTime;

            bool waiting = _timeElapsed > _timeToFullAngle;
            if (waiting)
            {
                waiting = (_timeElapsed < (_timeToFullAngle + _pauseAtMinMaxAngle));
                if (!waiting)
                {
                    _timeElapsed = 0.0f;
                    _invertDirection = !_invertDirection;
                }
            }
            else
            {
                _swivel.transform.localRotation = Quaternion.Euler(
                      _swivel.transform.eulerAngles.x
                    , Mathf.Lerp(_minAngle, _maxAngle, _timeElapsed / _timeToFullAngle) * (_invertDirection ? 1 : -1)
                    , _swivel.transform.eulerAngles.z
                );
            }
        }
    }

}
