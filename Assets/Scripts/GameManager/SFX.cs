using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX instance;

    public AudioSource correct;
    public AudioSource incorrect;
    public AudioSource new_task;
    public AudioSource swipe;
    public AudioSource background;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudioClip(AudioSource source)
    {
        source.Play();
    }

}
