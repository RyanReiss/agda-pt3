using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioObject Audio;
    void Start()
    {
        if (Audio != null) {
            Audio.Play();
        }
    }
}
