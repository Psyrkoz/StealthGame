using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField]
    private float _detectionTime = 2.0f;

    private float _timeSpentInTrigger = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _timeSpentInTrigger = 0.0f;
            Debug.Log("Player entered camera collider");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _timeSpentInTrigger += Time.deltaTime;
            if(_timeSpentInTrigger > _detectionTime)
            {
                _timeSpentInTrigger = 0.0f;
                Debug.Log("You loosed btw");
                // TODO: Trigger the end of the game / alert guards
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Exit after " + _timeSpentInTrigger);
            _timeSpentInTrigger = 0.0f;
        }
    }
}
