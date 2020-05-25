using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOver : MonoBehaviour
{
    private AudioManager aManager;
    void Start()
    {
        aManager = FindObjectOfType<AudioManager>();
        aManager.Play("Theme Music");
    }
}
