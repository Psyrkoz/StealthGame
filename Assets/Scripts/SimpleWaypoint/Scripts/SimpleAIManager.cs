using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleWaypoint
{
    public class SimpleAIManager : MonoBehaviour
    {
        private List<SimpleAI> _AI = new List<SimpleAI>();

        [SerializeField]
        private SimpleAI _prefab;

        // Find all enemies on the map
        private void Start()
        {
            // Add to a list the _taken starting waypoint.
            // At the end of the start, reset it to untaken.
            // Just to prevent having 8 differents spawn and 3 _AI and all of them spawning at the same place.
            List<SimpleWaypoint> takenStartingWaypoints = new List<SimpleWaypoint>();
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("AI"))
            {
                SimpleWaypointRoute route = go.GetComponent<SimpleAI>().getRoute();

                if (route != null)
                {
                    SimpleAI a = go.GetComponent<SimpleAI>();

                    // Start by trying to take a random untaken start waypoint
                    // Waypoint can be _accessible or not
                    SimpleWaypoint start = a.getRoute().getRandomUntakenStartWaypoint();
                    if(start == null)
                    {
                        Debug.LogWarning("Not enough start waypoint set... Taking a random _accessible waypoint");
                        // If none found, just take a random waypoint
                        start = a.getRoute().getRandomUntakenAccessibleWaypoint();
                    }
                    else
                    {
                        start.setTaken(true);
                        takenStartingWaypoints.Add(start);
                    }
                    
                    if(start != null)
                    {
                        a.setPreviousWaypoint(start);

                        List<SimpleWaypoint> previous = start.getPossibleNextWaypoints();
                        if (previous.Count > 0)
                        {
                            SimpleWaypoint next = previous[Random.Range(0, previous.Count)];
                            a.setNextWaypoint(next);
                        }
                        else
                        {
                            a.setNextWaypoint(start);
                        }
                        
                        a.transform.position = start.transform.position;
                        _AI.Add(a);
                    }
                    else
                    {
                        Debug.LogError("Unable to find a possible waypoint for " + a.name);
                    }
                }
                else
                {
                        Debug.LogError("AI isn't on a _route");
                }
            }
            foreach (SimpleWaypoint waypoint in takenStartingWaypoints)
            {
                waypoint.setTaken(false);
            }
        }

        // This function will check wether an _AI is on a finishing point or not.
        // On a finish point, it will take a new random point and walk to it
        void Update()
        {
            foreach (SimpleAI a in _AI)
            {
                if (a.getStatus() == STATUS.READY)
                {
                    List<SimpleWaypoint> nextPossibilities = a.getCurrentWaypoint().getPossibleNextWaypoints();
                    if(nextPossibilities.Count > 0)
                    {
                        SimpleWaypoint next = nextPossibilities[Random.Range(0, nextPossibilities.Count)];
                        a.setNextWaypoint(next);
                    }
                    else
                    {
                        // Debug.Log("No possible next waypoint for " + a.name);
                    }
                }
            }
        }

        public SimpleAI getPrefab()
        {
            return _prefab;
        }
    }

}
