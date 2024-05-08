using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleWaypoint
{
    // IDLE = Waiting for X second on the waypoint
    // WALKING = Walking to a waypoint
    // READY = Is ready to go take a new way point and go to it!
    public enum STATUS { IDLE = 0, WALKING = 1, READY = 2 };

    public class SimpleAI : MonoBehaviour
    {
        private STATUS _status = STATUS.READY;

        private SimpleWaypoint _previousWaypoint = null;
        private SimpleWaypoint _currentWaypoint = null;

        [SerializeField]
        private SimpleWaypointRoute _route;

        private void Update()
        {
            // Need to add a rotation on Y axis
            if (_status == STATUS.WALKING && _currentWaypoint != _previousWaypoint)
            {
                // TODO: Move it to somewhere else to not update this value everytime since
                //       it needs only to be set when start walking.
                GetComponent<Animator>().SetInteger("Status", 1);
                transform.LookAt(new Vector3(
                      _currentWaypoint.transform.position.x
                    , transform.position.y
                    , _currentWaypoint.transform.position.z
                ));
                transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.transform.position, 1.2f * Time.deltaTime);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SimpleWaypoint>())
            {
                SimpleWaypoint collided = other.GetComponent<SimpleWaypoint>();
                if(collided == _currentWaypoint)
                {
                    _status = STATUS.IDLE;
                    GetComponent<Animator>().SetInteger("Status", 0);
                    StartCoroutine(doIdleTime(Random.Range(collided._minWaitTime, collided._maxWaitTime)));
                }
            }
        }  
        private IEnumerator doIdleTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _status = STATUS.READY;
        }

        public STATUS getStatus()
        {
            return _status;
        }
        public SimpleWaypointRoute getRoute()
        {
            return _route;
        }
       
        public void setRoute(SimpleWaypointRoute newRoute)
        {
            _route = newRoute;
        }
        public void setNextWaypoint(SimpleWaypoint nextWaypoint)
        {
            if (_currentWaypoint != null)
            {
                _previousWaypoint = _currentWaypoint;
                _previousWaypoint.setTaken(false);
            }

            _currentWaypoint = nextWaypoint;
            _currentWaypoint.setTaken(true);

            _status = STATUS.WALKING;
        }
        public void setPreviousWaypoint(SimpleWaypoint waypoint)
        {
            _previousWaypoint = waypoint;
        }

        public SimpleWaypoint getCurrentWaypoint()
        {
            return _currentWaypoint;
        }

        public SimpleWaypoint getPreviousWaypoint()
        {
            return _previousWaypoint;
        }
    }
}
