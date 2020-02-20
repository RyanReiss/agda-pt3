using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adapted from https://www.asoundeffect.com/game-audio-scripting-part-2/
// Handles pooling and playing audio sources.

public class AudioController : MonoBehaviour
{

    private static AudioController _instance;
    public static AudioController Instance { get { return _instance; } }

    // Pool of AudioPoolSources for loading sounds into and playing
    private List<AudioPoolSource> _pool = new List<AudioPoolSource>();

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public AudioPoolSource GetSource()
    {
        AudioPoolSource output = null;
        if (_pool.Count > 0) { // grab a source and return it
            Debug.Log(_pool.Count);
            output = _pool[0];
            _pool.Remove(output);
            Debug.Log("Removed from the pool. Pool items: " + _pool.Count);
            return output;
        } else {
            Debug.Log("Pool is empty, adding an audio source GameObject");
            GameObject go = new GameObject("Audio Source");
            return go.AddComponent<AudioPoolSource>();
        }
    }

    public void PutSource(AudioPoolSource src) {
        if (!_pool.Contains(src)) {
            _pool.Add(src);
            Debug.Log("Added to the pool. Pool items: " + _pool.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
