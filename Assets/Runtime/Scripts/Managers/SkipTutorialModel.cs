using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkipTutorialModel : MonoBehaviour
{
    private float keyHoldTime = 0f;
    private bool keyHeld = false;

    void Update()
    {
        // Check if the Enter key is being held down
        if (Input.GetKey(KeyCode.Return))
        {
            if (!keyHeld)
            {
                keyHeld = true; // Start tracking hold time
                keyHoldTime = Time.time;
            }

            // If the key has been held for 3 seconds, send message
            if (Time.time - keyHoldTime >= 2f)
            {
                MenuManager menuManager = GetComponent<MenuManager>();
                menuManager.LoadNextScene();

                Debug.Log("Enter key held for 3 seconds!");
                keyHeld = false; // Reset to prevent repeated messages
            }
        }
        else
        {
            keyHeld = false; // Reset when key is released
        }
    }
}

