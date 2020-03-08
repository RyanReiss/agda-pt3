using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// adapted from https://www.asoundeffect.com/game-audio-scripting-part-2/
// Handles pooling and playing audio sources. Also contains some audio utility functions.

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance { get { return _instance; } }

    // Pool of AudioPoolSources for loading sounds into and playing
    private List<AudioPoolSource> _pool = new List<AudioPoolSource>();
    private AudioMixer _mixer;

    public enum Fade { MUSIC, SFX, ALL }

    // if fade in and out time aren't the same, bugs can occur when you switch rooms too fast
    public float fadeTime = 0.5f; 

    void Awake()
    {
        _mixer = Resources.Load<AudioMixer>("AudioMixer");
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
            output = _pool[0];
            _pool.RemoveAt(0);
            // Debug.Log("Removed from the pool. Pool items: " + _pool.Count);
            return output;
        } else {
            // Debug.Log("Pool is empty, adding an audio source GameObject");
            GameObject go = new GameObject("Audio Source");
            go.tag = "Audio";
            return go.AddComponent<AudioPoolSource>();
        }
    }

    public void PutSource(AudioPoolSource src) 
    {
        if (!_pool.Contains(src)) {
            _pool.Add(src);
            // Debug.Log("Added to the pool. Pool items: " + _pool.Count);
        }
    } 

    public void RemoveSource(AudioPoolSource src) {
        if (_pool.Contains(src)) {
            _pool.Remove(src);
            // Debug.Log("Added to the pool. Pool items: " + _pool.Count);
        }
    }


    // returns the name of the last track that played or null if there wasn't one
    public string StopMusic(string nextTrack=null)
    {
        string lastTrack = null;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Audio")) {
            AudioPoolSource aps = go.GetComponent<AudioPoolSource>();
            if (nextTrack == aps.clipName && aps.isPlaying) {
                lastTrack = nextTrack;
                continue;
            }
            if (aps.playsMusic && aps.isPlaying) {
                aps.Stop();
                return aps.clipName;
            }
        }
        return lastTrack;
    }

    public void FadeIn(Fade fadeGroup)
    {
        StartCoroutine(FadeTo(0f, this.fadeTime, fadeGroup));
    }

    public void FadeOut(Fade fadeGroup)
    {
        StartCoroutine(FadeTo(-18f, this.fadeTime, fadeGroup));
    }
    public IEnumerator FadeTo(float newVolume, float duration, Fade fadeGroup)
    {
        Debug.Log("Fading to " + newVolume + "dB");
        float elapsed = 0f;
        string group = "masterVolume";
        switch (fadeGroup) {
            case Fade.MUSIC:
                group = "musicVolume";
            break;
            case Fade.SFX:
                group = "sfxVolume";
            break;
        }

        float vol;
        _mixer.GetFloat(group, out vol);
        while (elapsed <= duration) {
            elapsed += Time.deltaTime;
            float nextVol = Mathf.Lerp(vol, newVolume, elapsed / duration);
            _mixer.SetFloat(group, nextVol);
            yield return null;
        }
        _mixer.SetFloat(group, newVolume);
        yield break;
    }
}
