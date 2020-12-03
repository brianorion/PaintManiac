using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float endTime;

    private float seconds;
    private bool play;

    private void Start()
    {
        backgroundMusic.clip = clip;
    }
    private void Update()
    {
        seconds += 1 * Time.deltaTime;
        if (!play)
        {
            backgroundMusic.Play();
            play = true;
        }
        if (seconds > endTime)
        {
            backgroundMusic.Stop();
            play = false;
            seconds = 0;
        }
    }
}
