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
        private STATUS status = STATUS.READY;

        private SimpleWaypoint previousWaypoint = null;
        private SimpleWaypoint currentWaypoint = null;

        [SerializeField]
        private SimpleWaypointRoute route;

        private void Update()
        {
            // Need to add a rotation on Y axis
            if (status == STATUS.WALKING && currentWaypoint != previousWaypoint)
            {
                // TODO: Move it to somewhere else to not update this value everytime since
                //       it needs only to be set when start walking.
                GetComponent<Animator>().SetInteger("Status", 1);
                transform.LookAt(new Vector3(currentWaypoint.transform.position.x, transform.position.y, currentWaypoint.transform.position.z));
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, 1.2f * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SimpleWaypoint>())
            {
                SimpleWaypoint collided = other.GetComponent<SimpleWaypoint>();
                if(collided == currentWaypoint)
                {
                    status = STATUS.IDLE;
                    GetComponent<Animator>().SetInteger("Status", 0);
                    StartCoroutine(doIdleTime(collided.waitTime));
                }
            }
        }
        
        private IEnumerator doIdleTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            status = STATUS.READY;
        }

        public STATUS getStatus()
        {
            return status;
        }
        
        public SimpleWaypointRoute getRoute()
        {
            return route;
        }
        public void setRoute(SimpleWaypointRoute newRoute)
        {
            route = newRoute;
        }

        public void setNextWaypoint(SimpleWaypoint nextWaypoint)
        {
            if (currentWaypoint != null)
            {
                previousWaypoint = currentWaypoint;
                previousWaypoint.taken = false;
            }

            currentWaypoint = nextWaypoint;
            currentWaypoint.taken = true;

            status = STATUS.WALKING;
        }

        // SHOUD ONLY BE USED ONCE
        // TODO: Not using it... maybe ?
        public void setPreviousWaypoint(SimpleWaypoint waypoint)
        {
            previousWaypoint = waypoint;
        }

        public SimpleWaypoint getCurrentWaypoint()
        {
            return currentWaypoint;
        }

        public SimpleWaypoint getPreviousWaypoint()
        {
            return previousWaypoint;
        }
    }
}
