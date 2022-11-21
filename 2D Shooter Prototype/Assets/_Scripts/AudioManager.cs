using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    private enum Sound
    {
        //Example Sound types
        Walk,
        Attack
    }

    //So that we store the last played time for sounds that we don't want to overlap
    //and take the time(float) value to play at intervals like:
    //lastPlayedTime + interval < Time.time => Play
    private static Dictionary<Sound, float> soundTimerDictionary;

    private static GameObject _OneShotGameObject;
    private static AudioSource _OneShotAudioSource;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
    }

    public static void Play(AudioClip clip)
    {
        if(_OneShotGameObject == null)
        {
            _OneShotGameObject = new GameObject("One Shot Sound");
            _OneShotAudioSource = _OneShotGameObject.AddComponent<AudioSource>();
        }
        _OneShotAudioSource.PlayOneShot(clip);
    }

    //Open to object pool creation
    public static void PlayAt(AudioClip clip, Vector3 pointToPlayFrom)
    {
        //soundTimerDictionary is used here to check if something non-overlapping can be played
        GameObject sound = new GameObject("Sound");
        AudioSource audioSource = sound.AddComponent<AudioSource>();
        sound.transform.Translate(pointToPlayFrom);
        audioSource.clip = clip;
        audioSource.maxDistance = 100f;
        audioSource.Play();

        Object.Destroy(sound, clip.length);
    }
}
