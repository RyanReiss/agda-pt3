using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adapted from https://www.asoundeffect.com/game-audio-scripting/
// Stores audio data to be sent to the AudioController instance.

[CreateAssetMenu()]
public class AudioObject : ScriptableObject
{
    public List<AudioClip> clips;
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float spatialBlend = 1f;
    public bool loop;
    public bool music = false;

    private AudioClip RandomClip()
    {
        return (clips.Count > 0) ? clips[Random.Range(0, clips.Count - 1)] : null;
    }

    public AudioPoolSource Play(int clip=-1, Transform parent=null, Vector3? position=null)
    {
        AudioPoolSource src;

        if (parent != null) {
            src = Play(position: parent.position);
            src.SetParent(parent);
            return src;
        }

        src = AudioController.Instance.GetSource();
        src.SetProperties((clip == -1) ? RandomClip() : clips[clip], pitch, volume, spatialBlend, loop, music);
        if (position != null) {
            src.SetPosition(position.GetValueOrDefault());
        }
        src.Play();
        return src;
    }

    public AudioPoolSource PlayWithVolume(float vol=-1, int clip=-1, Transform parent=null, Vector3? position=null)
    {
        AudioPoolSource src;

        if (parent != null) {
            src = Play(position: parent.position);
            src.SetParent(parent);
            return src;
        }

        src = AudioController.Instance.GetSource();
        src.SetProperties((clip == -1) ? RandomClip() : clips[clip], pitch, (vol == -1) ? volume : vol, spatialBlend, loop, music);
        if (position != null) {
            Debug.Log("Setting position to " + position.GetValueOrDefault());
            src.SetPosition(position.GetValueOrDefault());
        }
        src.Play();
        return src;
    }

    public AudioPoolSource PlayAll(float msDelay, Transform parent=null) {
        AudioPoolSource src = AudioController.Instance.GetSource();
        for (int i = 0; i < clips.Count; i++) {
            src.PlayAfter((msDelay / clips.Count) * i, clips[i], pitch, volume, spatialBlend, loop, music, parent);
        }
        return src;
    }
}
