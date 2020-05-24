﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SelectorPosition { up, middle, down };

public class MenuSelection : MonoBehaviour
{
    private Vector2 newPosition;
    private float newPositionY;
    private float newPositionX;
    private SelectorPosition handPointer;
    private AudioManager aManager;

    void Start()
    {
        //Cacheing the Audio Manager in the local variable
        aManager = FindObjectOfType<AudioManager>();

        //Setting up initial pointer SelectorPosition
        handPointer = SelectorPosition.up;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && handPointer == SelectorPosition.up)
        {
            aManager.Play("Selection");
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (handPointer == SelectorPosition.up)
            {
                handPointer = SelectorPosition.middle;
                newPositionY = -3.75f;
            }
            else if (handPointer == SelectorPosition.middle)
            {
                handPointer = SelectorPosition.down;
                newPositionY = -4.75f;
            }
            else
            {
                handPointer = SelectorPosition.up;
                newPositionY = -2.75f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (handPointer == SelectorPosition.up)
            {
                handPointer = SelectorPosition.down;
                newPositionY = -4.75f;
            }
            else if (handPointer == SelectorPosition.middle)
            {
                handPointer = SelectorPosition.up;
                newPositionY = -2.75f;
            }
            else
            {
                handPointer = SelectorPosition.middle;
                newPositionY = -3.75f;
            }
        }

        newPosition.y = newPositionY;
        newPosition.x = newPositionX;
        transform.position = newPosition;
    }
}
