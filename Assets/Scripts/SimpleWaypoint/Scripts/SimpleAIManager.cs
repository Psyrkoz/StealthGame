using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleWaypoint
{
    public class SimpleAIManager : MonoBehaviour
    {
        private List<SimpleAI> AI = new List<SimpleAI>();

        // Find all enemies on the map
        private void Start()
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("AI"))
            {
                SimpleWaypointRoute route = go.GetComponent<SimpleAI>().getRoute();

                if (route != null)
                {
                    SimpleAI a = go.GetComponent<SimpleAI>();

                    SimpleWaypoint start = a.getRoute().getRandomUntakenWaypoint();
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
                        AI.Add(a);
                    }
                    else
                    {
                        Debug.LogError("Unable to find a possible waypoint for " + a.name);
                    }
                }
                else
                {
                    Debugger.log("AI isn't on a route", Debugger.LEVEL.ERROR);
                }
            }
        }

        // This function will check wether an AI is on a finishing point or not.
        // On a finish point, it will take a new random point and walk to it
        void Update()
        {
            foreach (SimpleAI a in AI)
            {
                if (a.getStatus() == STATUS.READY)
                {
                    List<SimpleWaypoint> nextPossibilities = a.getCurrentWaypoint().getPossibleNextWaypoints();
                    if(nextPossibilities.Count > 0)
                    {
                        SimpleWaypoint next = nextPossibilities[Random.Range(0, nextPossibilities.Count)];
                        a.setNextWaypoint(next);
                    }   
                }
            }
        }
    }

}
