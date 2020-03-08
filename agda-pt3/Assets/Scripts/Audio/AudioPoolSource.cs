using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// adapted from https://www.asoundeffect.com/game-audio-scripting-part-2/
// Wrapper for AudioSource that has some additional things for pool-based use

public class AudioPoolSource : MonoBehaviour
{
    private AudioSource _src;
    private Transform _parent; // for audio positioning
    private Transform _transform; 
    private bool _initialized = false;
    private bool _claimed = false;
    private AudioMixer _mixer;
    public bool playsMusic = false;

    public bool isPlaying {
        get { return _claimed || _src.isPlaying; }
    }

    public string clipName {
        get { return _src.clip.name; }
    }


    void Awake() 
    {
        _src = GetComponent<AudioSource>();
        _mixer = Resources.Load<AudioMixer>("AudioMixer");

        if (_src == null) {
            _src = gameObject.AddComponent<AudioSource>();
        }
        _transform = gameObject.transform;

    }

    void OnDestroy() {
        AudioController.Instance.RemoveSource(this);
    }

    public void SetProperties(AudioClip clip, float pitch, float volume, float spatialBlend, bool loop, bool music) 
    {
        _src.clip = clip;
        _src.pitch = pitch;
        _src.volume = volume;
        _src.spatialBlend = spatialBlend;
        _src.loop = loop;
        AudioMixerGroup grp = _mixer.FindMatchingGroups(music ? "Master/Music" : "Master/SFX")[0];
        _src.outputAudioMixerGroup = grp;
        playsMusic = music;
        if (music) {
            DontDestroyOnLoad(this.gameObject);
        }
        _initialized = true;
    }

    public void Play() 
    {
        if (_initialized) {
            if (playsMusic) {
                string currentTrack = AudioController.Instance.StopMusic(clipName);
                Debug.Log(currentTrack + " " + this.clipName);
                if (currentTrack != this.clipName) {
                    // music needs to be changed, play
                    Debug.Log("Music is playing but it needs to be changed, playing");
                    _src.Play();
                }
            } else {
                // it's a sound effect, play
                _src.Play();
            }
            _claimed = true;
        } else {
            Debug.LogError("Tried to play an uninitialized AudioPoolSource!");
        }
    }

    // unpleasant but straightforward workaround for coroutines not being possible in ScriptableObjects
    IEnumerator Play(float msTime, AudioClip clip, float pitch, float volume, float spatialBlend, bool loop, bool music, Transform parent=null) 
    {
        yield return new WaitForSeconds(msTime/1000f);
        SetProperties(clip, pitch, volume, spatialBlend, loop, music);
        if (parent != null) {
            SetParent(parent);
        }
        Play();
    }

    public void PlayAfter(float msTime, AudioClip clip, float pitch, float volume, float spatialBlend, bool loop, bool music, Transform parent=null) 
    {
        StartCoroutine(Play(msTime, clip, pitch, volume, spatialBlend, loop, parent));
    }

    public void Stop()
    {
        _src.Stop();
        Reset();
        AudioController.Instance.PutSource(this);
    }

    public void SetPosition(Vector3 position) {
        _transform.position = position;
    }

    public void SetParent(Transform parent) {
        _transform = parent;
    }

    void LateUpdate()
    {
        if (_claimed && !_src.isPlaying) {
            Stop();
        } else if (_parent != null) {
            _transform.position = _parent.position;
        }
    }

    void Reset() {
        _parent = null;
        _claimed = false;
    }
}
