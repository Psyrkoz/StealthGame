using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField]
    private float detectionTime = 2.0f;

    private float timeSpentInTrigger = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timeSpentInTrigger = 0.0f;
            Debug.Log("Player entered camera collider");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeSpentInTrigger += Time.deltaTime;
            if(timeSpentInTrigger > detectionTime)
            {
                timeSpentInTrigger = 0.0f;
                // TODO: Trigger the end of the game / alert guards
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Exit after " + timeSpentInTrigger);
            timeSpentInTrigger = 0.0f;
        }
    }
}
