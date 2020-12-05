using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current;

    private AudioSource source;

    void Awake()
    {
        current = this;

        source = Utility.InitializeComponent<AudioSource>(gameObject);
    }



    public void Play(AudioClip clip)
    {
        if(clip == null) return;
        source.PlayOneShot(clip);
    }



    








}
