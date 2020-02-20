using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioObject TestSound;
    void Start()
    {
        if (TestSound != null) {
            TestSound.Play();
        }
    }
}
