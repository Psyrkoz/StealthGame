using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIManager : MonoBehaviour
{
    private enum STATUS { IDLE, WALKING };
    private GameObject[] enemies;
    private int Status;
    private bool pressed = false;

    // Find all enemies on the map
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Status = 0;
        Debug.Log("Enemy count: " + enemies.Length);
    }

    // This function will check wether an AI is on a finishing point or not.
    // On a finish point, it will take a new random point and walk to it
    void Update()
    {
        Keyboard k = Keyboard.current;
        if (k.spaceKey.wasPressedThisFrame && !pressed)
        {
            Status = Status == 0 ? 1 : 0;
            Debug.Log("Space key was pressed");
            pressed = true;
            foreach(GameObject e in enemies)
            {
                e.GetComponent<Animator>().SetInteger("Status", Status);
            }
        }

        if (k.spaceKey.wasReleasedThisFrame)
            pressed = false;
    }
}
